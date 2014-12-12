using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSC.BusinessObjects.FileTransfer
{
    public class FolderObject
    {        private string folder_name;
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Folder_name
        {
            get { return folder_name; }
            set { folder_name = value; }
        }
        private int parent_folder;

        public int Parent_folder
        {
            get { return parent_folder; }
            set { parent_folder = value; }
        }
         public FolderObject(string foldername, int parentfolder)
        {
            this.Folder_name = folder_name;
            this.Parent_folder = parent_folder;


        }
         public FolderObject() { }
    }
}
