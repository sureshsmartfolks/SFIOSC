using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ResourceManager.Models;

namespace ResourceManager.BusinessObject.DAL
{
    public class ResourceDataAccess
    {
        public DataTable getAllResources()
        {
            DataTable dt = new DataTable("Translators");
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ResourceManager"].ToString());
            SqlCommand oCommand = oConnection.CreateCommand();

            

            oCommand.CommandText = "Select * from Translator order by name" ;

            SqlDataAdapter Adapt = new SqlDataAdapter(oCommand);

            oConnection.Open();
            Adapt.Fill(dt);
            oConnection.Close();

            return dt;
        }


        public DataTable GetContact(int id)
        {
            DataTable dt = new DataTable("Contact");
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ResourceManager"].ToString());
            SqlCommand oCommand = oConnection.CreateCommand();

            oCommand.CommandText = "Select * from Contact where translatorID=@ID";
            oCommand.Parameters.AddWithValue("@ID", id);
            SqlDataAdapter Adapt = new SqlDataAdapter(oCommand);

            oConnection.Open();
            Adapt.Fill(dt);
            oConnection.Close();

            return dt;
        }


        public DataTable GetLanguage(int id)
        {
            DataTable dt = new DataTable("Language");
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ResourceManager"].ToString());
            SqlCommand oCommand = oConnection.CreateCommand();

            
            oCommand.CommandText = "Select * from TranslatorLanguage where translatorID=@ID";
            oCommand.Parameters.AddWithValue("@ID", id);
            SqlDataAdapter Adapt = new SqlDataAdapter(oCommand);

            oConnection.Open();
            Adapt.Fill(dt);
            oConnection.Close();

            return dt;
        }


        public void DeleteResource(int resourceID) 
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ResourceManager"].ToString());
            SqlCommand oCommand = oConnection.CreateCommand();


            oCommand.CommandText = @"Delete from Translator where id=@ID
                                    Delete from Contact where translatorID=@ID
                                    Delete from TranslatorLanguage where translatorID=@ID";

            oCommand.Parameters.AddWithValue("@ID", resourceID);
            oConnection.Open();
            oCommand.ExecuteNonQuery();
            oConnection.Close();
        }





        public void AddResource(Resource resource)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ResourceManager"].ToString());
            SqlCommand oCommand = oConnection.CreateCommand();
            int translatorID = 0;
           try
           {
            //Add Translator
            oCommand.CommandText = "Insert Into [Translator] (name) VALUES (@Name)";
            oCommand.CommandText += " SELECT SCOPE_IDENTITY() as ID;";

            oCommand.Parameters.AddWithValue("@Name", resource.Name);
            oConnection.Open();
            translatorID = Convert.ToInt32(oCommand.ExecuteScalar());
            oCommand.Parameters.Clear();
            
            //Add Contact and Language
            oCommand.CommandText = @"Insert into TranslatorLanguage (translatorID,language) Values(@ID,@Language)
                                    Insert into Contact (translatorID, contactType, contactValue) Values(@ID, 'Phone', @Phone)
                                    Insert into Contact (translatorID, contactType, contactValue) Values(@ID, 'Email', @Email)";
            
               
            oCommand.Parameters.AddWithValue("@ID", translatorID);
            oCommand.Parameters.AddWithValue("@Phone", resource.Phone);
            oCommand.Parameters.AddWithValue("@Email", resource.Email);
            oCommand.Parameters.AddWithValue("@Language", resource.Language);
            oCommand.ExecuteNonQuery();
            }
            catch (SystemException ex)
            {

            }
            finally 
            {
                oConnection.Close();
            }
        }


        public void UpdateResource(Resource resource)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ResourceManager"].ToString());
            SqlCommand oCommand = oConnection.CreateCommand();


            oCommand.CommandText = @"Update Translator Set name=@Name where id=@ID
            Update TranslatorLanguage Set language=@Language where translatorID=@ID
            Delete From Contact where translatorID=@ID
            Insert into Contact (translatorID, contactType, contactValue) Values(@ID, 'Phone', @Phone)
            Insert into Contact (translatorID, contactType, contactValue) Values(@ID, 'Email', @Email)";

            oCommand.Parameters.AddWithValue("@ID", resource.ResourceID);
            oCommand.Parameters.AddWithValue("@Name", resource.Name);
            oCommand.Parameters.AddWithValue("@Language", resource.Language);
            oCommand.Parameters.AddWithValue("@Phone", resource.Phone);
            oCommand.Parameters.AddWithValue("@Email", resource.Email);
            oConnection.Open();
            oCommand.ExecuteNonQuery();
            oConnection.Close();
        }


    }
}