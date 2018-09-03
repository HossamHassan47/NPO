using NPO.Code.Entity;
using NPO.Code.FilterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NPO.Code.Repository
{
    public class EmailRepository
    {

        #region ObjectDataSource
        public static DataTable GetEmails(EmailFilter filter, int maximumRows, int startRowIndex)
        {
            DataTable dataTable = new DataTable();
            //  var sql = "SELECT [Subject] ,[Body]  ,[From] ,[To] ,[CC] FROM [NPODB].[dbo].[Email] WHERE (1 = 1) ";
            var sql = @"
SELECT * 
from (
    SELECT ROW_NUMBER() OVER ( ORDER BY EmailId DESC ) ROWNUMBER,
            [Subject] ,
            [From] ,
            [To] ,
            [CC] , 
            DateTimeReceived 
    FROM [NPODB].[dbo].[Email]  
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

        public static int GetEmailsCount(EmailFilter filter)
        {
            int count;

            var sql = "SELECT COUNT(*) FROM Email where (1=1)" + getFilterString(filter);


            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand sqlCommand = new SqlCommand(sql , sqlConnection);
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
        private static string getFilterString(EmailFilter filter )
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

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString)) {
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


    }
}
