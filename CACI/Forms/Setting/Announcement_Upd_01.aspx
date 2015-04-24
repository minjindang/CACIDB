<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Announcement_Upd_01.aspx.cs" Inherits="Announcement_Upd_01" %>

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
            <td class="title_2c">公告代號: </td>
            <td class="text_2c" style="color:Red">
                <asp:Label ID="lbl_Ann_Code" runat="server" Text=""></asp:Label>
            </td>
            <td class="title_2c">公告日期: </td>
            <td class="text_2c">
                <kd:DateTextBox ID="txt_Ann_BgnTime" runat="server" title="公告起始日期" DateType="Taiwan" isAllowNull="false"></kd:DateTextBox>
                ~
                <kd:DateTextBox ID="txt_Ann_EndTime" runat="server" title="公告結束日期" DateType="Taiwan" isAllowNull="false"></kd:DateTextBox>
                
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_Ann_BgnTime" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" >
                </asp:CalendarExtender>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_Ann_EndTime" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" >
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="title_2c">公告類型: </td>
            <td class="text_2c" colspan="3" >
                <kd:CoDropDownList ID="ddl_Ann_Type"  runat="server" title="公告類型" DataTextField="Sys_CdText" DataValueField="Sys_CdCode" Enabled="false" >
                </kd:CoDropDownList>
            </td>
            <!--<td class="title_2c">所屬專案: </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_Pj_Code" runat="server" DataTextField="Pj_Name" DataValueField="Pj_Code" title="所屬專案" isAllowNull="true">
                </kd:CoDropDownList>
            </td>
            -->
        </tr>
        <tr>
            <td class="title_2c">公告對象: </td>
            <td class="text_more" colspan="3">
                <asp:CheckBoxList ID="chk_Ann_Taker" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="申辦單位" Value="1"></asp:ListItem>
                    <asp:ListItem Text="承辦人" Value="1"></asp:ListItem>
                    <asp:ListItem Text="顧問人員" Value="1"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">公告主旨: </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_Ann_Name" runat="server" DB_Length="50" title="公告主旨" Width="95%" ></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">公告內容: </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_Ann_Text" runat="server" DB_Length="1000" title="公告內容" TextMode="MultiLine" Width="95%" Height="100px" ></kd:StrTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="server">
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
