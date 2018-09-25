using Microsoft.Exchange.WebServices.Data;
using NPO.Code;
using NPO.Code.Entity;
using NPO.Code.FilterEntity;
using NPO.Code.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
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
            this.Form.DefaultButton = this.btnSearch.UniqueID;

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


      private void SendMailPassword(User user)
        {
            if (txtEmailAddressAdd.ToString().Trim() != string.Empty)
            {
                EntityEmail email = new EntityEmail();
                email.To = user.EmailAddress.Trim();
                email.Subject = "Welcome into NPO tool";
                email.Body = @"
                                Welcome " +
                              user.FullName + @"
                              , you have now new account in NPO tool <br/> EmailAddress :
                            " + user.NokiaUserName.Trim() + @"<br/> 
                            password :" + user.Password;
                MailHelper.SendMail(email);
               
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtid.Text);

            if (string.IsNullOrWhiteSpace(txtNameAdd.Text.ToString().Trim()))
            {
                lblCheckEmail.Text = "User must have Name";
                lblCheckEmail.ForeColor = Color.Red;
                lblCheckEmail.Font.Bold = true;
                lblCheckEmail.Visible = true;
                txtNameAdd.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNokiaNameAdd.Text.ToString().Trim()))
            {
                lblCheckEmail.Text = "User must have Nokia user name";
                lblCheckEmail.ForeColor = Color.Red;
                lblCheckEmail.Font.Bold = true;
                lblCheckEmail.Visible = true;
                txtNokiaNameAdd.Focus();
                return;
            }
            bool CheckMailVaild = new EmailAddressAttribute().IsValid(txtEmailAddressAdd.Text.ToString().Trim());
            
            if (!CheckMailVaild)
            {
                lblCheckEmail.Text = "Email Address isn't correct please confirm and try again";
                lblCheckEmail.ForeColor = Color.Red;
                lblCheckEmail.Font.Bold = true;
                lblCheckEmail.Visible = true;
                return;
            }
            
            UserRepository userRep = new UserRepository();

            int nokiaId = userRep.CheckNokiaUserName(txtNokiaNameAdd.Text.ToString().Trim());
            if (nokiaId != -1)
            {
                if (nokiaId != id) {
                    lblCheckEmail.Text = "Nokia user name must be unique ";
                    txtNokiaNameAdd.Focus();
                    lblCheckEmail.ForeColor = Color.Red;
                    lblCheckEmail.Font.Bold = true;
                    lblCheckEmail.Visible = true;
                    return;
                }
            }

            int emailAddId = userRep.CheckEmailAddress(txtEmailAddressAdd.Text.ToString().Trim());
            if (emailAddId != -1)
            {
                if (emailAddId != id)
                {
                    lblCheckEmail.Text = "Email address must be unique ";
                    txtEmailAddressAdd.Focus();
                    lblCheckEmail.ForeColor = Color.Red;
                    lblCheckEmail.Font.Bold = true;
                    lblCheckEmail.Visible = true;
                    return;
                }

            }

            User user = GetVaules();

            if (id < 0)
            {
                int idUser = userRep.InsertNewUser(user);
                if (idUser > 0)
                {
                    BindUsersGrid();
                    SendMailPassword(user);
                    lblCheckEmail.Text = "User add successfuly.";
                    lblCheckEmail.ForeColor = Color.Green;
                    lblCheckEmail.Font.Bold = true;
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
                    lblCheckEmail.Text = "User Updated successfuly.";
                    lblCheckEmail.ForeColor = Color.Green;
                    lblCheckEmail.Font.Bold = true;
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