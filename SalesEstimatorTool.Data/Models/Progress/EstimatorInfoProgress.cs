using System;
using System.Collections.Generic;

namespace SalesEstimatorTool.Data.Models.Progress
{
    public class EstimatorInfoProgress : EstimatorBaseProgress
    {
        public EstimatorInfoProgress()
        {
            DateTime = DateTime.Now;
            GenerateExcelPaths = new List<String>();
        }

        public Int32? ReadMessage { get; set; }

        public Int32? UnreadMessage { get; set; }

        public String Subject { get; set; }

        public List<String> GenerateExcelPaths { get; set; }
    }
}
