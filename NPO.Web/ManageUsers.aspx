<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="NPO.Web.ManageUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>




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
                <asp:CheckBox ID="isadmin" runat="server" Width="142px" Text="Is Admin " Checked="false" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="18px"></asp:CheckBox>
            </td>

            <td style="height: 45px">
                <asp:CheckBox ID="isactive" runat="server" Width="300px" Text=" Is Active " Checked="true" Font-Size="Small" ForeColor="Black"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td></td>
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
        CssClass="btn btn-default btnAddNew" Font-Bold="True"
        Font-Size="Small" ForeColor="Black" Height="33px" OnClick="btnAdd_Click" />



    <ajaxToolkit:ModalPopupExtender
        ID="btnAdd_ModalPopupExtender" runat="server"
        BehaviorID="btnAdd_ModalPopupExtender"
        TargetControlID="btnAdd"
        PopupControlID="PanelAdd"
        BackgroundCssClass="ModalPopupBG">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Label ID="DoneOrNot" runat="server"></asp:Label>

    <br />
    <asp:Panel ID="PanelAdd" runat="server" Style="text-align: center; background-color: rgb(255, 251, 251); margin-top: 60px; border-radius: 30px 30px 30px 30px; margin-bottom: 0px;"
        Width="497px" Height="330px " BackColor="#999999" BorderColor="#666666" BorderStyle="Solid">

        <div style="height: 30px; background-color: #2471a3; border-radius: 4000px 4000px 200px 200px; width: 100%; align-content: center" />
        <asp:Label ID="labelUser" runat="server" ForeColor="White" Text="Add User " Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>
        <br />
        <asp:TextBox ID="txtid" runat="server" Visible="false" Text="-1"></asp:TextBox>
        <div>
            <tr>
                <td style="font-size: small; color: #000000; font-style: normal; font-weight: bold; width: 174px; height: 70px;">
                    <asp:Label ID="l1" runat="server" ForeColor="Black" Text="Full Name : " Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <td style="width: 329px; height: 70px;">
                <asp:TextBox ID="txtNameAdd" runat="server" Width="299px"></asp:TextBox>
            </td>
        </div>
        <br>

        <div>
            <tr>
                <td style="font-size: small; color: #000000; font-style: normal; font-weight: bold; width: 174px; height: 70px;">
                    <asp:Label ID="l2" runat="server" ForeColor="Black" Text="Nokia User Name : " Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 329px; height: 70px;">
                    <asp:TextBox ID="txtNokiaNameAdd" runat="server" Width="299px"></asp:TextBox>
                </td>
            </tr>
        </div>
        <br>
        <div>
            <tr>
                <td style="font-size: small; color: #000000; font-style: normal; font-weight: bold; width: 174px; height: 70px;">
                    <asp:Label ID="l3" runat="server" ForeColor="Black" Text="Email Address : " Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 329px; height: 70px;">
                    <asp:TextBox ID="txtEmailAddressAdd" runat="server" Width="299px"></asp:TextBox>
                </td>
            </tr>
        </div>
        <br>
        <div>
            <tr>
                <td style="font-size: small; color: #000000; font-style: normal; font-weight: bold; width: 174px; height: 70px;">
                    <asp:Label ID="l4" runat="server" ForeColor="Black" Text="Password : " Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 329px; height: 70px;">
                    <asp:TextBox ID="txtPasswordAdd" runat="server" Width="299px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
        </div>
        <br>
        <asp:CheckBox ID="CheckBoxIsAdmin" runat="server" Checked="True" Text=" Is Admin " />
        <asp:CheckBox ID="CheckBoxIsActive" runat="server" Text=" Is Active " Checked="false" Visible="false"></asp:CheckBox>

        <br />
        <br />
        <div>
            <asp:Button ID="btnSave" runat="server" CssClass="col-md-offset-0"
                Height="30px" OnClick="btnSave_Click" Text="Save" Width="123px" Font-Bold="True" BackColor="#2471a3" ForeColor="White" />
            <asp:Button ID="btnCancel" runat="server" Height="30px"
                Style="margin-left: 31px" Text="Cancel" Width="123px" Font-Bold="True" OnClick="btnCancel_Click" />
        </div>

    </asp:Panel>
    <br />
    <br />


    <asp:GridView ID="gvUsers" runat="server" CellPadding="7" GridLines="Vertical" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvUsers_RowDataBound"
        BorderWidth="1px" AllowCustomPaging="True" BackColor="White" BorderColor="#999999"
        BorderStyle="Solid" PageSize="20" AllowPaging="True"
        CellSpacing="2" HorizontalAlign="Center" OnRowCommand="gvUsers_RowCommand">
        <AlternatingRowStyle BackColor="#E5E8E8" />

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
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />

    </asp:GridView>



</asp:Content>
