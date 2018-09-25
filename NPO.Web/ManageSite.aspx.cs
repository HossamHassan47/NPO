using NPO.Code.Entity;
using NPO.Code.FilterEntity;
using NPO.Code.Repository;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace NPO.Web
{
    public partial class ManageSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSearch.UniqueID;

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
            entity.RegionId = 1;
            if (ddlType.SelectedIndex != 0) entity.SiteType = Convert.ToInt32(ddlType.SelectedValue);
            else { entity.SiteType = 0; }
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
                    string s = dataTable.Rows[index][5].ToString();
                    ddlAddCityZone.SelectedValue = s;
                }
                if (Convert.ToUInt32(dataTable.Rows[index][6]) == 0)
                {
                    ddlType.SelectedIndex = 0;
                }
                else
                {
                    ddlType.SelectedValue = dataTable.Rows[index][6].ToString();
                }

                BindControllersList();
                //2G
                if (Convert.ToInt32(dataTable.Rows[index][7]) == 1)
                {
                    txt2G.Checked = true;
                    if (Convert.ToInt32(dataTable.Rows[index][10]) == 0) { ddlControllers2g.SelectedIndex = 0; }
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
                    else ddlControllers3g.SelectedValue = dataTable.Rows[index][11].ToString();

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
                    DoneOrNot.Text = "You have problem";
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
            ddlType.SelectedIndex = 0;
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


            int selectedValue;
            if (ddlAddCityName.SelectedIndex != 0) { selectedValue = Convert.ToInt32(ddlAddCityName.SelectedValue); }
            else { selectedValue = 0; }
            ddlAddCityZone.DataSource = siteRepository.CityZone(selectedValue);
            ddlAddCityZone.DataTextField = "ZoneName";
            ddlAddCityZone.DataValueField = "ZoneId";
            ddlAddCityZone.DataBind();
            ddlAddCityZone.Items.Insert(0, "--Select Zone--");

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
            gvSites.PageIndex = 0;

            gvSites.DataBind();

        }

        protected void btnExportToXls_Click(object sender, EventArgs e)
        {
            var dtSites = new SiteRepository().GetSitesForExcel(GetFilter());
            new EppLusExcel().ExporttoExcel(dtSites, "Sites", "NPO_Sites");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BindControllersList();
            BindCityListPopPanel();
            BindCityZonePopPanel();
            ddlType.SelectedValue = "0";
            setTextBoxesNull();
            btnAdd_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SiteRepository siteRep = new SiteRepository();
            if (string.IsNullOrWhiteSpace(txtAddSiteCode.Text.ToString().Trim()))
            {
                lblErrorMsg.Text = "Site must have code";

                txtAddSiteCode.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAddSiteName.Text.ToString().Trim()))
            {
                lblErrorMsg.Text = "Site must have name";
                lblErrorMsg.ForeColor = Color.Red;
                lblErrorMsg.Font.Bold = true;
                txtAddSiteName.Focus();
                return;
            }
            if (ddlAddCityName.SelectedIndex == 0)
            {
                lblErrorMsg.Text = "Site must have city";
                lblErrorMsg.ForeColor = Color.Red;
                lblErrorMsg.Font.Bold = true;
                ddlAddCityName.Focus();
                return;
            }
            if (ddlAddCityZone.SelectedIndex == 0)
            {
                lblErrorMsg.Text = "Site must have cityZone";
                lblErrorMsg.ForeColor = Color.Red;
                lblErrorMsg.Font.Bold = true;
                ddlAddCityZone.Focus();
                return;
            }
            if (ddlType.SelectedIndex == 0)
            {
                lblErrorMsg.Text = "Site must have type";
                lblErrorMsg.ForeColor = Color.Red;
                lblErrorMsg.Font.Bold = true;
                ddlType.Focus();
                return;
            }
            if (txt2G.Checked && ddlControllers2g.SelectedIndex == 0)
            {
                lblErrorMsg.Text = "Site must have contoller2g name ";
                lblErrorMsg.ForeColor = Color.Red;
                lblErrorMsg.Font.Bold = true;
                ddlControllers2g.Focus();
                return;
            }
            if (txt3G.Checked && ddlControllers3g.SelectedIndex == 0)
            {
                lblErrorMsg.Text = "Site must have contoller3g name ";
                lblErrorMsg.ForeColor = Color.Red;
                lblErrorMsg.Font.Bold = true;
                ddlControllers2g.Focus();
                return;
            }
            if (txt4G.Checked && ddlControllers4g.SelectedIndex == 0)
            {
                lblErrorMsg.Text = "Site must have contoller4g name ";
                lblErrorMsg.ForeColor = Color.Red;
                lblErrorMsg.Font.Bold = true;
                ddlControllers2g.Focus();
                return;
            }
            int id = Convert.ToInt32(txtid.Text);
            if (id < 0)
            {

                int siteid = siteRep.InsertNewSite(GetVaules());
                if (siteid > 0)
                {
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    DoneOrNot.Text = "Site added successfully";
                    lblErrorMsg.ForeColor = Color.Green;
                    lblErrorMsg.Font.Bold = true;
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
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    DoneOrNot.Text = "Site updated successfully";
                    DoneOrNot.ForeColor = Color.Green;
                    DoneOrNot.Font.Bold = true;
                    setTextBoxesNull();
                }
                else
                {
                    DoneOrNot.Text = "You have problem";
                    DoneOrNot.ForeColor = Color.Red;
                    DoneOrNot.Font.Bold = true;
                    setTextBoxesNull();
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

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

        #region uploadExelFile
        private bool IsTemplateValid(DataTable sitesTemplate, out string message)
        {
            if (!sitesTemplate.Columns.Contains("Site_Code"))
            {
                message = "Error: Site_Code is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("Site_Name"))
            {
                message = "Error: Site_Name is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("City"))
            {
                message = "Error: City is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("City_Zone"))
            {
                message = "Error: City_Zone is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("Type"))
            {
                message = "Error: Type is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("BSC"))
            {
                message = "Error: BSC is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("RNC"))
            {
                message = "Error: Type is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("LTE_City"))
            {
                message = "Error: LTE_City is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("2G"))
            {
                message = "Error: 2G is missing.";
                return false;
            }

            else if (!sitesTemplate.Columns.Contains("3G"))
            {
                message = "Error: 3G is missing.";
                return false;
            }


            else if (!sitesTemplate.Columns.Contains("LTE"))
            {
                message = "Error: LTE is missing.";
                return false;
            }

            message = "OK";
            return true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                lblUploadResult.Text = "Please, select file to upload.";
                lblUploadResult.ForeColor = Color.Red;
                lblUploadResult.Font.Bold = true;
                return;
            }

            // Save file to Temp folder
            string filePath = @"~/Temp/Sites_" + DateTime.Now.Ticks + @".xlsx";
            FileUpload1.SaveAs(Server.MapPath(filePath));
            // Read file from temp folder
            var eppLusExcel = new EppLusExcel();

            var sites = eppLusExcel.GetDataSetFromExcel(Server.MapPath(filePath)).Tables[0];

            // Validate template
            string message;
            bool isValid = this.IsTemplateValid(sites, out message);
            // Upload sites
            if (isValid)
            {
                SiteRepository siteRepository = new SiteRepository();
                bool update = UpdateDB_ExcelData(sites, out message);
                if (update)
                {
                    gvSites.DataBind();
                    lblUploadResult.Text = "";

                }
            }
            // display upload result
            lblUploadResult.Text = message;
            lblUploadResult.ForeColor = Color.Red;
            lblUploadResult.Font.Bold = true;
        }

        public bool UpdateDB_ExcelData(DataTable sites, out string message)
        {

            SiteRepository siteRep = new SiteRepository();
            if (!sites.Columns.Contains("Result"))
            {
                sites.Columns.Add("Result", typeof(string));
            }

            foreach (DataRow row in sites.Rows)
            {

                Site site = new Site();

                site.RegionId = siteRep.GetRegionId(row["Region"].ToString().Trim());
                site.SiteName = row["Site_Name"].ToString().Trim();
                site.SiteCode = row["Site_Code"].ToString().Trim();
                site.CityId = siteRep.GetCityId(row["City"].ToString().Trim(), site.RegionId);
                site.ZoneId = siteRep.GetCityZoneId(row["City_zone"].ToString().Trim(), site.CityId);
                site.SiteType = siteRep.GetTypeId(row["Type"].ToString().Trim());

                if (row["BSC"].ToString().Trim() == "#N/A" || row["BSC"].ToString().Trim() == "" || row["BSC"].ToString().Trim() == "NaN")
                {
                    site.ControlerId2g = 0;
                }
                else
                {
                    site.ControlerId2g = siteRep.GetCOntrollerId_2G(row["BSC"].ToString());
                }

                if (row["RNC"].ToString().Trim() == "#N/A" || row["RNC"].ToString().Trim() == "" || row["RNC"].ToString().Trim() == "NaN")
                {
                    site.ControlerId3g = 0;
                }
                else
                {
                    site.ControlerId3g = siteRep.GetCOntrollerId_3G(row["RNC"].ToString());
                }

                if (row["LTE_City"].ToString().Trim() == "#N/A" || (row["LTE_City"].ToString().Trim() == "") || (row["LTE_City"].ToString().Trim() == "NaN"))
                {
                    site.ControlerId4g = 0;
                }
                else
                {
                    site.ControlerId4g = siteRep.GetCOntrollerId_4G(row["LTE_City"].ToString());
                }
                
                site._2g = row["2G"] == null ? false : (row["2G"].ToString() == "1");
                site._3g = row["3G"] == null ? false : (row["3G"].ToString() == "1");
                site._4g = row["LTE"] == null ? false : (row["LTE"].ToString() == "1");

                if (string.IsNullOrEmpty(site.SiteCode))
                {
                    row["Result"] = "Site code must not equal null";
                    continue;

                }
                if (string.IsNullOrEmpty(site.SiteName))
                {
                    row["Result"] = "Site name must not equal null";
                    continue;

                }
                if (site.RegionId < 0)
                {
                    row["Result"] = "Missing region";
                    continue;
                }
                if (site.CityId < 0)
                {
                    row["Result"] = "Missing city";
                    continue;
                }
                if (site.ZoneId < 0)
                {
                    row["Result"] = "Missing zone";
                    continue;
                }
                if (site._2g && site.ControlerId2g == 0)
                {
                    row["Result"] = "Missing controller 2G name";
                    continue;
                }
                if (site._3g && site.ControlerId3g == 0)
                {
                    row["Result"] = "Missing  controller 3G name";
                    continue;
                }
                if (site._4g && site.ControlerId4g == 0)
                {
                    row["Result"] = "Missing  controller 4G name";
                    continue;
                }

                int check = siteRep.CheckSiteExist(row["Site_Code"].ToString());

                if (check > -1)
                {
                    site.SiteId = check;
                    bool updated = siteRep.UpdateSite(site);
                    if (updated)
                    {
                        row["Result"] = "Updated successfully";
                    }
                    else
                    {
                        row["Result"] = "";

                    }
                }
                else
                {
                    int inserted = siteRep.InsertNewSite(site);
                    if (inserted != -1)
                    {
                        row["Result"] = "Inserted successfully";

                    }
                    else
                    {
                        row["Result"] = "";

                    }
                }

            }
            message = "Done";
            lblUploadResult.Text = message;
            gvSites.DataBind();
            EppLusExcel down = new EppLusExcel();
            down.ExporttoExcel(sites, "Sheet1", "SitesUploadResult");
            return true;
        }
        #endregion
    }
}