using ImportLibrary;
using NUnit.Framework;
using Tests.TestImportClasses;

namespace Tests
{
    [TestFixture]
    public class MapFromLogicTests
    {
        private readonly MapFromLogic mapFromLogic = new MapFromLogic();

        [Test]
        public void ShouldReturnEmptyListForEmptyClass()
        {
            var result = mapFromLogic.GetMappedProperties(typeof(Empty));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ShouldReturnAllPropertiesForSpecificClass()
        {
            const string BusinessKey = "BusinessName";
            const string DateKey = "Date";

            var result = mapFromLogic.GetMappedProperties(typeof(ActivityExportCsvFile));

            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Unique);
            Assert.That(result.ContainsKey(BusinessKey), Is.True);
            Assert.That(result.ContainsKey(DateKey), Is.True);
            Assert.That(result[BusinessKey], Is.EqualTo("BUSINESS NAME"));
            Assert.That(result[DateKey], Is.EqualTo("DATE"));
        }
    }
}