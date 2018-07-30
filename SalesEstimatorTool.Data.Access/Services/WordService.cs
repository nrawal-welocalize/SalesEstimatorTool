using System;
using System.Reflection;
using NetOffice.WordApi;
using NetOffice.WordApi.Enums;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Models;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class WordService : IWordService
    { 
        public WordComputeStatistic GetComputeStatistic(String path)
        {
            using (var application = new Application())
            {
                WordComputeStatistic result;
                using (var document = application.Documents.Open(path, Missing.Value, false))
                {
                    result = new WordComputeStatistic
                    {
                        Words = document.ComputeStatistics(WdStatistic.wdStatisticWords, false),
                        WordsIncludeNotes = document.ComputeStatistics(WdStatistic.wdStatisticWords, true),
                        CharsWithSpaces = document.ComputeStatistics(WdStatistic.wdStatisticCharacters, false),
                        CharsWithSpacesIncludeNotes = document.ComputeStatistics(WdStatistic.wdStatisticCharacters, true),
                        CharsWithoutSpaces = document.ComputeStatistics(WdStatistic.wdStatisticCharactersWithSpaces, false),
                        CharsWithoutSpacesIncludeNotes = document.ComputeStatistics(WdStatistic.wdStatisticCharactersWithSpaces, true)
                    };
                    document.Save();
                    document.Close(false);
                } 
                application.Quit(false);
                return result;
            }
        }

        #region Private 

        #endregion Private
    }
}
