using System;
using System.Collections.Generic;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Enums;

namespace SalesEstimatorTool.Data.Models
{
    public class SalesEstimator
    {
        public SalesEstimator()
        {
            WordCounterList = new List<WordCounter>();
            NotSupportedFileList = new List<WordCounter>();
        }
        public LanguageMapping DestinationLanguage { get; set; }
        public LanguageMapping SourceLanguage { get; set; }
        public Tier Tier { get; set; }
        public Rush Rush { get; set; }
        public Boolean IsSourceLanguageEnglish { get; set; }
        public List<WordCounter> WordCounterList { get; set; }
        public List<WordCounter> NotSupportedFileList { get; set; }
        public String Errors { get; private set; }

        public void AddError(String error)
        {
            if (String.IsNullOrEmpty(Errors))
            {
                Errors = "Errors:";
            }
            Errors = String.Format("{0}<br> {1}", Errors, error);
        }

        /// <summary>
        /// Get the cost of file translation
        /// </summary>
        /// <param name="wordCounter">file data</param>
        /// <returns></returns>
        public Decimal GetCost(WordCounter wordCounter)
        {
            Decimal result;
            if (IsSourceLanguageEnglish)
            {
                result = wordCounter.WordCount * DestinationLanguage.Prices[Tier];
            }
            else
            {
                result = SourceLanguage.Prices[Tier];

                switch (SourceLanguage.Action)
                {
                    case ActionType.Divide:
                        result = result * wordCounter.CharCount / SourceLanguage.Divisor;
                        break;
                    case ActionType.DivideWord:
                        result = result * wordCounter.WordCount / SourceLanguage.Divisor;
                        break;
                    case ActionType.Multiply:
                        result = result * wordCounter.CharCount * SourceLanguage.Divisor;
                        break;
                    case ActionType.MultiplyWord:
                        result = result * wordCounter.WordCount * SourceLanguage.Divisor;
                        break;
                } 
            }

            return Decimal.Round(result, 2);
        }

        public Double GetTurnaroundTime(WordCounter wordCounter)
        {
            var language = IsSourceLanguageEnglish
                ? DestinationLanguage
                : SourceLanguage;

            return Rush == Rush.No
                ? (Double)wordCounter.WordCount / language.TurnaroundTime[Tier]
                : wordCounter.WordCount / (Double)(language.TurnaroundTime[Tier.Tier35] * language.Rush[Rush]);
        }
    }
}
