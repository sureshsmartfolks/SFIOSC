using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSC.ProjectManagement.BusinessObjects
{
    public class STORMProject
    {
        public int projectID;
        public Client client;
        public string projectName;
        public int sourceLanguageID;
        public List<resource> resources;
        public List<Document> documents;
        public List<invoice> invoices;
        public string trackPath;
        public string trackStatus;
    }
}
