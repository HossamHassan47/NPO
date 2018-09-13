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
    public partial class ManageContoller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvControllers.DataBind();
            }
            
        }

        private ControllerFilter GetFilter()
        {
            ControllerFilter entity = new ControllerFilter();
            entity.ControllerName = txtControllerName.Text.ToString();
            entity.TechnologyId = int.Parse(DropDownListTech.SelectedValue);
            return entity;
        }

        protected void DsGvcon_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filter"] = GetFilter();
        }

        private Controller GetVaules()
        {
            Controller entity = new Controller();
            entity.ControllerName = txtConName.Text.ToString();
            entity.TechnologyId= Convert.ToInt32(ddlTechnology.SelectedValue);
            
           return entity;
        }


        private Controller GetUpdateVaules(int id)
        {
            Controller entity = new Controller();
            entity.ControllerId = id;
            entity.ControllerName = txtConName.Text.ToString();
            entity.TechnologyId = Convert.ToInt32(ddlTechnology.SelectedValue);
            return entity;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ControllerRepository controllerRep = new ControllerRepository();


            int id = Convert.ToInt32(hdnId.Value);

            if (id < 0)
            {
                int idController = controllerRep.InsertNewController(GetVaules());
                if (idController > 0)
                {
                    gvControllers.DataBind();
                    setTextBoxesNull();
                }
                else
                {
                    setTextBoxesNull();

                }
            }
            else
            {

                bool idController = controllerRep.UpdateController(GetUpdateVaules(id));

                if (idController)
                {
                    gvControllers.DataBind(); 
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            setTextBoxesNull();
        }


        protected void gvControllers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "editController")
            {
                
                labelController.Text = "Update Controller";

                int id = Convert.ToInt32(e.CommandArgument.ToString().Split(';')[0]);
                int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
                GridViewRow row = gvControllers.Rows[index];
                hdnId.Value = id.ToString();
                txtConName.Text = row.Cells[1].Text;
                ddlTechnology.SelectedValue = e.CommandArgument.ToString().Split(';')[1];
                
                btnAdd_ModalPopupExtender.Show();

            }

            if (e.CommandName == "deleteController")
            {
                ControllerRepository controllerRep = new ControllerRepository();
                int id = Convert.ToInt32(e.CommandArgument);
                bool isController = controllerRep.DeleteController(GetUpdateVaules(id));
                if (isController)
                {
                    gvControllers.DataBind();
                    DoneOrNot.Text = "Done";
                }
                else
                {
                    gvControllers.DataBind();
                    DoneOrNot.Text = "You have problem";
                }

            }
            if (e.CommandName == "UserAdd")
            {
                txtCotrollerId.Text = e.CommandArgument.ToString();

                BindDDlUser();
                BindRepeater();
                btnExAddUsers_ModalPopupExtender.Show();
            }
        }

        private void setTextBoxesNull()
        {
            hdnId.Value = "-1";
            txtConName.Text = "";
            ddlTechnology.Text = "";
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvControllers.DataBind();
        }

        // Fixed Colume Width  gridView
        protected void gvControllers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("10%");
                e.Row.Cells[1].Width = new Unit("20%");
            }
        }

        private void BindRepeater()
        {
            EmailRepository conUserReb = new EmailRepository();
            DataTable dataTable = conUserReb.GetControllerAssignUsers(Convert.ToInt32(txtCotrollerId.Text));
            RepeaterUsersControllers.DataSource = dataTable;
            RepeaterUsersControllers.DataBind();

        }
        private void BindDDlUser()
        {
            ControllerRepository users = new ControllerRepository();
            DataTable Users = users.GetUsers();

            ddlUsers.DataValueField = "UserID";
            ddlUsers.DataTextField = "FullName";
            ddlUsers.DataSource = Users;
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, "--Select User--");
        }
        protected void AddUserController_Click(object sender, EventArgs e)
        {
            ControllerRepository conUserReb = new ControllerRepository();
            conUserReb.AddUserController(Convert.ToInt32(ddlUsers.SelectedValue),Convert.ToInt32(txtCotrollerId.Text));
            BindRepeater();
        }

        protected void DeleteUserCon_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            int conUserId = Convert.ToInt32(btn.CommandArgument);
            ControllerRepository conUserReb = new ControllerRepository();
            conUserReb.DeleteUserController(conUserId);
            BindRepeater();
        }
    }
}