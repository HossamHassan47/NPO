<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageContoller.aspx.cs" Inherits="NPO.Web.ManageContoller" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="zebra" style="height: 81px; width: 784px;">
                
        <tr>
            <td style="height: 86px; width: 4px;">
                </td>
            <td style="height: 86px" >
                 Controller Name :&nbsp;
                <asp:TextBox ID="txtControllerName" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp; 
                Technology Name : &nbsp;<asp:DropDownList ID="DropDownListTech" runat="server" CssClass="col-md-offset-0" Height="34px" Width="68px"  > 
                    <asp:ListItem Text=""></asp:ListItem>
                    <asp:ListItem Text="2G" ></asp:ListItem>
                    <asp:ListItem Text="3G" ></asp:ListItem>
                    <asp:ListItem Text="4G" ></asp:ListItem>
                </asp:DropDownList>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnSearch" runat="server"  Text="Search" Width="187px" 
                         CssClass="btn btn-default btnSearch"  />
                 <br />
&nbsp;</td>
        </tr>
    </table>

    <br />
    <br />
    <asp:GridView ID="gvcon" runat="server" CellPadding="3" GridLines="Vertical" AutoGenerateColumns ="False" 
        BorderWidth="1px" BackColor="White" BorderColor="Black" BorderStyle="Solid" AllowPaging="True" 
        EmptyDataText="No Data Found" DataSourceID="DsGvCon" PageSize="2">

        <AlternatingRowStyle BackColor="#DCDCDC" />




        <Columns>
            <asp:BoundField DataField="ControllerName" HeaderText="Controller Name" />
            <asp:BoundField DataField="TechnologyId" HeaderText="Technology Name" SortExpression="TechnologyName" />
        </Columns>




        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />

    </asp:GridView>




    <asp:ObjectDataSource ID="DsGvCon" OnSelecting="DsGvcon_Selecting"
        EnablePaging="True" runat="server" 
        SelectCountMethod="GetControllersCount" 
        SelectMethod="GetControllers"
         TypeName="NPO.Code.Repository.ControllerRepository"
        ></asp:ObjectDataSource>




    <br />
    <br />
    


</asp:Content>
