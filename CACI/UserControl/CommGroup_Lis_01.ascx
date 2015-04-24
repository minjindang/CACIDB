<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommGroup_Lis_01.ascx.cs"
    Inherits="CommGroup_Lis_01" %>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <table class="table_lightblue" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
                border="1" width="100%">
                <tr>
                    <td class="lightblue_tb_title_more">
                        組別代號<font style="color: Red">*</font>&nbsp;
                    </td>
                    <td class="lightblue_tb_text_more" colspan="3">
                        <asp:Label ID="lbl_CmGp_NumName" runat="server"></asp:Label>
                    </td>
                    <td class="lightblue_tb_title_more">
                        組別名稱
                    </td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_CmGp_Name" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
