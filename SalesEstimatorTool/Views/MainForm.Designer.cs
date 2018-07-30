namespace SalesEstimatorTool.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.pMsgTypes = new System.Windows.Forms.Panel();
            this.rbError = new System.Windows.Forms.RadioButton();
            this.rbWarning = new System.Windows.Forms.RadioButton();
            this.rbInfo = new System.Windows.Forms.RadioButton();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.gbCurrentTaskInfo = new System.Windows.Forms.GroupBox();
            this.lblCurrTaskStatusCaption = new System.Windows.Forms.Label();
            this.lblCurrTaskUnreadEmailsCaption = new System.Windows.Forms.Label();
            this.lblTimeToStartNew = new System.Windows.Forms.Label();
            this.lblCurrTaskTime = new System.Windows.Forms.Label();
            this.lblCurrTaskUnreadEmails = new System.Windows.Forms.Label();
            this.lblCurrTaskStatus = new System.Windows.Forms.Label();
            this.lblCurrTaskReadEmailsCaption = new System.Windows.Forms.Label();
            this.lblCurrTaskReadEmails = new System.Windows.Forms.Label();
            this.lblTimeToStartNewCaption = new System.Windows.Forms.Label();
            this.lblCurrTaskTimeCaption = new System.Windows.Forms.Label();
            this.gbSummaryInfo = new System.Windows.Forms.GroupBox();
            this.lblStatusCaption = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblReadEmailsCaption = new System.Windows.Forms.Label();
            this.lblReadEmails = new System.Windows.Forms.Label();
            this.lblTimeCaption = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.tmTotal = new System.Windows.Forms.Timer(this.components);
            this.tmCurrentTask = new System.Windows.Forms.Timer(this.components);
            this.tmCurrentTaskRestart = new System.Windows.Forms.Timer(this.components);
            this.ilListView = new System.Windows.Forms.ImageList(this.components);
            this.gbLog.SuspendLayout();
            this.pMsgTypes.SuspendLayout();
            this.gbMain.SuspendLayout();
            this.gbCurrentTaskInfo.SuspendLayout();
            this.gbSummaryInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLog
            // 
            this.gbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLog.Controls.Add(this.rtbLog);
            this.gbLog.Controls.Add(this.pMsgTypes);
            this.gbLog.Location = new System.Drawing.Point(277, 0);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(609, 437);
            this.gbLog.TabIndex = 0;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(3, 42);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbLog.Size = new System.Drawing.Size(603, 392);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // pMsgTypes
            // 
            this.pMsgTypes.Controls.Add(this.rbError);
            this.pMsgTypes.Controls.Add(this.rbWarning);
            this.pMsgTypes.Controls.Add(this.rbInfo);
            this.pMsgTypes.Controls.Add(this.rbText);
            this.pMsgTypes.Controls.Add(this.rbAll);
            this.pMsgTypes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMsgTypes.Location = new System.Drawing.Point(3, 16);
            this.pMsgTypes.Name = "pMsgTypes";
            this.pMsgTypes.Size = new System.Drawing.Size(603, 26);
            this.pMsgTypes.TabIndex = 2;
            // 
            // rbError
            // 
            this.rbError.AutoSize = true;
            this.rbError.Location = new System.Drawing.Point(254, 3);
            this.rbError.Name = "rbError";
            this.rbError.Size = new System.Drawing.Size(52, 17);
            this.rbError.TabIndex = 4;
            this.rbError.Tag = "3";
            this.rbError.Text = "Errors";
            this.rbError.UseVisualStyleBackColor = true;
            this.rbError.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // rbWarning
            // 
            this.rbWarning.AutoSize = true;
            this.rbWarning.Location = new System.Drawing.Point(170, 3);
            this.rbWarning.Name = "rbWarning";
            this.rbWarning.Size = new System.Drawing.Size(70, 17);
            this.rbWarning.TabIndex = 3;
            this.rbWarning.Tag = "2";
            this.rbWarning.Text = "Warnings";
            this.rbWarning.UseVisualStyleBackColor = true;
            this.rbWarning.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // rbInfo
            // 
            this.rbInfo.AutoSize = true;
            this.rbInfo.Location = new System.Drawing.Point(113, 3);
            this.rbInfo.Name = "rbInfo";
            this.rbInfo.Size = new System.Drawing.Size(43, 17);
            this.rbInfo.TabIndex = 2;
            this.rbInfo.Tag = "1";
            this.rbInfo.Text = "Info";
            this.rbInfo.UseVisualStyleBackColor = true;
            this.rbInfo.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // rbText
            // 
            this.rbText.AutoSize = true;
            this.rbText.Location = new System.Drawing.Point(53, 3);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(46, 17);
            this.rbText.TabIndex = 1;
            this.rbText.Tag = "0";
            this.rbText.Text = "Text";
            this.rbText.UseVisualStyleBackColor = true;
            this.rbText.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(3, 3);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbLogFilter_CheckedChanged);
            // 
            // gbMain
            // 
            this.gbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbMain.Controls.Add(this.gbCurrentTaskInfo);
            this.gbMain.Controls.Add(this.gbSummaryInfo);
            this.gbMain.Controls.Add(this.btStart);
            this.gbMain.Controls.Add(this.btStop);
            this.gbMain.Location = new System.Drawing.Point(0, 0);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(271, 434);
            this.gbMain.TabIndex = 1;
            this.gbMain.TabStop = false;
            this.gbMain.Text = "Main";
            // 
            // gbCurrentTaskInfo
            // 
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskStatusCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskUnreadEmailsCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblTimeToStartNew);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskTime);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskUnreadEmails);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskStatus);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskReadEmailsCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskReadEmails);
            this.gbCurrentTaskInfo.Controls.Add(this.lblTimeToStartNewCaption);
            this.gbCurrentTaskInfo.Controls.Add(this.lblCurrTaskTimeCaption);
            this.gbCurrentTaskInfo.Location = new System.Drawing.Point(12, 139);
            this.gbCurrentTaskInfo.Name = "gbCurrentTaskInfo";
            this.gbCurrentTaskInfo.Size = new System.Drawing.Size(250, 152);
            this.gbCurrentTaskInfo.TabIndex = 5;
            this.gbCurrentTaskInfo.TabStop = false;
            this.gbCurrentTaskInfo.Text = "Current Task Information";
            // 
            // lblCurrTaskStatusCaption
            // 
            this.lblCurrTaskStatusCaption.AutoSize = true;
            this.lblCurrTaskStatusCaption.Location = new System.Drawing.Point(6, 25);
            this.lblCurrTaskStatusCaption.Name = "lblCurrTaskStatusCaption";
            this.lblCurrTaskStatusCaption.Size = new System.Drawing.Size(40, 13);
            this.lblCurrTaskStatusCaption.TabIndex = 1;
            this.lblCurrTaskStatusCaption.Text = "Status:";
            // 
            // lblCurrTaskUnreadEmailsCaption
            // 
            this.lblCurrTaskUnreadEmailsCaption.AutoSize = true;
            this.lblCurrTaskUnreadEmailsCaption.Location = new System.Drawing.Point(6, 50);
            this.lblCurrTaskUnreadEmailsCaption.Name = "lblCurrTaskUnreadEmailsCaption";
            this.lblCurrTaskUnreadEmailsCaption.Size = new System.Drawing.Size(77, 13);
            this.lblCurrTaskUnreadEmailsCaption.TabIndex = 1;
            this.lblCurrTaskUnreadEmailsCaption.Text = "Unread emails:";
            // 
            // lblTimeToStartNew
            // 
            this.lblTimeToStartNew.AutoSize = true;
            this.lblTimeToStartNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTimeToStartNew.Location = new System.Drawing.Point(93, 128);
            this.lblTimeToStartNew.Name = "lblTimeToStartNew";
            this.lblTimeToStartNew.Size = new System.Drawing.Size(27, 13);
            this.lblTimeToStartNew.TabIndex = 2;
            this.lblTimeToStartNew.Text = "- - -";
            // 
            // lblCurrTaskTime
            // 
            this.lblCurrTaskTime.AutoSize = true;
            this.lblCurrTaskTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskTime.Location = new System.Drawing.Point(93, 102);
            this.lblCurrTaskTime.Name = "lblCurrTaskTime";
            this.lblCurrTaskTime.Size = new System.Drawing.Size(27, 13);
            this.lblCurrTaskTime.TabIndex = 2;
            this.lblCurrTaskTime.Text = "- - -";
            // 
            // lblCurrTaskUnreadEmails
            // 
            this.lblCurrTaskUnreadEmails.AutoSize = true;
            this.lblCurrTaskUnreadEmails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskUnreadEmails.Location = new System.Drawing.Point(93, 50);
            this.lblCurrTaskUnreadEmails.Name = "lblCurrTaskUnreadEmails";
            this.lblCurrTaskUnreadEmails.Size = new System.Drawing.Size(27, 13);
            this.lblCurrTaskUnreadEmails.TabIndex = 2;
            this.lblCurrTaskUnreadEmails.Text = "- - -";
            // 
            // lblCurrTaskStatus
            // 
            this.lblCurrTaskStatus.AutoSize = true;
            this.lblCurrTaskStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskStatus.Location = new System.Drawing.Point(93, 25);
            this.lblCurrTaskStatus.Name = "lblCurrTaskStatus";
            this.lblCurrTaskStatus.Size = new System.Drawing.Size(27, 13);
            this.lblCurrTaskStatus.TabIndex = 2;
            this.lblCurrTaskStatus.Text = "- - -";
            // 
            // lblCurrTaskReadEmailsCaption
            // 
            this.lblCurrTaskReadEmailsCaption.AutoSize = true;
            this.lblCurrTaskReadEmailsCaption.Location = new System.Drawing.Point(6, 75);
            this.lblCurrTaskReadEmailsCaption.Name = "lblCurrTaskReadEmailsCaption";
            this.lblCurrTaskReadEmailsCaption.Size = new System.Drawing.Size(68, 13);
            this.lblCurrTaskReadEmailsCaption.TabIndex = 1;
            this.lblCurrTaskReadEmailsCaption.Text = "Read emails:";
            // 
            // lblCurrTaskReadEmails
            // 
            this.lblCurrTaskReadEmails.AutoSize = true;
            this.lblCurrTaskReadEmails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrTaskReadEmails.Location = new System.Drawing.Point(93, 75);
            this.lblCurrTaskReadEmails.Name = "lblCurrTaskReadEmails";
            this.lblCurrTaskReadEmails.Size = new System.Drawing.Size(27, 13);
            this.lblCurrTaskReadEmails.TabIndex = 2;
            this.lblCurrTaskReadEmails.Text = "- - -";
            // 
            // lblTimeToStartNewCaption
            // 
            this.lblTimeToStartNewCaption.AutoSize = true;
            this.lblTimeToStartNewCaption.Location = new System.Drawing.Point(6, 128);
            this.lblTimeToStartNewCaption.Name = "lblTimeToStartNewCaption";
            this.lblTimeToStartNewCaption.Size = new System.Drawing.Size(68, 13);
            this.lblTimeToStartNewCaption.TabIndex = 1;
            this.lblTimeToStartNewCaption.Text = "Time to new:";
            // 
            // lblCurrTaskTimeCaption
            // 
            this.lblCurrTaskTimeCaption.AutoSize = true;
            this.lblCurrTaskTimeCaption.Location = new System.Drawing.Point(6, 102);
            this.lblCurrTaskTimeCaption.Name = "lblCurrTaskTimeCaption";
            this.lblCurrTaskTimeCaption.Size = new System.Drawing.Size(79, 13);
            this.lblCurrTaskTimeCaption.TabIndex = 1;
            this.lblCurrTaskTimeCaption.Text = "Time from start:";
            // 
            // gbSummaryInfo
            // 
            this.gbSummaryInfo.Controls.Add(this.lblStatusCaption);
            this.gbSummaryInfo.Controls.Add(this.lblTime);
            this.gbSummaryInfo.Controls.Add(this.lblStatus);
            this.gbSummaryInfo.Controls.Add(this.lblReadEmailsCaption);
            this.gbSummaryInfo.Controls.Add(this.lblReadEmails);
            this.gbSummaryInfo.Controls.Add(this.lblTimeCaption);
            this.gbSummaryInfo.Location = new System.Drawing.Point(12, 19);
            this.gbSummaryInfo.Name = "gbSummaryInfo";
            this.gbSummaryInfo.Size = new System.Drawing.Size(250, 114);
            this.gbSummaryInfo.TabIndex = 4;
            this.gbSummaryInfo.TabStop = false;
            this.gbSummaryInfo.Text = "Summary Information";
            // 
            // lblStatusCaption
            // 
            this.lblStatusCaption.AutoSize = true;
            this.lblStatusCaption.Location = new System.Drawing.Point(6, 25);
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.Size = new System.Drawing.Size(40, 13);
            this.lblStatusCaption.TabIndex = 1;
            this.lblStatusCaption.Text = "Status:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTime.Location = new System.Drawing.Point(91, 74);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(57, 13);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "00:00:00";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(91, 25);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(54, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Stopped";
            // 
            // lblReadEmailsCaption
            // 
            this.lblReadEmailsCaption.AutoSize = true;
            this.lblReadEmailsCaption.Location = new System.Drawing.Point(6, 50);
            this.lblReadEmailsCaption.Name = "lblReadEmailsCaption";
            this.lblReadEmailsCaption.Size = new System.Drawing.Size(68, 13);
            this.lblReadEmailsCaption.TabIndex = 1;
            this.lblReadEmailsCaption.Text = "Read emails:";
            // 
            // lblReadEmails
            // 
            this.lblReadEmails.AutoSize = true;
            this.lblReadEmails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReadEmails.Location = new System.Drawing.Point(93, 50);
            this.lblReadEmails.Name = "lblReadEmails";
            this.lblReadEmails.Size = new System.Drawing.Size(14, 13);
            this.lblReadEmails.TabIndex = 2;
            this.lblReadEmails.Text = "0";
            // 
            // lblTimeCaption
            // 
            this.lblTimeCaption.AutoSize = true;
            this.lblTimeCaption.Location = new System.Drawing.Point(6, 74);
            this.lblTimeCaption.Name = "lblTimeCaption";
            this.lblTimeCaption.Size = new System.Drawing.Size(79, 13);
            this.lblTimeCaption.TabIndex = 1;
            this.lblTimeCaption.Text = "Time from start:";
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(12, 297);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(90, 35);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "START";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(108, 297);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(90, 35);
            this.btStop.TabIndex = 1;
            this.btStop.Text = "STOP";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // tmTotal
            // 
            this.tmTotal.Interval = 1000;
            this.tmTotal.Tick += new System.EventHandler(this.tmTotal_Tick);
            // 
            // tmCurrentTask
            // 
            this.tmCurrentTask.Interval = 1000;
            this.tmCurrentTask.Tick += new System.EventHandler(this.tmCurrentTask_Tick);
            // 
            // tmCurrentTaskRestart
            // 
            this.tmCurrentTaskRestart.Interval = 1000;
            this.tmCurrentTaskRestart.Tick += new System.EventHandler(this.tmCurrentTaskRestart_Tick);
            // 
            // ilListView
            // 
            this.ilListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilListView.ImageStream")));
            this.ilListView.TransparentColor = System.Drawing.Color.Transparent;
            this.ilListView.Images.SetKeyName(0, "ExcelIcon.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 436);
            this.Controls.Add(this.gbMain);
            this.Controls.Add(this.gbLog);
            this.MinimumSize = new System.Drawing.Size(900, 475);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Estimator Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbLog.ResumeLayout(false);
            this.pMsgTypes.ResumeLayout(false);
            this.pMsgTypes.PerformLayout();
            this.gbMain.ResumeLayout(false);
            this.gbCurrentTaskInfo.ResumeLayout(false);
            this.gbCurrentTaskInfo.PerformLayout();
            this.gbSummaryInfo.ResumeLayout(false);
            this.gbSummaryInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Panel pMsgTypes;
        private System.Windows.Forms.RadioButton rbError;
        private System.Windows.Forms.RadioButton rbWarning;
        private System.Windows.Forms.RadioButton rbInfo;
        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.GroupBox gbCurrentTaskInfo;
        private System.Windows.Forms.Label lblCurrTaskStatusCaption;
        private System.Windows.Forms.Label lblCurrTaskUnreadEmailsCaption;
        private System.Windows.Forms.Label lblTimeToStartNew;
        private System.Windows.Forms.Label lblCurrTaskTime;
        private System.Windows.Forms.Label lblCurrTaskUnreadEmails;
        private System.Windows.Forms.Label lblCurrTaskStatus;
        private System.Windows.Forms.Label lblCurrTaskReadEmailsCaption;
        private System.Windows.Forms.Label lblCurrTaskReadEmails;
        private System.Windows.Forms.Label lblTimeToStartNewCaption;
        private System.Windows.Forms.Label lblCurrTaskTimeCaption;
        private System.Windows.Forms.GroupBox gbSummaryInfo;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblReadEmailsCaption;
        private System.Windows.Forms.Label lblReadEmails;
        private System.Windows.Forms.Label lblTimeCaption;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Timer tmTotal;
        private System.Windows.Forms.Timer tmCurrentTask;
        private System.Windows.Forms.Timer tmCurrentTaskRestart;
        private System.Windows.Forms.ImageList ilListView;
    }
}

