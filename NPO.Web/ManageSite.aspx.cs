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
    public partial class ManageSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCityList();
                gvSites.DataBind();

            }


        }

        private DataTable BindSite()
        {
            SiteRepository siteRep = new SiteRepository();
            DataTable dataTable = siteRep.GetSites(GetFilter());

            return dataTable;

        }

        private SiteFilter GetFilter()
        {
            SiteFilter entity = new SiteFilter();

            entity.SiteName = txtSiteName.Text.ToString();
            entity.SiteCode = txtSiteCode.Text.ToString();
            if (ddlCity.SelectedIndex == 0) { entity.CityId = 0; }
            else { entity.CityId = Convert.ToInt32(ddlCity.SelectedValue); }

            if (_2g.Checked) { entity._2g = true; }
            else { entity._2g = false; }

            if (_3g.Checked) { entity._3g = true; }
            else { entity._3g = false; }

            if (_4g.Checked) { entity._4g = true; }
            else { entity._4g = false; }




            return entity;
        }

        private Site GetVaules()
        {
            Site entity = new Site();

            entity.SiteName = txtAddSiteName.Text.ToString();
            entity.SiteCode = txtAddSiteCode.Text.ToString();

            if (ddlAddCityName.SelectedIndex != 0) entity.CityId = Convert.ToInt32(ddlAddCityName.SelectedValue);
            else { entity.CityId = 0; }
            if (ddlAddCityZone.SelectedIndex != 0) entity.ZoneId = Convert.ToInt32(ddlAddCityZone.SelectedValue);
            else { entity.ZoneId = 0; }



            if (ddlControllers2g.SelectedIndex != 0) entity.ControlerId2g = Convert.ToInt32(ddlControllers2g.SelectedValue);
            else { entity.ControlerId2g = 0; }
            if (ddlControllers3g.SelectedIndex != 0) entity.ControlerId3g = Convert.ToInt32(ddlControllers3g.SelectedValue);
            else { entity.ControlerId3g = 0; }
            if (ddlControllers4g.SelectedIndex != 0) entity.ControlerId4g = Convert.ToInt32(ddlControllers4g.SelectedValue);
            else { entity.ControlerId4g = 0; }

            if (txt2G.Checked)
            {
                entity._2g = true;

            }
            else
            {
                entity._2g = false;

            }

            if (txt3G.Checked)
            {
                entity._3g = true;

            }
            else
            {
                entity._3g = false;

            }
            if (txt4G.Checked)
            {
                entity._4g = true;

            }
            else
            {
                entity._4g = false;

            }

            return entity;
        }


        protected void DsGvSites_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filter"] = GetFilter();
        }

        protected void gvSites_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            #region Edit
            if (e.CommandName == "editsite")
            {

                l1.Text = "Update Site";

                int id = Convert.ToInt32(e.CommandArgument);

                int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
                GridViewRow row = gvSites.Rows[index];
                txtid.Text = id.ToString();
                txtAddSiteCode.Text = row.Cells[1].Text;
                txtAddSiteName.Text = row.Cells[2].Text;
                BindCityListPopPanel();

                DataTable dataTable = BindSite();
                if (Convert.ToUInt32(dataTable.Rows[index][4]) == 0)
                {
                    ddlAddCityName.SelectedIndex = 0;
                }
                else
                {
                    ddlAddCityName.SelectedValue = dataTable.Rows[index][4].ToString();
                }
                BindCityZonePopPanel();
                if (Convert.ToUInt32(dataTable.Rows[index][5]) == 0)
                {
                    ddlAddCityZone.SelectedIndex = 0;
                }
                else
                {
                    ddlAddCityZone.SelectedValue = dataTable.Rows[index][5].ToString();
                }

                BindControllersList();
                //2G
                if (Convert.ToInt32(dataTable.Rows[index][7]) == 1)
                {
                    txt2G.Checked = true;
                    if(Convert.ToInt32(dataTable.Rows[index][10]) == 0) { ddlControllers2g.SelectedIndex = 0; }
                    else
                    {
                        ddlControllers2g.SelectedValue = dataTable.Rows[index][10].ToString();
                    }
                    ddlControllers2g.Visible = true;
                }
                else
                {
                    txt2G.Checked = false;
                }
                //3G
                if (Convert.ToInt32(dataTable.Rows[index][8]) == 1)
                {
                    txt3G.Checked = true;
                    if (Convert.ToInt32(dataTable.Rows[index][11]) == 0) { ddlControllers3g.SelectedIndex = 0; }
                    else  ddlControllers3g.SelectedValue = dataTable.Rows[index][11].ToString();

                    ddlControllers3g.Visible = true;

                }
                else
                {
                    txt3G.Checked = false;
                }
                //4G
                if (Convert.ToInt32(dataTable.Rows[index][9]) == 1)
                {
                    txt4G.Checked = true;
                    if (Convert.ToInt32(dataTable.Rows[index][12]) == 0) { ddlControllers4g.SelectedIndex = 0; }
                    else ddlControllers4g.SelectedValue = dataTable.Rows[index][12].ToString();

                    ddlControllers4g.Visible = true;

                }
                else
                {
                    txt4G.Checked = false;
                }
                btnAdd_ModalPopupExtender.Show();
            }
            #endregion
            #region delete
            if (e.CommandName == "deletesite")
            {
                SiteRepository siteRep = new SiteRepository();
                int id = Convert.ToInt32(e.CommandArgument);
                bool isUser = siteRep.DeleteSite(id);
                if (isUser)
                {
                    gvSites.DataBind();
                    DoneOrNot.Text = "Done";
                }
                else
                {
                    gvSites.DataBind();
                    DoneOrNot.Text = "YOu have problem";
                }

            }
            #endregion

        }

        private void setTextBoxesNull()
        {
            txtid.Text = "-1";
            txtSiteName.Text = "";
            txtSiteCode.Text = "";
            txt2G.Checked = false;
            txt3G.Checked = false;
            txt4G.Checked = false;
            txtAddSiteName.Text = "";
            txtAddSiteCode.Text = "";
            ddlAddCityName.SelectedIndex = 0;
            ddlAddCityZone.SelectedIndex = 0;
            ddlControllers2g.Visible = false;
            ddlControllers3g.Visible = false;
            ddlControllers4g.Visible = false;
        }
        #region DDlBind

        private void BindCityList()
        {
            SiteRepository siteRepository = new SiteRepository();

            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityId";
            ddlCity.DataSource = siteRepository.GetCities();
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, "--Select City");

        }


        // pop Panel

        private void BindCityListPopPanel()
        {
            SiteRepository siteRepository = new SiteRepository();

            ddlAddCityName.DataTextField = "CityName";
            ddlAddCityName.DataValueField = "CityId";
            ddlAddCityName.DataSource = siteRepository.GetCities();
            ddlAddCityName.DataBind();
            ddlAddCityName.Items.Insert(0, "--Select City");

        }

        private void BindCityZonePopPanel()
        {
            SiteRepository siteRepository = new SiteRepository();

            ddlAddCityZone.DataTextField = "ZoneName";
            ddlAddCityZone.DataValueField = "ZoneId";
            int selectedValue = 0;
            if (ddlAddCityName.SelectedIndex != 0) { selectedValue = Convert.ToInt32(ddlAddCityName.SelectedValue); }
            ddlAddCityZone.DataSource = siteRepository.CityZone(selectedValue);
            ddlAddCityZone.DataBind();
            ddlAddCityZone.Items.Insert(0, "--Select Zone");

        }

        private void BindControllersList()
        {
            SiteRepository siteRepository = new SiteRepository();
            //2g

            ddlControllers2g.DataTextField = "ControllerName";
            ddlControllers2g.DataValueField = "ControllerId";
            ddlControllers2g.DataSource = siteRepository.GetControllers(2);
            ddlControllers2g.DataBind();
            ddlControllers2g.Items.Insert(0, "--Select Controller 2G--");

            //3g
            ddlControllers3g.DataTextField = "ControllerName";
            ddlControllers3g.DataValueField = "ControllerId";
            ddlControllers3g.DataSource = siteRepository.GetControllers(3);
            ddlControllers3g.DataBind();
            ddlControllers3g.Items.Insert(0, "--Select Controller 3G--");


            //4g
            ddlControllers4g.DataTextField = "ControllerName";
            ddlControllers4g.DataValueField = "ControllerId";
            ddlControllers4g.DataSource = siteRepository.GetControllers(4);
            ddlControllers4g.DataBind();
            ddlControllers4g.Items.Insert(0, "--Select Controller 4G--");


        }


        #endregion

        #region btnClick
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvSites.DataBind();

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BindControllersList();
            BindCityListPopPanel();
            setTextBoxesNull();
            btnAdd_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SiteRepository siteRep = new SiteRepository();
            int id = Convert.ToInt32(txtid.Text);
            if (id < 0)
            {
                int siteid = siteRep.InsertNewSite(GetVaules());
                if (siteid > 0)
                {
                    gvSites.DataBind();
                    setTextBoxesNull();
                }
                else
                {
                    setTextBoxesNull();

                }
            }
            else
            {
                Site site = GetVaules();
                site.SiteId = id;
                bool siteid = siteRep.UpdateSite(site);

                if (siteid)
                {
                    gvSites.DataBind();
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
        #endregion

        #region selectedPanel

        protected void ddlAddCityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCityZonePopPanel();
            btnAdd_ModalPopupExtender.Show();

        }

        protected void txt2G_CheckedChanged(object sender, EventArgs e)
        {
            if (txt2G.Checked)
            {

                ddlControllers2g.Visible = true;
            }
            else
            {
                ddlControllers2g.Visible = false;

            }
        }

        protected void txt3G_CheckedChanged(object sender, EventArgs e)
        {
            if (txt3G.Checked)
            {
                ddlControllers3g.Visible = true;
            }
            else
            {
                ddlControllers3g.Visible = false;

            }
        }

        protected void txt4G_CheckedChanged(object sender, EventArgs e)
        {
            if (txt4G.Checked)
            {
                ddlControllers4g.Visible = true;
            }
            else
            {
                ddlControllers4g.Visible = false;

            }
        }
        #endregion


       

    }
}