using ImportLibrary;

namespace Tests.TestImportClasses
{
    public class ActivityExportCsvFile : IImportFile
    {
        [MapFrom("BUSINESS NAME")]
        public string BusinessName { get; set; }

        [MapFrom("DATE")]
        public string Date { get; set; }
    }
}