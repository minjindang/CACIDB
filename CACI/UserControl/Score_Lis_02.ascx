<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Score_Lis_02.ascx.cs"
    Inherits="Score_Lis_02" %>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <table class="table_lightblue" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
                border="1" width="100%">
                <tr>
                    <td class="lightblue_tb_title_more">評分項目<font style="color: Red">*</font>&nbsp;
                    </td>
                    <td class="lightblue_tb_text_more" colspan="3">
                        <asp:Label ID="lbl_Score_Items" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="lightblue_tb_title_more">最大值</td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Score_Max" runat="server"></asp:Label>
                    </td>
                    <td class="lightblue_tb_title_more">配分比</td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Score_Percent" runat="server"></asp:Label>%
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
