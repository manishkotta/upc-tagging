using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonEntities
{
    public static class Constants
    {
        private static string CurrentDirectory = Directory.GetCurrentDirectory();

        public static string GetTempFilePath(string fileName)
        {
            return CurrentDirectory + @"\TempararyFiles\" + fileName;
        }
    }
}
