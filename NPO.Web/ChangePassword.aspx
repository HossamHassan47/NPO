<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="NPO.Web.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label runat="server" ID="l1" Text="Old password"></asp:Label>
            <br />
            <input type="password" placeholder="Old Password" runat="server" id="txtOldPassword" onkeydown = "return (event.keyCode!=13);" />
            <br />
        <asp:Label runat="server" ID="l2" Text="New password"></asp:Label>

    <br />
            <input type="password" placeholder="New password" runat="server" id="txtNewPassword" onkeydown = "return (event.keyCode!=13);">

            <br />

     <asp:Label runat="server" ID="l3" Text="Confirm new password"></asp:Label>

    <br />
            <input type="password" placeholder="Confirm new password" runat="server" id="txtConPassword" onkeydown = "return (event.keyCode!=13);">

            <br />

    <asp:Button ID="btnDone" Text="Done" runat="server" OnClick="btnDone_Click" />

    <asp:Label Text ="" ID="ErrorMsg" runat="server" ></asp:Label>
</asp:Content>
