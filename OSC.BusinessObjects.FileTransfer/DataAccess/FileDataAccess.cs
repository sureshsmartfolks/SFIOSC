using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;


namespace OSC.BusinessObjects.FileTransfer.DataAccess
{
    public class FileDataAccess
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbFileRepo"].ConnectionString;

        public  FileObject GetFile(string fileid)
        {
            FileObject p = null;
            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =
                    "select * from tbl_FilesRepo where fileid=@id";
                    cmd.Parameters.AddWithValue("@id", fileid);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            p = new FileObject();
                            p.fileid = fileid;
                            p.filename =
                            (string)reader["filename"];
                            p.filepath =
                            (string)reader["filepath"];
                            p.filesize =
                            (long)reader["filesize"];
                            p.uploadby =
                            (string)reader["uploadedby"];
                            p.dbid  =
                            (int)reader["id"];
                            p.uploaddate =
                            (DateTime)reader["uploaddate"];
                         }
                    }
                }
            }
            return p;

        }

        public  List<FileObject> GetFiles(int folderid)
        {
            List<FileObject> p = new List<FileObject>();
            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {

                #region Get all files within requested folder
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =
                    "select * from tbl_FilesRepo wherer folderid = @folderid";
                    cmd.Parameters.AddWithValue("@folderid", folderid);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileObject tmp = new FileObject();

                            tmp.fileid = (string)reader["fileid"];
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
          
            return p;
        }

        public  bool StoreFile(string filename, string fileid, string filepath, long filesize, string uploadedby,int folderid,bool isFolder)
        {
            bool ret = true;
            try
            {
                using (SqlConnection conn =
                new SqlConnection(connectionString))
                {
                    string cmdStr = @"INSERT INTO tbl_FilesRepo(fileid,filepath,filename,filesize,uploadedby,folderid,filetypeid,parentid) VALUES(@fileid,@filepath,@filename,@filesize,@uploadedby,@folderid,@filetypeid,2136)";

                    using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@fileid",
                        fileid);
                        cmd.Parameters.AddWithValue("@filepath",
                        filepath);
                        cmd.Parameters.AddWithValue("@filename",
                        filename);
                        cmd.Parameters.AddWithValue("@filesize",
                        filesize);
                        cmd.Parameters.AddWithValue("@uploadedby",
                        uploadedby);
                        cmd.Parameters.AddWithValue("@folderid", folderid);
                        int filetype = 0;
                        if (isFolder)
                        {
                            filetype = 1007;
                        }
                        else { filetype = 1008; }
                        cmd.Parameters.AddWithValue("@filetypeid", filetype);
                        conn.Open();
                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            ret = false;
                        }
                        conn.Close();
                    }
                    return ret;
                }
            }
            catch (Exception e)
            {
                ret = false;
                string p = e.Message;
                return ret;
            }
        }

        public List<FolderObject> GetFolders(int id)
        {
            List<FolderObject> p = new List<FolderObject>();
            using (SqlConnection conn =
            new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =
                    "select * from folder_table where parent_folder = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FolderObject tmp = new FolderObject();

                            tmp.Folder_name = (string)reader["folder_name"];
                            tmp.Id =
                            (int)reader["id"];
                            tmp.Parent_folder =
                            (int)reader["parent_folder"];

                            p.Add(tmp);
                        }
                    }
                }
            }
            return p;
        }
        public FolderObject getFolder(int id)
        {
            FolderObject p = new FolderObject();
            using (SqlConnection conn =
      new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =
                    "select * from folder_table where id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p = new FolderObject();
                            p.Folder_name = (string)reader["folder_name"];
                            p.Id =
                            (int)reader["id"];
                            p.Parent_folder =
                            (int)reader["parent_folder"];


                        }
                    }
                }
            }
            return p;
        }
        public bool StoreFolder(string folder_name, int parent_id)
        {
            bool ret = true;
            try
            {
                using (SqlConnection conn =
                new SqlConnection(connectionString))
                {
                    string cmdStr = @"INSERT INTO folder_table(parent_folder,folder_name) VALUES(@parent,@foldername)";

                    using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@parent",
                        parent_id);
                        cmd.Parameters.AddWithValue("@foldername",
                        folder_name);
                        conn.Open();
                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            ret = false;
                        }
                        conn.Close();
                    }
                    return ret;
                }
            }
            catch (Exception e)
            {
                ret = false;
                string p = e.Message;
                return ret;
            }

        }
    }
}
