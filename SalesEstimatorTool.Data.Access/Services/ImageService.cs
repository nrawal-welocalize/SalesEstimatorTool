using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Enums;
using SalesEstimatorTool.Data.Models;
using SalesEstimatorTool.Data.Models.Progress;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class ImageService: IImageService
    {
        private readonly ISettingsService _settingsService;

        public ImageService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void AbbyProcessed(SalesEstimator model, IList<AttachmentUnit> units, String tempPath, IProgress<EstimatorBaseProgress> progress)
        {
            String abbyInPath = _settingsService.AbbyHotFolderInPath;
            String abbyOutPath = _settingsService.AbbyHotFolderOutPath;
            Boolean abbyInExists = Directory.Exists(abbyInPath);
            Boolean abbyOutExists = Directory.Exists(abbyOutPath);
            String fullAbbyInPath = abbyInPath + model.SourceLanguage.Language;
            String fullAbbyOutPath = abbyOutPath + model.SourceLanguage.Language;
            Boolean fullAbbyInExists = Directory.Exists(fullAbbyInPath);
            Boolean fullAbbyOutExists = Directory.Exists(fullAbbyOutPath);
            if (!abbyInExists || !abbyOutExists || !fullAbbyInExists || !fullAbbyOutExists)
            {
                if (!abbyInExists)
                {
                    String error = String.Format("Folder {0} not found ", abbyInPath);
                    throw new Exception(error);
                }
                if (!abbyOutExists)
                {
                    String error = String.Format("Folder {0} not found ", abbyOutPath);
                    throw new Exception(error);
                }
                if (!fullAbbyInExists)
                {
                    String error = String.Format("Folder {0} not found ", fullAbbyInPath);
                    throw new Exception(error);
                }
                else
                {
                    String error = String.Format("Folder {0} not found ", fullAbbyOutPath);
                    throw new Exception(error);
                }
            }
            else
            {
                foreach (AttachmentUnit unit in units.Where(u=>u.CurrentType == FileType.Abby))
                {
                    SetAbbyInFile(model, unit, fullAbbyInPath);
                }
                foreach (AttachmentUnit unit in units.Where(u=>u.CurrentType == FileType.Abby))
                {
                    GetAbbyOutFile(tempPath, unit, fullAbbyOutPath);
                }

            }
        }

        private void GetAbbyOutFile(String tempPath, AttachmentUnit unit, String fullAbbyOutPath)
        {
            String fileName = Path.GetFileName(unit.AbbyFilePath);
            String format = Path.GetExtension(fileName) ?? String.Empty;
            if (fileName != null)
            {
                var newFileName = fileName.Replace(format, ".docx");
                var fullNamePath = String.Format("{0}\\{1}", fullAbbyOutPath, newFileName);
                DateTime maxTime = DateTime.UtcNow.AddMinutes(_settingsService.AbbyMaxWaitTimeMinutes);
                while (DateTime.UtcNow < maxTime)
                {
                    if (File.Exists(fullNamePath))
                    {
                        String phisicalName = String.Format("{0}\\{1}", tempPath, newFileName);
                        File.Move(fullNamePath, phisicalName);
                        unit.CurrentType = FileType.Docx;
                        unit.PhisicalName = phisicalName;
                        if (File.Exists(unit.AbbyFilePath))
                        {
                            File.Delete(unit.AbbyFilePath);
                        }
                        break;
                    }
                    else
                    {
                        Thread.Sleep(_settingsService.AbbySleepTimeMilliseconds);
                    }
                }
            }
        }

        private void SetAbbyInFile(SalesEstimator model, AttachmentUnit unit, String fullAbbyInPath)
        {
            String fileName = Path.GetFileNameWithoutExtension(unit.PhisicalName);
            String extension = Path.GetExtension(unit.PhisicalName);
            if (!String.IsNullOrWhiteSpace(fileName))
            {
                unit.AbbyFilePath = String.Format("{0}\\{1}{2}{3}", fullAbbyInPath, fileName, DateTime.UtcNow.Ticks, extension);
              
                File.Move(unit.PhisicalName, unit.AbbyFilePath);
            }
            else
            {
                model.AddError(String.Format("Incorect file name: {0}\n file:{1}", fileName, unit.PhisicalName));
            }
        }

    }
}
