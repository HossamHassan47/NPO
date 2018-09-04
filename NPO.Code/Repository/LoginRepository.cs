using NPO.Code.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace NPO.Code.Repository
{
    public class LoginRepository
    {
        public bool IsValid(LoginEntity emailAndPassword)

        {
            using (SqlConnection conDatabase = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.[User] where  EmailAddress = @emailAdress and Password = @password;");
                cmd.Parameters.AddWithValue("@emailAdress",emailAndPassword.EmailAdress);
                cmd.Parameters.AddWithValue("@password",emailAndPassword.Password);
                cmd.Connection = conDatabase;
                conDatabase.Open();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conDatabase.Close();

                bool loginSuccessful = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));

                if (loginSuccessful)
                {

                    return true;
                }
                else
                {
                   // Console.WriteLine("Invalid Email Address or Password");
                    return false;
                }
            }
            
           
        }
    }
   
}
