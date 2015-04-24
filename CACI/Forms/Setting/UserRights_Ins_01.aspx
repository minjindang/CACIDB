<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="UserRights_Ins_01.aspx.cs" Inherits="UserRights_Ins_01" %>

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
            <td class="title_2c">用戶代號<font style="color: Red">*</font></td>
            <td class="text_2c" style="color:Red">
                <kd:StrTextBox ID="txt_User_Code" runat="server" DB_Length="10" title="用戶代號" isAllowNull="false" Width="90%"></kd:StrTextBox>
            </td>
            <td class="title_2c"></td>
            <td class="text_2c"></td>
        </tr>
        <tr>
            <td class="title_2c">所屬單位<font style="color: Red">*</font></td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_UsDp_Code" runat="server" title="所屬單位" DataTextField="UsDp_Name" DataValueField="UsDp_Code" isAllowNull="false">
                </kd:CoDropDownList>
            </td>
            <td class="title_2c">人員姓名<font style="color: Red">*</font></td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_User_Name" runat="server" DB_Length="50" title="人員姓名" isAllowNull="false" Width="90%"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">帳號狀態</td>
            <td class="text_2c" style="color:Red">
                <asp:CheckBox ID="chk_User_AcStatus" runat="server" />&nbsp;<font style="color:Red">(鎖定)</font>
            </td>
            <td class="title_2c">用戶等級</td>
            <td class="text_2c">
                <asp:CheckBox ID="chk_User_Level" runat="server" />&nbsp;<font style="color:Red">(系統管理者)</font>
            </td>
        </tr>
        <tr>
            <td class="title_2c">聯絡電話</td>
            <td class="text_2c">
                 <kd:StrTextBox ID="txt_User_Tel" runat="server" DB_Length="20" title="連絡電話" isAllowNull="true" Width="90%"></kd:StrTextBox>
            </td>
            <td class="title_2c">聯絡手機: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_User_Cell" runat="server" DB_Length="10" title="聯絡手機" isAllowNull="true" Width="90%" ></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">電子郵件</td>
            <td class="text_more" colspan="3">
                <kd:EmailTextBox ID="txt_User_Mail" runat="server" DB_Length="100" title="電子郵件" isAllowNull="true" Width="90%" ></kd:EmailTextBox>
            </td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height:300px" >
        <tr>
            <td style="width:45%; height:15px; background-color:#C5E2F9; border-top:1px solid #989898; border-left:1px solid #989898; border-right:1px solid #989898; ">程式清單列表</td>
            <td style="width:10%; height:15px;"></td>
            <td style="width:45%; height:15px; background-color:#C5E2F9; border-top:1px solid #989898; border-left:1px solid #989898; border-right:1px solid #989898; ">已設定程式</td>
        </tr>
        <tr>
            <td style="vertical-align:top;" >
                <asp:ListBox ID="lis_Program" runat="server" SelectionMode="Multiple" DataTextField="Prog_Name" DataValueField="Prog_Num" Width="100%" Height="285px"></asp:ListBox>
            </td>
            <td style="vertical-align:middle; text-align:center;" >
                <asp:Button ID="btn_Forward" runat="server" Text="加入 >>" 
                    onclick="btn_Forward_Click" />
                <br />
                <asp:Button ID="btn_Retreat" runat="server" Text="取消 <<" 
                    onclick="btn_Retreat_Click" />
            </td>
            <td style="vertical-align:top;" >
                <asp:ListBox ID="lis_SaveProgram" runat="server" SelectionMode="Multiple" DataTextField="Prog_Name" DataValueField="Prog_Num" Width="100%" Height="285px"></asp:ListBox>
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
