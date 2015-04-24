<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="SM1011M.aspx.cs" Inherits="SM1011M" %>
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
            <td class="title_2c">Mcol_1 :</td>
            <td class="text_2c">
                <asp:Label ID="lbl_Mcol_1" runat="server" />
            </td>
            <td class="title_2c">Mcol_2</td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Mcol_2" runat="server" DB_Length="10" title="Mcol_2" Width="24" ></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">Mcol_3 :</td>
            <td class="text_2c">
                <kd:NumTextBox ID="txt_Mcol_3" runat="server" title="Mcol_3" DB_IntLength="10" DB_DecLength="0" Width="40"></kd:NumTextBox>
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

