<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="UserPwd_Upd_01.aspx.cs" Inherits="UserPwd_Upd_01" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" Runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InsertContent" Runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">單位名稱:</td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_ComName" runat="server" ></asp:Label>
            </td>
            <td class="title_2c">登入帳號:</td>
            <td class="text_2c">
                <asp:Label ID="lbl_Account" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">名稱:</td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_Name" runat="server" ></asp:Label>
                <asp:HiddenField ID="hid_Comm_Code" runat="server" />
                <asp:HiddenField ID="hid_Type" runat="server" />
                <asp:HiddenField ID="hid_Account" runat="server" />
                <asp:HiddenField ID="hid_Email" runat="server" />
            </td>
            <td class="title_2c">狀態:</td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_Comm_AcStatus" runat="server">
                    <asp:ListItem Text="正常" Value=""></asp:ListItem>
                    <asp:ListItem Text="鎖定" Value="L"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
            <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_StateUpdate.png" />
                &nbsp;
                <asp:ImageButton ID="btn_PwdReset" runat="server" 
                    ImageUrl="~/image/btn_PwdRset.png" onclick="btn_PwdReset_Click" />
                &nbsp;  
                <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
                &nbsp;
                <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
            </td>
        </tr>
    </table>
</asp:Content>

