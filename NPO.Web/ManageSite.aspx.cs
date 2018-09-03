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
    public partial class ManageSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindSiteGrid();
        }
        private void BindSiteGrid()
        {
            SiteRepository siteRep = new SiteRepository();
            DataTable dataTable = siteRep.GetSites(GetFilter());

            gvSites.DataSource = dataTable;
            gvSites.DataBind();

        }
        private SiteFilter GetFilter()
        {
            SiteFilter entity = new SiteFilter();
            entity.SiteName = txtSiteName.Text.ToString();
            entity.SiteCode = txtSiteCode.Text.ToString();
            return entity;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSiteGrid();

        }
    }
}