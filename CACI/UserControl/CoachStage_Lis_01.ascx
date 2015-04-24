<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoachStage_Lis_01.ascx.cs"
    Inherits="CoachStage_Lis_01" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<table class="table_lightYellow" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
    border="1" width="100%">
    <tr>
        <td class="lightyellow_tb_title_more">
            階段紀錄
        </td>
        <td class="lightyellow_tb_text_more" colspan="3">
            <asp:Label ID="lbl_ChSg_Text" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>