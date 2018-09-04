<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLogin.aspx.cs" Inherits="NPO.Web.ManageLogin" %>

<!DOCTYPE html>

<html lang="en">
<head>

  <meta charset="UTF-8">

  <title>Login </title>

  <link rel="stylesheet" href="Content/reset.css">
    <link rel="stylesheet" href="Content/style.css" media="screen" type="text/css" />

</head>
<body>
<form id="form1" runat="server">

    <div class="wrap">
		
		<input type="text" placeholder="Email Address" required runat="server" id="txtEmailAddress">
		<div class="bar">
			<i></i>
		</div>

		<input type="password" placeholder="password" required runat="server" id="txtPassword">

	
        <asp:Button ID="btnLogin" runat="server"  BackColor="#3FDED5" Text="Login" Width="242px"
                     CssClass="btn btn-default btnLogin" Font-Bold="True"
                    Font-Size="Small" ForeColor="Black" Height="33px" OnClick="btnLogin_Click" />
	    <asp:Label ID="Label1" runat="server" Text="Invalid Email Address"></asp:Label>
	</div>


</form>
    </body>
</html>


