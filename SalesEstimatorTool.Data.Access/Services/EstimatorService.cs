using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Enums;
using SalesEstimatorTool.Data.Models;
using SalesEstimatorTool.Data.Models.Progress;
using Task = System.Threading.Tasks.Task;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class EstimatorService : IEstimatorService
    {
        private readonly ISettingsService _settingsService;
        private readonly IEmailExchangeService _emailExchangeService;
        private readonly IParsingService _parsingService;
        private readonly IExcelService _excelService;
        private readonly IWordService _wordService;
        private readonly IPowerPointService _presentService;
        private readonly IFileService _fileService;
        private readonly IImageService _imageService;
        private readonly IZipService _zipService;

        private readonly Object _locker = new Object();
        private CancellationTokenSource _cancellationTokenSource;


        public EstimatorService(ISettingsService settingsService,
                                IEmailExchangeService emailExchangeService,
                                IParsingService parsingService,
                                IExcelService excelService,
                                IWordService wordService,
                                IPowerPointService presentService,
                                IFileService fileService, IImageService imageService, IZipService zipService)
        {
            _settingsService = settingsService;
            _emailExchangeService = emailExchangeService;
            _parsingService = parsingService;
            _excelService = excelService;
            _wordService = wordService;
            _presentService = presentService;
            _fileService = fileService;
            _imageService = imageService;
            _zipService = zipService;
        }

        public Boolean IsProcessRunning { get; private set; }

        public Task StartEstimatorTaskAsync(IProgress<EstimatorBaseProgress> progress)
        {
            Task task;

            lock (_locker)
            {
                if (!IsProcessRunning)
                {
                    IsProcessRunning = true;

                    if (TaskStarted != null)
                    {
                        TaskStarted(this, EventArgs.Empty);
                    }

                    _cancellationTokenSource = new CancellationTokenSource();

                    task = Task.Factory
                        .StartNew(() => RunTask(progress))
                        .ContinueWith(n => CompleteTask(n, progress), TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    task = Task.Factory.StartNew(() => RunEmptyTask(progress));
                }
            }

            return task;
        }

        public void StopCalculatorTask()
        {
            if (IsProcessRunning)
            {
                _cancellationTokenSource.Cancel(true);
            }
            else
            {
                if (TaskStoped != null)
                {
                    TaskStoped(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler TaskStarted;

        public event EventHandler TaskStoped;

        #region Private

        private void RunTask(IProgress<EstimatorBaseProgress> progress)
        {
            ProgressLogTask(progress, "Task is started.");

            while (true)
            {
                ProgressStatusTask(progress, ProgressTaskStatus.Working);
                ProgressIterationTask(progress, true);
                ProgressLogTask(progress, "Iteration is started.");
                WaitTask();

                IList<LanguageMapping> fromeEnglishMappings = _excelService.GetFromEnglishMappings().ToList();
                IList<LanguageMapping> toEnglishMappings = _excelService.GetToEnglishMappings().ToList();

                ProgressLogTask(progress, "Excel settings are read correctly.");
                WaitTask();

                IList<EmailMessageItem> messages = _emailExchangeService.GetUnreadMessages().ToList();
                ProgressInfoTask(progress, null, messages.Count, null, null);
                ProgressLogTask(progress, String.Format("Found {0} emails.", messages.Count));
                WaitTask();

                foreach (EmailMessageItem message in messages)
                {
                    try
                    {
                        SalesEstimator model = EmailMessageItemProcessed(message, fromeEnglishMappings, toEnglishMappings, progress);

                        _emailExchangeService.Reply(message, model);
                        _emailExchangeService.MarkAsRead(message);

                        ProgressInfoTask(progress, 1, null, message.Subject, null);
                        ProgressLogTask(progress, String.Format("Emails with Subject: '{0}' processed success.", message.Subject));
                        WaitTask();
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch(ApplicationException ex)
                    {
                        ProgressLogTask(progress, ex.Message, ProgressType.Error);
                        _emailExchangeService.ReplyWithException(message, ex);
                    }
                    catch (Exception exception)
                    {
                        ProgressLogTask(progress, exception.Message, ProgressType.Error);
                        _emailExchangeService.ResendExceptionToAdmin(message, exception);
                    }
                }

                if (messages.Any())
                {
                    ProgressLogTask(progress, "All emails processed.");
                    WaitTask();
                }

                ProgressStatusTask(progress, ProgressTaskStatus.Ended);
                ProgressIterationTask(progress, false);

                Int32 seconds = _settingsService.TaskPauseSeconds;
                ProgressLogTask(progress, String.Format("Waiting {0} seconds to next iteration.", seconds));
                WaitTask(seconds * 1000);
            }
        }

        private SalesEstimator EmailMessageItemProcessed(EmailMessageItem message, IEnumerable<LanguageMapping> fromEnglishMappings, IEnumerable<LanguageMapping> toEnglishMappings, IProgress<EstimatorBaseProgress> progress)
        {
            String tempFolder = _settingsService.TempAttachmentFilePath;
            Boolean exists = Directory.Exists(tempFolder);
            if (!exists)
            {
                Directory.CreateDirectory(tempFolder);
            }

            String tempPath = String.Format("{0}{1}\\", tempFolder, DateTime.UtcNow.Ticks);
            exists = Directory.Exists(tempPath);
            if (!exists)
            {
                Directory.CreateDirectory(tempPath);
            }
            SalesEstimator model = new SalesEstimator();
            String body = _emailExchangeService.GetEmailBody(message);
            ProgressLogTask(progress, String.Format("Parsing email {0}.", message.Subject));
            WaitTask();
            InputInformation inputInformation = _parsingService.ParseEmailBody(body, fromEnglishMappings, toEnglishMappings, model);
            String status = inputInformation.NonCorrectStatus;

            if (!String.IsNullOrWhiteSpace(status))
            {
                String error = String.Format("Incorrect parsing email with Body: '{0}' <br>Error: {1}", body, status);
                throw new ApplicationException(error);
            }

            IList<Attachment> attach = _emailExchangeService.GetEmailAttachments(message); 

            _fileService.FileProcessed(attach, progress, tempPath);
            ProgressLogTask(progress, String.Format("Sorting attachments {0}.", message.Subject));

            IList<AttachmentUnit> units = _fileService.GetAttachmentUnits(tempPath);

            try
            {
                _zipService.UnzipProcessed(units, tempPath, progress);

            }
            catch (Exception exception)
            {
                model.AddError(exception.Message);
            }

            try
            {
                _imageService.AbbyProcessed(model, units, tempPath, progress);
            }
            catch (Exception exception)
            {
                model.AddError(exception.Message);
            }

            //convert all Xlsx and xls files to docx
            try
            {
                foreach (var unit in units.Where(u => u.CurrentType == FileType.Xlsx))
                {
                    ConvertExcelToText(unit, progress);
                }
            }
            catch (Exception ex)
            {
                model.AddError(ex.Message);
            }

            //convert all Pptx and Ppt files to docx
            try
            {
                foreach (var unit in units.Where(u => u.CurrentType == FileType.Pptx))
                {
                    ConvertPresentToText(unit, progress);
                }
            }
            catch (Exception ex)
            {
                model.AddError(ex.Message);
            }

            try
            {
                foreach (AttachmentUnit unit in units.Where(u => u.CurrentType == FileType.Docx))
                {
                    var statistic = GetStatistic(unit.PhisicalName, progress);

                    String fileName = Path.GetFileName(unit.PhisicalName);
                    model.WordCounterList.Add(new WordCounter
                    {
                        FileName = fileName,
                        WordCount = statistic.Words,
                        CharWithSpaceCount = statistic.CharsWithSpaces,
                        CharWithoutSpaceCount = statistic.CharsWithoutSpaces,
                        FullFilePath = unit.ZipFileName
                    });
                }
            }
            catch (Exception exception)
            {
                model.AddError(exception.Message);
            }
            foreach (AttachmentUnit unit in units.Where(u => u.CurrentType == FileType.None))
            {
                String fileName = Path.GetFileName(unit.PhisicalName);
                model.NotSupportedFileList.Add(new WordCounter
                {
                    FileName = fileName,
                    WordCount = 0,
                    FullFilePath = unit.ZipFileName
                });
            }

            Directory.Delete(tempPath, true);
            return model;
        }

        private void RunEmptyTask(IProgress<EstimatorBaseProgress> progress)
        {
            const String message = @"At present task is already carried out. It is necessary to wait for completion of task before starting new task.";
            ProgressLogTask(progress, message, ProgressType.Warning);
        }

        private void WaitTask(Int32 millisseconds = 100)
        {
            var canceled = _cancellationTokenSource.Token.WaitHandle.WaitOne(millisseconds);

            if (canceled)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }
        }

        private void CompleteTask(Task task, IProgress<EstimatorBaseProgress> progress)
        {
            IsProcessRunning = false;

            if (task.Exception != null)
            {
                if (task.Exception.InnerException.GetType() == typeof(OperationCanceledException))
                {
                    ProgressLogTask(progress, "Task was canceled by user.", ProgressType.Warning);

                    if (TaskStoped != null)
                    {
                        TaskStoped(this, EventArgs.Empty);
                    }
                }
                else
                {
                    throw task.Exception;
                }
            }
        }

        private void ProgressLogTask(IProgress<EstimatorBaseProgress> progress, String message, ProgressType type = ProgressType.Information)
        {
            var model = new EstimatorLogProgress
            {
                Message = message,
                Type = type
            };

            progress.Report(model);
        }

        private void ProgressStatusTask(IProgress<EstimatorStatusProgress> progress, ProgressTaskStatus status)
        {
            var model = new EstimatorStatusProgress
            {
                Status = status
            };

            progress.Report(model);
        }

        private void ProgressInfoTask(IProgress<EstimatorBaseProgress> progress, Int32? readMessage, Int32? unreadMessage, String subject, String generateExcelPath)
        {
            var model = new EstimatorInfoProgress
            {
                ReadMessage = readMessage,
                UnreadMessage = unreadMessage,
                Subject = subject,
                GenerateExcelPaths = !String.IsNullOrEmpty(generateExcelPath) ? new List<String> { generateExcelPath } : new List<String>()
            };

            progress.Report(model);
        }

        private void ProgressIterationTask(IProgress<EstimatorBaseProgress> progress, Boolean isIterationRestart)
        {
            var model = new EstimatorIterationProgress
            {
                IsIterationRestart = isIterationRestart,
                TimeToRestartSeconds = (!isIterationRestart) ? _settingsService.TaskPauseSeconds : (Int32?)null
            };

            progress.Report(model);
        }

        private WordComputeStatistic GetStatistic(String path, IProgress<EstimatorBaseProgress> progress)
        {
            ProgressLogTask(progress, "Reading word count");
            WaitTask();

            var statistic = _wordService.GetComputeStatistic(path);
            return statistic;
        }

        private void ConvertExcelToText(AttachmentUnit unit, IProgress<EstimatorBaseProgress> progress)
        {
            ProgressLogTask(progress, "Convert a Excel file to a text file");
            WaitTask();

            var newpath = _excelService.ConvertToWordDocument(unit.PhisicalName);
            unit.PhisicalName = newpath;
            unit.CurrentType = FileType.Docx;
        }

        private void ConvertPresentToText(AttachmentUnit unit, IProgress<EstimatorBaseProgress> progress)
        {
            ProgressLogTask(progress, "Convert a Presentation file to a text file");
            WaitTask();

            var newpath = _presentService.ConvertToWordDocument(unit.PhisicalName);
            unit.PhisicalName = newpath;
            unit.CurrentType = FileType.Docx;
        }

        #endregion
    }
}
