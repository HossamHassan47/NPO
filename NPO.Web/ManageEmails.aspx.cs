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
            if (!IsPostBack)
            {
                gvEmails.DataBind();
            }
        }


        private  EmailFilter GetFilter()
        {
            EmailFilter entity = new EmailFilter();
            entity.From = txtEmailFrom.Text.ToString();
            entity.To = txtEmailTo.Text.ToString();
            entity.Subject = txtEmailSub.Text.ToString();
            entity.dateTimeReceived = txtEmailDate.Text.ToString();
            entity.dateTimeReceivedOperater = dateDropDownList.Text.ToString();
          
            return entity;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvEmails.DataBind();

        }

        protected void DsGvEmails_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filter"] = GetFilter();

        }

        protected void gvEmails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("10%");
                e.Row.Cells[1].Width = new Unit("20%");
                e.Row.Cells[2].Width = new Unit("30%");
                e.Row.Cells[3].Width = new Unit("20%");
                e.Row.Cells[2].Width = new Unit("20%");


            }
        }

       
    }
}