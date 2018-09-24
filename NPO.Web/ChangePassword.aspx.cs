using NPO.Code.Repository;
using System;


namespace NPO.Web
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDone.Focus();
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            UserRepository rep = new UserRepository();
            if (txtNewPassword.Value.ToString() == txtConPassword.Value.ToString())
            {
                bool change = rep.ChangePassword(txtOldPassword.Value.ToString(), txtNewPassword.Value.ToString(),Convert.ToInt32(Session["UserId"]));
                if (change)
                {
                    Session["CurrentUser"] = null;
                    Response.Redirect("CSLLogin.aspx");
                }else
                {
                    ErrorMsg.Text = "Password not correct";

                }
            }
            else
            {
                ErrorMsg.Text = "New password and confirm not same ";
            }
        }
    }
}