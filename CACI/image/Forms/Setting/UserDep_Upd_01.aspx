<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="UserDep_Upd_01.aspx.cs" Inherits="UserDep_Upd_01" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" Runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InsertContent" Runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">單位代碼: </td>
            <td class="text_2c" style="color:Red">
                <asp:Label ID="lbl_UsDp_Code" runat="server" ></asp:Label>
            </td>
            <td class="title_2c">單位名稱: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_UsDp_Name" runat="server" DB_Length="100" title="單位名稱" Width="90%"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">有效: </td>
            <td class="text_2c">
                 <asp:CheckBox ID="chk_UsDp_IsUsd" runat="server" />
            </td>
            <td class="title_2c"></td>
            <td class="text_2c"></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
                &nbsp;
                <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
                &nbsp;
                <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
            </td>
        </tr>
    </table>
</asp:Content>

