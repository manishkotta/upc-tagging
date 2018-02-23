using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IBusiness;

namespace BusinessProvider
{
    public class FileService : IFileService
    {



        public void SaveFileToTempFolder(Stream stream, string filePath)
        {
            using (var targetStream = File.Create(filePath))
            {
                stream.CopyToAsync(targetStream);
            }
        }

        public void ReadFileFromTempFolder(string filePath)
        {

        }
    }
}
