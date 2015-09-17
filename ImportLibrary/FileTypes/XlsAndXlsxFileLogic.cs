using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ImportLibrary.FileTypes
{
    public class XlsAndXlsxFileLogic : IFileTypeLogic
    {
        public FileBody ReadData(string fullFileName)
        {
            var headers = new List<string>();
            var contents = new List<List<string>>();

            var isXlsx = fullFileName.ToLower().EndsWith("xlsx");
            ISheet sheet = GetFirstSheet(fullFileName, isXlsx);

            AddHeaders(sheet, headers);
            AddContents(sheet, contents);

            return new FileBody { Contents = contents.Select(x => x.ToArray()).ToArray(), Headers = headers.ToArray() };
        }

        private static void AddContents(ISheet sheet, ICollection<List<string>> rowsAndCells)
        {
            var lastRow = sheet.LastRowNum;
            for (int i = 1; i <= lastRow; i++)
            {
                var row = new List<string>();

                row.AddRange(
                    sheet.GetRow(i)
                        .Cells.Select(
                            x => x.CellType == CellType.Numeric ? x.NumericCellValue.ToString(CultureInfo.InvariantCulture) : x.StringCellValue));
                rowsAndCells.Add(row);
            }
        }

        private static void AddHeaders(ISheet sheet, List<string> headers)
        {
            headers.AddRange(sheet.GetRow(0).Cells.Select(x => x.StringCellValue));
        }

        private static ISheet GetFirstSheet(string fullFileName, bool isXlsx)
        {
            var fileStream = File.Open(fullFileName, FileMode.Open, FileAccess.Read);
            IWorkbook workBook = isXlsx ? new XSSFWorkbook(fileStream) as IWorkbook : new HSSFWorkbook(fileStream);
            return workBook.GetSheetAt(0);
        }
    }
}