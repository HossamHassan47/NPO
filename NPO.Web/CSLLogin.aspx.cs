
using NPO.Code.Entity;
using NPO.Code.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            entity.UserName = txtEmailAddress.Value.ToString();
            entity.Password = txtPassword.Value.ToString();

            return entity;
        }

        private void MakeSession(User user)
        {
            Session["CurrentUser"] = user;
            Session["UserId"] = user.UserID;
            Session["isAdmin"] = user.IsAdmin;
            Session["UserName"] = user.FullName;
        }

        private User getUser(DataTable dataTable)
        {
            User user = new User();
            user.UserID = Convert.ToInt32(dataTable.Rows[0][0]);
            user.FullName = dataTable.Rows[0][1].ToString();
            user.NokiaUserName = dataTable.Rows[0][2].ToString();
            user.EmailAddress = dataTable.Rows[0][3].ToString();
            user.Password = dataTable.Rows[0][4].ToString();
            if (Convert.ToInt32(dataTable.Rows[0][5])== 1)
            {
                user.IsAdmin = true;
            }else
            {
                user.IsAdmin = false;
            }
            if (Convert.ToInt32(dataTable.Rows[0][6]) == 1)
            {
                user.IsActive = true;
            }
            else
            {
                user.IsActive = false;
            }
            return user;
            
        }

        protected void SendMail_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(txtEmail.Text.ToString().Trim()))
            {
                LabelErrorMsg.Text = "Please enter your nokia user name";
                LabelErrorMsg.Visible = true;
                txtEmail.Focus();
                return;
            }

            LoginRepository login = new LoginRepository();
            int sended =login.SendEmail(txtEmail.Text.ToString());

            System.Threading.Thread.Sleep(1000);

            if (sended == 1)
            {
                LabelErrorMsg.Text = "Done you can go to your mail to get your password";

                LabelErrorMsg.Visible = true;
               

            }
            else 
            {
                LabelErrorMsg.Text = "User name not exist or Internet connction Error";
                LabelErrorMsg.Visible = true;

            }
          

        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmailAddress.Value.ToString().Trim())) 
            {
                ErrorMsg.Text = "Please enter your Nokia user name";
                ErrorMsg.Visible = true;
                txtEmailAddress.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtPassword.Value.ToString().Trim()))
            {
                ErrorMsg.Text = "Please enter your password";
                ErrorMsg.Visible = true;
                txtPassword.Focus();
                return;
            }

            LoginRepository repository = new LoginRepository();
            DataTable dataTable = repository.IsValid(GetLoginEntity());


            if (dataTable.Rows.Count > 0)
            {
                User user = getUser(dataTable);
                if (user.IsActive)
                {
                    MakeSession(user);
                    Response.Redirect(url: "ManageEmails");
                }
                else
                {
                    ErrorMsg.Text = "This User Don't Active Please Call Your Admin";
                    ErrorMsg.Visible = true;
                }
            }
            else
            {
                ErrorMsg.Text = "Your username or your password isn't correct please try again later";
                ErrorMsg.Visible = true;

            }
        }

        protected void Cancel_Click(object sender, ImageClickEventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }
    }
           
 }
