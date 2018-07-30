using System;
using SalesEstimatorTool.Data.Enums;

namespace SalesEstimatorTool.Data.Models.Progress
{
    public class EstimatorStatusProgress : EstimatorBaseProgress
    {
        public EstimatorStatusProgress()
        {
            DateTime = DateTime.Now;
        }

        public ProgressTaskStatus Status { get; set; }
    }
}
