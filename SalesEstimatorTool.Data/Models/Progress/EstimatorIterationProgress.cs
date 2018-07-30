using System;

namespace SalesEstimatorTool.Data.Models.Progress
{
    public class EstimatorIterationProgress : EstimatorBaseProgress
    {
        public EstimatorIterationProgress()
        {
            DateTime = DateTime.Now;
        }

        public Boolean IsIterationRestart { get; set; }

        public Int32? TimeToRestartSeconds { get; set; }
    }
}
