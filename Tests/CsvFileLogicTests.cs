using ImportLibrary.FileTypes;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CsvFileLogicTests
    {
        [Test]
        public void ShouldReturnProperSeparatedLinesRowsAndCells()
        {
            const int HeaderLength = 5;
            var fullFileName = "ActivityExport.csv".GetFullFileNamePathForTestFilesFolder();

            var result = new CsvFileLogic().ReadData(fullFileName);

            Assert.That(result.Headers, Has.Length.EqualTo(HeaderLength));
            Assert.That(result.Contents, Has.Length.EqualTo(4));

            foreach (string[] line in result.Contents)
            {
                Assert.That(line, Has.Length.EqualTo(HeaderLength));
            }
        }
    }
}