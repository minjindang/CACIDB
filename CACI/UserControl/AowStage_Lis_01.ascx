<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AowStage_Lis_01.ascx.cs"
    Inherits="AowStage_Lis_01" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<table class="table_lightYellow" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
    border="1" width="100%">
    <tr>
        <td class="lightyellow_tb_title_more" colspan="4">
            階段內容
            <asp:HiddenField ID="hf_Pj_Code" runat="server" />
            <asp:HiddenField ID="hf_Aow_Code" runat="server" />
            <asp:HiddenField ID="hf_Stage_Index" runat="server" />
            <asp:HiddenField ID="hf_IsNew" runat="server" Value="Y" />
        </td>
    </tr>
    <%--<tr>
        <td class="lightyellow_tb_title_more">
            起始時間
        </td>
        <td class="lightyellow_tb_text_more" colspan="3">
            <asp:Label ID="lbl_Stage_Date" runat="server" Text=""></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td class="lightyellow_tb_title_more">
            階段內容說明
        </td>
        <td class="lightyellow_tb_text_more" colspan="3">
            <asp:Label ID="lbl_Stage_Text" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="lightyellow_tb_title_more" rowspan="2">
            階段紀錄
        </td>
        <td class="lightyellow_tb_text_more">
            <asp:Label ID="lbl_AwSg_Date" runat="server" Text=""></asp:Label>
        </td>
        <td class="lightyellow_tb_title_more">
            審核結果
        </td>
        <td class="lightyellow_tb_text_more">
            <asp:Label ID="lbl_AwSg_Verify" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="lightyellow_tb_text_more" colspan="3">
            <asp:Label ID="lbl_AwSg_Text" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<asp:Panel runat="server" ID="paMeetingInfo" Visible="false">
    <table class="table_lightYellow" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
        border="1" width="100%">
        <tr>
            <td class="lightyellow_tb_title_more" colspan="2">
                會議內容
            </td>
        </tr>
        <%--<tr>
            <td class="lightyellow_tb_title_more">
                會議記錄
            </td>
            <td class="lightyellow_tb_text_more">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td class="lightyellow_tb_title_more">
                評審委員
            </td>
            <td class="lightyellow_tb_text_more">
                <kd:MDGridView ID="grv_Committee" runat="server" AutoGenerateColumns="False" Width="100%"
                    BorderWidth="1px" DataKeyNames="Aow_Code,Meeting_Code,Meeting_Index ,Comm_Code" CssClass="table_lightblue"
                    Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                    LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                    <Columns>
                        <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                        <asp:BoundField DataField="Comm_Name" HeaderStyle-Font-Bold="false" HeaderText="評審委員"
                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                        </asp:BoundField>
                    </Columns>
                </kd:MDGridView>
                <%--<asp:GridView ID="grv_Committee" runat="server" AutoGenerateColumns="False" Width="100%"
                    BorderWidth="1px" DataKeyNames="" CssClass="table_lightblue" Style="margin-top: 10px">
                    <Columns>
                        <asp:BoundField DataField="Comm_Name" HeaderStyle-Font-Bold="false" HeaderText="評審委員"
                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>--%>
            </td>
        </tr>
    </table>
</asp:Panel>
