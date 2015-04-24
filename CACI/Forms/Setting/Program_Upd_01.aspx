<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Program_Upd_01.aspx.cs" Inherits="Program_Upd_01" %>
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
            <td class="title_2c">程式代號: </td>
            <td class="text_2c" style="color:Red">
                <asp:Label ID="lbl_Prog_Num" runat="server" ></asp:Label>
            </td>
            <td class="title_2c">程式類型: </td>
            <td class="text_2c">
                <asp:RadioButtonList ID="rad_Prog_Type" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="Q" Selected="True">查詢</asp:ListItem>
                    <asp:ListItem Value="I">新增</asp:ListItem>
                    <asp:ListItem Value="U">更新</asp:ListItem>
                    <asp:ListItem Value="L">查看</asp:ListItem>
                </asp:RadioButtonList>
                
            </td>
        </tr>
        <tr>
            <td class="title_2c">作業類別: </td>
            <td class="text_2c">
                 <kd:CoDropDownList ID="ddl_Prog_Class" runat="server" DataTextField="Sys_CdText" 
                    DataValueField="Sys_CdCode" title="作業類別" isAllowNull="false" ></kd:CoDropDownList>
            </td>
            <td class="title_2c">程式名稱: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Prog_Name" runat="server" DB_Length="200" title="程式名稱" isAllowNull="false" Width="90%" ></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">程式路徑: </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_Prog_Path" runat="server" DB_Length="200" title="程式路徑" isAllowNull="false" Width="90%" ></kd:StrTextBox>
            </td>
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

