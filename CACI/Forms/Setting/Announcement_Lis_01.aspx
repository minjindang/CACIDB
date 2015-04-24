<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Announcement_Lis_01.aspx.cs" Inherits="Announcement_Lis_01" %>

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
    <asp:HiddenField ID="hid_From" runat="server" />
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">公告代號: </td>
            <td class="text_2c" style="color:Red">
                <asp:Label ID="lbl_Ann_Code" runat="server" Text=""></asp:Label>
            </td>
            <td class="title_2c">公告日期: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Ann_BgnTime" runat="server" Text=""></asp:Label>
                ~
                <asp:Label ID="lbl_Ann_EndTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">公告類型: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Ann_Type" runat="server" Text=""></asp:Label>
            </td>
            <%--
            <td class="title_2c">所屬專案: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_Code" runat="server" Text=""></asp:Label>
            </td>--%>

        </tr>
        <tr>
            <td class="title_2c">公告對象: </td>
            <td class="text_more" colspan="3">
                <asp:CheckBoxList ID="chk_Ann_Taker" runat="server" RepeatDirection="Horizontal" Enabled="false">
                    <asp:ListItem Text="申辦單位" Value="1"></asp:ListItem>
                    <asp:ListItem Text="承辦人" Value="1"></asp:ListItem>
                    <asp:ListItem Text="顧問人員" Value="1"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">公告主旨: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Ann_Name" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">公告內容: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Ann_Text" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
