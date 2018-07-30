using System; 

namespace SalesEstimatorTool.Data.Models
{
    public class WordCounter
    {
        public Int32 CharWithSpaceCount { get; set; }
        public Int32 CharWithoutSpaceCount { get; set; }
        public Int32 WordCount { get; set; }
        public String FileName { get; set; }
        public String FullFilePath { get; set; }

        public Decimal CharCount
        {
            get { return (CharWithSpaceCount + CharWithoutSpaceCount) * 0.5m; }
        }
    }
}
