using ProjectManagement_API.DataAccess;
using ProjectManagement_API.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using OSC.ProjectManagement.BusinessObjects;
using ProjectManagement_API.Models;

namespace ProjectManagement_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectManagementController : ApiController
    {

        public HttpResponseMessage Options()
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        //GET api/projectManagement/getProject/1
        [HttpGet]
        public Models.ProjectManagementResponse getProject(int id)
        {
            Models.ProjectManagementResponse response = new Models.ProjectManagementResponse();
            DataManager dm = new DataManager();
            //dm.getProject(id);
            //Document rc = dm.getDocument(id);
            //response.documents.Add(Mapper.toDTO(rc));

            //SQLDataAccess sqd = new SQLDataAccess();
            //Document rc = sqd.getDocument(id);

            //temp.rcd = Mapper.toDTO(rc);

            Models.DTO.projectDTO pDTO = new Models.DTO.projectDTO();
            pDTO = Mapper.toDTO(dm.getProject(id));
            response.projects = new List<Models.DTO.projectDTO>();
            response.projects.Add(pDTO);

            return response;
        }

        //GET api/projectManagement/getDocument/1
        [HttpGet]
        public Models.ProjectManagementResponse getDocument(int id)
        {
            Models.ProjectManagementResponse response = new Models.ProjectManagementResponse();
            DataManager dm = new DataManager();
            //Document rc = dm.getDocument(id);
            //response.documents.Add(Mapper.toDTO(rc));

            //SQLDataAccess sqd = new SQLDataAccess();
            //Document rc = sqd.getDocument(id);

            //temp.rcd = Mapper.toDTO(rc);

            Models.DTO.projectDTO pDTO = new Models.DTO.projectDTO();
            pDTO.documents = new List<Models.DTO.documentDTO>();
            pDTO.documents.Add(Mapper.toDTO(dm.getDocument(id)));
            response.projects = new List<Models.DTO.projectDTO>();
            response.projects.Add(pDTO);

            return response;
        }

        //GET api/projectManagement/getClient/1
        [HttpGet]
        public Models.ProjectManagementResponse getClient(int id)
        {
            Models.ProjectManagementResponse response = new Models.ProjectManagementResponse();
            DataManager dm = new DataManager();
           // Client rc = dm.getClient(id);
            //response = Mapper.toDTO(rc);
            //response.project = rc;

            //SQLDataAccess sqd = new SQLDataAccess();
            //Document rc = sqd.getDocument(id);
            //temp.rcd = Mapper.toDTO(rc);
            Models.DTO.projectDTO pDTO = new Models.DTO.projectDTO();

            pDTO.client = Mapper.toDTO(dm.getClient(id));
            response.projects = new List<Models.DTO.projectDTO>();
            response.projects.Add(pDTO);
            return response;
        }

        [HttpGet]
        public ProjectManagementResponse getAllProjects()
        {
            Models.ProjectManagementResponse response = new Models.ProjectManagementResponse();
            DataManager dm = new DataManager();
            Models.DTO.projectDTO pDTO = new Models.DTO.projectDTO();

            response.projects = Mapper.toDTO(dm.getAllProjects());

            return response;
        }

        ////GET api/projectManagement/getProject/1
        //[HttpGet]
        //public Models.ProjectManagementResponse getProject(int id)
        //{
        //    Models.ProjectManagementResponse response = new Models.ProjectManagementResponse();
        //    DataManager dm = new DataManager();
        //    STORMProject rc = dm.getProject(id);

        //    response.project = Mapper.toDTO(rc);

        //    //response.documents.Add(Mapper.toDTO(rc));

        //    //SQLDataAccess sqd = new SQLDataAccess();
        //    //Document rc = sqd.getDocument(id);

        //    //temp.rcd = Mapper.toDTO(rc);

        //    return response;
        //}
        // POST api/projectManagement/postProject/
        [HttpPost]
        [EnableCors("*", "*", "POST")]
        public Models.ProjectManagementRequest postProject([FromBody]Models.ProjectManagementRequest request)
        {
            Models.ProjectManagementRequest response = new Models.ProjectManagementRequest();
            STORMProject rc = new STORMProject();
            DataManager dm = new DataManager();

            rc = Mapper.toBO(request.projects[0]);
            response.status = dm.insertProject(rc).ToString();


            //dm.insertProject(request.);

            //rc = Mapper.toBO(

            //request.project.clientID = 0;


            //response.status = "it WORKED";

            return response;
        }


//        [HttpGet]
//        public ProjectManagementResponse getAllProjects()
//        {
//            Models.ProjectManagementResponse response = new Models.ProjectManagementResponse();
//            DataManager dm = new DataManager();
//            Models.DTO.projectDTO pDTO = new Models.DTO.projectDTO();

//            response.projects = Mapper.toDTO(dm.getAllProjects());
////            response.projects = new List<Models.DTO.projectDTO>();
//            //response.projects = Mapper.toDTO(dm.getAllProjects());



//            //pDTO = Mapper.toDTO(dm.getProject(id));
//            //response.projects = pDTO;

//            //try
//            //{

//            //}
//            //catch
//            //{
//            //}
//            //return data;
//            return response;
//        }

        // Franks fun ------\/

        [HttpGet]
        public ProjectResponse OLDgetAllProjects()
        {
            //getAll
            ProjectResponse test = new ProjectResponse();
            try
            {
                test.Results = ProjectDataAccess.GetAllProjects();
            }
            catch (Exception ex)
            {
                test.ErrorStatus = true;
                test.ErrorMessage = ex.Message;
            }

            return test;
        }

        [HttpGet]
        public ProjectResponse getUserProjects()
        {
            string userName = "frankAF";
            //get All Projects for current user.
            ProjectResponse test = new ProjectResponse();
            try
            {
                test.Results = ProjectDataAccess.GetUserProjects(userName);
            }
            catch (Exception ex)
            {
                test.ErrorStatus = true;
                test.ErrorMessage = ex.Message;
            }

            return test;
        }

        [HttpGet]
        public ProjectResponse OLDgetProject(int id)
        {
            //get
            ProjectResponse test = new ProjectResponse();
            try
            {
                test.Results = ProjectDataAccess.GetProject(id);
            }
            catch (Exception ex)
            {
                test.ErrorStatus = true;
                test.ErrorMessage = ex.Message;
            }

            return test;
        }

        [HttpPut]
        public ProjectResponse putProject(ProjectManagementDTO obj)
        {
            //update
            ProjectResponse test = new ProjectResponse();
            try
            {
                ProjectDataAccess.UpdateProject(obj);
            }
            catch (Exception ex)
            {
                test.ErrorStatus = true;
                test.ErrorMessage = ex.Message;
            }

            return test;
        }

        [HttpDelete]
        public ProjectResponse deleteProject(ProjectManagementDTO obj)
        {
            //delete
            ProjectResponse test = new ProjectResponse();
            try
            {
                ProjectDataAccess.DeleteProject(obj);
            }
            catch (Exception ex)
            {
                test.ErrorStatus = true;
                test.ErrorMessage = ex.Message;
            }

            return test;
        }

        [HttpPost]
        public ProjectResponse OLDpostProject(ProjectManagementDTO obj)
        {
            //insert
            ProjectResponse test = new ProjectResponse();
            int newID = -1;
            try
            {
                newID = ProjectDataAccess.InsertProject(obj);
            }
            catch (Exception ex)
            {
                test.ErrorStatus = true;
                test.ErrorMessage = ex.Message;
            }

            return test;
        }
    }
}
