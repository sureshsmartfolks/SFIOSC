using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OSC.BusinessObjects.FileTransfer;
using OSC.BusinessObjects.FileTransfer.DataAccess;
using System.Data.SqlClient;
using System.Web.Http.Cors;
using System.Web.Configuration;
using OCS.HelperObjects.FileTransfer;
using System.IO;
using System.Net.Http.Headers;
using System.Configuration;
using System.IO.Compression;
using System.Web;
using OSC.Services.FileTransfer.Models;
using System.Web.Mvc;

namespace OSC.Services.FileTransfer.Controllers
{
    public class FileTransferController : ApiController
    {
        [EnableCors(origins: "*", headers: "*",methods: "HEAD,OPTIONS,GET,POST")]
        public HttpResponseMessage Head()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };

        }
        // GET api/filetransfer
        //[EnableCors("*", "", "GET")]
        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        public HttpResponseMessage Get(int folderid)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["dbFilerepo"].ConnectionString;
            List<FileObject> p = new List<FileObject>();
            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {


                #region Get all sub folders within requested folder
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =
                    "select * from folder_table where parent_folder = @id";
                    cmd.Parameters.AddWithValue("@id", folderid);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileObject tmp = new FileObject();

                            tmp.fileid = reader["Id"].ToString();
                            tmp.filename =
                            (string)reader["Folder_name"];
                            tmp.filepath =
                            reader["Parent_folder"].ToString();
                            tmp.filesize =
                            0;

                            tmp._type = "Folder";


                            p.Add(tmp);
                        }
                    }
                }
                #endregion

                #region get all files within requested folder
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =
                    "select * from tbl_FilesRepo where folderid = @folderid";
                    cmd.Parameters.AddWithValue("@folderid", folderid); ;

                    cmd.Connection = conn;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileObject tmp = new FileObject();

                            tmp.fileid = reader["fileid"].ToString();
                            tmp.filename =
                            (string)reader["filename"];
                            tmp.filepath =
                            (string)reader["filepath"];
                            tmp.filesize =
                            (long)reader["filesize"];
                            tmp.uploadby =
                            (string)reader["uploadedby"];
                            tmp.dbid =
                            (int)reader["id"];
                            tmp.uploaddate =
                            (DateTime)reader["uploaddate"];
                            tmp._type = "File";
                            p.Add(tmp);
                        }
                    }
                }
                #endregion
            }


            FileDataAccess fda = new FileDataAccess();
            FolderObject fo = fda.getFolder(folderid);
            if (fo.Parent_folder == 1000)
            {
                fo.Parent_folder = 0002;
            }
            if (p.Count < 1)
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { ParentFolder = fo.Parent_folder, FolderID = fo.Id, FolderName = fo.Folder_name, Result = "OK", Message = "Folder is Empty" }) };
            }


            return new HttpResponseMessage()
            {
                Content = new JsonContent(new
                {
                    ParentFolder = fo.Parent_folder,
                    FolderID = fo.Id,
                    FolderName = fo.Folder_name,
                    Result = "OK",
                    Records = p.ToList()

                })
            };

        }

        // GET api/filetransfer/5
        public HttpResponseMessage Get(string id)
        {
            FileObject result = new FileObject();
            FileDataAccess fda = new FileDataAccess();
            FileProvider fileProvider;


            var response = new HttpResponseMessage();
            string[] p = id.Split(',');
            string error = "";

            if (p.Length > 0)
            {
                if (p.Length == 1)
                {
                    fileProvider = new FileProvider("FilesLocation");
                    result = fda.GetFile(p[0]);
                    if (result == null)
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent(
                                "<strong><font color='red'>Sorry! Selected file(s) not found. Please contact adminitrator.</font></strong>",
                                System.Text.Encoding.UTF8,
                                "text/html"
                            )
                        };

                    }

                    if (!fileProvider.Exists(result.fileid))
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent(
                                 "<strong><font color='red'>Sorry! Selected file(s) not found. Please contact adminitrator.</font></strong>",
                                 System.Text.Encoding.UTF8,
                                 "text/html"
                             )
                        };
                    }

                    FileStream fileStream = fileProvider.Open(result.fileid);
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentDisposition
                        = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = result.filename;
                    response.Content.Headers.ContentType
                        = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentLength
                            = fileProvider.GetLength(result.fileid);
                    return response;

                }
                else
                {
                    string adhoc = ConfigurationManager.AppSettings["AdHocLocation"];

                    string tempfolder = DateTime.Now.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() +
                                     DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();

                    string targetdirectory = Path.Combine(adhoc + tempfolder);
                    Directory.CreateDirectory(targetdirectory);

                    if (Directory.Exists(targetdirectory))
                    {
                        string randomzipfilename = Guid.NewGuid().ToString() + ".zip";
                        string zipfile = targetdirectory + "\\" + randomzipfilename;

                        //outputzip = ZipFile.Open(zipfile, ZipArchiveMode.Create);

                        fileProvider = new FileProvider();
                        fileProvider.setFilelocation("filesLocation");

                        using (ZipArchive newFile = ZipFile.Open(zipfile, ZipArchiveMode.Create))
                        {

                            foreach (string file in p)
                            {
                                FileObject rep = fda.GetFile(file);
                                if (!fileProvider.Exists(rep.fileid))
                                {
                                    error += "File " + rep.filename + " cannot be found<br>";
                                    continue;
                                }
                                string targetFile = Path.Combine(targetdirectory + "\\" + rep.filename);
                                //File.Copy(rep.filepath, targetFile);

                                newFile.CreateEntryFromFile(rep.filepath, rep.filename, CompressionLevel.Fastest);
                                //newFile.CreateEntryFromFile(@"C:\Temp\File2.txt", "File2.txt", CompressionLevel.Fastest);

                            }
                        }
                        if (ZipFileCount(zipfile) > 0)
                        {
                            fileProvider.setFilelocation("AdHocLocation");
                            FileStream fileStream = fileProvider.Open(zipfile);
                            response.Content = new StreamContent(fileStream);
                            response.Content.Headers.ContentDisposition
                                = new ContentDispositionHeaderValue("attachment");
                            response.Content.Headers.ContentDisposition.FileName = randomzipfilename;
                            response.Content.Headers.ContentType
                                = new MediaTypeHeaderValue("application/octet-stream");
                            response.Content.Headers.ContentLength
                                    = fileProvider.GetLength(zipfile);

                            return response;

                        }
                        else
                        {
                            return new HttpResponseMessage()
                            {

                                Content = new StringContent(
                                    "<strong><font color='red'>Sorry! Selected file(s) not found. Please contact adminitrator.</font></strong>",
                                    System.Text.Encoding.UTF8,
                                    "text/html"
                                )
                            };

                        }

                    }
                    return new HttpResponseMessage()
                    {

                        Content = new StringContent(
                            "<font color='red'>Sorry! Adhoc folder could not be found. Please contact adminitrator.</font>",
                            System.Text.Encoding.UTF8,
                            "text/html"
                        )
                    };

                }

            }



            return new HttpResponseMessage(HttpStatusCode.NoContent);

        }

        // POST api/filetransfer
        //[Authorize(Roles="Administrators")]
        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        public HttpResponseMessage Post()
        {
            string uploadpath = ConfigurationManager.AppSettings["FilesLocation"];
            //string actualFileName = "";
            //string proxyName = "";
            FileDataAccess fda = new FileDataAccess();
            var request = HttpContext.Current.Request;
            int fid = 0;

            var r = new List<DataUploadFileResults>();

            if (request.Form.Get("folder_id1") == null)
            {
                DataUploadFileResults dur = (new DataUploadFileResults()
                {

                    name = "",
                    size = 0,
                    type = "",
                    url = "",
                    error = "Error! Upload folder was not specified."
                    //delete_url = "",
                    // thumbnail_url = "",
                    //delete_type = "GET"
                });
                HttpResponseMessage msg4 = new HttpResponseMessage();
                JsonContent js4 = new JsonContent(new { files = dur });
                js4.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                msg4.Content = js4;

                return msg4;
            }

             bool m = Int32.TryParse(request.Form.Get("folder_id1").ToString(), out fid);
             if (!m)
             {
                 DataUploadFileResults dur = (new DataUploadFileResults()
                 {
 
                     name = "",
                     size = 0,
                     type = "",
                     url = "",
                     error = "Error! Upload folder was not specified."
                 });
                 HttpResponseMessage msg5 = new HttpResponseMessage();
                 JsonContent js5 = new JsonContent(new { Records = dur });
                 js5.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                 msg5.Content = js5;
 
                 return msg5;
 
             }

            foreach (string file in HttpContext.Current.Request.Files)
            {
                var statuses = new List<DataUploadFileResults>();
                var headers = HttpContext.Current.Request.Headers;

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(request, statuses, fda, fid);
                }
                else
                {
                    //UploadPartialFile(headers["X-File-Name"], Request, statuses);
                    if (request.Files.Count != 1) throw new HttpRequestValidationException("Partial upload is not allowed at this point");
                }

                HttpResponseMessage msg = new HttpResponseMessage();
                JsonContent js = new JsonContent(new { files = statuses });
                js.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                msg.Content = js;


                return msg;
            }

            HttpResponseMessage msg1 = new HttpResponseMessage();
            JsonContent js1 = new JsonContent(new { Records = r.ToList() });
            js1.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            msg1.Content = js1;

            return msg1;
        }

        // PUT api/filetransfer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/filetransfer/5
        public void Delete(int id)
        {
        }

        // Number of files within zip archive
        public static int ZipFileCount(String zipFileName)
        {
            using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Read))
            {
                int count = 0;

                // We count only named (i.e. that are with files) entries
                foreach (var entry in archive.Entries)
                    if (!String.IsNullOrEmpty(entry.Name))
                        count += 1;

                return count;
            }

        }


        //Helper function to store uploaded files onto the server and in DB; also create a response messages to be returned to client
        private void UploadWholeFile(HttpRequest request, List<DataUploadFileResults> statuses, FileDataAccess fda,int fid)
        {
            string uploadpath = ConfigurationManager.AppSettings["FilesLocation"];
            string actualFileName = "";
            string proxyName = "";

            foreach (string file in request.Files)
            {
                var postedFile = request.Files[file];
                if (request.Browser.Browser.ToLower() == "ie" || request.Browser.Browser.ToLower() == "internetexplorer")
                {
                    string[] files = postedFile.FileName.Split(new char[] { '\\' });
                    actualFileName = files[files.Length - 1];
                }
                else
                {
                    actualFileName = postedFile.FileName;
                }
                proxyName = Guid.NewGuid().ToString();

                var fullPath = Path.Combine(uploadpath, proxyName);

                postedFile.SaveAs(fullPath);

                fda.StoreFile(actualFileName, proxyName, fullPath, postedFile.ContentLength, "User",fid, false);
                statuses.Add(new DataUploadFileResults()
                {
                    name = actualFileName,
                    size = postedFile.ContentLength,
                    type = postedFile.ContentType,
                    url = "http://localhost:61175/api/FileTransfer/?id=" + proxyName
                    //delete_url = "",
                    // thumbnail_url = "",
                    //delete_type = "GET"
                });
            }
        }
    }
}
