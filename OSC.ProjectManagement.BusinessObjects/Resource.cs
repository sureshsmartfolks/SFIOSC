using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSC.ProjectManagement.BusinessObjects
{
    public class resource
    {
        public string firstName;
        public string lastName;
        public string company;
        public string address;
        public string type;
        public language languageAssociations;
        public List<Document> documents;
        public bool isCompany;
        public int resourceID;

    }
}
