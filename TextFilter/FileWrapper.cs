using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter
{
    public class FileWrapper : IFileWrapper
    {
        public bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public string GetTextFromFilePath(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
