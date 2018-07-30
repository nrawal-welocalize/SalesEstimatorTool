using System;


namespace SalesEstimatorTool.Data.Models
{
    public class InputInformation
    {
        public LanguageMapping TargetLanguage { get; set; }
        public LanguageMapping SourceLanguage { get; set; }
        public String NonCorrectStatus { get; set; }
    }
}

