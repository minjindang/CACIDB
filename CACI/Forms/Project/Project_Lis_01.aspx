<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Project_Lis_01.aspx.cs" Inherits="Project_Lis_01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">
                專案名稱:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_Name" runat="server"></asp:Label>
                <asp:Label ID="lbl_Pj_Code" runat="server" Visible="false"></asp:Label>
            </td>
            <td class="title_2c">
                網路申請開放時間:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_BgnDate" runat="server"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                專案啟動日期:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_StartDate" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                是否提供轉件:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_Trans" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                承辦人:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_UserAcc" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                專案經費
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_Fund" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                外網專案說明:
            </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Pj_WebExp" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                專案簡介:
            </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Pj_PjIntro" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                專案註記要點:
            </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Pj_PjNote" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                上傳附件:
            </td>
            <td class="text_more" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="title_2c">申請表單樣式: </td>
            <td class="text_more" colspan="3">
                <kd:CoDropDownList ID="ddl_Pj_PjFill" runat="server" title="申請表單樣式" Enabled="false" isAllowNull="false">
                </kd:CoDropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="評分項目">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail 資料列式區 -->
                            <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" width="100%" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <div style="margin-left: 5px; float: left; background-color: White;">
                                                                    <div style="float: left; padding-left: 3px;">
                                                                        <img src="/CACI/image/yellow_ball.png" />
                                                                    </div>
                                                                    <div style="float: left; padding-left: 3px; font-weight: bold">
                                                                        評分資料列表
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="lblPage" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlGridView" runat="server" Width="100%" CssClass="inScroll">
                                                        <kd:MDGridView ID="grv_Score" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Score_Code" CssClass="table_lightblue" Style="margin-top: 10px"
                                                            CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg" LoadControlMode="UserControl"
                                                            TemplateCachingBase="Tablename" TemplateDataMode="RunTime" 
                                                            ControlColumnWidth="39px">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="Score_Items" HeaderText="項目">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Score_Max" HeaderText="最大值">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Score_Percent" HeaderText="配分比(%)">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
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
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="申請流程">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail2 資料列式區 -->
                            <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" width="100%" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <div style="margin-left: 5px; float: left; background-color: White;">
                                                                    <div style="float: left; padding-left: 3px;">
                                                                        <img src="/CACI/image/yellow_ball.png" />
                                                                    </div>
                                                                    <div style="float: left; padding-left: 3px; font-weight: bold">
                                                                        流程資訊列表
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; position: absolute; float: left; background-color: White;">
                                                                    <asp:Label ID="lblPage2" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                                    <asp:Panel ID="pnlGridView2" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_PjStage" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Pj_Code,Stage_Index" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime"
                                                            OnRowDataBound="grv_PjStage_RowDataBound">
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
                                                                <asp:BoundField DataField="Stage_Kind" HeaderStyle-Font-Bold="false" HeaderText="階段類型"
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
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="評選組別">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail2 資料列式區 -->
                            <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" width="100%" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <div style="margin-left: 5px; float: left; background-color: White;">
                                                                    <div style="float: left; padding-left: 3px;">
                                                                        <img src="/CACI/image/yellow_ball.png" />
                                                                    </div>
                                                                    <div style="float: left; padding-left: 3px; font-weight: bold">
                                                                        評選組別列表
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; position: absolute; float: left; background-color: White;">
                                                                    <asp:Label ID="lblPage3" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                                    <asp:Panel ID="pnlGridView3" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_CommGroup" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Pj_Code" CssClass="table_lightblue" Style="margin-top: 10px"
                                                            CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg" LoadControlMode="UserControl"
                                                            TemplateCachingBase="Tablename" TemplateDataMode="RunTime" >
                                                            <Columns>
                                                                <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="CmGp_NumName" HeaderStyle-Font-Bold="false" HeaderText="組別代號"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CmGp_Name" HeaderStyle-Font-Bold="false" HeaderText="組別名稱"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CmGp_Num" HeaderStyle-Font-Bold="false" HeaderText="組別代號"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
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
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
