using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Enums;
using SalesEstimatorTool.Data.Models;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class ParsingService : IParsingService
    {
        public InputInformation ParseEmailBody(String body, IEnumerable<LanguageMapping> fromEnglishMappings, IEnumerable<LanguageMapping> toEnglishMappings, SalesEstimator model)
        {
            var result = new InputInformation();
            var hasEngLang = false;

            if(body == null)
            {
                result.NonCorrectStatus += @"Body has to contains information about Source and Target Languages (Tier and Rush are optional).";
                return result;
            }

            //Regex will parse strings likes "Rush: 3", but not "Rush - 3"
            body = body.Replace(" -", ":");

            var regex = new Regex(@"Source Language:(?<SourceLanguage>.+)\s");
            var @group = regex.Match(body).Groups["SourceLanguage"];
            if (group.Success)
            {
                var language = group.Value.Trim();
                if (language != "English")
                {
                    result.SourceLanguage = toEnglishMappings.SingleOrDefault(n => language == n.Language);
                    if (result.SourceLanguage != null)
                        model.SourceLanguage = result.SourceLanguage;
                    else
                        result.NonCorrectStatus += string.Format("<br>Unknown Source Language - {0}", language);
                }
                else
                {
                    hasEngLang = true;
                    result.SourceLanguage = new LanguageMapping { Language = language };
                    model.SourceLanguage = result.SourceLanguage;
                    model.IsSourceLanguageEnglish = true;
                }
            }
            else
            {
                result.NonCorrectStatus += @"<br> There is no Source Language. This information is required.";
            }

            regex = new Regex(@"Target Language:(?<TargetLanguage>.+)\s");
            @group = regex.Match(body).Groups["TargetLanguage"];
            if (group.Success)
            {
                var language = group.Value.Trim();
                if (language != "English")
                {
                    result.TargetLanguage = fromEnglishMappings.SingleOrDefault(n => language == n.Language);
                    if (result.TargetLanguage != null)
                        model.DestinationLanguage = result.TargetLanguage;
                    else
                        result.NonCorrectStatus += string.Format("<br>Unknown Target Language - {0}", language);
                }
                else
                {
                    hasEngLang = true;
                    result.TargetLanguage = new LanguageMapping { Language = language };
                    model.DestinationLanguage = result.TargetLanguage;
                    model.IsSourceLanguageEnglish = false;
                }
            }
            else
            {
                result.NonCorrectStatus += @"<br>There is no Target Language. This information is required.";
            }

            //Either Source Language or Target Language has to be English
            if (!hasEngLang)
                result.NonCorrectStatus += @"<br>Either Source Language or Target Language has to be English.";

            regex = new Regex(@"Tier:(?<Tier>.+)\s");
            @group = regex.Match(body).Groups["Tier"];
            if (group.Success)
            {
                var tier = group.Value.Trim();

                switch (tier)
                {
                    case "3":
                        model.Tier = Tier.Tier3;
                        break;
                    case "3.5":
                        model.Tier = Tier.Tier35;
                        break;
                    case "4":
                        model.Tier = Tier.Tier4;
                        break;
                    default:
                        result.NonCorrectStatus 
                            += string.Format("<br>Unknown Tier - {0}. Please select from next options: 3, 3.5, 4", tier);
                        break;
                }
            }
            else
            {
                model.Tier = Tier.Tier4;
            }

            regex = new Regex(@"Rush:(?<Rush>.+)\s");
            @group = regex.Match(body).Groups["Rush"];
            if (group.Success)
            {
                string rush = group.Value.Trim().ToLower();

                switch (rush)
                {
                    case "1":
                        model.Rush = Rush.Rush1;
                        break;
                    case "3":
                        model.Rush = Rush.Rush3;
                        break;
                    case "6":
                        model.Rush = Rush.Rush6;
                        break;
                    case "no":
                        model.Rush = Rush.No;
                        break;
                    default:
                        result.NonCorrectStatus 
                            += string.Format("<br>Unknown Rush - {0}. Please select from next options: No, 1, 3, 6", rush);
                        break;
                }
            }
            else
            {
                model.Rush = Rush.No;
            }

            return result;
        }

        #region Private

        #endregion Private
    }
}
