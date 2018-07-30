using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Enums;
using SalesEstimatorTool.Data.Models;
using SalesEstimatorTool.Data.Models.Progress;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class ZipService : IZipService
    {
        private readonly IFileService _fileService;
        public ZipService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void UnzipProcessed(IList<AttachmentUnit> units, String tempPath,
            IProgress<EstimatorBaseProgress> progress)
        {
            IList<AttachmentUnit> removeUnites = new List<AttachmentUnit>();
            IList<AttachmentUnit> addUnites = new List<AttachmentUnit>();
            foreach (AttachmentUnit unit in units.Where(n => n.CurrentType == FileType.Zip))
            {
                String folder = String.Format("{0}{1}\\", tempPath, Path.GetFileNameWithoutExtension(unit.OriginalFileName));
                Boolean exists = Directory.Exists(folder);
                if (!exists)
                {
                    Directory.CreateDirectory(folder);
                }
                var encoding = System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage;
                ZipFile.ExtractToDirectory(unit.PhisicalName, folder, Encoding.GetEncoding(encoding));
                IList<AttachmentUnit> extractedFiles = _fileService.GetAttachmentUnits(folder);
                foreach (AttachmentUnit extractedFile in extractedFiles)
                {
                    extractedFile.ZipFileName = String.Format("{0}/{1}", unit.ZipFileName, extractedFile.ZipFileName);
                } 
                UnzipProcessed(extractedFiles, folder, progress);
                foreach (AttachmentUnit extractedFile in extractedFiles)
                {
                    addUnites.Add(extractedFile);
                } 
                
                removeUnites.Add(unit);
            }
            foreach (AttachmentUnit removeUnite in removeUnites)
            {
                units.Remove(removeUnite);
            }
            foreach (AttachmentUnit addUnite in addUnites)
            {
                units.Add(addUnite);
            }
        }
    }
}
