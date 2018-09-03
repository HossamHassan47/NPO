<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageEmails.aspx.cs" Inherits="NPO.Web.ManageEmails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <table class="zebra">
        <tr>
            <td >
                <span><span style="color: #000000; background-color: #FFFFFF">From</span><span style="color: #000000">:</span></span>
            </td>
            <td>
                <asp:TextBox ID="txtEmailFrom" runat="server" Width="300px" ></asp:TextBox>
            </td>
            <td >
                <asp:Label ID="Label1" runat="server"  Text="To:" style="color: #000000"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtEmailTo" runat="server"  Width="299px"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server"  Text="Subject:" style="color: #000000"></asp:Label>                
            </td>
            <td>
                <asp:TextBox ID="txtEmailSub" runat="server"  Width="299px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server"  Text="Date:" style="color: #000000"></asp:Label>
            </td>
               
               
            <td >
                <asp:DropDownList ID="dateDropDownList" runat="server">
                    <asp:ListItem Text="="  >=</asp:ListItem>
                    <asp:ListItem Text=">=" >>=</asp:ListItem>
                    <asp:ListItem Text="<=" ><=</asp:ListItem>
                    <asp:ListItem Text="<" ><</asp:ListItem>
                    <asp:ListItem Text=">" >></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtEmailDate" runat="server" Width="235px" ></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEmailDate" Format="MM-dd-yyyy"/>
            </td>
           
            
        </tr>
        
        <tr>
            <td>

            </td>
            <td>
                 <asp:Button ID="btnSearch" runat="server"  Text="Search" Width="187px" 
                        OnClick="btnSearch_Click" CssClass="btn btn-default btnSearch"  />
            </td>
        </tr>
    </table>
    

    <br />
    <br />
    <asp:GridView ID="gvEmails" runat="server" CellPadding="3" GridLines="Vertical" AutoGenerateColumns ="False" 
        BorderWidth="1px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" AllowPaging="True" onrowdatabound="gvEmails_RowDataBound"
        EmptyDataText="No Data Found"
        DataSourceID="DsGvEmails"  HeaderStyle-Wrap="false" Width="100%" PageSize="20" >
       
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <PagerStyle CssClass="pagination-ys" />

        <Columns>
             

            <asp:BoundField DataField="DateTimeReceived" HeaderText="Date Received" dataformatstring="{0:dd-MM-yyyy hh:mm tt }" /> 
       
            <asp:BoundField DataField="From" HeaderText="From"/>
            
            <asp:BoundField DataField="Subject" HeaderText="Subject" />
             
            <asp:BoundField DataField="To" HeaderText="To" />
            
            <asp:BoundField DataField="CC" HeaderText="CC"/>
              
        </Columns>
       
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White"  />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center"  />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black"  />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"/> 
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9"  />
        <SortedDescendingCellStyle BackColor="#CAC9C9"   />
        <SortedDescendingHeaderStyle BackColor="#000065" />
       
    </asp:GridView>
    <asp:ObjectDataSource ID="DsGvEmails" 
        EnablePaging="True" runat="server" 
        SelectCountMethod="GetEmailsCount" 
        SelectMethod="GetEmails"
         TypeName="NPO.Code.Repository.EmailRepository" OnSelecting="DsGvEmails_Selecting"
        >
       

    </asp:ObjectDataSource>

       
    
   

</asp:Content>
