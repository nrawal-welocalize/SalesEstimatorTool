using System;
using System.Threading.Tasks;
using Autofac;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Enums;
using SalesEstimatorTool.Data.Models.Progress;

namespace SalesEstimatorTool.Business
{
    public class SalesEstimatorManager
    {
        #region Field
        private readonly IEstimatorService _estimatorService;
        private Task _salesEstimatorTask;
        #endregion

        #region Event
        public event EventHandler TaskStarted;
        public event EventHandler TaskStopped;
        #endregion

        #region Property
        public Boolean IsCalculatorRunning
        {
            get
            {
                return (_salesEstimatorTask != null && !_salesEstimatorTask.IsCompleted);
            }
        }
        #endregion

        #region Constructor

        public SalesEstimatorManager()
        {
            var container = RegistrationHelper.CreateDependenciesContainer();
            _estimatorService = container.Resolve<IEstimatorService>();
            _estimatorService.TaskStarted += (s, e) =>
            {
                if (TaskStarted != null)
                {
                    TaskStarted(this, e);
                }
            };

            _estimatorService.TaskStoped += (s, e) =>
            {
                if (TaskStopped != null)
                {
                    TaskStopped(this, e);
                }
            };
        } 
        #endregion

        #region Methods
        public async Task RunSalesEstimatorToolAsync(IProgress<EstimatorBaseProgress> progress)
        {
            try
            {
                _salesEstimatorTask = _estimatorService.StartEstimatorTaskAsync(progress);
                await _salesEstimatorTask;
            }
            catch (Exception exception)
            {
                var model = new EstimatorLogProgress
                {
                    Message = exception.Message,
                    Type = ProgressType.Error,
                    Exception = exception
                };

                progress.Report(model);

                StopCalculator();
            }
        }

        public void StopCalculator()
        {
            _estimatorService.StopCalculatorTask();
        }
        #endregion


    }
}
