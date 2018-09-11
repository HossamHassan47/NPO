<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLogin.aspx.cs" Inherits="NPO.Web.ManageLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html lang="en">
<head>

    <meta charset="UTF-8">

    <title>Login </title>

    <link rel="stylesheet" href="Content/reset.css">
    <link rel="stylesheet" href="Content/style.css" media="screen" type="text/css" />

    <style type="text/css">
        .psw {
            color: white;
            float: none;
            margin-left: 170px;
            color: white;
        }

        psw :hover {
            color: black;
        }
    </style>

</head>

<body runat="server" style="width: 460px; margin-left: 440px; margin-right: 0px; margin-top: 150px;">

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div style="margin-left: 80px;">

            <img src="Content/nokia_white_logo.png" />

        </div>
        <div class="wrap">

            <input type="email" placeholder="Email Address" runat="server" id="txtEmailAddress">
            <asp:RegularExpressionValidator ID="EmailAddressFormatValidator" runat="server"
                ControlToValidate="txtEmailAddress"
                ErrorMessage="RegularExpressionValidator"
                Display="Dynamic"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*"
                EnableClientScript="False">Email Address is not valid.<br />       
            </asp:RegularExpressionValidator>
            <br />
            <input type="password" placeholder="Password" runat="server" id="txtPassword">

            <br />
            <asp:Label ID="ErrorMsg" runat="server" Visible="false" Style="color: red; background-color: white; font-weight: bold;"> </asp:Label>
            <br />
            <br />

            <asp:Button ID="btnLogin" runat="server" BackColor="#7aa2ca" Text="Login" Width="202px"
                CssClass="btn btn-default btnLogin" Font-Bold="True"
                Font-Size="Small" ForeColor="Black" Height="31px" OnClick="btnLogin_Click" />
            <br />
        </div>
        <asp:LinkButton class="psw" runat="server" ID="LinkButton1">Forgot password?</asp:LinkButton>
        <ajaxToolkit:ModalPopupExtender
            ID="LinkButton1_ModalPopupExtender"
            runat="server"
            BehaviorID="LinkButton1_ModalPopupExtender"
            TargetControlID="LinkButton1"
            PopupControlID="ForgetPanal">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="ForgetPanal" runat="server" CssClass="ForgetPanalStyle">

            <div style="height: 30px; background-color: #2471a3; border-radius: 4000px 4000px 200px 200px; width: 100%; align-content: center">
                <asp:Label ID="labelUser" runat="server" ForeColor="White" Text="Forget Password" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            </div>
            <br />
            <br />
            <br />
            <br />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="height: 300px;">
                <ContentTemplate>
                    <asp:Label ID="l1" runat="server" ForeColor="Black" Text="Your Email :  " Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" Width="300px" TextMode="Email" Height="29px" placeholder="Email address"></asp:TextBox>
                  
                    <br />
                    <br />
                    <asp:UpdateProgress runat="server" ID="PageUpdateProgress">

                        <ProgressTemplate>
                            <asp:Image runat="server" ID="ImgProgress" ImageUrl="~/Content/gifProgress.gif" Height="53px" Width="64px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="LabelErrorMsg" runat="server" Visible="false" Style="color: red; background-color: white; font-weight: bold;"> </asp:Label>

                    <br />
                    <br />


                    <asp:Button ID="SendMail" runat="server" Text="Send Mail" CssClass="btnForget" OnClick="SendMail_Click" />


                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:ImageButton ID="Cancel" runat="server" ImageUrl="~/Content/icExit.png" formnovalidate="formnovalidate" Style="float: right" OnClick="Cancel_Click" />


        </asp:Panel>

    </form>


</body>

</html>
