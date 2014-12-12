using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OSC.ProjectManagement.BusinessObjects
{
    /// <summary>
    /// 
    /// </summary>
    public static class SQLDataHelper
    {
        public static Object ExecuteScalarQuery(string ConnectionString, string command, Dictionary<string, object> parameters)
        {
            Object result = new Object();
            try
            {
                string connectionName = Convert.ToString(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);

                SqlConnection conn = new SqlConnection(connectionName);

                conn.Open();

                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.CommandType = CommandType.Text;

                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, Convert.ChangeType(parameter.Value, parameter.Value.GetType()));
                }

                result = cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="paramters"></param>
        public static void ExecuteNonQuery(string ConnectionString, string command, Dictionary<string, object> parameters)
        {
            DataTable results = new DataTable("Results");
            try
            {
                string connectionName = Convert.ToString(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);

                SqlConnection conn = new SqlConnection(connectionName);

                conn.Open();

                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.CommandType = CommandType.Text;

                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, Convert.ChangeType(parameter.Value, parameter.Value.GetType()));
                }

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="Query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataQuery(string ConnectionString, string Query, Dictionary<string, object> parameters)
        {
            DataTable results = new DataTable("Results");
            try
            {
                string connectionName = Convert.ToString(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);

                SqlConnection conn = new SqlConnection(connectionName);

                conn.Open();

                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.CommandType = CommandType.Text;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="ProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataStoredProcedure(string ConnectionString, string ProcedureName, Dictionary<string, object> parameters)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="ProcedureName"></param>
        /// <param name="parameters"></param>
        public static void ExecuteNonStoredProcedure(string ConnectionString, string ProcedureName, Dictionary<string, object> parameters)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="ProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteScalarStoredProc(string ConnectionString, string ProcedureName, Dictionary<string, object> parameters)
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
    }
}
