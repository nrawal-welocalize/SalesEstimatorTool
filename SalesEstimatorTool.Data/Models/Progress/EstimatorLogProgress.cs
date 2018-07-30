using System;
using SalesEstimatorTool.Data.Enums;

namespace SalesEstimatorTool.Data.Models.Progress
{
    public class EstimatorLogProgress : EstimatorBaseProgress
    {
        public EstimatorLogProgress()
        {
            DateTime = DateTime.Now;
            Type = ProgressType.Text;
        }

        public String Message { get; set; }

        public Exception Exception { get; set; }

        public ProgressType Type { get; set; }
    }
}
