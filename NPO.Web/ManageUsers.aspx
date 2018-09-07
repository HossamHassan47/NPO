﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="NPO.Web.ManageUsers" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <table class="zebra">

        <tr>
            <td style="font-size: small; color: #000000; font-style: normal; font-weight: bold; width: 174px; height: 70px;">
                <asp:Label ID="Label2" runat="server" Text="Nokia User Name : " ForeColor="Black"></asp:Label>
            </td>

            <td style="width: 329px; height: 70px;">
                <asp:TextBox ID="txtMemberNokiaUserName" runat="server" Width="299px"></asp:TextBox>
            </td>
            <td style="font-size: small; color: #000000; font-style: normal; font-weight: bold; width: 436px; height: 70px;">
                <asp:Label ID="Label4" runat="server" Text="Full Name : " ForeColor="Black"></asp:Label>
                <asp:TextBox ID="txtFullName" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td style="height: 70px">&nbsp;</td>

        </tr>
        <tr>
            <td style="font-size: small; color: #000000; font-style: normal; font-weight: bold; width: 174px; height: 45px;">
                <asp:Label ID="Label3" runat="server" Text="Email Address : " ForeColor="Black"></asp:Label>
            </td>
            <td style="height: 45px">
                <asp:TextBox ID="TxtEmailAddress" runat="server" Width="300px"></asp:TextBox>
            </td>

            <td style="height: 45px; width: 436px">
                <asp:CheckBox ID="isadmin" runat="server" Width="142px" Text="Is Admin " Checked="True" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="18px"></asp:CheckBox>
            </td>

            <td style="height: 45px">
                <asp:CheckBox ID="isactive" runat="server" Width="300px" Text=" Is Active " Checked="True" Font-Size="Small" ForeColor="Black"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td style="width: 174px"></td>
            <td></td>
            <td style="color: #000000; font-size: small; font-style: normal; font-weight: bold; width: 436px;">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="203px"
                    OnClick="btnSearch_Click" CssClass="btn btn-default btnSearch" Font-Bold="True"
                    Font-Size="Small" ForeColor="Black" Height="33px" />
            </td>


        </tr>


    </table>

    <br />
    <asp:Button ID="btnAdd" runat="server" Text="ADD" Width="203px"
        OnClick="btnSearch_Click" CssClass="btn btn-default btnAddNew" Font-Bold="True"
        Font-Size="Small" ForeColor="Black" Height="33px" />
    
   

    <ajaxToolkit:ModalPopupExtender 
        ID="btnAdd_ModalPopupExtender" runat="server"
         BehaviorID="btnAdd_ModalPopupExtender" 
         TargetControlID="btnAdd"
        PopupControlID="PanelAdd"
        CancelControlID="btnCancel"
       >
        
    </ajaxToolkit:ModalPopupExtender>
    
   

    <br />
    <asp:Panel ID="PanelAdd" runat="server" style="text-align: center; background-color: #3366FF" Width="500px" Height="300px" >

        Name :<br />
        <asp:TextBox ID="txtNameAdd" runat="server"></asp:TextBox>
        <br />
        Nokia Name:<br />
        <asp:TextBox ID="txtNokiaNameAdd" runat="server"></asp:TextBox>
        <br />
        Email :<br />
        <asp:TextBox ID="txtEmailAddressAdd" runat="server"></asp:TextBox>
       <br />
         Password:<br />
        <asp:TextBox ID="txtPasswordAdd" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox ID="CheckBoxIsAdmin" runat="server"  Text=" Is Admin " Checked="True"></asp:CheckBox>
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" Width="203px" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="203px" />
      
    </asp:Panel>
    <br />



    <asp:GridView ID="gvUsers" runat="server" CellPadding="7" GridLines="Vertical" AutoGenerateColumns="False" Width="1337px"
        BorderWidth="1px" AllowCustomPaging="True" BackColor="White" BorderColor="#999999"
        BorderStyle="Solid" PageSize="20" AllowPaging="true"
        CellSpacing="2" HorizontalAlign="Center"
        Style="margin-right: 728px; margin-left: 0px;" OnRowCommand="gvUsers_RowCommand">
        <AlternatingRowStyle BackColor="#E5E8E8" />
        <PagerSettings Mode="Numeric" PageButtonCount="4" Visible="true" />

        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Content/icSave.png" CommandName="edituser" CommandArgument='<%#Eval("UserID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
            <asp:BoundField DataField="NokiaUserName" HeaderText="Nokia User Name" ItemStyle-Wrap="true">
                <ItemStyle Wrap="True"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EmailAddress" HeaderText="Email" />
            <asp:BoundField DataField="IsAdmin" HeaderText="IS Admin" />
            <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
        </Columns>

        <FooterStyle BackColor="White" ForeColor="Black" BorderStyle="None" />
        <HeaderStyle BackColor="#2471A3" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />

    </asp:GridView>



</asp:Content>
