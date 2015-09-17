using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImportLibrary.FileTypes
{
    public class CsvFileLogic : IFileTypeLogic
    {
        public FileBody ReadData(string fullFileName)
        {
            string[] lines = File.ReadAllLines(fullFileName);

            string[] headerValues = lines[0].Split(',');
            string[][] contentValues = SplitBySpecificConstraints(lines.Skip(1).ToArray());

            return new FileBody { Headers = headerValues, Contents = contentValues };
        }

        private string[][] SplitBySpecificConstraints(string[] contentValues)
        {
            var results = new string[contentValues.Count()][];

            for (var i = 0; i < contentValues.Length; i++)
            {
                var cells = GetCellValues(contentValues[i]);
                results[i] = cells.ToArray();
            }

            return results;
        }

        private static IEnumerable<string> GetCellValues(string contentValuesTemp)
        {
            const char Comma = ',';
            const char QuotationMark = '"';

            var cells = new List<string>();
            var cellValue = string.Empty;

            bool quotationMarkShowed = false;
            foreach (char currentChar in contentValuesTemp)
            {
                if (currentChar == QuotationMark)
                {
                    quotationMarkShowed = !quotationMarkShowed;
                }

                if (currentChar == Comma && quotationMarkShowed == false)
                {
                    cells.Add(cellValue);
                    cellValue = string.Empty;
                }

                if (currentChar != Comma || quotationMarkShowed)
                {
                    cellValue += currentChar;
                }
            }
            cells.Add(cellValue);

            return cells.ToArray();
        }
    }
}