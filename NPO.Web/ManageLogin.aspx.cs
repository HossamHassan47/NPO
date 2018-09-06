using NPO.Code.Entity;
using NPO.Code.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NPO.Web
{
    public partial class ManageLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private LoginEntity GetLoginEntity()
        {
            LoginEntity entity = new LoginEntity();
            entity.EmailAdress = txtEmailAddress.Value.ToString();
            entity.Password = txtPassword.Value.ToString();

            return entity;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginRepository repository = new LoginRepository();
            DataTable dataTable = repository.IsValid(GetLoginEntity());
            if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0][6])==1)
            {
                Session["CurrentUser"] = dataTable.Rows[0];
                Response.Redirect(url: "ManageEmails");
            }
            else
            {


            }
        }
    }
}