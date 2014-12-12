using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace OCS.HelperObjects.FileTransfer
{
   public class FileProvider : IFileProvider
    {
        private string _filesDirectory;
        const string DefaultFileLocation = "Files";
        //private const string AppSettingsKey = "FilesLocation";

        public FileProvider()
        {
            _filesDirectory = DefaultFileLocation;

        }

        public FileProvider(string folderpathkey)
        {
            var fileLocation = ConfigurationManager.AppSettings[folderpathkey];
            if (!String.IsNullOrWhiteSpace(fileLocation))
            {
                _filesDirectory = fileLocation;
            }

        }

        public void setFilelocation(string path)
        {


            _filesDirectory = ConfigurationManager.AppSettings[path];
        }

        public bool Exists(string name)
        {
            //make sure we dont access directories outside of our store for security reasons
            string file = Directory.GetFiles(_filesDirectory, name, SearchOption.TopDirectoryOnly)
                    .FirstOrDefault();
            return file != null;
        }

        public FileStream Open(string name)
        {
            return File.Open(GetFilePath(name),
                FileMode.Open, FileAccess.Read);
        }

        public long GetLength(string name)
        {
            return new FileInfo(GetFilePath(name)).Length;
        }

        private string GetFilePath(string name)
        {
            return Path.Combine(_filesDirectory, name);
        }
    }
}
