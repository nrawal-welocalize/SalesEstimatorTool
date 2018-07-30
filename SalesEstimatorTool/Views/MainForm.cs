using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesEstimatorTool.Business;
using SalesEstimatorTool.Data.Enums;
using SalesEstimatorTool.Data.Extensions;
using SalesEstimatorTool.Data.Models.Progress;

namespace SalesEstimatorTool.Views
{
    public partial class MainForm : Form
    {
        private readonly SalesEstimatorManager _salesEstimatorManager = new SalesEstimatorManager();
        private readonly List<EstimatorLogProgress> _progressList = new List<EstimatorLogProgress>();
        private ProgressType? _currentProgressType;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            _salesEstimatorManager.TaskStarted += salesEstimatorManager_TaskStarted;
            _salesEstimatorManager.TaskStopped += salesEstimatorManager_TaskStopped;

            btStart.Select();
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_salesEstimatorManager.IsCalculatorRunning)
            {
                e.Cancel = true;
                btStop.PerformClick();

                await WaitingTaskCompletionAsync();

                Close();
            }
        }

        void salesEstimatorManager_TaskStarted(object sender, EventArgs e)
        {
            btStart.Enabled = false;
            btStop.Enabled = true;

            lblStatus.Text = @"Working";

            tmTotal.Start();
        }

        void salesEstimatorManager_TaskStopped(object sender, EventArgs e)
        {
            btStart.Enabled = true;
            btStop.Enabled = false;

            lblStatus.Text = @"Stopped";

            tmTotal.Stop();
            tmCurrentTask.Stop();
            tmCurrentTaskRestart.Stop();
        }

        private async void btStart_Click(object sender, EventArgs e)
        {
            var progress = new Progress<EstimatorBaseProgress>(UpdateProgress);
            await _salesEstimatorManager.RunSalesEstimatorToolAsync(progress);
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            lblStatus.Text = @"Stopping";

            UpdateProgress(new EstimatorLogProgress
            {
                Message = "Task is stopping. Please wait...",
                Type = ProgressType.Text
            });

            _salesEstimatorManager.StopCalculator();
        }

        private void rbLogFilter_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = (RadioButton)sender;

            if (radioButton != null && radioButton.Checked)
            {
                var tag = String.Format("{0}", radioButton.Tag);
                _currentProgressType = String.IsNullOrWhiteSpace(tag) ? null : (ProgressType?)Int32.Parse(tag);

                FilterLogMessages();
            }
        }

        private void tmTotal_Tick(object sender, EventArgs e)
        {
            var timeSpan = lblTime.Text.ToTimeSpan();
            lblTime.Text = timeSpan.Add(TimeSpan.FromMilliseconds(tmTotal.Interval)).ToFormattedString();
        }

        private void tmCurrentTask_Tick(object sender, EventArgs e)
        {
            var timeSpan = ParseLabelTextToTimeSpan(lblCurrTaskTime.Text);
            lblCurrTaskTime.Text = timeSpan.Add(TimeSpan.FromMilliseconds(tmCurrentTask.Interval)).ToFormattedString();
        }

        private void tmCurrentTaskRestart_Tick(object sender, EventArgs e)
        {
            var timeSpan = ParseLabelTextToTimeSpan(lblTimeToStartNew.Text);
            lblTimeToStartNew.Text = timeSpan.Add(TimeSpan.FromMilliseconds(-1 * tmCurrentTask.Interval)).ToFormattedString();
        }

        #endregion

        #region Private

        private Task WaitingTaskCompletionAsync()
        {
            UseWaitCursor = true;

            return Task.Factory.StartNew(() =>
            {
                while (_salesEstimatorManager.IsCalculatorRunning)
                {
                    Thread.Sleep(100);
                }
            });
        }
        
        private void UpdateProgress(EstimatorBaseProgress model)
        {
            var logModel = model as EstimatorLogProgress;
            if (logModel != null)
            {
                UpdateLog(logModel);
            }

            var statusModel = model as EstimatorStatusProgress;
            if (statusModel != null)
            {
                UpdateStatus(statusModel);
            }

            var infoModel = model as EstimatorInfoProgress;
            if (infoModel != null)
            {
                UpdateInfo(infoModel);
            }

            var iterationModel = model as EstimatorIterationProgress;
            if (iterationModel != null)
            {
                UpdateIterationInfo(iterationModel);
            }
        }

        private void UpdateStatus(EstimatorStatusProgress model)
        {
            lblCurrTaskStatus.Text = model.Status.Description();
        }

        private void UpdateLog(EstimatorLogProgress model)
        {
            _progressList.Add(model);
            ShowLogMessage(model);
        }

        private void UpdateInfo(EstimatorInfoProgress model)
        {
            if (model.UnreadMessage.HasValue)
            {
                lblCurrTaskUnreadEmails.Text = model.UnreadMessage.Value.ToString();
            }

            if (model.ReadMessage.HasValue)
            {
                lblReadEmails.Text = (ParseLabelTextToInt32(lblReadEmails.Text) + model.ReadMessage.Value).ToString();
                lblCurrTaskReadEmails.Text = (ParseLabelTextToInt32(lblCurrTaskReadEmails.Text) + model.ReadMessage.Value).ToString();
                lblCurrTaskUnreadEmails.Text = (ParseLabelTextToInt32(lblCurrTaskUnreadEmails.Text) - model.ReadMessage.Value).ToString();
            }
        }

        private void UpdateIterationInfo(EstimatorIterationProgress model)
        {
            if (model.IsIterationRestart)
            {
                tmCurrentTaskRestart.Stop();

                tmCurrentTask.Start();
            }
            else
            {
                tmCurrentTask.Stop();

                if (model.TimeToRestartSeconds.HasValue)
                {
                    lblTimeToStartNew.Text = TimeSpan.FromSeconds(model.TimeToRestartSeconds.Value).ToFormattedString();
                    tmCurrentTaskRestart.Start();
                }
            }
            
        }

        private void FilterLogMessages()
        {
            rtbLog.SuspendLayout();
            rtbLog.Clear();

            var models = _currentProgressType.HasValue
                ? _progressList.Where(n => n.Type == _currentProgressType).ToList()
                : _progressList;

            models.ForEach(ShowLogMessage);

            rtbLog.ResumeLayout(false);
        }
        private void ShowLogMessage(EstimatorLogProgress model)
        {
            if (!_currentProgressType.HasValue || _currentProgressType.Value == model.Type)
            {
                var message = String.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}", model.DateTime, model.Message);

                rtbLog.SelectionColor = model.Type.ForeColor();
                rtbLog.AppendText(rtbLog.TextLength > 0 ? String.Concat(Environment.NewLine, message) : message);

                if (model.Exception != null)
                {
                    rtbLog.AppendText(String.Concat(Environment.NewLine, model.Exception.ToString()));
                }
            }
        }

        private static Int32 ParseLabelTextToInt32(String text)
        {
            var value = text.Replace("-", String.Empty).Trim();

            if (String.IsNullOrEmpty(value))
            {
                value = "0";
            }

            return Int32.Parse(value);
        }

        private static TimeSpan ParseLabelTextToTimeSpan(String text)
        {
            var value = text.Replace("-", String.Empty).Trim();

            if (String.IsNullOrEmpty(value))
            {
                value = "00:00:00";
            }

            return value.ToTimeSpan();
        }

        #endregion
    }
}
