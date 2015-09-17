using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ImportLibrary
{
    public class MapFromLogic : IMapFromLogic
    {
        public Dictionary<string, string> GetMappedProperties(Type currentType)
        {
            var mappedProperties = new Dictionary<string, string>();

            PropertyInfo[] currentTypeProperties = currentType.GetProperties();

            foreach (PropertyInfo property in currentTypeProperties)
            {
                object[] allCustomPropertiesForProperty = property.GetCustomAttributes(true);

                var mappedPropertyToColumn = this.GetMappedPropertyToColumn(allCustomPropertiesForProperty, property);

                mappedProperties.Add(mappedPropertyToColumn.Key, mappedPropertyToColumn.Value);
            }

            return mappedProperties;
        }

        private KeyValuePair<string, string> GetMappedPropertyToColumn(IEnumerable<object> allCustomPropertiesForProperty, PropertyInfo property)
        {
            var propertyWithMapFromAttribute = allCustomPropertiesForProperty.OfType<MapFromAttribute>().FirstOrDefault();

            return propertyWithMapFromAttribute != null
                       ? new KeyValuePair<string, string>(property.Name, propertyWithMapFromAttribute.ColumnName)
                       : new KeyValuePair<string, string>(property.Name, property.Name);
        }
    }
}