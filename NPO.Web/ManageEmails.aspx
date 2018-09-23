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
            <td>
                <asp:Label ID="Label4" runat="server" Text="Reference :" Style="color: #000000"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmailRef" runat="server" Width="150px"></asp:TextBox>
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
                <asp:TextBox ID="txtEmailDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEmailDate" Format="MM-dd-yyyy" />
            </td>


            <td>
                <asp:Label ID="Label5" runat="server" Text="Status:" Style="color: #000000"></asp:Label>

            </td>
            <td>
                <asp:DropDownList ID="ddlAssign" runat="server" Width="100%">
                    <asp:ListItem Value="-1">--Select Status--</asp:ListItem>

                    <asp:ListItem Value="0">New</asp:ListItem>

                    <asp:ListItem Value="1">Assigned</asp:ListItem>

                    <asp:ListItem Value="2">InProgress</asp:ListItem>

                    <asp:ListItem Value="3">Closed</asp:ListItem>

                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>

            <td>
                <asp:Button ID="Button1" runat="server" Text="Search" Width="120px"
                    CssClass="btn btn-default btnSearch" OnClick="btnSearch_Click" />

            </td>
        </tr>

    </table>
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
        PopupControlID="PanalAssign"
          BackgroundCssClass="ModalPopupBG"
        >
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanalAssign" runat="server" BackColor="White" CssClass="PanalAssignStyle" Height="400px" Width="532px">
        <asp:ImageButton ID="btnCancelAssign" runat="server" ImageUrl="~/Content/icExit.png" CssClass="PanelBodyStylebutton" OnClick="btnCancelAssign_Click"/>
        <asp:TextBox ID="txtEmailId" style="display:none" runat="server" Text="-1"></asp:TextBox>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="zebra">
                    <tr>
                        <td>Controllers:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlContrcoller" Width="300px">
                               <asp:ListItem Value="0">"--Select Controller--"</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtConId" style="display:none" runat="server" Text="-1"></asp:TextBox>
                                    <asp:Button ID="AddController" runat="server" Text="Add" OnClick="AddController_Click" />
                        </td>
                    </tr>
                   
                </table>
                <br />
                <asp:Repeater ID="RepeaterController" runat="server">
                    <HeaderTemplate>
                        <table class="zebra">
                            <tr style="font-weight: bold;">
                   
                                <td>Controller Name</td>
                                <td>Delete</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            
        
                           
                            <td><%#Eval("ControllerName")%></td>
                           <td><asp:ImageButton ID="DeleteController" runat="server" ImageUrl="~/Content/icExit.png" OnClick="DeleteController_Click" CommandArgument='<%#Eval("EmailControllerId")+ "," +Eval("ControllerId")%>'/> 

                           </td> 
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

    <asp:Button ID="btnExAddUsers" runat="server" Style="display:none" />
    <ajaxToolkit:ModalPopupExtender 
        runat="server" 
        BehaviorID="btnExAddUsers_ModalPopupExtender"
         TargetControlID="btnExAddUsers" 
        ID="btnExAddUsers_ModalPopupExtender"
        PopupControlID="PanalAddUsers"
         BackgroundCssClass="ModalPopupBG"

        >

    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanalAddUsers" runat="server" BackColor="White" CssClass="PanalAssignStyle" Height="400px" Width="532px">
        <asp:ImageButton ID="btnCancelAssignUsers" runat="server" ImageUrl="~/Content/icExit.png" CssClass="PanelBodyStylebutton" OnClick="btnCancelAssign_Click" />        

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>

                <table class="zebra">
                    <tr>
                        <td>Users:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlUsers" Width="300px">
                               <asp:ListItem Value="0">"--Select User--"</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEId" style="display:none" runat="server" Text="-1"></asp:TextBox>
                                    <asp:Button ID="AddUserEmail" runat="server" Text="Add" OnClick="AddUserEmail_Click" />
                        </td>
                    </tr>
                   
                </table>
                <br />
                <asp:Repeater ID="RepeaterUsersEmail" runat="server">
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
                           <td><asp:ImageButton ID="DeleteUserEmail" runat="server" ImageUrl="~/Content/icExit.png" OnClick="DeleteUserEmail_Click" CommandArgument='<%#Eval("EmailUserId")%>'/> 

                           </td> 
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btnAssign1" runat="server" OnClick="btnAssign1_Click" Text="Assign" />
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
                    <asp:ImageButton ID="btnAssign" runat="server" ImageUrl='<%#(bool)Eval("IsAssign") ? "~/Content/icAssigned.png":"~/Content/icNotAssign.png" %>' CommandName="Assign"
                        CommandArgument='<%# Eval("EmailId").ToString()%>'
                        AlternateText="Body" ToolTip="Assign" />
                      
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField>
                <ItemTemplate>
                     <asp:ImageButton ID="btnUser" runat="server" ImageUrl="~/Content/icAddUser.png" CommandName="UserAdd" 
                        CommandArgument='<%#Eval("EmailId") %>' />

                      
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnStatus" runat="server" ImageUrl='<%# Eval("Status").ToString() == "InProgress" ? "~/Content/icInProgress.png":"~/Content/icNotAssign.png" %>' CommandName="changestatus"
                        CommandArgument='<%# Eval("EmailId").ToString()+","+ Eval("EmailStatus").ToString()%>'
                        AlternateText="Body" ToolTip="Inprogress" />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:BoundField DataField="DateTimeReceived" HeaderText="Date Received" DataFormatString="{0:dd-MM-yyyy hh:mm tt }" />

            <asp:BoundField DataField="From" HeaderText="From" />

            <asp:BoundField DataField="Subject" HeaderText="Subject" />

            
            <asp:BoundField DataField="To" HeaderText="To" />

            <asp:BoundField DataField="CC" HeaderText="CC" />

             <asp:BoundField DataField="Status" HeaderText="Status" />

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnBody" runat="server" ImageUrl="~/Content/icRightArrow.png" CommandName="showBody" CommandArgument='<%#Eval("EmailId")%>'
                        AlternateText="Body" Width="20px" Height="20px" ToolTip="View body" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/Content/icDownload.png" CommandName="download"
                        CommandArgument='<%#Eval("EmailId")%>' Width="20px" Height="20px" ToolTip="Download Email"  />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <%--<asp:ImageButton ID="btnchild" runat="server" ImageUrl="~/Content/icReply.png" CommandName="replies"
                        CommandArgument='<%#Eval("EmailId")%>' Width="20px" Height="20px" ToolTip="Children Email" />--%>

                    <asp:HyperLink ID="lnk" ImageUrl="~/Content/icReply.png" runat="server" NavigateUrl='<%# "ManageChildrenEmails.aspx?EmailId="+ Eval("EmailId") %>' Target="_blank">
                        
                    </asp:HyperLink>
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
