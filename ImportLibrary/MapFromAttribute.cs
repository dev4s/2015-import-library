using System;

namespace ImportLibrary
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class MapFromAttribute : Attribute
    {
        public string ColumnName { get; private set; }

        public MapFromAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}