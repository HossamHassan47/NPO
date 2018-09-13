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
    SELECT ROW_NUMBER() OVER ( ORDER BY ControllerId  ) ROWNUMBER,
            [dbo].[Controller].[ControllerId],
            [dbo].[Controller].[ControllerName] ,
            [dbo].[Controller].[TechnologyId],
			dbo.Technology.TechnologyName
    FROM [dbo].[Controller]
    inner join dbo.Technology on [dbo].[Controller].TechnologyId = dbo.Technology.TechnologyId
	WHERE (1 = 1)  ";

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

            var sql = @"
SELECT COUNT(*) FROM [dbo].[Controller]
    inner join dbo.Technology on [dbo].[Controller].TechnologyId = dbo.Technology.TechnologyId
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

        public List<Controller> GetAllControllers()
        {
            return new List<Controller>();
        }

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
            var sql = "SELECT UserID , FullName , EmailAddress FROM [NPODB].[dbo].[User] WHERE (1 = 1) ";

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



    }
}
