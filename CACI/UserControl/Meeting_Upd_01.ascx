<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Meeting_Upd_01.ascx.cs"
    Inherits="Meeting_Upd_01" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<kd:MDGridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
    BorderWidth="1px" DataKeyNames="Meeting_Code,Meeting_Index,Com_Code"
    CssClass="table_lightblue" Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png"
    OpenIconPath="~/image/btn_Collapse.png" LoadControlMode="UserControl" TemplateCachingBase="Tablename"
    TemplateDataMode="RunTime" onrowdatabound="grvQuery_RowDataBound">
    <Columns>
        <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
        <asp:BoundField DataField="Com_Name" HeaderText="申請單位">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Code" HeaderText="申請案號">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Pj_Kind" HeaderText="專案類別">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Pj_Code" HeaderText="專案代碼">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
</kd:MDGridView>
<%--<asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
    BorderWidth="1px" DataKeyNames="Meeting_Code,Meeting_Index,Com_Code" CssClass="table_lightblue" 
    Style="margin-top: 10px" onrowdatabound="grvQuery_RowDataBound">
    <Columns>
        <asp:BoundField DataField="Com_Name" HeaderText="申請單位">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Code" HeaderText="申請案號">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Pj_Kind" HeaderText="專案類別">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Pj_Code" HeaderText="專案代碼">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
--%>