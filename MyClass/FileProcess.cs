using System;
using System.IO;

namespace MyClasses
{
    public class FileProcess
    {
        public bool FileExists(string fileName)
        {
            if(!string.IsNullOrEmpty(fileName))
            {
                return File.Exists(fileName);
            }
            else
            {
                throw new ArgumentNullException("fileName");
            }
        }
    }
}
