using ImportLibrary;

namespace Tests.TestImportClasses
{
    public class RiskTrackerExportXlsFile : IImportFile
    {
        [MapFrom("Safe Number")]
        public string SafeNumber { get; set; }
        
        public string Reference { get; set; }
    }
}