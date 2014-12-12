using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResourceManager.Models;

namespace ProjectManagement_API.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public Language SourceLanguage { get; set; }
        public List<Language> TargetLanguages { get; set; }
        public int RootFolderID { get; set; }
        public int WordCount { get; set; }
        public List<Resource> Resources { get; set; }
    }
}