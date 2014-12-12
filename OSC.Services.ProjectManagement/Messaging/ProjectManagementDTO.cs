using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResourceManager.Models;

namespace ProjectManagement_API.Messaging
{
    public class ProjectManagementDTO
    {
        public int ProjectID { get; set; }
        public int SourceLanguageID { get; set; }
        public List<int> TargetLanguageIDs { get; set; }
        public int RootFolderID { get; set; }
        public List<Resource> Resources { get; set; }
    }
}