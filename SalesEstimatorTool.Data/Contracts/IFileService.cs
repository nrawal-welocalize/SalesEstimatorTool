using System;
using System.Collections.Generic;
using SalesEstimatorTool.Data.Models.Progress;
using Microsoft.Exchange.WebServices.Data;
using SalesEstimatorTool.Data.Models;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface IFileService
    {
        void FileProcessed(IList<Attachment> attachmentList, IProgress<EstimatorBaseProgress> progress, String path);
        IList<String> GetWordFiles(String path);
        IList<String> GetImageFiles(String path);
        IEnumerable<String> GetFiles(String path);
        IList<AttachmentUnit> GetAttachmentUnits(String path);
    }
}
