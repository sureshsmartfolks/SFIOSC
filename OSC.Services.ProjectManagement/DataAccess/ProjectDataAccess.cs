using ProjectManagement_API.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProjectManagement_API.Models;
using ResourceManager.Models;

namespace ProjectManagement_API.DataAccess
{
    public class ProjectDataAccess
    {
        public static List<Project> GetAllProjects()
        {
            //SQL code to return all projects.
            Dictionary<string,object> parameters = new Dictionary<string,object>();
            DataTable Results = ExecStoredProcWithResults("Dev_Storm","GetAllProjects",parameters);
            List<Project> projectList = MapResultsToProjects(Results);
            return projectList;
        }

        public static List<Project> GetUserProjects(string UserName)
        {
            //SQL code to return a user's projects.
            Dictionary<string,object> parameters = new Dictionary<string,object>();
            parameters.Add("@UserName",UserName);
            DataTable Results = ExecStoredProcWithResults("Dev_Storm","GetUserProjects",parameters);
            List<Project> projectList = MapResultsToProjects(Results);
            return projectList;
        }

        public static List<Project> GetProject(int ProjectID)
        {
            //SQL code to return a single project.
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ProjectID", ProjectID);
            DataTable Results = ExecStoredProcWithResults("Dev_Storm", "GetProject", parameters);
            Project result = MapResultsToProjects(Results).Single();
            List<Project> projectList = new List<Project>();
            projectList.Add(result);
            return projectList;
        }

        public static void UpdateProject(ProjectManagementDTO obj)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            try
            {
                //SQL code to update a single project.
                
                parameters.Add("@ProjectID", obj.ProjectID);
                parameters.Add("@RootFolderID", obj.RootFolderID);
                parameters.Add("@SourceLanguage", obj.SourceLanguageID);
                ExecStoredProc("Dev_Storm", "UpdateProject", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                parameters = new Dictionary<string, object>();
                parameters.Add("@ProjectID", obj.ProjectID);
                ExecStoredProc("Dev_Storm", "DeleteLanguageAssociations", parameters);
                foreach (var Language in obj.TargetLanguageIDs)
                {
                    parameters = new Dictionary<string, object>();
                    parameters.Add("@ProjectID", obj.ProjectID);
                    parameters.Add("@LanguageID", Language);
                    ExecStoredProc("Dev_Storm", "AddLanguageAssociations", parameters);
                }

                parameters = new Dictionary<string, object>();
                parameters.Add("@ProjectID", obj.ProjectID);
                ExecStoredProc("Dev_Storm", "DeleteResourceAssociations", parameters);
                foreach (var resource in obj.Resources)
                {
                    parameters = new Dictionary<string, object>();
                    parameters.Add("@ProjectID", obj.ProjectID);
                    parameters.Add("@ResourceID", resource);
                    ExecStoredProc("Dev_Storm", "AddResourceAssociation", parameters);
                }
            }

        }

        public static void DeleteProject(ProjectManagementDTO obj)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ProjectID", obj.ProjectID);
            ExecStoredProc("Dev_Storm", "DeleteProject", parameters);
        }

        public static int InsertProject(ProjectManagementDTO obj)
        {
            //SQL code to insert a single project.
            int newID = -1;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            try
            {
                
                parameters.Add("@RootFolderID", obj.RootFolderID);
                parameters.Add("@SourceLanguage", obj.SourceLanguageID);
                newID = ExecStoredProcReturnID("Dev_Storm", "InsertProject", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (newID > -1)
                {
                    foreach (var Language in obj.TargetLanguageIDs)
                    {
                        parameters = new Dictionary<string, object>();
                        parameters.Add("@ProjectID", newID);
                        parameters.Add("@LanguageID", Language);
                        ExecStoredProc("Dev_Storm", "AddLanguageAssociations", parameters);
                    }
                    foreach (var resource in obj.Resources)
                    {
                        parameters = new Dictionary<string, object>();
                        parameters.Add("@ProjectID", newID);
                        parameters.Add("@ResourceID", resource);
                        ExecStoredProc("Dev_Storm", "AddResourceAssociation", parameters);
                    }
                }
            }

            return newID;
        }

        private static DataTable ExecStoredProcWithResults(string ConnectionString, string ProcedureName, Dictionary<string, object> parameters)
        {
            DataTable results = new DataTable("Results");
            try
            {
                string connectionName = Convert.ToString(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);

                SqlConnection conn = new SqlConnection(connectionName);

                conn.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, conn);
                cmd.CommandType = CommandType.StoredProcedure;


                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, Convert.ChangeType(parameter.Value, parameter.Value.GetType()));
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(results);

                conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return results;
        }

        private static void ExecStoredProc(string ConnectionString, string ProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                string connectionName = Convert.ToString(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);

                SqlConnection conn = new SqlConnection(connectionName);

                conn.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, conn);
                cmd.CommandType = CommandType.StoredProcedure;


                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, Convert.ChangeType(parameter.Value, parameter.Value.GetType()));
                }

                cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception ex)
            {
            }
        }

        private static int ExecStoredProcReturnID(string ConnectionString, string ProcedureName, Dictionary<string, object> parameters)
        {
            int id = -1;
            try
            {
                string connectionName = Convert.ToString(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);

                SqlConnection conn = new SqlConnection(connectionName);

                conn.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, conn);
                cmd.CommandType = CommandType.StoredProcedure;


                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, Convert.ChangeType(parameter.Value, parameter.Value.GetType()));
                }

                id = (int)cmd.ExecuteScalar();

                conn.Close();

            }
            catch (Exception ex)
            {
            }

            return id;
        }

        private static List<Project> MapResultsToProjects(DataTable results)
        {
            List<Project> ProjectList = new List<Project>();

            foreach (DataRow row in results.Rows)
            {
                Project temp = new Project();
                temp.ProjectID = (int)row["ProjectID"];
                temp.Title = (string)row["Title"];
                temp.RootFolderID = (int)row["RootFolder"];
                temp.WordCount = (int)row["WordCount"];

                var LanguageParameters = new Dictionary<string,object>();
                LanguageParameters.Add("@LanguageID",row["SourceLanguage"]);
                temp.SourceLanguage = new Language();
                var LangResult = ExecStoredProcWithResults("Dev_Storm","GetLanguage",LanguageParameters);
                temp.SourceLanguage.LanguageID = (int)LangResult.Rows[0]["LanguageID"];
                temp.SourceLanguage.Name = (string)LangResult.Rows[0]["Name"];

                LanguageParameters = new Dictionary<string,object>();
                LanguageParameters.Add("@ProjectID",temp.ProjectID);
                var TargetResults = ExecStoredProcWithResults("Dev_Storm", "GetLanguageAssociationsWithInformation", LanguageParameters);
                List<Language> targetList = new List<Language>();

                foreach (DataRow langrow in TargetResults.Rows)
                {
                    Language newTarget = new Language();
                    newTarget.LanguageID = (int)langrow["LanguageID"];
                    newTarget.Name = (string)langrow["Name"];
                    targetList.Add(newTarget);
                }

                temp.TargetLanguages = targetList;

                var ResourceParameters = new Dictionary<string, object>();
                ResourceParameters.Add("@ProjectID", temp.ProjectID);

                var ResourceResults = ExecStoredProcWithResults("Dev_Storm", "GetResourceAssociationsWithInformation", ResourceParameters);

                List<Resource> ResourceList = new List<Resource>();

                foreach (DataRow resourceRow in ResourceResults.Rows)
                {
                    Resource newResource = new Resource();
                    newResource.Name = (string)resourceRow["name"];
                    newResource.Email = (string)resourceRow["email"];
                    newResource.Phone = (string)resourceRow["phone"];
                    ResourceList.Add(newResource);
                }

                temp.Resources = ResourceList;

                ProjectList.Add(temp);
            }

            return ProjectList;
        }

    }
}