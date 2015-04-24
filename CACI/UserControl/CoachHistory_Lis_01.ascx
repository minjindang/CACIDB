<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoachHistory_Lis_01.ascx.cs"
    Inherits="CoachHistory_Lis_01" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<table class="table_lightYellow" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
    border="1" width="100%">
    <tr>
        <td class="lightyellow_tb_title_more">
            會議清單
        </td>
    </tr>
    <tr>
        <td class="lightyellow_tb_text_more" >
            <asp:GridView ID="grv_Meeting" runat="server" AutoGenerateColumns="False" Width="100%"
                BorderWidth="1px" DataKeyNames="Coach_Code,Meeting_Code,Meeting_Index"
                CssClass="table_lightblue" Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png"
                OpenIconPath="~/image/btn_Collapse.png" LoadControlMode="UserControl" TemplateCachingBase="Tablename"
                TemplateDataMode="RunTime">
                <Columns>
                    <asp:BoundField DataField="Meeting_Index" HeaderStyle-Font-Bold="false" HeaderText="會議場次"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Meeting_BgnTime" HeaderStyle-Font-Bold="false" HeaderText="會議時間"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Meeting_Name" HeaderStyle-Font-Bold="false" HeaderText="會議主旨"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Record_Text" HeaderStyle-Font-Bold="false" HeaderText="會議紀錄"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<table class="table_lightYellow" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
    border="1" width="100%">
    <tr>
        <td class="lightyellow_tb_title_more">
            輔導階段紀錄
        </td>
    </tr>
    <tr>
        <td class="lightyellow_tb_text_more" >
            <asp:GridView ID="grv_CoachStage" runat="server" AutoGenerateColumns="False" Width="100%"
                BorderWidth="1px" DataKeyNames="Coach_Code,Pj_Code,Stage_Index"
                CssClass="table_lightblue" Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png"
                OpenIconPath="~/image/btn_Collapse.png" LoadControlMode="UserControl" TemplateCachingBase="Tablename"
                TemplateDataMode="RunTime">
                <Columns>
                    <asp:BoundField DataField="Stage_Index" HeaderStyle-Font-Bold="false" HeaderText="階段順序"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ChSg_Date" HeaderStyle-Font-Bold="false" HeaderText="執行時間"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ChSg_Verify" HeaderStyle-Font-Bold="false" HeaderText="審核結果"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ChSg_Text" HeaderStyle-Font-Bold="false" HeaderText="階段紀錄"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>