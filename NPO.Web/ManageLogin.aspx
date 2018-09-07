<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLogin.aspx.cs" Inherits="NPO.Web.ManageLogin" %>

<!DOCTYPE html>

<html lang="en">
<head>

  <meta charset="UTF-8">

  <title>Login </title>

  <link rel="stylesheet" href="Content/reset.css">
    <link rel="stylesheet" href="Content/style.css" media="screen" type="text/css" />

    <style type="text/css">
        .auto-style1 {
            height: 208px;
        }

        </style>

</head>
<body style="width: 460px; margin-left: 440px; margin-right: 0px; margin-top: 150px;">
<form id="form1" runat="server">

    <div class="image"></div>   
    <div class="wrap">
        
		<input type="text" placeholder="Email Address" required runat="server" id="txtEmailAddress">
		
		<input type="text" placeholder="Password" required runat="server" id="txtPassword" class="auto-style1">

	
        <asp:Button ID="btnLogin" runat="server"  BackColor="#7aa2ca" Text="Login" Width="202px"
                     CssClass="btn btn-default btnLogin" Font-Bold="True"
                    Font-Size="Small" ForeColor="Black" Height="33px" OnClick="btnLogin_Click" />
	</div>

    
</form>

    </body>
</html>



