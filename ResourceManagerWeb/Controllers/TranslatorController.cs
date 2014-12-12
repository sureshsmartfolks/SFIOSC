using ResourceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ResourceManager.BusinessObject.DAL;
using System.Data;
using System.Net.Http;

namespace ProductsApp.Controllers
{
    public class ResourceController : ApiController
    {

        #region privateFunctions
        private string dataTableToJson(DataTable table)
        {
            List<Dictionary<string, object>> listToReturn = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            for (int rowIndex = 0; rowIndex < table.Rows.Count; ++rowIndex)
            {
                row = new Dictionary<string, object>();
                for (int colIndex = 0; colIndex < table.Columns.Count; ++colIndex)
                {
                    DataColumn col = table.Columns[colIndex];
                    DataRow dr = table.Rows[rowIndex];
                    if (col.ColumnName == "EndDate" || col.ColumnName == "StartDate" || col.ColumnName == "UpdateDate" || col.ColumnName == "CreateDate" || col.ColumnName == "CreatedDate" || col.ColumnName == "ValidDate")
                    {
                        string value = dr[col].ToString();
                        if (value != string.Empty && value != null && value != "")
                        {
                            DateTime value2 = Convert.ToDateTime(value);
                            value = value2.ToString("dd/MM/yyyy");
                            row.Add(col.ColumnName, value.Trim());
                        }
                        else
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                listToReturn.Add(row);
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 50000000;
            string strToReturn = serializer.Serialize(listToReturn);
            return strToReturn;
        }
        #endregion

        public void PostResource(Resource translator)
        {

            ResourceDataAccess translatorDA = new ResourceDataAccess();
            if (translator.ResourceID == -1)
            {// Add New
                translatorDA.AddResource(translator);
            }
            else 
            { //Edit
                translatorDA.UpdateResource(translator);
            }

        }



        public void UpdateTranslator(int translatorID, string name, string language, string phone, string email)
        {

            ResourceDataAccess translatorDA = new ResourceDataAccess();
            translatorDA.DeleteResource(translatorID);

        }

        public void DeleteTranslator(int translatorID) 
        {

            ResourceDataAccess translatorDA = new ResourceDataAccess();
            translatorDA.DeleteResource(translatorID);

        }

        public Resource GetAllTranslators()
        {
            ResourceDataAccess translatorDA = new ResourceDataAccess();
            DataTable dt = translatorDA.getAllResources();
            DataTable outputdT = new DataTable();
            outputdT.Columns.Add("id", typeof(int));
            outputdT.Columns.Add("name", typeof(string));
            outputdT.Columns.Add("language", typeof(string));
            outputdT.Columns.Add("phone", typeof(string));
            outputdT.Columns.Add("email", typeof(string));
            outputdT.Columns.Add("note", typeof(string));
            

            int id = 0;
            string name = "";
            string language = "";
            string phone = "";
            string email = "";
            string note = "";

            foreach (DataRow row in dt.Rows) 
            {
                id = Convert.ToInt32(row["id"]);
                name = Convert.ToString(row["name"]);
                note = (row["note"]== DBNull.Value) ? "" :  Convert.ToString(row["note"]);
                email = "";
                phone = "";
                language = "";

                DataTable dtContact = translatorDA.GetContact(id);
                foreach (DataRow contactRow in dtContact.Rows)
                {
                    if (contactRow["contactType"].ToString().ToLower() == "email")
                    {
                        email += Convert.ToString(contactRow["contactValue"]) + Environment.NewLine;
                    }
                    else 
                    {
                        phone +=  Convert.ToString(contactRow["contactValue"]) + Environment.NewLine;
                    }

                }

                DataTable dTLanguage = translatorDA.GetLanguage(id);
                foreach (DataRow languageRow in dTLanguage.Rows)
                {
                    language += Convert.ToString(languageRow["language"])+ "     " ;                   
                }
                outputdT.Rows.Add(id,name,language,phone,email,note);
            }

            Resource returnObj = new Resource();
            returnObj.JsonResults = dataTableToJson(outputdT);

            return returnObj;
        }

     
    }
}