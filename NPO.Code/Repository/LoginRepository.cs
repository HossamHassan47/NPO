using NPO.Code.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using Microsoft.Exchange.WebServices.Data;
using System.Net;

namespace NPO.Code.Repository
{
    public class LoginRepository
    {
        public DataTable IsValid(LoginEntity emailAndPassword)

        {
            using (SqlConnection conDatabase = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.[User] where  NokiaUserName = @emailAdress and Password = @password;");
                cmd.Parameters.AddWithValue("@emailAdress",emailAndPassword.UserName);
                cmd.Parameters.AddWithValue("@password",emailAndPassword.Password);
                cmd.Connection = conDatabase;
                conDatabase.Open();

                DataTable dataTable = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                conDatabase.Close();


                return dataTable;
            }
            
           
        }

        public int SendEmail(string NokiaUserName)

        {
            ExchangeService _service = MailHelper.Exchange_Service();

            

            string email = string.Empty;
            string password = string.Empty;
            
            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT EmailAddress, [Password] , NokiaUserName FROM [User] WHERE NokiaUserName = @NokiaUserName"))
                {
                    cmd.Parameters.AddWithValue("@NokiaUserName", NokiaUserName.Trim());
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            email = sdr["EmailAddress"].ToString();
                            password = sdr["Password"].ToString();
                        }
                    }
                    con.Close();
                }
            }
            if (!string.IsNullOrEmpty(password))
            {
                EntityEmail sendemail = new EntityEmail();
                sendemail.To = email.Trim();
                sendemail.Subject = "Password Recovery";
                sendemail.Body = string.Format("Hi {0},<br /><br />Your password is {1}.<br /><br />Thank You.", NokiaUserName, password);
                bool check =MailHelper.SendMail(sendemail);
                if (check)
                {
                    return 1;
                }else
                {
                    return 2;
                }
                
            }
            return 2;
        }
    }
   
}
