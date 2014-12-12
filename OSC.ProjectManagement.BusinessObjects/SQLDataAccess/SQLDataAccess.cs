using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OSC.ProjectManagement.BusinessObjects
{
    public class SQLDataAccess
    {
        // document access

        // something might be wrong with this setup
        public int insertResource(resource rc)
        {
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            int newID = -1;
            if (rc.resourceID < 1)
            {

                string sComm = "DECLARE @resourceID int;\n" +
                    "INSERT INTO Resource " +
                    "(firstName, lastName, company, address, type, isCompany)" + 
                    "VALUES" +
                    "(@firstName, @lastName, @company, @address, @type, @isCompany);\n" + 
                    "SET @resourceID = SCOPE_IDENTITY();\n";
                foreach (int ID in rc.languageAssociations.translateDirectionID)
                {
                    sComm += "INSERT INTO ResourceLanguage (resourceID, targetLanguageID) VALUES (@resourceID, " + ID + ");\n";
                }

                sComm += "SELECT @resourceID;";

                //Dictionary<string, object> parameters = new Dictionary<string, object>();
                //parameters.Add("@firstName", rc.firstName);
                //parameters.Add("@lastName", rc.lastName);
                //parameters.Add("@company", rc.company);
                //parameters.Add("@address", rc.address);
                //parameters.Add("@type", rc.type);
                //parameters.Add("@isCompany", rc.isCompany);

                //SQLDataHelper.ExecuteDataQuery("Storm", sComm, parameters);

                //return 0;

                SqlConnection conn = new SqlConnection(sConn);
                SqlCommand comm = new SqlCommand(sComm, conn);

                comm.Parameters.AddWithValue("@firstName", rc.firstName);
                comm.Parameters.AddWithValue("@lastName", rc.lastName);
                comm.Parameters.AddWithValue("@company", rc.company);
                comm.Parameters.AddWithValue("@address", rc.address);
                comm.Parameters.AddWithValue("@type", rc.type);
                comm.Parameters.AddWithValue("@isCompany", rc.isCompany);

                conn.Open();
                newID = (int)comm.ExecuteScalar();
                conn.Close();

                // Get this ID fron SQL....
                return newID;
            }
            else
            {
                string sComm = "UPDATE Resource SET " +
                     "firstName = @firstName, "+
                     "lastName = @lastName, "+
                     "company = @company, "+
                     "address = @address, "+
                     "type = @type, "+
                     "isCompany = @isCompany "+
                     " WHERE resourceID=" + rc.resourceID + ";\n" +
                     "DELETE FROM ResourceLanguage WHERE documentID=" + rc.resourceID + ";\n";

                foreach (int ID in rc.languageAssociations.translateDirectionID)
                {
                    sComm += "INSERT INTO ResourceLanguage (resourceID, targetLanguageID) VALUES (" + rc.resourceID + ", " + ID + ");\n";
                }


                //Dictionary<string, object> parameters = new Dictionary<string, object>();
                //parameters.Add("@firstName", rc.firstName);
                //parameters.Add("@lastName", rc.lastName);
                //parameters.Add("@company", rc.company);
                //parameters.Add("@address", rc.address);
                //parameters.Add("@type", rc.type);
                //parameters.Add("@isCompany", rc.isCompany);

                //SQLDataHelper.ExecuteScalarQuery("Storm", sComm, parameters);
                
                //return 0;
                SqlConnection conn = new SqlConnection(sConn);
                SqlCommand comm = new SqlCommand(sComm, conn);

                comm.Parameters.AddWithValue("@firstName", rc.firstName);
                comm.Parameters.AddWithValue("@lastName", rc.lastName);
                comm.Parameters.AddWithValue("@company", rc.company);
                comm.Parameters.AddWithValue("@address", rc.address);
                comm.Parameters.AddWithValue("@type", rc.type);
                comm.Parameters.AddWithValue("@isCompany", rc.isCompany);

                conn.Open();
                newID = (int)comm.ExecuteScalar();
                conn.Close();

                // Get this ID fron SQL....
                return newID;
            }


        }

        public int insertDocument(Document rc)
        {
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;

            if (rc.documentID < 1)
            {

                int newID = -1;
                string sComm = "DECLARE @documentID int;\n" +
                    "INSERT INTO Document " +
                    "(filePath, status, version, typeID, sourceLanguageID, name)" +
                    "VALUES" +
                    "(@filePath, @status, @version, @typeID, @sourceLanguageID, @name);\n" +
                    "SET @documentID = SCOPE_IDENTITY();\n";
                foreach (int ID in rc.translateDirections.translateDirectionID)
                {
                    sComm += "INSERT INTO DocumentLanguage (documentID, targetLanguageID) VALUES (@documentID, " + ID + ");\n";
                }

                sComm += "SELECT @documentID;";

                SqlConnection conn = new SqlConnection(sConn);
                SqlCommand comm = new SqlCommand(sComm, conn);

                comm.Parameters.AddWithValue("@filePath", rc.filePath);
                comm.Parameters.AddWithValue("@status", rc.status);
                comm.Parameters.AddWithValue("@version", rc.version);
                comm.Parameters.AddWithValue("@typeID", rc.typeID);
                comm.Parameters.AddWithValue("@sourceLanguageID", rc.sourceLanguageID);
                comm.Parameters.AddWithValue("@name", rc.name);
                //comm.Parameters.AddWithValue("@documentID", rc.projectID);

                conn.Open();
                newID = (int)comm.ExecuteScalar();


                //sComm = "";


                //comm.CommandText = sComm;
                //comm.ExecuteNonQuery();
                //newID = (int)(decimal)comm.ExecuteScalar();
                conn.Close();

                // Get this ID fron SQL....
                return newID;
            }
            else
            {
                string sComm = "UPDATE Document SET " +
                    "filePath = @filePath, " +
                    "status = @status, " +
                    "sourceLanguageID = @sourceLanguageID, " +
                    "typeID = @typeID " +
                    " WHERE documentID=" + rc.documentID + ";\n" +
                    "DELETE FROM DocumentLanguage WHERE documentID=" + rc.documentID + ";\n";

                foreach (int ID in rc.translateDirections.translateDirectionID)
                {
                    sComm += "INSERT INTO DocumentLanguage (documentID, targetLanguageID) VALUES (" + rc.documentID + ", " + ID + ");\n";
                }

                SqlConnection conn = new SqlConnection(sConn);
                SqlCommand comm = new SqlCommand(sComm, conn);

                comm.Parameters.AddWithValue("@filePath", rc.filePath);
                comm.Parameters.AddWithValue("@status", rc.status);
                //comm.Parameters.AddWithValue("@version", rc.version);
                comm.Parameters.AddWithValue("@typeID", rc.typeID);
                comm.Parameters.AddWithValue("@sourceLanguageID", rc.sourceLanguageID);
                //comm.Parameters.AddWithValue("@documentID", rc.projectID);

                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                return 0;

                //return -1;
            }


        }

        public int insertClient(Client rc)
        {
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            if (rc.ID < 1)
            {
                int newID = -1;

                // Must check if client exists
                string sComm = "INSERT INTO " +
                "Client (" +
                    "Name, Email, phoneNumber" +
                ") VALUES (" +
                    "@name, @email, @phoneNumber); SELECT SCOPE_IDENTITY();";

                SqlConnection conn = new SqlConnection(sConn);
                SqlCommand comm = new SqlCommand(sComm, conn);

                comm.Parameters.AddWithValue("@name", rc.name);
                comm.Parameters.AddWithValue("@email", rc.email);
                comm.Parameters.AddWithValue("@phoneNumber", rc.phoneNumber);

                conn.Open();
                newID = (int)(decimal)comm.ExecuteScalar();
                conn.Close();

                //create doc/Lang associations

                // Get this ID fron SQL....

                return newID;
            }
            else
            {
                string sComm = "UPDATE Client SET " +
                    "Name=@name, " +
                    "Email=@email, " +
                    "phoneNumber=@phoneNumber " + 
                    "WHERE clientID=" + rc.ID + ";\n";

                SqlConnection conn = new SqlConnection(sConn);
                SqlCommand comm = new SqlCommand(sComm, conn);

                comm.Parameters.AddWithValue("@name", rc.name);
                comm.Parameters.AddWithValue("@email", rc.email);
                comm.Parameters.AddWithValue("@phoneNumber", rc.phoneNumber);

                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
        }

        public int insertProject(STORMProject rc)
        {
            int newID = -1;

            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;

            if (rc.projectID < 1)
            {
                string sComm = "DECLARE @projectID int;\n" +

                    "INSERT INTO Project " +
                    "(client, Title, SourceLanguage, trackPath, trackStatus)" +
                    "VALUES" +
                    "(@clientID, @projectName, @SourceLanguage, @trackPath, @trackStatus); " +
                    "SET @projectID = SCOPE_IDENTITY();\n";
                foreach (Document doc in rc.documents)
                {
                    //doc.projectID = newID;
                    doc.documentID = insertDocument(doc);
                    sComm += "INSERT INTO ProjectDocument (projectID, documentID) VALUES (@projectID, " + doc.documentID + ");\n";
                    sComm += "UPDATE Document SET projectID=@projectID WHERE documentID= " + doc.documentID + ";\n";
                }

                foreach (resource res in rc.resources)
                {
                    //doc.projectID = newID;
                    res.resourceID = insertResource(res);
                    sComm += "INSERT INTO ProjectResource (projectID, resourceID) VALUES (@projectID, " + res.resourceID + ");\n";
                    sComm += "UPDATE Resource SET projectID = @projectID WHERE resourceID= " + res.resourceID + ";\n";
                }

                sComm += "SELECT @projectID;";

                SqlConnection conn = new SqlConnection(sConn);
                SqlCommand comm = new SqlCommand(sComm, conn);

                comm.Parameters.AddWithValue("@clientID", rc.client.ID);
                comm.Parameters.AddWithValue("@projectName", rc.projectName);
                comm.Parameters.AddWithValue("@SourceLanguage", rc.sourceLanguageID);
                comm.Parameters.AddWithValue("@trackPath", ""); //rc.trackPath);
                comm.Parameters.AddWithValue("@trackStatus", ""); //rc.trackStatus);

                conn.Open();
                newID = (int)comm.ExecuteScalar();
                conn.Close();

                //create proj/doc associations
                //create proj/lang associations
                //create proj/rec associations

                //create rec/Lang associations

                // Get this ID fron SQL....
                return newID;
            }
            else
            {
                string sComm = "UPDATE Project SET " +
                    "Title=@projectName, " +
                    "trackPath=@trackPath, " +
                    "trackStatus=@trackStatus " +
                    "WHERE projectID=" + rc.projectID + ";\n";

                //search for null and delete
                foreach (Document doc in rc.documents)
                {
                    if (doc.name == "0")
                    {
                        deleteDocument(doc.documentID);
                    }
                    else if (doc.documentID < 1)
                    {
                        doc.documentID = insertDocument(doc);
                        sComm += "INSERT INTO ProjectDocument (projectID, documentID) VALUES (" + rc.projectID + ", " + doc.documentID + ");\n";
                        sComm += "UPDATE Document SET projectID=" + rc.projectID + " WHERE documentID= " + doc.documentID + ";\n";
                    }
                    else if (doc.documentID > 0)
                    {
                        sComm += "UPDATE Document SET" +
                            " name = '" + doc.name + "'" + 
                            ", status = " + doc.status + 
                            ", typeID = " + doc.typeID +
                            " WHERE documentID=" + doc.documentID + ";\n";
                        sComm += "DELETE FROM DocumentLanguage WHERE documentID=" + doc.documentID + ";\n";

                        foreach (int ID in doc.translateDirections.translateDirectionID)
                        {
                            sComm += "INSERT INTO DocumentLanguage (documentID, targetLanguageID) VALUES (" + doc.documentID + ", " + ID + ");\n";
                        }
                    }
                }

                foreach (resource res in rc.resources)
                {
                    if (res.firstName == "0")
                    {
                        deleteResource(res.resourceID);
                    }
                    else if (res.resourceID < 1)
                    {
                        res.resourceID = insertResource(res);
                        sComm += "INSERT INTO ProjectResource (projectID, resourceID) VALUES (" + rc.projectID + ", " + res.resourceID + ");\n";
                        sComm += "UPDATE Resource SET projectID=" + rc.projectID + " WHERE resourceID= " + res.resourceID + ";\n";
                    }
                    else if (res.resourceID > 0)
                    {
                        sComm += "UPDATE Resource SET" +
                            " firstName = '" + res.firstName + "'" +
                            ", lastName = '" + res.lastName + "'" +
                            ", address = '" + res.address + "'" +
                            ", company = '" + res.company + "'" +
                            ", type = '" + res.type + "'" +
                            " WHERE resourceID=" + res.resourceID + ";\n";
                        sComm += "DELETE FROM ResourceLanguage WHERE resourceID=" + res.resourceID + ";\n";

                        foreach (int ID in res.languageAssociations.translateDirectionID)
                        {
                            sComm += "INSERT INTO ResourceLanguage (resourceID, targetLanguageID) VALUES (" + res.resourceID + ", " + ID + ");\n";
                        }
                    }
                }

                //sComm += "SELECT @projectID;";

                SqlConnection conn = new SqlConnection(sConn);
                SqlCommand comm = new SqlCommand(sComm, conn);

                comm.Parameters.AddWithValue("@projectName", rc.projectName);
                comm.Parameters.AddWithValue("@trackPath", rc.trackPath); //rc.trackPath);
                comm.Parameters.AddWithValue("@trackStatus", rc.trackStatus); //rc.trackStatus);

                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
        }

        // deletes document from everywhere.....
        public string deleteDocument(int id)
        {
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "DELETE FROM DocumentLanguage WHERE documentID=" + id + ";\n";
            sComm += "DELETE FROM ProjectDocument WHERE documentID=" + id + ";\n";
            sComm += "DELETE FROM Document WHERE documentID=" + id + ";";

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            return null;
        }

        public string deleteResource(int id)
        {
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "DELETE FROM ResourceLanguage WHERE resourceID=" + id + ";\n";
            sComm += "DELETE FROM ProjectResource WHERE resourceID=" + id + ";\n";
            sComm += "DELETE FROM Resource WHERE resourceID=" + id + ";";

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            return null;
        }

        public Client getClient(int id)
        {
            Client trc = new Client();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "SELECT clientID, email, name, phoneNumber FROM Client WHERE clientID=" + id.ToString();

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();

            SqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                trc.ID = Convert.ToInt32(dr["clientID"]);
                trc.email = dr["email"].ToString();
                trc.name = dr["name"].ToString();
                trc.phoneNumber = dr["phoneNumber"].ToString();
            }

            conn.Close();

            return trc;
        }

        public Document getDocument(int id)
        {
            Document trc = new Document();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "SELECT " + 
                "documentID, filePath, status, version, typeID, sourceLanguageID, projectID, name" + 
                " FROM document WHERE documentID=" + id.ToString();


            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();
            SqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                trc.documentID = Convert.ToInt32(dr["documentID"]);
                trc.filePath = dr["filePath"].ToString();
                trc.status = Convert.ToInt32(dr["status"]);//.ToString();
                trc.version = Convert.ToInt32(dr["version"]);
                trc.typeID = Convert.ToInt32(dr["typeID"]);
                trc.sourceLanguageID = Convert.ToInt32(dr["sourceLanguageID"]);
                trc.projectID = Convert.ToInt32(dr["projectID"]);
                trc.translateDirections = getTargetDocumentLanguages(id);
                trc.name = dr["name"].ToString();
            }
            conn.Close();
            return trc;
        }
        public resource getResource(int id)
        {
            resource trc = new resource();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "SELECT " +
                "firstName, lastName, company, address, type, isCompany, resourceID" +
                " FROM Resource WHERE resourceID=" + id.ToString();


            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();
            SqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                trc.firstName = dr["firstName"].ToString();
                trc.lastName = dr["lastName"].ToString();
                trc.company = dr["company"].ToString();
                trc.address = dr["address"].ToString();
                trc.type = dr["type"].ToString();
                trc.isCompany = Convert.ToBoolean(dr["isCompany"]);
                trc.resourceID = Convert.ToInt32(dr["resourceID"]);
                trc.languageAssociations = getTargetResourceLanguages(id);
            }
            conn.Close();
            return trc;
        }

        public language getTargetResourceLanguages(int resID)
        {
            language temp = new language();
            temp.translateDirectionID = new List<int>();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string scomm = "SELECT resourceID, targetLanguageID FROM resourceLanguage WHERE resourceID=" + resID.ToString() + ";";

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(scomm, conn);

            conn.Open();

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                temp.translateDirectionID.Add(Convert.ToInt32(dr["targetLanguageID"]));
            }

            conn.Close();
            return temp;//trc.translateDirections;

        }


        public language getTargetDocumentLanguages(int docID)
        {
            language temp = new language();
            temp.translateDirectionID = new List<int>();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string scomm = "SELECT documentID, targetLanguageID FROM documentLanguage WHERE documentID=" + docID.ToString() + ";";

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(scomm, conn);

            conn.Open();

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                temp.translateDirectionID.Add(Convert.ToInt32(dr["targetLanguageID"]));
            }

            conn.Close();
            return temp;//trc.translateDirections;

        }


        public List<Document> getAllProjectDocuments(int id)
        {
            List<Document> docs = new List<Document>();
            //Document trc = new Document();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "SELECT projectID, documentID FROM ProjectDocument WHERE projectID=" + id.ToString() +";";

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();

            SqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                docs.Add(getDocument(Convert.ToInt32(dr["documentID"])));
            }

            conn.Close();

            return docs;
        }


        public List<resource> getAllProjectResources(int id)
        {
            List<resource> resources = new List<resource>();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "SELECT projectID, resourceID FROM ProjectResource WHERE projectID=" + id.ToString() +";";

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();

            SqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                resources.Add(getResource(Convert.ToInt32(dr["resourceID"])));
            }

            conn.Close();

            return resources;
        }

        // project access
        public STORMProject getProject(int id)
        {
            STORMProject trc = new STORMProject();
            SQLDataAccess da = new SQLDataAccess();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "SELECT " +
                "projectID, Title, SourceLanguage, client, trackStatus, trackPath " + 
                "FROM project WHERE projectID=" + id.ToString();

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();

            SqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                trc.projectID = Convert.ToInt32(dr["projectID"]);
                trc.projectName  = dr["Title"].ToString();
                trc.sourceLanguageID = Convert.ToInt32(dr["SourceLanguage"]);
                trc.trackStatus = dr["trackStatus"].ToString();
                trc.trackPath = dr["trackPath"].ToString();
                
                // get resources
                trc.client = da.getClient(Convert.ToInt32(dr["client"]));
                trc.documents = da.getAllProjectDocuments(trc.projectID);
                trc.resources = da.getAllProjectResources(trc.projectID);
                // get documents ..... use assoctable

            }

            conn.Close();

            return trc;
        }

        public List<STORMProject> getAllProjects()
        {
            List<STORMProject> projects = new List<STORMProject>();
            //STORMProject trc = new STORMProject();
            string sConn = ConfigurationManager.ConnectionStrings["Storm"].ConnectionString;
            string sComm = "SELECT projectID FROM Project;";

            SqlConnection conn = new SqlConnection(sConn);
            SqlCommand comm = new SqlCommand(sComm, conn);

            conn.Open();

            SqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                projects.Add(getProject(Convert.ToInt32(dr["projectID"])));
            }

            conn.Close();

            return projects;       
        }

    }
}