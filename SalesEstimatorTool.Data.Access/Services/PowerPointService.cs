using System.IO;
using NetOffice.OfficeApi.Enums;
using NetOffice.PowerPointApi;
using NetOffice.PowerPointApi.Enums;
using SalesEstimatorTool.Data.Contracts;

namespace SalesEstimatorTool.Data.Access.Services
{
    public class PowerPointService : IPowerPointService
    {
        public string ConvertToWordDocument(string pathToPresentation)
        {
            /*
             * A workbook can contains more the one worksheet
             * We need export each worksheet to separate file and combine them
             */

            var baseName = Path.GetDirectoryName(pathToPresentation) + "\\"
                + Path.GetFileName(pathToPresentation);

            var rtfName = baseName + ".rtf";

            using (var app = new Application())
            {
                app.DisplayAlerts = PpAlertLevel.ppAlertsNone;
                var p = app.Presentations.Open(pathToPresentation, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                p.SaveAs(rtfName, PpSaveAsFileType.ppSaveAsRTF);
                app.Quit();
            }

            return rtfName;
        }
    }
}
