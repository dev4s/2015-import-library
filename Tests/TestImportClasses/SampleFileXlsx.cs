using ImportLibrary;

namespace Tests.TestImportClasses
{
    public class SampleFileXlsx : IImportFile
    {
        [MapFrom("Safe Number")]
        public string SafeNumber { get; set; }

        [MapFrom("Blame me for everything")]
        public string Reference { get; set; }
    }
}