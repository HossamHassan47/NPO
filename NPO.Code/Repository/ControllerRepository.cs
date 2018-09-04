using NPO.Code.Entity;
using NPO.Code.FilterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace NPO.Code.Repository
{
    public class ControllerRepository
    {
        #region ObjectDataSource
        public static DataTable GetControllers(ControllerFilter filter, int maximumRows, int startRowIndex)
        {
            DataTable dataTable = new DataTable();
         var sql = "SELECT * from (SELECT ROW_NUMBER() OVER ( ORDER BY ControllerId  ) ROWNUMBER,[ControllerName] ,[TechnologyId] FROM [NPODB].[dbo].[Controller]  WHERE (1 = 1)  ";

            sql += getFilterString(filter);

            sql += ") As query where  query.ROWNUMBER > " + startRowIndex + " AND query.ROWNUMBER <= " + (startRowIndex + maximumRows);

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlAdapter.Fill(dataTable);

            }

            return dataTable;
        }

        public static int GetControllersCount(ControllerFilter filter)
        {
            int count;

            var sql = "SELECT COUNT(*) FROM Controller where (1=1)" + getFilterString(filter);


            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                try
                {
                    sqlConnection.Open();
                    count = (int)sqlCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return count;
        }

        private static string getFilterString(ControllerFilter filter)
        {
            string sql = "";

            if (!string.IsNullOrEmpty(filter.ControllerName))
            {
                sql += " AND [ControllerName] LIKE '%" + filter.ControllerName + "%'";
            }
            if (!string.IsNullOrEmpty(filter.TechnologyId))
            {
                if (string.Equals(filter.TechnologyId,"2G"))
                {
                    sql += "AND TechnologyId = 1";

                }else if(string.Equals(filter.TechnologyId, "3G"))
                {
                    sql += "AND TechnologyId = 2";
                }
                else
                {
                    sql += "AND TechnologyId = 3";
                }
            }
            
            return sql;
        }
        
        #endregion

        public List<Controller> GetAllControllers()
        {
            return new List<Controller>();
        }

        public int InsertNewController (Controller controller)
        {
            SqlCommand cmd = new SqlCommand();
            int ControllerId = 0;
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_Controller_Insert";
                cmd.Parameters.Add("@ControllerName", SqlDbType.NVarChar).Value = controller.ControllerName;
                cmd.Parameters.Add("@TechnologyId", SqlDbType.NVarChar).Value = controller.TechnologyId;
            
            //Add the output parameter to the command object
            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@ControlerId";
            outPutParameter.SqlDbType = SqlDbType.Int;
            outPutParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutParameter);

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    ControllerId = int.Parse(outPutParameter.Value.ToString());
                    cmd.Parameters.Clear();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            // return the inserted Controller Id
            return ControllerId;
        }



    }
}
