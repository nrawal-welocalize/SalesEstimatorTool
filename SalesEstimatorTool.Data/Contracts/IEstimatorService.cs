using System;
using System.Threading.Tasks;
using SalesEstimatorTool.Data.Models.Progress;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface IEstimatorService
    {
        Boolean IsProcessRunning { get; }

        Task StartEstimatorTaskAsync(IProgress<EstimatorBaseProgress> progress);

        void StopCalculatorTask();

        event EventHandler TaskStarted;

        event EventHandler TaskStoped;

    }
}
