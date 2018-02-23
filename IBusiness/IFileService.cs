using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace IBusiness
{
    public interface IFileService
    {
        void SaveFileToTempFolder(Stream stream, string filePath);

        void ReadFileFromTempFolder(string filePath);
    }
}
