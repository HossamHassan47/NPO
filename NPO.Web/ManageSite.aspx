<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageSite.aspx.cs"
    Inherits="NPO.Web.ManageSite" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table class="zebra">

        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Site Code : " ForeColor="Black"></asp:Label>
            </td>

            <td>
                <asp:TextBox ID="txtSiteCode" runat="server" Width="299px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Site Name : " ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSiteName" runat="server" Width="300px"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td >
                <asp:Label ID="Label3" runat="server" Text="City : " ForeColor="Black"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="ddlCity" runat="server" Width="300px">
                </asp:DropDownList>
            </td>

            <td >
                <asp:CheckBox ID="_2g" runat="server" Width="110px" Text="2G" Checked="false" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="21px"></asp:CheckBox>
            </td>

            <td>

                <asp:CheckBox ID="_3g" runat="server" Width="142px" Text="3G" Checked="false" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="18px"></asp:CheckBox>
                <asp:CheckBox ID="_4g" runat="server" Width="142px" Text="4G" Checked="false" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="18px" Style="margin-bottom: 0px"></asp:CheckBox>

            </td>
           
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
              <td >
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="120px"
                    CssClass="btn btn-default btnSearch" OnClick="btnSearch_Click" />
                <asp:Button ID="btnAdd" runat="server" Text="ADD" Width="120px"
                    CssClass="btn btn-default btnAddNew" Font-Bold="True"
                    Font-Size="Small" ForeColor="Black" />
            </td>

        </tr>

    </table>
 <br />
    <asp:Button ID="btnExtender" runat="server" Style="display: none" />
    <ajaxToolKit:ModalPopupExtender
        ID="btnAdd_ModalPopupExtender" runat="server"
        BehaviorID="btnAdd_ModalPopupExtender"
        TargetControlID="btnExtender"
        PopupControlID="PanelAdd"
        BackgroundCssClass="ModalPopupBG">
    </ajaxToolKit:ModalPopupExtender>
    <asp:Label ID="DoneOrNot" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="PanelAdd" runat="server" Style="text-align: center; background-color: rgb(255, 251, 251); margin-top: 60px; margin-bottom: 0px;"
        Width="515px" Height="530px" BackColor="#999999" BorderColor="#666666" BorderStyle="Solid">

        <div style="background-color: #006699;">
            <asp:Label ID="l1" runat="server" ForeColor="White" Text="Add Site " Font-Bold="True"
                Font-Size="X-Large"></asp:Label>
        </div>
        <br />

        <div>
            <asp:TextBox ID="txtid" runat="server" Visible="false" Text="-1"></asp:TextBox>
        </div>
        <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <table>

                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Label ID="L3" runat="server" Font-Bold="True" ForeColor="Black" Text="Site Code : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddSiteCode" runat="server" Width="299px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                       
                         <td>
                            <asp:Label ID="L2" runat="server" Font-Bold="True" ForeColor="Black" Text="Site Name : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddSiteName" runat="server" Width="299px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Black" Text="City : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddCityName" runat="server" OnSelectedIndexChanged="ddlAddCityName_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">--Select City--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Black" Text="City Zone : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddCityZone" runat="server" AutoPostBack="true">
                                <asp:ListItem Value="0">--Select CityZone</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="txt2G" runat="server" Checked="False" Text=" 2G " OnCheckedChanged="txt2G_CheckedChanged" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlControllers2g" runat="server" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="txt3G" runat="server" Checked="False" Text=" 3G " OnCheckedChanged="txt3G_CheckedChanged" AutoPostBack="true"/>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlControllers3g" runat="server" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="txt4G" runat="server" Checked="False" Text=" 4G " OnCheckedChanged="txt4G_CheckedChanged" AutoPostBack="true"/>

                        </td>
                        <td>

                            <asp:DropDownList ID="ddlControllers4g" runat="server" Visible="false">
                            </asp:DropDownList>

                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>



        <asp:Button ID="btnSave" runat="server" CssClass="col-md-offset-0"
            Height="30px" Text="Save" Width="123px"
            Font-Bold="True" BackColor="#2471a3" ForeColor="White" OnClick="btnSave_Click" />

        <asp:Button ID="btnCancel" runat="server" Height="30px"
            Style="margin-left: 31px" Text="Cancel" Width="123px" Font-Bold="True" OnClick="btnCancel_Click" />

    </asp:Panel>
   
    <asp:GridView ID="gvSites" runat="server"
         CellPadding="3" GridLines="Vertical" 
        AutoGenerateColumns="False" Width="100%"
        BorderWidth="1px" 
        AllowCustomPaging="True" 
        BackColor="White" 
        BorderColor="#999999"
        BorderStyle="Solid" 
        PageSize="20" 
        AllowPaging="True"
        CellSpacing="2" HorizontalAlign="Center" 
        OnRowCommand="gvSites_RowCommand"
         DataSourceID="DsGvSites">

      

        <Columns>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Content/icDelete.png" CommandName="deletesite" CommandArgument='<%#Eval("SiteId")%>' OnClientClick="return confirm('Are you sure you want to delete this User?');"
                        AlternateText="Delete" />
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Content/icEdit.png" CommandName="editsite" CommandArgument='<%#Eval("SiteId")%>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="SiteCode" HeaderText="Site Code" />

            <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
            <asp:CheckBoxField DataField="2g" HeaderText="2G" ReadOnly="true" />

            <asp:CheckBoxField DataField="3g" HeaderText="3G" ReadOnly="true" />

            <asp:CheckBoxField DataField="4g" HeaderText="4G" ReadOnly="true" />

        </Columns>

        
        <FooterStyle BackColor="White" ForeColor="Black" BorderStyle="None" />
        <HeaderStyle BackColor="#2471A3" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#003366" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
         <AlternatingRowStyle BackColor="#E5E8E8" />
        <PagerStyle CssClass="pagination-ys" />

    </asp:GridView>
    <asp:ObjectDataSource ID="DsGvSites"
        EnablePaging="True" runat="server"
        SelectCountMethod="GetSitesCount"
        SelectMethod="GetSites"
        TypeName="NPO.Code.Repository.SiteRepository"
        OnSelecting="DsGvSites_Selecting"></asp:ObjectDataSource>



</asp:Content>
