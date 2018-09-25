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
            var sql = @"
SELECT * from (
    SELECT ROW_NUMBER() OVER ( ORDER BY dbo.Technology.TechnologyID, dbo.Controller.ControllerName  ) ROWNUMBER,
            [dbo].[Controller].[ControllerId],
            [dbo].[Controller].[ControllerName] ,
            [dbo].[Controller].[TechnologyId],
			dbo.Technology.TechnologyName,
            STUFF(( SELECT ', '+ dbo.[User].EmailAddress
                 FROM dbo.[User]
                 JOIN dbo.ControllerUser ON ControllerUser.UserId = dbo.[User].UserID
                 WHERE dbo.ControllerUser.ControllerId = dbo.Controller.ControllerId
                FOR XML PATH('')), 1, 1, '') AS 'Assigned_Users'
    FROM [dbo].[Controller]
left outer join dbo.Technology on [dbo].[Controller].TechnologyId = dbo.Technology.TechnologyID
	WHERE (1 = 1)  ";

            sql += getFilterString(filter);

         //   sql += " ORDER BY dbo.Technology.TechnologyID, dbo.Controller.ControllerName ";

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

            var sql = @"
SELECT COUNT(*) FROM [dbo].[Controller]
    left outer join dbo.Technology on [dbo].[Controller].TechnologyId = dbo.Technology.TechnologyId
where (1=1)" + getFilterString(filter);


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
                sql += " AND [Controller].[ControllerName] LIKE '%" + filter.ControllerName + "%'";
            }

            if (filter.TechnologyId > 0)
            {
                sql += "AND [Controller].[TechnologyId] = " + filter.TechnologyId;
            }

            return sql;
        }

        #endregion

     

        public int InsertNewController(Controller controller)
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
                outPutParameter.ParameterName = "@ControllerId";
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

        public DataTable GetUsers()
        {
            var sql = "SELECT UserID , FullName , EmailAddress FROM [dbo].[User] WHERE (1 = 1) ";

            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlAdapter.Fill(dataTable);
            }

            return dataTable;
        }

      

        public bool UpdateController(Controller controller)
        {
            SqlCommand cmd = new SqlCommand();


            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Controller_Update";
                cmd.Parameters.Add("@ControllerId", SqlDbType.Int).Value = controller.ControllerId;
                cmd.Parameters.Add("@ControllerName", SqlDbType.NVarChar).Value = controller.ControllerName;
                cmd.Parameters.Add("@TechnologyId", SqlDbType.Int).Value = controller.TechnologyId;
                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        public void DeleteUserController(int conUserId)
        {
            var sql = "  DELETE  FROM ControllerUser WHERE ID = @ID ";

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@ID", conUserId);
               
                sqlcomm.ExecuteNonQuery();

            }
        }

        //public DataTable GetUsersController(int controllerId)
        //{
        //    DataTable dataTable = new DataTable();
        //    string sql = @"
        //        select dbo.[User].UserID,
        //            dbo.[User].FullName,
        //         dbo.[User].EmailAddress
        //        from ControllerUser
        //        inner join dbo.[User] on dbo.[User].UserID = ControllerUser.UserId
        //        where ControllerUser.ControllerId = @controllerId";
        //    using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
        //    {
        //        sqlConnection.Open();
        //        SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
        //        sqlcomm.Parameters.AddWithValue("@controllerId", controllerId);
        //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlcomm))
        //        {
        //            sqlAdapter.Fill(dataTable);
        //        }
        //        return dataTable;
        //    }
        //}

        public void AddUserController(int userId, int controllerId)
        {
            var sql = "Insert into ControllerUser Values(@userId,@controllerId)";

                using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                    sqlcomm.Parameters.AddWithValue("@userId", userId);
                    sqlcomm.Parameters.AddWithValue("@controllerId", controllerId);

                    sqlcomm.ExecuteNonQuery();

                }
          
        }

        public bool DeleteController(Controller controller)
        {
            SqlCommand cmd = new SqlCommand();


            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Controller_Delete";
                cmd.Parameters.Add("@ControllerId", SqlDbType.NVarChar).Value = controller.ControllerId;

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        public DataTable GetControllerAssign(int techId)
        {
            DataTable dataTable = new DataTable();
            var sql = "SELECT [ControllerId] , [ControllerName] from Controller where [TechnologyId] = @techId ";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@techId", techId);
                using(SqlDataReader dr = sqlcomm.ExecuteReader())
                {
                    dataTable.Load(dr);

                }
            }

            return dataTable;
        }

        
        public List<Controller> GetAllControllers()
        {
            List<Controller> liController = new List<Controller>();
            var sql = "SELECT [ControllerId] , [ControllerName] from Controller";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                using (SqlDataReader reader = sqlcomm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Controller con = new Controller();
                        con.ControllerId = Convert.ToInt32(reader["ControllerId"]);
                        con.ControllerName = reader["ControllerName"].ToString();
                        liController.Add(con);
                    }
                    
                }
            }

            return liController;
        }

        public List<int> GetControllerIdsBySiteId(int siteId)
        {
            List<int> conId = new List<int>();
            var sql = @"Select ControlerId2g , ControlerId3g , ControlerId4g from [Site]  where [Site].SiteId = @siteId";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@siteId", siteId);
                using (SqlDataReader dr = sqlcomm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        conId.Add(Convert.ToInt32(dr["ControlerId2g"].ToString()));
                        conId.Add(Convert.ToInt32(dr["ControlerId3g"].ToString()));
                        conId.Add(Convert.ToInt32(dr["ControlerId4g"].ToString()));

                    }
                }
            }

            return conId;
        }

        public DataTable GetControllersForExcel(ControllerFilter filter)
        {
            DataTable dataTable = new DataTable();
            var sql = @"
    SELECT dbo.Technology.TechnologyName,
           dbo.Controller.ControllerName,
           STUFF(( SELECT ', '+ dbo.[User].EmailAddress
                 FROM dbo.[User]
                 JOIN dbo.ControllerUser ON ControllerUser.UserId = dbo.[User].UserID
                 WHERE dbo.ControllerUser.ControllerId = dbo.Controller.ControllerId
                FOR XML PATH('')), 1, 1, '') AS 'Assigned_Users'
 FROM dbo.Controller
 INNER JOIN dbo.Technology ON dbo.Controller.TechnologyId = dbo.Technology.TechnologyID
WHERE (1 = 1)  ";

            sql += getFilterString(filter);

            sql += " ORDER BY dbo.Technology.TechnologyID, dbo.Controller.ControllerName ";

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlAdapter.Fill(dataTable);
            }

            return dataTable;
        }

    }
}
