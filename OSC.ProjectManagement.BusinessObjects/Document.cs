using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSC.ProjectManagement.BusinessObjects
{
    public class Document
    {
        public string filePath;
        public int typeID;
        public int status;
        public language translateDirections;
        public int sourceLanguageID;
        //public int targetLanguageID[]; // must generate?

        public int version;
        public int documentID;
        public int projectID;
        public string name;
        
    }
}