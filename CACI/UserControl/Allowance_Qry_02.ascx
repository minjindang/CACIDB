<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Allowance_Qry_02.ascx.cs"
    Inherits="Allowance_Qry_02" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<table class="table_lightYellow" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
    border="1" width="100%">
    <tr>
        <td class="lightyellow_tb_text_more" >
            <asp:GridView ID="grv_Aow" runat="server" AutoGenerateColumns="False" Width="100%"
                BorderWidth="1px" DataKeyNames="Aow_Code,Pj_Code"
                CssClass="table_lightblue" Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png"
                OpenIconPath="~/image/btn_Collapse.png" LoadControlMode="UserControl" TemplateCachingBase="Tablename"
                TemplateDataMode="RunTime">
                <Columns>
                    <asp:BoundField DataField="Stage_Index" HeaderStyle-Font-Bold="false" HeaderText="階段"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AwSg_Text" HeaderStyle-Font-Bold="false" HeaderText="階段項目"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AwSg_Date" HeaderStyle-Font-Bold="false" HeaderText="執行日期"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AwSg_Verify_Text" HeaderStyle-Font-Bold="false" HeaderText="審核結果"
                        ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                    </asp:BoundField>                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>