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
    public partial class ManageChildrenEmails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsersGrid();
            }
        }

        private DataTable BindUsersGrid()
        {
            int emailRef = int.Parse(this.Request.QueryString["EmailId"]);// Convert.ToInt32(Session["EmailId"]);
            EmailRepository emailRep = new EmailRepository();
            DataTable dataTable = emailRep.GetChildrenEmail(emailRef);
            gvChEmails.DataSource = dataTable;
            gvChEmails.DataBind();
            return dataTable;
        }
        protected void gvChEmails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvChEmails_RowCommand(object sender, GridViewCommandEventArgs e)
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
        }
    }
}