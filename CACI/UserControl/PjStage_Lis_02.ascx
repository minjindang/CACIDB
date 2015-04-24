<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PjStage_Lis_02.ascx.cs"
    Inherits="PjStage_Lis_02" %>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <table class="table_lightblue" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
                border="1" width="100%">
                <tr>
                    <td class="lightblue_tb_title_more">階段名稱<font style="color: Red">*</font>&nbsp;
                    </td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Stage_Name" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="lightblue_tb_title_more">階段順序<font style="color: Red">*</font>&nbsp;
                    </td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Stage_Index" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="lightblue_tb_title_more">執行日期</td>
                    <td class="lightblue_tb_text_more" colspan="3">
                        <asp:Label ID="lbl_Stage_Date" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="lightblue_tb_title_more">階段類型</td>
                    <td class="lightblue_tb_text_more" colspan="3">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rad_Stage_Kind_1" Text="起始階段" runat="server" GroupName="rad_Stage_Kind" Enabled="false" />
                                </td>
                                <td>
                                    <asp:RadioButton ID="rad_Stage_Kind_2" Text="中間階段(執行日期)" runat="server" GroupName="rad_Stage_Kind" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lbl_Stage_Days" runat="server" Text=""></asp:Label>
                                </td>
                                <td>天 </td>
                                <td>
                                    <asp:RadioButton ID="rad_Stage_Kind_3" Text="結束階段" runat="server" GroupName="rad_Stage_Kind" Enabled="false" />
                                </td>
                                <td>
                                    <asp:RadioButton ID="rad_Stage_Kind_4" Text="管考階段" runat="server" GroupName="rad_Stage_Kind" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="lightblue_tb_title_more">階段內容說明<font style="color: Red">*</font>&nbsp;
                    </td>
                    <td class="lightblue_tb_text_more" colspan="3">
                        <asp:Label ID="lbl_Stage_Text" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="lightblue_tb_title_more">是否召開會議 </td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Stage_IsMeeting" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="lightblue_tb_title_more">會議性質 </td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Stage_MtKind" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="lightblue_tb_title_more">是否提醒</td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Stage_RmFlag" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="lightblue_tb_title_more">提醒人員 </td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Stage_RmEmpl" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="lightblue_tb_title_more">提前提醒天數</td>
                    <td class="lightblue_tb_text_more">
                        <asp:Label ID="lbl_Stage_RmDays" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="lightblue_tb_title_more"></td>
                    <td class="lightblue_tb_text_more"></td>
                </tr>
                <tr>
                    <td class="lightblue_tb_title_more">提醒文稿</td>
                    <td class="lightblue_tb_text_more" colspan="3">
                        <asp:Label ID="lbl_Stage_RmText" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
