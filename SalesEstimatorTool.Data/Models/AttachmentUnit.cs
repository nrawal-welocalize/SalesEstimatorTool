using System;
using System.Collections.Generic;
using System.IO;

using SalesEstimatorTool.Data.Enums;

namespace SalesEstimatorTool.Data.Models
{
    public class AttachmentUnit
    {
        private FileType _currentType;

        public AttachmentUnit(String filePath, FileType fileType)
        {
            FileTypeHistory = new List<FileType>();
            OriginalFileName = Path.GetFileName(filePath);
            ZipFileName = Path.GetFileName(filePath);
            PhisicalName = filePath;
            CurrentType = fileType;
        }

        public String PhisicalName { get; set; }
        public String OriginalFileName { get; set; }
        public String ZipFileName { get; set; }
        public IList<FileType> FileTypeHistory { get; set; }
        public String AbbyFilePath { get; set; }
        public FileType CurrentType
        {
            get { return _currentType; }
            set
            {
                FileTypeHistory.Add(value);
                _currentType = value;
            }
        }
    }
}
