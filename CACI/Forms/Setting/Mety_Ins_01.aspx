<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Mety_Ins_01.aspx.cs" Inherits="Mety_Ins_01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InsertContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">會議類別代碼: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Mety_Code" runat="server" DB_Length="2" title="會議類別代碼" isAllowNull="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">專案類別: </td>
            <td class="text_2c" style="color:Red">
                <kd:CoDropDownList ID="ddl_Pj_Kind" runat="server" title="專案類別" DataTextField="Sys_CdText" DataValueField="Sys_CdCode" isAllowNull="true">
                </kd:CoDropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">會議類別名稱: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Mety_Name" runat="server" DB_Length="100" title="單位名稱" isAllowNull="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">可否自行新增: </td>
            <td class="text_2c">
                 <asp:CheckBox ID="chk_Can_Add" runat="server" />
            </td>
            
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="server">
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
