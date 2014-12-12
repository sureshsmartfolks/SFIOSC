using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement_API.Models
{
    // Client asks for info
    // Server ----> Client
    public class ProjectManagementResponse
    {
        public List<DTO.projectDTO> projects;
        public string errors;
        public string status;
    }
}