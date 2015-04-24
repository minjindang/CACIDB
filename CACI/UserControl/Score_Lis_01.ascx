<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Score_Lis_01.ascx.cs"
    Inherits="Score_Lis_01" %>
<table class="table_lightPink" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
    border="1" width="100%">
    <tr>
        <td class="lightpink_tb_title_more">最大值</td>
        <td class="lightpink_tb_text_more">
            <asp:Label ID="lbl_Score_Max" runat="server"></asp:Label>
        </td>
        <td class="lightpink_tb_title_more">配分比</td>
        <td class="lightpink_tb_text_more">
            <asp:Label ID="lbl_Score_Percent" runat="server"></asp:Label>% </td>
    </tr>
</table>
