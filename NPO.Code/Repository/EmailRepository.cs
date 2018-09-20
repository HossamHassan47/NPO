using NPO.Code.Entity;
using NPO.Code.FilterEntity;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NPO.Code.Repository
{
    public class EmailRepository
    {

        #region ObjectDataSource
        public static DataTable GetEmails(EmailFilter filter, int maximumRows, int startRowIndex, int userId, bool isAdmin)
        {
            DataTable dataTable = new DataTable();
            //  var sql = "SELECT [Subject] ,[Body]  ,[From] ,[To] ,[CC] FROM [NPODB].[dbo].[Email] WHERE (1 = 1) ";
            var sql = @"
SELECT * 
from (
    SELECT ROW_NUMBER() OVER ( ORDER BY Email.EmailId DESC ) ROWNUMBER,
            Email.EmailId,
            Email.[Subject] ,
            Email.[From] ,
            Email.[To] ,
            Email.[CC] , 
            Email.DateTimeReceived ,
            IsAssign,
            EmailRef,
            EmailStatus,
            EmailStatus.Status
            
    FROM [NPODB].[dbo].[Email] ";
            if (!isAdmin)

            {
                sql += @" 
                    left outer join dbo.[EmailController] on [Email].[EmailId] = EmailController.[EmailId]
                    left outer join dbo.[ControllerUser] on [ControllerUser].[ControllerId] = EmailController.[ControllerId]
                    left outer join dbo.[User] on [User].[UserID] = ControllerUser.[UserId]
                    left outer join EmailStatus on EmailStatus.ID = Email.EmailStatus
                    inner join EmailUser on Email.EmailId = EmailUser.EmailId;

                WHERE  ParentEmailId IS NULL  AND  [User].[UserID] = " + userId;
            }else
            {
                sql += @"
                    left outer join EmailStatus on EmailStatus.ID = Email.EmailStatus
                WHERE ParentEmailId IS NULL";
            }

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

      

        public static int GetEmailsCount(EmailFilter filter, int userId, bool isAdmin)
        {
            int count;

            var sql = @"
                SELECT COUNT(*) 
                FROM Email ";
            if (!isAdmin)

            {
                sql += @"
                    left outer join dbo.[EmailController] on [Email].[EmailId] = EmailController.[EmailId]
                    left outer join dbo.[ControllerUser] on [ControllerUser].[ControllerId] = EmailController.[ControllerId]
                    left outer join dbo.[User] on [User].[UserID] = ControllerUser.[UserId]
                 WHERE ParentEmailId IS NULL AND  [User].[UserID] = " + userId;
            }else
            {
                sql += @" WHERE ParentEmailId IS NULL ";
            }
            sql += getFilterString(filter);

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



        private static string getFilterString(EmailFilter filter)
        {
            string sql = "";

            if (!string.IsNullOrEmpty(filter.To))
            {
                sql += " AND [To] LIKE '%" + filter.To + "%'";
            }
            if (!string.IsNullOrEmpty(filter.From))
            {
                sql += " AND [From] LIKE '%" + filter.From + "%'";
            }
            if (!string.IsNullOrEmpty(filter.Subject))
            {
                sql += " AND Subject LIKE '%" + filter.Subject + "%'";
            }
            if (!string.IsNullOrEmpty(filter.EmailRef))
            {
                sql += " AND EmailRef LIKE '%" + filter.EmailRef + "%'";
            }
            if (filter.Status != -1)
            {
                sql += " AND EmailStatus = " + filter.Status ;
            }

            if (!string.IsNullOrEmpty(filter.dateTimeReceived))
            {

                sql += " AND Cast(DateTimeReceived AS DATE)" + filter.dateTimeReceivedOperater + " '" + filter.dateTimeReceived + " '";


            }
            return sql;
        }

       




        #endregion

        public List<Email> GetAllEmails()
        {
            return new List<Email>();
        }

        public int InsertNewEmail(Email email)
        {
            SqlCommand cmd = new SqlCommand();
            int EmailId = 0;

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Email_Insert";
                cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = email.Subject;
                cmd.Parameters.Add("@Body", SqlDbType.NVarChar).Value = email.Body;
                cmd.Parameters.Add("@From", SqlDbType.NVarChar).Value = email.From;
                cmd.Parameters.Add("@To", SqlDbType.NVarChar).Value = email.To;
                cmd.Parameters.Add("@CC", SqlDbType.NVarChar).Value = email.CC;
                cmd.Parameters.Add("@Path", SqlDbType.NVarChar).Value = email.Path;
                cmd.Parameters.Add("@DateTimeReceived", SqlDbType.NVarChar).Value = email.DateTimeReceived;
                cmd.Parameters.Add("@HtmlBody", SqlDbType.NVarChar).Value = email.BodyHtml;

                // cmd.Parameters.Add("@TicketID", SqlDbType.Int).Value = email.TicketId;
                //  cmd.Parameters.Add("@IsMain", SqlDbType.Bit).Value = email.IsMain;
                SqlParameter outPutParameter = new SqlParameter();
                outPutParameter.ParameterName = "@EmailId";
                outPutParameter.SqlDbType = SqlDbType.Int;
                outPutParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outPutParameter);
                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    EmailId = int.Parse(outPutParameter.Value.ToString());
                    cmd.Parameters.Clear();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            // Insert email into database then return the ID

            return EmailId;

        }

        public int InsertNewEmailHavePerant(Email email , int emailParentId)
        {
            SqlCommand cmd = new SqlCommand();
            int EmailId = 0;

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Email_Insert";
                cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = email.Subject;
                cmd.Parameters.Add("@Body", SqlDbType.NVarChar).Value = email.Body;
                cmd.Parameters.Add("@From", SqlDbType.NVarChar).Value = email.From;
                cmd.Parameters.Add("@To", SqlDbType.NVarChar).Value = email.To;
                cmd.Parameters.Add("@CC", SqlDbType.NVarChar).Value = email.CC;
                cmd.Parameters.Add("@Path", SqlDbType.NVarChar).Value = email.Path;
                cmd.Parameters.Add("@DateTimeReceived", SqlDbType.NVarChar).Value = email.DateTimeReceived;
                cmd.Parameters.Add("@HtmlBody", SqlDbType.NVarChar).Value = email.BodyHtml;
                cmd.Parameters.Add("@ParentEmailId", SqlDbType.Int).Value = emailParentId;

            
                SqlParameter outPutParameter = new SqlParameter();
                outPutParameter.ParameterName = "@EmailId";
                outPutParameter.SqlDbType = SqlDbType.Int;
                outPutParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outPutParameter);
                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    EmailId = int.Parse(outPutParameter.Value.ToString());
                    cmd.Parameters.Clear();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            // Insert email into database then return the ID

            return EmailId;

        }

        public int UpdateEmail(Email email)
        {
            // Update email then return the ID
            return 0;
        }

        public bool DeleteEmail(Email email)
        {
            // Delete email
            return true;
        }

        public string GetHtmlBody(int id)
        {
            string sql = "select [HtmlBody] from Email where EmailId = @id";
            string htmlBody = string.Empty;
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@id", id);

                using (SqlDataReader sqldr = sqlcomm.ExecuteReader())
                {
                    if (sqldr.Read())
                    {
                        htmlBody = sqldr["HtmlBody"].ToString();
                    }
                }
            }

            return htmlBody;

        }

        public void DownloadMail(int id)
        {
            string sql = "select [Path] from Email where EmailId = @id";
            string path = string.Empty;
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@id", id);

                using (SqlDataReader sqldr = sqlcomm.ExecuteReader())
                {
                    if (sqldr.Read())
                    {
                        path = sqldr["Path"].ToString();
                    }
                }
            }


        }

        public void ChangeStatusInprogress(int emailId)
        {
            string sql = "Update Email set EmailStatus = 2 where EmailId = " + emailId ;
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.ExecuteNonQuery();
       
            }

        }

        #region Assign
        public DataTable GetControllerAssignUsers(int controllerId)
        {
            DataTable dataTable = new DataTable();
            string sql = @"
                select dbo.[User].UserID,
                    dbo.[User].FullName,
	                dbo.[User].EmailAddress,
                    ID
                from ControllerUser
                inner join dbo.[User] on dbo.[User].UserID = ControllerUser.UserId
                where ControllerUser.ControllerId = @controllerId";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@controllerId", controllerId);
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlcomm))
                {
                    sqlAdapter.Fill(dataTable);
                }
                return dataTable;
            }

        }

        public bool AddControllerId(int controllerId, int emailId)
        {
           
            string sql = @"
                 Insert into EmailController values (@EmailId, @ControllerId)";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@controllerId", controllerId);
                sqlcomm.Parameters.AddWithValue("@EmailId", emailId);
                sqlcomm.ExecuteNonQuery();

            }

            return true;
        }


        public void UpdateStutes(string emailRef , int emailId)
        {
            string sql = @"
                 Update Email Set EmailRef = @emailRef , EmailStatus = 1, IsAssign = 1  where EmailId = @emailId ";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@emailRef", emailRef);
                sqlcomm.Parameters.AddWithValue("@emailId", emailId);


                sqlcomm.ExecuteNonQuery();

            }

        }
        #endregion

        #region ParentEmails
        public int GetParentEmailId(string emailRef)
        {

            int emailId;
            var sql = @"
                SELECT EmailId
            FROM [NPODB].[dbo].[Email] where EmailRef =@emailRef ";
          

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql,sqlConnection);
                sqlcomm.Parameters.AddWithValue("@EmailRef" , emailRef);

                using(SqlDataReader dr = sqlcomm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        emailId = Convert.ToInt32(dr["EmailId"]);
                        return emailId;
                    }
                }
            }

            return -1;
        }

        public List<string> GetAllSubjects()
        {
            List<string> subjects = new List<string>();

            var sql = "Select Subject from Email where EmailStatus <> 'closed' " ;
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                using (SqlDataReader reader = sqlcomm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string sub = reader["Subject"].ToString();
                        subjects.Add(sub);
                    }

                }
            }

            return subjects;

        }
        public int GetParentEmailIdsub(string subject)
        {
            int emailId;
            var sql = @"
                SELECT EmailId
            FROM [NPODB].[dbo].[Email] where Subject =@sub ";


            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@sub", subject);

                using (SqlDataReader dr = sqlcomm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        emailId = Convert.ToInt32(dr["EmailId"]);
                        return emailId;
                    }
                }
            }

            return -1;

            return -1;
        }

        public DataTable GetChildrenEmail(int emailParentId)
        {
            DataTable dataTable = new DataTable();
            var sql = "Select * from Email Where ParentEmailId = @emailParentId ";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@emailParentId", emailParentId);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlcomm);
                sqlAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        #endregion

        public void UpdateStutesClosed(int emailId)
        {
            string sql = "Update Email set EmailStatus = 3 where EmailId = " + emailId;
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.ExecuteNonQuery();

            }

        }



        public DataTable GetEmailAssignUsers(int EmailId)
        {
            DataTable dataTable = new DataTable();
            string sql = @"
                select dbo.[User].UserID,
                    dbo.[User].FullName,
	                dbo.[User].EmailAddress,
                    EmailUserId
                from EmailUser
                left outer join dbo.[User] on dbo.[User].UserID = EmailUser.UserId
                where EmailUser.EmailId = @EmailId";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@EmailId", EmailId);
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlcomm))
                {
                    sqlAdapter.Fill(dataTable);
                }
                return dataTable;
            }

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

        public void AddUserEmail(int UserId, int EmailId)
        {
            var sql = "Insert into EmailUser Values(@EmailId,@UserId)";

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@UserId", UserId);
                sqlcomm.Parameters.AddWithValue("@EmailId", EmailId);

                sqlcomm.ExecuteNonQuery();

            }

        }
        public void DeleteUserEmail(int EmailUserId)
        {
            var sql = "  DELETE  FROM EmailUser WHERE EmailUserId = @EmailUserId ";

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@EmailUserId", EmailUserId);

                sqlcomm.ExecuteNonQuery();

            }
        }
        /*********************************************************/
        public DataTable GetEmailAssignController(int EmailId)
        {
            DataTable dataTable = new DataTable();
            string sql = @"
                select dbo.[Controller].ControllerId,
                    dbo.[Controller].ControllerName,
                    EmailController.EmailControllerId
                from Controller
                left outer join dbo.EmailController on dbo.[Controller].ControllerId = EmailController.ControllerId
                where EmailController.EmailId = @EmailId";
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@EmailId", EmailId);
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlcomm))
                {
                    sqlAdapter.Fill(dataTable);
                }
                return dataTable;
            }

        }
        public DataTable GetControllers()
        {
            var sql = "SELECT ControllerId , ControllerName FROM [NPODB].[dbo].[Controller] WHERE (1 = 1) ";

            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlAdapter.Fill(dataTable);
            }

            return dataTable;
        }
        public void AddEmailController(int ControllerId, int EmailId)
        {
            var sql = "Insert into EmailController Values(@EmailId,@ControllerId)";

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@ControllerId", ControllerId);
                sqlcomm.Parameters.AddWithValue("@EmailId", EmailId);

                sqlcomm.ExecuteNonQuery();

            }

        }

        public void DeleteEmailController(int EmailConId)
        {
            var sql = "  DELETE  FROM EmailController WHERE EmailControllerId = @EmailControllerId ";

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                sqlcomm.Parameters.AddWithValue("@EmailControllerId", EmailConId);

                sqlcomm.ExecuteNonQuery();

            }
        }

    }
}
