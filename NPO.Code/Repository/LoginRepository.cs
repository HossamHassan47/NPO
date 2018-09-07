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
        public DataTable IsValid(LoginEntity emailAndPassword)

        {
            using (SqlConnection conDatabase = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.[User] where  EmailAddress = @emailAdress and Password = @password;");
                cmd.Parameters.AddWithValue("@emailAdress",emailAndPassword.EmailAdress);
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
    }
   
}
