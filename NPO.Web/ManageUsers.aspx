<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="NPO.Web.ManageUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table class="zebra">

        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Nokia User Name : "></asp:Label>
            </td>

            <td>
                <asp:TextBox ID="txtMemberNokiaUserName" runat="server" Width="299px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Full Name : " ></asp:Label>
                
            </td>
            <td><asp:TextBox ID="txtFullName" runat="server" Width="300px"></asp:TextBox></td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Email Address : " ></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtEmailAddress" runat="server" Width="300px"></asp:TextBox>
            </td>

            <td>
                <asp:CheckBox ID="isadmin" runat="server" Text="Is Admin" ></asp:CheckBox>
            </td>

            <td>
                <asp:CheckBox ID="isactive" runat="server" Text="Is Active " Checked="true" ></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>
                 <asp:Button ID="btnSearch" runat="server" Text="Search" Width="120px"
                    CssClass="btn btn-default btnSearch" OnClick="btnSearch_Click" />

                <asp:Button ID="btnAdd" runat="server" Text="ADD" Width="120px"
                    CssClass="btn btn-default btnAddNew" Font-Bold="True"
                    Font-Size="Small" ForeColor="Black" OnClick="btnAdd_Click" />
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
    <asp:Panel ID="PanelAdd" runat="server" CssClass="PanelAddStyle">
        <div style="height: 30px; background-color: #2471a3; width: 100%; align-content: center">
            <asp:Label ID="labelUser" runat="server" ForeColor="White" Text="Add User " Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>
        <br />
        <asp:TextBox ID="txtid" runat="server" Visible="false" Text="-1"></asp:TextBox>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>


                <div>
                    <asp:Label ID="l1" runat="server" ForeColor="Black" Text="Full Name : " Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtNameAdd" runat="server" Width="299px"></asp:TextBox>
                </div>
                <br>
                <div>
                    <asp:Label ID="l2" runat="server" ForeColor="Black" Text="Nokia User Name : " Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtNokiaNameAdd" runat="server" Width="299px"></asp:TextBox>
                </div>
                <br>
                <div>
                    <asp:Label ID="l3" runat="server" ForeColor="Black" Text="Email Address : " Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtEmailAddressAdd" runat="server" Width="299px" TextMode="Email"></asp:TextBox>

                </div>
                <br>

                <asp:CheckBox ID="CheckBoxIsAdmin" runat="server" Checked="True" Text=" Is Admin " />
                <asp:CheckBox ID="CheckBoxIsActive" runat="server" Text=" Is Active " Checked="false" Visible="false"></asp:CheckBox>
                <br />
                <br />
                <asp:Label ID="lblCheckEmail" runat="server"></asp:Label>
                <br />
                <br />

                <div>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default btnSave"
                        Height="30px" OnClick="btnSave_Click" Text="Save" Width="123px" Font-Bold="True" BackColor="#2471a3" ForeColor="White" />
                    <asp:Button ID="btnCancel" runat="server" Height="30px"
                        Style="margin-left: 31px" Text="Cancel" Width="123px" Font-Bold="True" OnClick="btnCancel_Click" formnovalidate="formnovalidate" />
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <br />
    <asp:GridView ID="gvUsers" runat="server"
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
         OnRowDataBound="gvUsers_RowDataBound"
        OnRowCommand="gvUsers_RowCommand">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Content/icDelete.png" CommandName="deleteuser" CommandArgument='<%#Eval("UserID")%>' OnClientClick="return confirm('Are you sure you want to delete this User?');"
                        AlternateText="Delete" />
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Content/icEdit.png" CommandName="edituser" CommandArgument='<%#Eval("UserID")%>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
            <asp:BoundField DataField="NokiaUserName" HeaderText="Nokia User Name" ItemStyle-Wrap="true">
                <ItemStyle Wrap="True"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EmailAddress" HeaderText="Email" />
            <asp:CheckBoxField DataField="IsAdmin" HeaderText="IS Admin" ReadOnly="true" />
            <asp:CheckBoxField DataField="IsActive" HeaderText="Is Active" ReadOnly="true" />
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
</asp:Content>
