using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sdl.LanguagePlatform.TranslationMemory;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using OwinSelfhostSample.Messaging;

namespace OwinSelfhostSample
{
    public class TmSearchController : ApiController
    {
        // GET api/values
        public List<TranslationResponse> Post([FromBody]TranslationRequestDTO obj)
        {
            //return new string[] { "value1", "value2" };
            Search search = new Search();
            List<TranslationResponse> results = new List<TranslationResponse>();

            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["COBRA"].ToString());
            SqlCommand comm = new SqlCommand("SELECT * from MemoryFile where SourceLanguage='" + obj.SourceLanguage + "' AND TargetLanguage='" + obj.TargetLanguage + "';",oConnection);

            DataTable dt = new DataTable("MemoryFiles");
            DataTable meta = new DataTable("MetaSearch");


            oConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter(comm);

            da.Fill(dt);
            DataTable bonusTable = new DataTable();
            string strsql = "SELECT MemoryID from MetaTag where MetaData Like '";
            if (obj.MetaData != null)
            {
                foreach (var searchterm in obj.MetaData)
	            {
                    strsql += searchterm + "' OR MetaData Like '";
	            }
                strsql = strsql.Substring(0,(strsql.Length - (" OR MetaData Like '".Length)));

                da.SelectCommand.CommandText = strsql;
                da.Fill(bonusTable);
            }
                
            
            
            oConnection.Close();



            foreach (DataRow row in dt.Rows)
            {

                List<TranslationResponse> searchResults = new List<TranslationResponse>();
                searchResults = search.SearchForText(row["FilePath"].ToString(),obj.Request,SearchMode.NormalSearch);
                int bonusScore = 0;
                if (bonusTable.Rows.Count > 0)
                {
                    bonusScore = (bonusTable.AsEnumerable().Where(bonusRow => row["MemoryID"].ToString() == bonusRow["MemoryID"].ToString())
                        .Count()) * 15;
                }
                foreach (var result in searchResults)
                {
                    result.Score += bonusScore;
                    results.Add(result);
                }
            }

          

            results = results.OrderByDescending(resp => resp.Score).ToList();

            
            return results;
        }

        

    }
}
