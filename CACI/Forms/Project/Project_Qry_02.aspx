<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master" AutoEventWireup="true" CodeFile="Project_Qry_02.aspx.cs" Inherits="Project_Qry_02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" Runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="QueryConditionContent" Runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">範本名稱: </td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_PjSamples" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_AddProjectBySample" runat="server" 
        ImageUrl="~/image/btn_AddProjectBySample.png" 
        onclick="btn_AddProjectBySample_Click" />
    &nbsp;
    <asp:ImageButton ID="btn_AddAllowanceProject" runat="server" 
        ImageUrl="~/image/btn_AddAllowanceProject.png" 
        onclick="btn_AddAllowanceProject_Click" />
    &nbsp;
    <asp:ImageButton ID="btn_AddCoachProject" runat="server" 
        ImageUrl="~/image/btn_AddCoachProject.png" 
        onclick="btn_AddCoachProject_Click" />
    </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="QueryControlContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="QueryResultContent" Runat="Server">
    
</asp:Content>

