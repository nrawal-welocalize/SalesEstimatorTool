using System;
using System.Collections.Generic;
using SalesEstimatorTool.Data.Models;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface IExcelService
    {
        IEnumerable<LanguageMapping> GetFromEnglishMappings();

        IEnumerable<LanguageMapping> GetToEnglishMappings();

        String ConvertToWordDocument(String pathToExcelFile);
    }
}
