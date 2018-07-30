using System;

namespace SalesEstimatorTool.Data.Models
{
    public class WordComputeStatistic
    {
        public Int32 Words { get; set; }
        public Int32 WordsIncludeNotes { get; set; }
        public Int32 CharsWithSpaces { get; set; }
        public Int32 CharsWithSpacesIncludeNotes { get; set; }
        public Int32 CharsWithoutSpaces { get; set; }
        public Int32 CharsWithoutSpacesIncludeNotes { get; set; }
    }
}
