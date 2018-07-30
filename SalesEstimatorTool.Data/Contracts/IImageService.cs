using System;
using System.Collections.Generic; 
using SalesEstimatorTool.Data.Models;
using SalesEstimatorTool.Data.Models.Progress;

namespace SalesEstimatorTool.Data.Contracts
{
    public  interface IImageService
    {
        void AbbyProcessed(SalesEstimator model, IList<AttachmentUnit> imagePaths, String tempPath, IProgress<EstimatorBaseProgress> progress);
    }
}
