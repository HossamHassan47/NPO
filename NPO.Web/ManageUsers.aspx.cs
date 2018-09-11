using Microsoft.Exchange.WebServices.Data;
using NPO.Code;
using NPO.Code.Entity;
using NPO.Code.FilterEntity;
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
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DoneOrNot.Text = "";
            if (!IsPostBack)
            {
                BindUsersGrid();

            }
        }


        private DataTable BindUsersGrid()
        {
            UserRepository userRep = new UserRepository();
            DataTable dataTable = userRep.GetUsers(GetFilter());
            gvUsers.DataSource = dataTable;
            gvUsers.DataBind();
            return dataTable;
        }

        private UserFilter GetFilter()
        {
            UserFilter entity = new UserFilter();
            entity.FullName = txtFullName.Text.ToString();
            entity.NokiaUserName = txtMemberNokiaUserName.Text.ToString();
            entity.EmailAddress = TxtEmailAddress.Text.ToString();
            if (!(isadmin.Checked))
            {
                entity.IsAdmin = false;

            }
            else
            {
                entity.IsAdmin = true;

            }
            if (!(isactive.Checked))
            {
                entity.IsActive = false;

            }
            else
            {
                entity.IsActive = true;

            }


            return entity;
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUsersGrid();
        }

        private User GetVaules()
        {
            UserRepository userReb = new UserRepository();
            User entity = new User();
            entity.FullName = txtNameAdd.Text.ToString();
            entity.NokiaUserName = txtNokiaNameAdd.Text.ToString();
            entity.EmailAddress = txtEmailAddressAdd.Text.ToString();
            entity.Password = userReb.GetPassword();
            if (!(CheckBoxIsAdmin.Checked))
            {
                entity.IsAdmin = false;

            }
            else
            {
                entity.IsAdmin = true;

            }


            return entity;
        }

        private User GetUpdateVaules(int id)
        {
            User entity = new User();
            entity.UserID = id;
            entity.FullName = txtNameAdd.Text.ToString();
            entity.NokiaUserName = txtNokiaNameAdd.Text.ToString();
            entity.EmailAddress = txtEmailAddressAdd.Text.ToString();
            //entity.Password = txtPasswordAdd.Text.ToString();

            if (!(CheckBoxIsAdmin.Checked))
            { entity.IsAdmin = false; }
            else { entity.IsAdmin = true; }
            if (!(CheckBoxIsActive.Checked))
            { entity.IsActive = false; }
            else { entity.IsActive = true; }
            return entity;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            setTextBoxesNull();
        }


      
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool s = new EmailAddressAttribute().IsValid(txtEmailAddressAdd.Text.ToString().Trim());
            if (!s)
            {
                lblCheckEmail.Text = "Email Address isn't correct please confirm and try again";
                return;
            }
            UserRepository userRep = new UserRepository();
            User user = GetVaules();
            if (txtEmailAddressAdd.ToString().Trim() != string.Empty)
            {
                EntityEmail email = new EntityEmail();
                email.To = txtEmailAddressAdd.Text.ToString().Trim();
                email.Subject = "Welcome into NPO tool";
                email.Body = @"
                                Welcome " +
                              user.FullName + @"
                              , you have now new account in NPO tool <br/> EmailAddress :
                            " + txtEmailAddressAdd.Text.ToString().Trim() + @"<br/> 
                            password :" + user.Password;
                bool sended = MailHelper.SendMail(email);
                if (!sended)
                {
                   lblCheckEmail.Text = "Check you internet connction";
                    return;
                }
            }
            int id = Convert.ToInt32(txtid.Text);
            if (id < 0)
            {
                int idUser = userRep.InsertNewUser(user);
                if (idUser > 0)
                {
                    BindUsersGrid();
                    lblCheckEmail.Text = "Done you can call user to check your Email";

                    setTextBoxesNull();
                }
                else
                {
                    setTextBoxesNull();

                }
            }
            else
            {

                bool idUser = userRep.UpdateUser(GetUpdateVaules(id));
                if (idUser)
                {
                    BindUsersGrid();
                    DoneOrNot.Text = "Done";
                    setTextBoxesNull();
                }
                else
                {
                    DoneOrNot.Text = "You have problem";
                    setTextBoxesNull();
                }

            }
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "edituser")
            {
                CheckBoxIsActive.Visible = true;
                labelUser.Text = "Update User";

                int id = Convert.ToInt32(e.CommandArgument);

                int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
                GridViewRow row = gvUsers.Rows[index];
                txtid.Text = id.ToString();
                txtNameAdd.Text = row.Cells[1].Text;
                txtNokiaNameAdd.Text = row.Cells[2].Text;
                txtEmailAddressAdd.Text = row.Cells[3].Text;

                DataTable dataTable = BindUsersGrid();
                //txtPasswordAdd.Text = dataTable.Rows[index][4].ToString();

                if (Convert.ToInt32(dataTable.Rows[index][5]) == 1)
                {
                    CheckBoxIsAdmin.Checked = true;
                }
                else
                {
                    CheckBoxIsAdmin.Checked = false;
                }

                if (Convert.ToInt32(dataTable.Rows[index][6]) == 1)
                {
                    CheckBoxIsActive.Checked = true;
                }
                else
                {
                    CheckBoxIsActive.Checked = false;

                }

                btnAdd_ModalPopupExtender.Show();

            }





            if (e.CommandName == "deleteuser")
            {
                UserRepository userRep = new UserRepository();
                int id = Convert.ToInt32(e.CommandArgument);
                if (id == 30)
                {
                    DoneOrNot.Text = "you cannot delete this user because he is tool admin ي بهايم يا بهايم  ";

                }
                else
                {
                    bool isUser = userRep.DeleteUser(GetUpdateVaules(id));
                    if (isUser)
                    {
                        BindUsersGrid();
                        DoneOrNot.Text = "Done";
                    }
                    else
                    {
                        BindUsersGrid();
                        DoneOrNot.Text = "You have problem";
                    }
                }

            }

        }

        private void setTextBoxesNull()
        {
            txtid.Text = "-1";
            txtNameAdd.Text = "";
            txtNokiaNameAdd.Text = "";
            txtEmailAddressAdd.Text = "";
            //txtPasswordAdd.Text = "";
        }
        // Fixed Colume Width  gridView
        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("7%");
                e.Row.Cells[1].Width = new Unit("20%");
                e.Row.Cells[2].Width = new Unit("30%");
                e.Row.Cells[3].Width = new Unit("20%");
                e.Row.Cells[2].Width = new Unit("20%");


            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            CheckBoxIsActive.Visible = false;
        }
    }
}