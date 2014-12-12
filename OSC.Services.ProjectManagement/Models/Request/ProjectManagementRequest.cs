using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement_API.Models
{
    // Client POSTs info
    // CLient ----> Server
    public class ProjectManagementRequest
    {
        public List<DTO.projectDTO> projects;
        public string errors;
        public string status;
    }
}