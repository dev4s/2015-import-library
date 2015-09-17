using ImportLibrary;
using ImportLibrary.FileTypes;
using NUnit.Framework;
using Tests.TestImportClasses;

namespace Tests
{
    [TestFixture]
    public class ImportLibraryTests
    {
        [Test]
        public void ShouldImportSpecificCsvDataToSpecificObject()
        {
            var importLib = new ImportLibrary.ImportLibrary(new MapFromLogic(), new CsvFileLogic());
            var fullFileName = "ActivityExport.csv".GetFullFileNamePathForTestFilesFolder();

            var result = importLib.GetContents<ActivityExportCsvFile>(fullFileName);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldImportSpecificXlsDataToSpecificObject()
        {
            var importLib = new ImportLibrary.ImportLibrary(new XlsAndXlsxFileLogic());
            var fullFileName = "RiskTrackerExport.Xls".GetFullFileNamePathForTestFilesFolder();

            var result = importLib.GetContents<RiskTrackerExportXlsFile>(fullFileName);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(6));
        }

        [Test]
        public void ShouldImportSpecificXlsxDataToSpecificObject()
        {
            var importLib = new ImportLibrary.ImportLibrary(new XlsAndXlsxFileLogic());
            var fullFileName = "SampleFile.Xlsx".GetFullFileNamePathForTestFilesFolder();

            var result = importLib.GetContents<SampleFileXlsx>(fullFileName);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(4));
        }
    }
}
