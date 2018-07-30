using System;
using System.Collections.Generic;
using SalesEstimatorTool.Data.Models;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface IParsingService
    {
        InputInformation ParseEmailBody(String body, IEnumerable<LanguageMapping> fromEnglishMappings, IEnumerable<LanguageMapping> toEnglishMappings, SalesEstimator model);
    }
}
