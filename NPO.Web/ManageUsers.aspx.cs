using NPO.Code.Entity;
using NPO.Code.FilterEntity;
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
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsersGrid();
            }
        }
        private void BindUsersGrid()
        {
            UserRepository userRep = new UserRepository();
            DataTable dataTable = userRep.GetUsers(GetFilter());

            gvUsers.DataSource = dataTable;
            gvUsers.DataBind();

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

        private User GetVaulesAddUser()
        {
            User entity = new User();
            entity.FullName = txtNameAdd.Text.ToString();
            entity.NokiaUserName = txtNokiaNameAdd.Text.ToString();
            entity.EmailAddress = txtEmailAddressAdd.Text.ToString();
            entity.Password = txtPasswordAdd.Text.ToString();
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserRepository userRep = new UserRepository();
            int idUser = userRep.InsertNewUser(GetVaulesAddUser());
            if(idUser > 0)
            {
                BindUsersGrid();
            }
            else
            {

            }
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edituser")
            {
               
            }
        }
    }
}