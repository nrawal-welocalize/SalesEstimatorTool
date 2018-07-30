using System;
using System.Collections.Generic; 
using SalesEstimatorTool.Data.Models;
using SalesEstimatorTool.Data.Models.Progress;

namespace SalesEstimatorTool.Data.Contracts
{
    public interface IZipService
    {
       void UnzipProcessed(IList<AttachmentUnit> units, String tempPath,
            IProgress<EstimatorBaseProgress> progress);
    }
}
