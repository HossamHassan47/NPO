using NPO.Code.Entity;
using NPO.Code.Repository;
using System;
using System.Collections.Generic;
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
            bool isValid = repository.IsValid(GetLoginEntity());
            if (isValid)
            {
                Response.Redirect(url: "ManageEmails");
            }
            else
            {


            }
        }
    }
}