using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ProjectManagement_API.Models;

namespace ProjectManagement_API.Messaging
{
    public class ProjectResponse
    {
        public bool ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
        public List<Project> Results { get; set; }
    }
}