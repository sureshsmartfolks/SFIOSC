using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSC.ProjectManagement.BusinessObjects
{
    public class DataManager
    {
        // document/s
        public int insertDocument(Document rc)
        {
            //Document rc = new Document();
            int newID = -1;
            SQLDataAccess da = new SQLDataAccess();
            newID = da.insertDocument(rc);
            return newID;
        }

        public Document getDocument(int id)
        {
            Document rc = new Document();
            SQLDataAccess da = new SQLDataAccess();
            rc = da.getDocument(id);
            return rc;
        }

        public List<Document> getAllDocuments()
        {
            return null;
        }

        // -------------------------------------------------------

        // project/s
        public int insertProject(STORMProject trc)
        {
            SQLDataAccess da = new SQLDataAccess();
            // Create/Update project
            int newID = -1;
            trc.client.ID = da.insertClient(trc.client);
            newID = da.insertProject(trc);
            return newID;
        }

        public STORMProject getProject(int id)
        {
            STORMProject rc = new STORMProject();
            SQLDataAccess da = new SQLDataAccess();


            rc = da.getProject(id);
            return rc;
        }
        public List<STORMProject> getAllProjects()
        {
            List<STORMProject> rc = new List<STORMProject>();
            SQLDataAccess da = new SQLDataAccess();
            rc = da.getAllProjects();

            return rc;
        }

        // -------------------------------------------------------

        // client
        public Client getClient(int id)
        {
            Client rc = new Client();
            SQLDataAccess da = new SQLDataAccess();
            rc = da.getClient(id);
            return rc;
        }

    }
}