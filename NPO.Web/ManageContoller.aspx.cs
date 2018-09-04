using NPO.Code.FilterEntity;
using System;
using System.Collections.Generic;
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
            gvcon.DataBind();
        }
        private ControllerFilter GetFilter()
        {
            ControllerFilter entity = new ControllerFilter();
            entity.ControllerName = txtControllerName.Text.ToString();
            entity.TechnologyId = DropDownListTech.SelectedItem.Text.ToString();
            return entity;
        }
        protected void DsGvcon_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filter"] = GetFilter();

        }

       
    }
}