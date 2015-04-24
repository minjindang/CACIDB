<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Coach_Upd_02.aspx.cs" Inherits="Coach_Upd_02" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="text_more" colspan="4">
                單位(公司)基本資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                單位(公司)名稱:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Name" runat="server"></asp:Label>
                <asp:Label ID="lbl_Com_Code" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbl_Coach_Code" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbl_Pj_Code" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="title_2c">
                稅編/身份證字號/立案號:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Tonum" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                單位(公司)負責人:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Boss" runat="server"></asp:Label>
            </td>
            <td class="text_2c" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                申請輔導項目:
            </td>
            <td class="text_more">
                <asp:Label ID="lbl_ChKd_Name" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                專案名稱:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_Name" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                    <tr>
                        <td>
                            <div style="margin-top: 10px; margin-left: 16px; position: absolute; float: left;
                                background-color: White; padding-left: 2px;">
                                <div style="float: left;">
                                    <img src="/CACI/image/blueBall.jpg" />
                                </div>
                                <div style="float: left; margin: 0px 3px 0px 3px;">
                                    階段資訊列表
                                </div>
                            </div>
                            <div style="float: left; position: absolute; margin: 0px 10px 0px 680px; background-color: white;
                                padding: 10px 0px 0px 6px;">
                                <asp:Label ID="lblPage" runat="server"></asp:Label>&nbsp;&nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0px" cellspacing="1px" width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" CssClass="detail_result">
                                <kd:MDGridView ID="grv_CoachStage" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="Pj_Code,Coach_Code,Stage_Index" CssClass="table_lightblue" Style="margin-top: 10px"
                                    CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png" LoadControlMode="UserControl"
                                    TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                    <Columns>
                                        <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                        <asp:BoundField DataField="Stage_Index" HeaderStyle-Font-Bold="false" HeaderText="階段順序"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Stage_Name" HeaderStyle-Font-Bold="false" HeaderText="階段名稱"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CoachStage_Date" HeaderStyle-Font-Bold="false" HeaderText="執行日期"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ChSg_Verify" HeaderStyle-Font-Bold="false" HeaderText="階段狀態"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                    </Columns>
                                </kd:MDGridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
