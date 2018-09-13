<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageEmails.aspx.cs" Inherits="NPO.Web.ManageEmails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table class="zebra">
        <tr>
            <td>
                <span><span style="color: #000000; background-color: #FFFFFF">From</span><span style="color: #000000">:</span></span>
            </td>
            <td>
                <asp:TextBox ID="txtEmailFrom" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="To:" Style="color: #000000"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmailTo" runat="server" Width="300px"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Subject:" Style="color: #000000"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmailSub" runat="server" Width="299px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Date:" Style="color: #000000"></asp:Label>
            </td>


            <td>
                <asp:DropDownList ID="dateDropDownList" runat="server">
                    <asp:ListItem Text="=">=</asp:ListItem>
                    <asp:ListItem Text=">=">>=</asp:ListItem>
                    <asp:ListItem Text="<="><=</asp:ListItem>
                    <asp:ListItem Text="<"><</asp:ListItem>
                    <asp:ListItem Text=">">></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtEmailDate" runat="server" Width="235px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEmailDate" Format="MM-dd-yyyy" />
            </td>
             <td>  <asp:Button ID="Button1" runat="server" Text="Search" Width="120px"
                    CssClass="btn btn-default btnSearch" OnClick="btnSearch_Click" />

            </td>

        </tr>
       
    </table>


    <br />
    <br />
    <asp:Button ID="btnExtendBody" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender
        runat="server"
        BehaviorID="btnExtendBody_ModalPopupExtender"
        TargetControlID="btnExtendBody"
        ID="btnExtendBody_ModalPopupExtender"
        PopupControlID="PanalBody"
        CancelControlID="CancelBody">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanalBody" runat="server" BackColor="White" CssClass="PanelBodyStyle">
        <asp:ImageButton ID="CancelBody" runat="server" ImageUrl="~/Content/icExit.png" CssClass="PanelBodyStylebutton" />
    </asp:Panel>


    <asp:Button ID="btnPanalAssign" runat="server" Style="display: none" />


    <ajaxToolkit:ModalPopupExtender
        runat="server"
        BehaviorID="btnPanalAssign_ModalPopupExtender"
        TargetControlID="btnPanalAssign"
        ID="btnPanalAssign_ModalPopupExtender"
        PopupControlID="PanalAssign"  BackgroundCssClass="ModalPopupBG"
        CancelControlID="btnCancelAssign">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanalAssign" runat="server" BackColor="White" CssClass="PanalAssignStyle" Height="400px">
        <asp:ImageButton ID="btnCancelAssign" runat="server" ImageUrl="~/Content/icExit.png" CssClass="PanelBodyStylebutton" />
        <asp:TextBox ID="txtEmailId" style="display:none" runat="server" Text="-1"></asp:TextBox>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="zebra">
                    <tr>
                        <td>Tehnology:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="DropDownListTech" Width="300px"
                                OnSelectedIndexChanged="DropDownListTech_SelectedIndexChanged" AutoPostBack="true" Style="margin-top: 20px">
                                <asp:ListItem Value="0">--Select Technology--</asp:ListItem>
                                <asp:ListItem Value="2">2G</asp:ListItem>
                                <asp:ListItem Value="3">3G</asp:ListItem>
                                <asp:ListItem Value="4">4G(LTE)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Controller:</td>
                        <td>
                            <asp:DropDownList ID="DropDownListControllers" runat="server" Width="300px"
                                OnSelectedIndexChanged="DropDownListControllers_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">"--Select Controller--"</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Repeater ID="RepeaterAssignUsers" runat="server">
                    <HeaderTemplate>
                        <table class="zebra">
                            <tr style="font-weight: bold;">
                                <td>User Name</td>
                                <td>User Email</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("FullName")%></td>
                            <td><%#Eval("EmailAddress")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button runat="server" Text="Assign" ID="btnAssign"  OnClick="btnAssign_Click"/>
    </asp:Panel>


    <asp:GridView ID="gvEmails" runat="server"
         CellPadding="3" GridLines="Vertical" 
        AutoGenerateColumns="False" Width="100%"    
        BorderWidth="1px" 
        AllowCustomPaging="True" 
        BackColor="White" 
        BorderColor="#999999"
        BorderStyle="Solid" 
        PageSize="20" 
        AllowPaging="True"
        CellSpacing="2" 
        HorizontalAlign="Center" 
         OnRowDataBound="gvEmails_RowDataBound"
        EmptyDataText="No Data Found"
        DataSourceID="DsGvEmails" 
         OnRowCommand="gvEmail_RowCommand">

        <AlternatingRowStyle BackColor="#DCDCDC" />
        <PagerStyle CssClass="pagination-ys" />

        <Columns>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnAssign" runat="server" ImageUrl='<%#Eval("ControllerId").ToString() == ""? "~/Content/icNotAssign.png":"~/Content/icAssigned.png" %>' CommandName="Assign"
                         CommandArgument='<%# Eval("EmailId").ToString() +"," + Eval("ControllerId").ToString() +"," + Eval("TechnologyId").ToString() %>'
                        AlternateText="Body" ToolTip="Assign" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="DateTimeReceived" HeaderText="Date Received" DataFormatString="{0:dd-MM-yyyy hh:mm tt }" />

            <asp:BoundField DataField="From" HeaderText="From" />

            <asp:BoundField DataField="Subject" HeaderText="Subject" />


            <asp:BoundField DataField="To" HeaderText="To" />

            <asp:BoundField DataField="CC" HeaderText="CC" />

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnBody" runat="server" ImageUrl="~/Content/icRightArrow.png" CommandName="showBody" CommandArgument='<%#Eval("EmailId")%>'
                        AlternateText="Body" Width="20px" Height="20px" ToolTip="View body" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/Content/icDownload.png" CommandName="download" 
                        CommandArgument='<%#Eval("EmailId")%>'  Width="20px" Height="20px" ToolTip="Download Email" />
                </ItemTemplate>
            </asp:TemplateField>



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
    <asp:ObjectDataSource ID="DsGvEmails"
        EnablePaging="True" runat="server"
        SelectCountMethod="GetEmailsCount"
        SelectMethod="GetEmails"
        TypeName="NPO.Code.Repository.EmailRepository" OnSelecting="DsGvEmails_Selecting"></asp:ObjectDataSource>
</asp:Content>
