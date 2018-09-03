<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageSite.aspx.cs" Inherits="NPO.Web.ManageSite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 55%; height: 216px;">
        <tr>
            <td style="width: 259px"><span style="font-size: medium">Site Code </span>
                <asp:TextBox ID="txtSiteName" runat="server" Height="29px" style="font-size: medium" Width="299px"></asp:TextBox>
            </td>
            <td>Site Name<br />
                <asp:TextBox ID="txtSiteCode" runat="server" Height="29px" style="font-size: medium" Width="299px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSearch" runat="server" Height="36px" Text="Search" Width="189px" OnClick="btnSearch_Click"  />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <asp:GridView ID="gvSites" runat="server" CellPadding="3" GridLines="Vertical" AutoGenerateColumns ="False"  Height="157px"  Top="0%" Width="70%" BorderWidth="1px" AllowCustomPaging="True" AllowPaging="True" BackColor="White" BorderColor="#999999" BorderStyle="Solid" PageSize="50"  >
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="SiteCode" HeaderText="Site Code" />
            <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
         
           
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>

    </asp:Content>
