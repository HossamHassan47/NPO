<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageChildrenEmails.aspx.cs" Inherits="NPO.Web.ManageChildrenEmails" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="btnExtendBody" runat="server" Style="display: none" />


    <ajaxToolkit:ModalPopupExtender runat="server"
         BehaviorID="btnExtendBody_ModalPopupExtender" 
        TargetControlID="btnExtendBody" 
        ID="btnExtendBody_ModalPopupExtender"
        PopupControlID="PanalBody" >

    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanalBody" runat="server" BackColor="White" CssClass="PanelBodyStyle">

        <asp:ImageButton ID="CancelBody" runat="server" ImageUrl="~/Content/icExit.png" CssClass="PanelBodyStylebutton" />
    </asp:Panel>
  
    
    
         <asp:GridView ID="gvChEmails" runat="server"
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
        OnRowDataBound="gvChEmails_RowDataBound"
        EmptyDataText="No Data Found"
        OnRowCommand="gvChEmails_RowCommand">

        <AlternatingRowStyle BackColor="#DCDCDC" />
        <PagerStyle CssClass="pagination-ys" />

        <Columns>

           

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
                        CommandArgument='<%#Eval("EmailId")%>' Width="20px" Height="20px" ToolTip="Download Email" />
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
</asp:Content>
