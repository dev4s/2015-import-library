using System;
using System.Collections.Generic;

namespace ImportLibrary
{
    public interface IMapFromLogic
    {
        Dictionary<string, string> GetMappedProperties(Type currentType);
    }
}