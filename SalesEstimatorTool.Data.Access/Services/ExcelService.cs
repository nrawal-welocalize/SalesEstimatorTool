using System;
using ClosedXML.Excel;  
using SalesEstimatorTool.Data.Contracts;
using SalesEstimatorTool.Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;
using SalesEstimatorTool.Data.Enums;
using Path = System.IO.Path;


namespace SalesEstimatorTool.Data.Access.Services
{
    public class ExcelService : IExcelService
    {
        private readonly string _dot = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private readonly ISettingsService _settingsService;

        public ExcelService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public IEnumerable<LanguageMapping> GetFromEnglishMappings()
        {
            List<LanguageMapping> result;

            using (var book = new XLWorkbook(_settingsService.ExcelSettingPath))
            {
                result = book.Worksheet(1).Rows()
                    .Where((n, i) => i > 0 && !string.IsNullOrWhiteSpace(n.Cell(1).Value.ToString()))
                    .Select(n => new LanguageMapping
                    {
                        Language = n.Cell(1).Value.ToString().Trim(),
                        Prices = new Dictionary<Tier, Decimal>
                        {
                            {Tier.Tier3, GetDecValue(n.Cell(2))},
                            {Tier.Tier35, GetDecValue(n.Cell(3))},
                            {Tier.Tier4, GetDecValue(n.Cell(4))}
                        },
                        TurnaroundTime = new Dictionary<Tier, Int32>
                        {
                            {Tier.Tier3, Int32.Parse(n.Cell(5).Value.ToString())},
                            {Tier.Tier35, Int32.Parse(n.Cell(6).Value.ToString())},
                            {Tier.Tier4, Int32.Parse(n.Cell(7).Value.ToString())}
                        },
                        Rush = new Dictionary<Rush, Decimal>
                        {
                            {Rush.Rush1, GetDecValue(n.Cell(8))},
                            {Rush.Rush3, GetDecValue(n.Cell(9))},
                            {Rush.Rush6, GetDecValue(n.Cell(10))}
                        }
                    })
                    .ToList();
            }

            return result;
        }

        public IEnumerable<LanguageMapping> GetToEnglishMappings()
        {
            List<LanguageMapping> result;

            using (var book = new XLWorkbook(_settingsService.ExcelSettingPath))
            {
                result = book.Worksheet(2).Rows()
                    .Where((n, i) => i > 0 && !string.IsNullOrWhiteSpace(n.Cell(1).Value.ToString()))
                    .Select(n => new LanguageMapping
                    {
                        Language = n.Cell(1).Value.ToString().Trim(),
                        Prices = new Dictionary<Tier, Decimal>
                        {
                            {Tier.Tier3, GetDecValue(n.Cell(2))},
                            {Tier.Tier35, GetDecValue(n.Cell(3))},
                            {Tier.Tier4, GetDecValue(n.Cell(4))}
                        },
                        Action = (ActionType)Enum.Parse(typeof(ActionType), n.Cell(5).Value.ToString(), true),
                        Divisor = GetDecValue(n.Cell(6)),
                        TurnaroundTime = new Dictionary<Tier, Int32>
                        {
                            {Tier.Tier3, Int32.Parse(n.Cell(7).Value.ToString())},
                            {Tier.Tier35, Int32.Parse(n.Cell(8).Value.ToString())},
                            {Tier.Tier4, Int32.Parse(n.Cell(9).Value.ToString())}
                        },
                        Rush = new Dictionary<Rush, Decimal>
                        {
                            {Rush.Rush1, GetDecValue(n.Cell(10))},
                            {Rush.Rush3, GetDecValue(n.Cell(11))},
                            {Rush.Rush6, GetDecValue(n.Cell(12))}
                        }
                    })
                    .ToList();
            }

            return result;
        }

        public string ConvertToWordDocument(string pathToExcelFile)
        {
            /*
             * A workbook can contains more the one worksheet
             * We need export each worksheet to separate file and combine them
             */

            var filebaseFilename = Path.GetDirectoryName(pathToExcelFile) + "\\"
                + Path.GetFileName(pathToExcelFile);
            var outputFilename = filebaseFilename + ".txt";

            var fileNames = new List<string>();

            using (var app = new Application())
            {
                app.DisplayAlerts = false;
                var book = app.Workbooks.Open(pathToExcelFile);

                foreach (Worksheet sheet in book.Sheets)
                {
                    var filename = filebaseFilename + "_"+ (fileNames.Count + 1) + ".txt";

                    sheet.Activate();
                    book._SaveAs(filename, XlFileFormat.xlUnicodeText);
                    fileNames.Add(filename);
                }

                app.Quit();
            }

            //combine files
            using (var output = File.Create(outputFilename))
                foreach (var file in fileNames)
                    using (var input = File.OpenRead(file))
                        input.CopyTo(output);

            return outputFilename;
        }

        #region Private

        private Decimal GetDecValue(IXLCell cell)
        {
            var str = cell.Value.ToString();
            return Decimal.Parse(str.Replace(".", _dot).Replace(",", _dot));
        }

        #endregion Private
    }
}
