using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.WebServices.Data;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Extensions;
using SalesEstimatorTool.Data.Models;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class EmailExchangeService : IEmailExchangeService
    {
        private readonly ExchangeService _exchangeService;
        private readonly ISettingsService _settingsService;

        public EmailExchangeService(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            _exchangeService = new ExchangeService
            {
                Credentials = new WebCredentials(_settingsService.ExchangeLogin, _settingsService.ExchangePassword)
            };
        }

        public IEnumerable<EmailMessageItem> GetUnreadMessages()
        {
            LoginToExchangeService();

            
            var searchFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false);

            var view = new ItemView(50)
            {
                PropertySet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.Subject, ItemSchema.DateTimeReceived),
                Traversal = ItemTraversal.Shallow
            };
            view.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);

            var messages = _exchangeService.FindItems(WellKnownFolderName.Inbox, searchFilter, view).Items
                .OfType<EmailMessage>() 
                .Select(n => new EmailMessageItem
                {
                    MessageId = n.Id.UniqueId,
                    Subject = n.Subject.Trim()
                })
                .Where(n => new Regex(_settingsService.SubjectRegexFilter).IsMatch(n.Subject))
                .ToList();

            return messages;
        }

        public String GetEmailBody(EmailMessageItem message)
        {
            var propertySet = new PropertySet
            {
                RequestedBodyType = BodyType.Text,
                BasePropertySet = BasePropertySet.FirstClassProperties
            };

            var emailMessage = EmailMessage.Bind(_exchangeService, message.MessageId, propertySet);

            return emailMessage.Body;
        }

        public IList<Attachment> GetEmailAttachments(EmailMessageItem message)
        {
            var propertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Attachments);

            var emailMessage = EmailMessage.Bind(_exchangeService, message.MessageId, propertySet);

            return emailMessage.Attachments.ToList();
        }

        public void MarkAsRead(EmailMessageItem message)
        {
            var emailMessage = EmailMessage.Bind(_exchangeService, message.MessageId);

            emailMessage.IsRead = true;
            emailMessage.Update(ConflictResolutionMode.AlwaysOverwrite);
        }

        public void Reply(EmailMessageItem message, SalesEstimator salesEstimator)
        {
            var emailMessage = EmailMessage.Bind(_exchangeService, message.MessageId);

            var body = GetReplyBody(message, salesEstimator);

            emailMessage.Forward(body, emailMessage.From);
        }

        public void ResendExceptionToAdmin(EmailMessageItem message, Exception exception)
        {
            var emailMessage = EmailMessage.Bind(_exchangeService, message.MessageId);

            var body = String.Format("Message <strong>'{0}'</strong> processing error.<br/>{1}", message.Subject, exception);
            var emails = _settingsService.ExchangeAdminEmails.Select(n => new EmailAddress(n));

            emailMessage.Forward(body, emails);
        }

        public void ReplyWithException(EmailMessageItem message, Exception exception)
        {
            var emailMessage = EmailMessage.Bind(_exchangeService, message.MessageId);

            var body = exception.Message;

            emailMessage.Forward(body, emailMessage.From);
        }

        #region Private

        private void LoginToExchangeService()
        {
            if (_exchangeService.Url == null)
            {
                _exchangeService.AutodiscoverUrl(_settingsService.ExchangeLogin, RedirectionUrlValidationCallback);
            }
        }

        private static Boolean RedirectionUrlValidationCallback(String redirectionUrl)
        {
            return new Uri(redirectionUrl).Scheme == "https";
        }

        private String GetReplyBody(EmailMessageItem message, SalesEstimator salesEstimator)
        {
            var totalTat = 0.0;
            var totalCost = 0m;
            var totalWordCount = 0;
            var body = new StringBuilder();

            body.AppendFormat("Message <strong>'{0}'</strong> processing completed successfully!", message.Subject);
            body.AppendFormat("<br> Source Language - {0}", salesEstimator.SourceLanguage.Language);
            body.AppendFormat("<br> Target Language - {0}", salesEstimator.DestinationLanguage.Language);
            body.AppendFormat("<br> Rush - {0}", salesEstimator.Rush.Description());
            body.AppendFormat("<br> Tier - {0}", salesEstimator.Tier.Description() );
            body.AppendFormat("<br>");

            foreach (var wordCounter in salesEstimator.WordCounterList)
            {
                var result = salesEstimator.GetCost(wordCounter);
                body.AppendFormat("<br> File {0} - ${1} ({2} word(s))", wordCounter.FullFilePath, result.ToString("0.00", CultureInfo.InvariantCulture), wordCounter.WordCount);
                totalCost += result;
                totalWordCount += wordCounter.WordCount;
                totalTat += salesEstimator.GetTurnaroundTime(wordCounter);
            }

            body.AppendFormat("<br><br> <strong>Overall word count  - {0}</strong>", totalWordCount);
            body.AppendFormat("<br><strong>Overall cost - ${0}</strong> {1}",
                Math.Max(totalCost, 150).ToString("0.00", CultureInfo.InvariantCulture), 
                totalCost < 150 ? "<i>(there is a minimum charge of $150 associated with this request)</i>" : string.Empty);
            body.AppendFormat("<br><strong>Turnaround time: {0} day(s)</strong>", Math.Ceiling(totalTat));

            if (salesEstimator.NotSupportedFileList.Any())
            {
                body.Append("<br><br> Files not processed:");

                foreach (var wordCounter in salesEstimator.NotSupportedFileList)
                {
                    body.AppendFormat("<br> &#09; {0}", wordCounter.FullFilePath);
                }                
            }

            body.Append("<br><br>NOTE:");
            body.Append("<br>This quote is for <strong>estimation purposes</strong> and is not a guarantee of cost for services. " +
                        "Quote is based on current information from client about the project requirements. " +
                        "<strong>Actual cost may change</strong> once project elements are finalized or negotiated. " +
                        "Client will be notified of any changes in cost prior to them being incurred.");

            return body.ToString();
        }

        #endregion Private
    }
}
