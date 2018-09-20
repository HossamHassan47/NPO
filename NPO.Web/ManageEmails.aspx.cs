using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using NPO.Code;
using System.Data;
using NPO.Code.FilterEntity;
using NPO.Code.Repository;


namespace NPO.Web
{
    public partial class ManageEmails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isAdmin"] != null)
            {
                if ((bool)Session["isAdmin"])
                {
                    gvEmails.Columns[2].Visible = false;
                }
                else
                {
                    gvEmails.Columns[0].Visible = false;
                    gvEmails.Columns[1].Visible = false;
                }
            }
            if (!IsPostBack)
            {
                gvEmails.DataBind();
            }
        }

        private EmailFilter GetFilter()
        {

            EmailFilter entity = new EmailFilter();
            entity.From = txtEmailFrom.Text.ToString();
            entity.To = txtEmailTo.Text.ToString();
            entity.Subject = txtEmailSub.Text.ToString();
            entity.dateTimeReceived = txtEmailDate.Text.ToString();
            entity.EmailRef = txtEmailRef.Text.ToString();
            if (ddlAssign.SelectedValue == "-1")
            {
                entity.Status = -1;
            }
            else
            {
                entity.Status = int.Parse(ddlAssign.SelectedValue);
            }
            entity.dateTimeReceivedOperater = dateDropDownList.Text.ToString();

            return entity;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvEmails.DataBind();

        }

        protected void gvEmail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "showBody")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                EmailRepository htmlBody = new EmailRepository();

                Literal ltrDyn = new Literal();
                ltrDyn.Text = htmlBody.GetHtmlBody(id);
                PanalBody.Controls.Add(ltrDyn);
                btnExtendBody_ModalPopupExtender.Show();
            }
            if (e.CommandName == "download")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                EmailRepository downloadMail = new EmailRepository();
                downloadMail.DownloadMail(id);
            }
            if (e.CommandName == "UserAdd")
            {
                txtEId.Text = e.CommandArgument.ToString();

                BindDDlUser();
                BindRepeater();
                btnExAddUsers_ModalPopupExtender.Show();
            }
            if (e.CommandName == "Assign")
            {
                if ((bool)Session["isAdmin"])
                {
                    // 1212,32,1
                    var values = e.CommandArgument.ToString().Split(',');

                    txtEmailId.Text = Convert.ToInt32(values[0]).ToString();
                    BindDDlController();
                    BindControllerRepeater();
                    if (values[1] == "1")
                    {
                    }
                    else
                    {

                    }

                    btnPanalAssign_ModalPopupExtender.Show();
                }
            }
            if (e.CommandName == "changestatus")
            {
                var arg = e.CommandArgument.ToString().Split(',');
                if (!(bool)Session["isAdmin"])
                {
                    if (!(arg[1] == "3"))
                    {
                        int emailId = Convert.ToInt32(arg[0]);
                        EmailRepository changeStatus = new EmailRepository();
                        changeStatus.ChangeStatusInprogress(emailId);
                        gvEmails.DataBind();
                    }
                }
            }

        }

        protected void DsGvEmails_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filter"] = GetFilter();
            e.InputParameters["userId"] = Convert.ToInt32(Session["UserId"]);
            e.InputParameters["isAdmin"] = Session["isAdmin"];

        }

        protected void gvEmails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("3%");
                e.Row.Cells[1].Width = new Unit("3%");
                e.Row.Cells[2].Width = new Unit("5%");
                e.Row.Cells[3].Width = new Unit("10%");
                e.Row.Cells[4].Width = new Unit("20%");
                e.Row.Cells[5].Width = new Unit("30%");
                e.Row.Cells[6].Width = new Unit("20%");
                e.Row.Cells[7].Width = new Unit("20%");
                e.Row.Cells[8].Width = new Unit("3%");
                e.Row.Cells[9].Width = new Unit("3%");
                e.Row.Cells[10].Width = new Unit("3%");


            }
        }

        protected void DropDownListTech_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.BindControllersList(DropDownListTech.SelectedValue);

            btnPanalAssign_ModalPopupExtender.Show();
        }

        private void BindControllersList(string technologyId)
        {
            DataTable dataTable = new DataTable();

            ControllerRepository controllers = new ControllerRepository();

            dataTable = controllers.GetControllerAssign(int.Parse(technologyId));

            ddlContrcoller.DataValueField = "ControllerId";
            ddlContrcoller.DataTextField = "ControllerName";
            ddlContrcoller.DataSource = dataTable;
            ddlContrcoller.DataBind();
            ddlContrcoller.Items.Insert(0, "--Select Controller--");

        }

        protected void DropDownListControllers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindUsersList(ddlContrcoller.SelectedValue);

            btnPanalAssign_ModalPopupExtender.Show();



        }

        private void BindUsersList(string controllerId)
        {
            EmailRepository getControllerAssignUsers = new EmailRepository();
            DataTable dataTable = getControllerAssignUsers.GetControllerAssignUsers(Convert.ToInt32(controllerId));
            RepeaterUsersEmail.DataSource = dataTable;
            RepeaterUsersEmail.DataBind();

        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            //   int emailId= Convert.ToInt32(txtEmailId.Text);
            //    int controllerId = Convert.ToInt32(DropDownListControllers.SelectedValue);
            //    EmailRepository emailReb = new EmailRepository();
            //    string EmailRef = "NPO#" + emailId;
            //    bool updated = emailReb.UpdateControllerId(controllerId,emailId, EmailRef);
            //    gvEmails.DataBind();
            //    DataTable dataTable= emailReb.GetControllerAssignUsers(Convert.ToInt32(DropDownListControllers.SelectedValue));
            //    string emails = "";
            //    for (int i = 0; i < dataTable.Rows.Count; i++)
            //    {
            //        emails += dataTable.Rows[i][2].ToString() + ",";

            //    }

            //    EntityEmail email = new EntityEmail();

            //    email.To = emails;
            //    email.Body = "you have a new Assign search By Email Reference : " +  EmailRef;
            //    email.Subject = "NPO Tool";

            //    MailHelper.SendMail(email);


        }

        protected void btnAssign1_Click(object sender, EventArgs e)
        {
            int EmailId = Convert.ToInt32(txtEId.Text);
            // int UserID = Convert.ToInt32(ddlUsers.SelectedValue);
            EmailRepository emailReb = new EmailRepository();
            // bool updated = emailReb.UpdateControllerId(UserId, EmailId);
            gvEmails.DataBind();
            DataTable dataTable = emailReb.GetEmailAssignUsers(EmailId);
            string emails = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                emails += dataTable.Rows[i][2].ToString() + ",";

            }

            EntityEmail email = new EntityEmail();

            email.To = emails;
            email.Body = "you have a new Assign";
            email.Subject = "NPO Tool";

            MailHelper.SendMail(email);


        }


        private void BindRepeater()
        {
            EmailRepository EmailUserReb = new EmailRepository();
            DataTable dataTable = EmailUserReb.GetEmailAssignUsers(Convert.ToInt32(txtEId.Text));
            RepeaterUsersEmail.DataSource = dataTable;
            RepeaterUsersEmail.DataBind();

        }
        private void BindDDlUser()
        {
            EmailRepository users = new EmailRepository();
            DataTable Users = users.GetUsers();

            ddlUsers.DataValueField = "UserID";
            ddlUsers.DataTextField = "FullName";
            ddlUsers.DataSource = Users;
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, "--Select User--");
        }
        protected void AddUserEmail_Click(object sender, EventArgs e)
        {
            EmailRepository EmailUserReb = new EmailRepository();
            EmailUserReb.AddUserEmail(Convert.ToInt32(ddlUsers.SelectedValue), Convert.ToInt32(txtEId.Text));
            BindRepeater();
        }

        protected void DeleteUserEmail_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            int EmailUserId = Convert.ToInt32(btn.CommandArgument);
            EmailRepository EmailUserReb = new EmailRepository();
            EmailUserReb.DeleteUserEmail(EmailUserId);
            BindRepeater();
        }

        private void BindControllerRepeater()
        {
            EmailRepository EmailControllerReb = new EmailRepository();
            DataTable dataTable = EmailControllerReb.GetEmailAssignController(Convert.ToInt32(txtEmailId.Text));
            RepeaterController.DataSource = dataTable;
            RepeaterController.DataBind();

        }

        private void BindDDlController()
        {
            EmailRepository controllers = new EmailRepository();
            DataTable Controllers = controllers.GetControllers();

            ddlContrcoller.DataValueField = "ControllerId";
            ddlContrcoller.DataTextField = "ControllerName";
            ddlContrcoller.DataSource = Controllers;
            ddlContrcoller.DataBind();
            ddlContrcoller.Items.Insert(0, "--Select Controller--");
        }

        protected void AddController_Click(object sender, EventArgs e)
        {
            EmailRepository ControllerReb = new EmailRepository();
            ControllerReb.AddEmailController(Convert.ToInt32(ddlContrcoller.SelectedValue), Convert.ToInt32(txtEmailId.Text));
            BindControllerRepeater();
        }
        protected void DeleteController_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            int EmailConId = Convert.ToInt32(btn.CommandArgument);
            EmailRepository EmailControllerReb = new EmailRepository();
            EmailControllerReb.DeleteEmailController(EmailConId);
            BindControllerRepeater();

        }
    }
}