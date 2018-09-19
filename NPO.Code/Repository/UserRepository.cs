using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPO.Code.FilterEntity;
using NPO.Code.Repository;
using System.Data;
using System.Data.SqlClient;
using NPO.Code.Entity;

namespace NPO.Code.Repository
{
    public class UserRepository
    {
        public List<User> GetAllUsers()
        {
            return new List<User>();
        }

        public DataTable GetUsers(UserFilter filter)
        {
            DataTable dataTable = new DataTable();
            var sql = getSelectStatment(filter);
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlAdapter.Fill(dataTable);
            }

            return dataTable;
        }

       

        private string getSelectStatment(UserFilter filter)
        {
            var Sql = "SELECT * FROM [NPODB].[dbo].[User] WHERE (1 = 1) ";

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                Sql += " AND [FullName] LIKE '%" + filter.FullName + "%'";
            }
            if (!string.IsNullOrEmpty(filter.NokiaUserName))
            {
                Sql += " AND [NokiaUserName] LIKE '%" + filter.NokiaUserName + "%'";
            }
            if (!string.IsNullOrEmpty(filter.EmailAddress))
            {
                Sql += " AND EmailAddress LIKE '%" + filter.EmailAddress + "%'";
            }
            if (filter.IsAdmin)
            {
                Sql += " AND IsAdmin = 1 ";
            }
            if (filter.IsActive)
            {
                Sql += " AND IsActive = 1  ";
            }



            return Sql;

        }


        public int InsertNewUser(User user)
        {
            SqlCommand cmd = new SqlCommand();
            int UserID = 0;

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_User_Insert";
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.FullName;
                cmd.Parameters.Add("@NokiaUserName", SqlDbType.NVarChar).Value = user.NokiaUserName;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = user.EmailAddress;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                cmd.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = user.IsAdmin;


                SqlParameter outPutParameter = new SqlParameter();
                outPutParameter.ParameterName = "@UserID";
                outPutParameter.SqlDbType = SqlDbType.Int;
                outPutParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outPutParameter);
                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    UserID = int.Parse(outPutParameter.Value.ToString());
                    cmd.Parameters.Clear();
                    return UserID;
                }
                catch
                {
                    return -1;
                }
            }


        }

        public string GetPassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            int length = 7;
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


        public bool UpdateUser(User user)
        {
            SqlCommand cmd = new SqlCommand();


            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_User_Update";
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = user.UserID;
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.FullName;
                cmd.Parameters.Add("@NokiaUserName", SqlDbType.NVarChar).Value = user.NokiaUserName;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = user.EmailAddress;
                //cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                cmd.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = user.IsAdmin;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
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
        public bool DeleteUser(User user)
        {
            SqlCommand cmd = new SqlCommand();


            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_User_Delete";
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = user.UserID;

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
        public bool ChangePassword(string oldPassword, string newPassword,int userID)
        {
            int userId = CheckoldPassword(oldPassword, userID);
            if (userId != -1 )
            {
                var sql = "Update [User] Set Password = '" + newPassword + "' Where UserID = " + userId; 
                using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
                {
                    SqlCommand sqlcomm = new SqlCommand(sql, con);
                    con.Open();
                    sqlcomm.ExecuteNonQuery();
                }
                return true;

            }

            return false;
        }

        private int CheckoldPassword(string oldPassword , int userID)
        {
            var sql = "Select UserID from [User] where Password = '" + oldPassword + "' AND UserID = "+ userID ;
            int emailId = -1; 
            using(SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand sqlcomm = new SqlCommand(sql,con);
                con.Open();
                using (SqlDataReader dr = sqlcomm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        emailId = Convert.ToInt32(dr["UserID"]);
                        return emailId; 
                    }
                }
            }
            return -1;
        }
    }
}
