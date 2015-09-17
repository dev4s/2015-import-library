using System;
using System.Collections.Generic;
using ImportLibrary.FileTypes;

namespace ImportLibrary
{
    public class ImportLibrary
    {
        private readonly IMapFromLogic mapFromLogic;

        private readonly IFileTypeLogic fileTypeLogic;

        public ImportLibrary(IFileTypeLogic fileTypeLogic) : this(new MapFromLogic(), fileTypeLogic) { }

        public ImportLibrary(IMapFromLogic mapFromLogic, IFileTypeLogic fileTypeLogic)
        {
            if (mapFromLogic == null)
            {
                throw new ArgumentNullException("mapFromLogic");
            }

            if (fileTypeLogic == null)
            {
                throw new ArgumentNullException("fileTypeLogic");
            }

            this.mapFromLogic = mapFromLogic;
            this.fileTypeLogic = fileTypeLogic;
        }

        public IEnumerable<T> GetContents<T>(string fullFileName) where T : IImportFile, new()
        {
            var results = new List<T>();
            var currentType = typeof(T);

            Dictionary<string, string> mappedProperties = mapFromLogic.GetMappedProperties(currentType);
            FileBody headersAndContents = fileTypeLogic.ReadData(fullFileName);

            foreach (string[] contents in headersAndContents.Contents)
            {
                var genericRow = new T();
                var properties = currentType.GetProperties();

                foreach (var property in properties)
                {
                    if (!mappedProperties.ContainsKey(property.Name))
                    {
                        continue;
                    }

                    var mappedPropertyValue = mappedProperties[property.Name];

                    int headerValueNumber = Array.IndexOf(headersAndContents.Headers, mappedPropertyValue);

                    property.SetValue(genericRow, headerValueNumber == -1 ? string.Empty : contents[headerValueNumber]);
                }

                results.Add(genericRow);
            }
            
            return results;
        }
    }
}