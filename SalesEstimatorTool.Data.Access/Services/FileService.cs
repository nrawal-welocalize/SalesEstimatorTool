using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Enums;
using SalesEstimatorTool.Data.Models;
using SalesEstimatorTool.Data.Models.Progress;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class FileService : IFileService
    {
        private readonly ISettingsService _settingsService;
        public FileService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void FileProcessed(IList<Attachment> attachmentList, IProgress<EstimatorBaseProgress> progress, String path)
        {
            foreach (Attachment attachment in attachmentList)
            {
                FileAttachment fileAttachment = attachment as FileAttachment;
                if (fileAttachment != null)
                {
                    fileAttachment.Load(string.Format("{0}{1}", path, fileAttachment.Name));
                }
            }
        }

        public IList<String> GetWordFiles(String path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(n => n.EndsWith(".doc", StringComparison.OrdinalIgnoreCase)
                    || n.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public IList<String> GetImageFiles(String path)
        {
            var formats = _settingsService.AbbyFormats;
            return Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(n => formats.Any(f => n.EndsWith(f, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
        public IEnumerable<String> GetFiles(String path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly).ToList();
        }

        public IList<AttachmentUnit> GetAttachmentUnits(String path)
        {
            return GetFiles(path)
                .Select(n => new AttachmentUnit(n, GetFileType(n)))
                .ToList();
        }

        private FileType GetFileType(String fileName)
        {
            var result = FileType.None;

            if (_settingsService.AbbyFormats
                .Any(f => fileName.EndsWith(f, StringComparison.OrdinalIgnoreCase)))
            {
                result = FileType.Abby;
            }

            if (fileName.EndsWith(".doc", StringComparison.OrdinalIgnoreCase)
                    || fileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
            {
                result = FileType.Docx;
            }

            if (fileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase)
                    || fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                result = FileType.Xlsx;
            }

            if (fileName.EndsWith(".ppt", StringComparison.OrdinalIgnoreCase)
                    || fileName.EndsWith(".pptx", StringComparison.OrdinalIgnoreCase))
            {
                result = FileType.Pptx;
            }

            if (fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                result = FileType.Zip;
            }

            return result;
        }
    }
}
