<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageContoller.aspx.cs" Inherits="NPO.Web.ManageContoller" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="zebra">
        <tr>
            <td>Controller Name:</td>
            <td>
                <asp:TextBox ID="txtControllerName" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td>Technology Name :
            </td>
            <td>
                <asp:DropDownList ID="DropDownListTech" runat="server" CssClass="col-md-offset-0" Width="200px">
                    <asp:ListItem Text="-- Select --" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="2G" Value="1"></asp:ListItem>
                    <asp:ListItem Text="3G" Value="2"></asp:ListItem>
                    <asp:ListItem Text="4G" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="120px"
                    CssClass="btn btn-default btnSearch" OnClick="btnSearch_Click" />
                <asp:Button ID="btnAdd" runat="server" Text="ADD" Width="120px"
                    CssClass="btn btn-default btnAddNew" Font-Bold="True"
                    Font-Size="Small" ForeColor="Black" />
            </td>
        </tr>
    </table>

    <ajaxToolkit:ModalPopupExtender
        ID="btnAdd_ModalPopupExtender" runat="server"
        BehaviorID="btnAdd_ModalPopupExtender"
        TargetControlID="btnAdd"
        PopupControlID="PanelAdd"
        BackgroundCssClass="ModalPopupBG">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Label ID="DoneOrNot" runat="server"></asp:Label>

    <br />
    <asp:Panel ID="PanelAdd" runat="server"
        Style="background-color: rgb(255, 251, 251);"
        Width="450px" Height="290px" BackColor="#999999" BorderColor="#666666" BorderStyle="Solid">

        <div style="height: 30px; background-color: #2471a3;  width: 100%; text-align: center;">
            <asp:Label ID="labelController" runat="server" ForeColor="White" Text="Add Controller " Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>

        <br />
        <asp:HiddenField ID="hdnId" runat="server" value="-1"></asp:HiddenField>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
              <table class="zebra" style="width:98%; margin:5px;">
            <tr>
                <td>
                    <asp:Label ID="l1" runat="server" Text="Controller Name:"></asp:Label>
                </td>

                <td >
                    <asp:TextBox ID="txtConName" runat="server" Width="300px" ></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Technology Name:" ></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTechnology" runat="server" Width="200px">
                        <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                        <asp:ListItem Text="2G" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3G" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4G" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="120px" CssClass="btn btn-default btnSave" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="120px" CssClass="btn btn-default btnCancel"  OnClick="btnCancel_Click" formnovalidate="formnovalidate"  /></td>
            </tr>
                   <tr>
                       <td></td>
                        <td >
                            <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
        </table>
                         </ContentTemplate>

        </asp:UpdatePanel>
      




    </asp:Panel>
   

    <asp:Button ID="btnExAddUsers" runat="server" Style="display:none" />
    <ajaxToolkit:ModalPopupExtender 
        runat="server" 
        BehaviorID="btnExAddUsers_ModalPopupExtender"
         TargetControlID="btnExAddUsers" 
        ID="btnExAddUsers_ModalPopupExtender"
        PopupControlID="PanalAddUsers"
        >

    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanalAddUsers" runat="server" BackColor="White" CssClass="PanalAssignStyle" Height="400px">
        <asp:ImageButton ID="btnCancelAssign" runat="server" ImageUrl="~/Content/icExit.png" CssClass="PanelBodyStylebutton" />
        

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <table class="zebra">
                    <tr>
                        <td>Users:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlUsers" Width="300px">
                               
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCotrollerId" style="display:none" runat="server" Text="-1"></asp:TextBox>
                                    <asp:Button ID="AddUserController" runat="server" Text="Add" OnClick="AddUserController_Click" />
                        </td>
                    </tr>
                   
                </table>
                <br />
                <asp:Repeater ID="RepeaterUsersControllers" runat="server">
                    <HeaderTemplate>
                        <table class="zebra">
                            <tr style="font-weight: bold;">
                                <td>User Name</td>
                                <td>User Email</td>
                                <td>Delete</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("FullName")%></td>
                            <td><%#Eval("EmailAddress")%></td>
                           <td><asp:ImageButton ID="DeleteUserCon" runat="server" ImageUrl="~/Content/icExit.png" OnClick="DeleteUserCon_Click" CommandArgument='<%#Eval("ID")%>'/> 

                           </td> 
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

            </ContentTemplate>
        </asp:UpdatePanel>

    </asp:Panel>

    <asp:GridView ID="gvControllers" runat="server" CellPadding="7" GridLines="Vertical" AutoGenerateColumns="False"
        Width="100%" OnRowDataBound="gvControllers_RowDataBound"
        BorderWidth="1px" AllowCustomPaging="True" BackColor="White" BorderColor="#999999"
        BorderStyle="Solid" PageSize="20" AllowPaging="True" EmptyDataText="No Data Found" DataSourceID="DsGvCon" OnRowCommand="gvControllers_RowCommand"
        CellSpacing="2" HorizontalAlign="Center">
        <AlternatingRowStyle BackColor="#E5E8E8" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Content/icDelete.png" CommandName="deleteController" CommandArgument='<%#Eval("ControllerId")%>' OnClientClick="return confirm('Are you sure you want to delete this Controller?');"
                        AlternateText="Delete" />
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Content/icEdit.png" CommandName="editController" 
                        CommandArgument='<%#Eval("ControllerId") + ";" + Eval("TechnologyId")%>' />
                    <asp:ImageButton ID="btnUser" runat="server" ImageUrl="~/Content/icAddUser.png" CommandName="UserAdd" 
                        CommandArgument='<%#Eval("ControllerId") %>' />
                </ItemTemplate>
            </asp:TemplateField>    

            <asp:BoundField DataField="ControllerName" HeaderText="Controller Name" />
            <asp:BoundField DataField="TechnologyName" HeaderText="Technology Name" SortExpression="TechnologyName" />
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

    </asp:GridView>
    <asp:ObjectDataSource ID="DsGvCon" OnSelecting="DsGvcon_Selecting"
        EnablePaging="True" runat="server"
        SelectCountMethod="GetControllersCount"
        SelectMethod="GetControllers"
        TypeName="NPO.Code.Repository.ControllerRepository"></asp:ObjectDataSource>
</asp:Content>
