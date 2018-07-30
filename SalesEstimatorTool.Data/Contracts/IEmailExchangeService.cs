using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;
using SalesEstimatorTool.Data.Models;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface IEmailExchangeService
    {
        IEnumerable<EmailMessageItem> GetUnreadMessages();
        void MarkAsRead(EmailMessageItem message);

        String GetEmailBody(EmailMessageItem message);
        IList<Attachment> GetEmailAttachments(EmailMessageItem message);
        void Reply(EmailMessageItem message, SalesEstimator salesEstimator);
        void ReplyWithException(EmailMessageItem message, Exception exception);
        void ResendExceptionToAdmin(EmailMessageItem message, Exception exception);
    }
}
