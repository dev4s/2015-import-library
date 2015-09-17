using System;
using System.IO;

namespace Tests
{
    public static class Extensions
    {
        public static string GetFullFileNamePathForTestFilesFolder(this string fileName)
        {
            string binPath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(binPath, "TestFiles", fileName);
        }
    }
}