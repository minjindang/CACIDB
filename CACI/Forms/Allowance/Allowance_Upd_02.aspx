﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="Allowance_Upd_02.aspx.cs" Inherits="Allowance_Upd_02" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" style="background: white" cellpadding="0" cellspacing="0"
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        單位(公司)基本資料
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>單位(公司)名稱:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" title="單位(公司)名稱"
                                    isAllowNull="false"></kd:StrTextBox>
                                <asp:Label ID="lbl_Com_Code" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbl_Aow_Code" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td class="title_2c">
                                <span class="style1">*</span>立案字號:
                            </td>
                            <td class="text_2c">
                                <kd:CoIDNumTextBox ID="txt_Com_Tonum" runat="server" title="立案字號" isAllowNull="false"></kd:CoIDNumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                資本額:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Capital" runat="server" DB_Length="20" title="資本額"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">
                                年營業額
                            </td>
                            <td class="text_2c">
                                <kd:NumTextBox ID="txt_Com_LTover" runat="server" title="年營業額" DB_DecLength="0" DB_IntLength="10"></kd:NumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>組織形式:
                            </td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_Com_OrgForm" runat="server" title="組織形式">
                                </kd:CoDropDownList>
                            </td>
                            <td class="title_2c">
                                <span class="style1">*</span>負責人:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Boss" runat="server" DB_Length="50" title="負責人" isAllowNull="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>負責人電話:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_BsTel" runat="server" DB_Length="20" title="負責人電話" isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">
                                <span class="style1">*</span>聯絡人:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttName" runat="server" DB_Length="50" title="聯絡人" isAllowNull="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>聯絡人電話:
                            </td>
                            <td class="text_more">
                                <kd:StrTextBox ID="txt_Com_CttTel" runat="server" DB_Length="20" title="聯絡人電話" isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">
                                傳真:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Fax" runat="server" DB_Length="20" title="傳真"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                公司登記所在地:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="up_Com_PostCode" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <kd:CoDropDownList ID="ddl_Com_PostCode" runat="server" title="公司登記所在地" isAllowNull="true">
                                        </kd:CoDropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>Email:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_Email" runat="server" DB_Length="100" Width="95%" isAllowNull="false"
                                    title="Email"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>地址:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_OPAddr" runat="server" DB_Length="200" Width="95%" title="地址"
                                    isAllowNull="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                網址:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_Url" runat="server" DB_Length="200" Width="95%" title="網址"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>主要產品及服務:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_MnProduct" runat="server" DB_Length="500" title="主要產品及服務"
                                    isAllowNull="false" Width="95%" TextMode="MultiLine" Columns="5" Height="50px"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>產業別:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:RadioButtonList ID="rbl_Com_MnSectors" RepeatColumns="2" RepeatDirection="Horizontal"
                                    runat="server">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                登入帳號:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Account" runat="server" DB_Length="20" title="登入帳號" isAllowNull="true"
                                    Enabled="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">
                                登入密碼:
                            </td>
                            <td class="text_2c">
                                <asp:TextBox ID="txt_Com_Pass" runat="server" TextMode="Password" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <table class="table_gray" style="background: white" cellpadding="0" cellspacing="0"
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        計畫資料
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>計畫名稱:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_ApPj_Name" runat="server" DB_Length="200" Width="95%" title="計畫名稱"
                                    isAllowNull="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>實施期程:
                            </td>
                            <td class="text_2c">
                                <kd:DateTextBox ID="txt_ApPj_BgnDate" runat="server" Width="60" DateType="Taiwan"
                                    isAllowNull="false" title="實施期程(起)"></kd:DateTextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_ApPj_BgnDate"
                                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                                </asp:CalendarExtender>
                                ~
                                <kd:DateTextBox ID="txt_ApPj_EndDate" runat="server" Width="60" DateType="Taiwan"
                                    isAllowNull="false" title="實施期程(迄)"></kd:DateTextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_ApPj_EndDate"
                                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                                </asp:CalendarExtender>
                            </td>
                            <td class="title_2c">
                                <span class="style1">*</span>商品類別:
                            </td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_ApPj_ProdCls" runat="server" title="商品類別">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>申請組別:
                            </td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_ApPj_ApGroup" runat="server" title="申請組別">
                                </kd:CoDropDownList>
                            </td>
                            <td class="title_2c">
                                <span class="style1">*</span>總經費:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_ApPj_TotAmt" runat="server" DB_Length="50" title="總經費" isAllowNull="false"></kd:StrTextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_ApPj_TotAmt"
                                    DisplayMoney="None" MaskType="Number" Mask="9,999,999">
                                </asp:MaskedEditExtender>
                                千元
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>申請補助:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_ApPj_AowAmt" runat="server" DB_Length="50" title="申請補助" isAllowNull="false"></kd:StrTextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_ApPj_AowAmt"
                                    DisplayMoney="None" MaskType="Number" Mask="9,999,999">
                                </asp:MaskedEditExtender>
                                千元
                            </td>
                            <td class="title_2c">
                                其他經費來源:
                            </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_ApPj_OthAmt" runat="server" DB_Length="100" title="其他經費來源"></kd:StrTextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_ApPj_OthAmt"
                                    DisplayMoney="None" MaskType="Number" Mask="9,999,999">
                                </asp:MaskedEditExtender>
                                千元
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                <span class="style1">*</span>計畫內容摘要:
                            </td>
                            <td class="text_more" colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            一、計畫目標
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text_more">
                                            <kd:StrTextBox ID="txt_ApPj_Goal" runat="server" DB_Length="1000" title="計畫目標" isAllowNull="false"
                                                Width="95%" TextMode="MultiLine" Columns="5" Height="50px"></kd:StrTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            二、實施策略及方法
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text_more">
                                            <kd:StrTextBox ID="txt_ApPj_Policies" runat="server" Columns="5" DB_Length="1000"
                                                Height="50px" TextMode="MultiLine" title="實施策略及方法" Width="95%" isAllowNull="false"></kd:StrTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            三、預期效益及成果評估指標
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text_more">
                                            <kd:StrTextBox ID="txt_ApPj_Profit" runat="server" Columns="5" isAllowNull="false"
                                                DB_Length="500" Height="50px" TextMode="MultiLine" title="預期效益及成果評估指標" Width="95%"></kd:StrTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            四、計畫限制條件及解決構想
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text_more">
                                            <kd:StrTextBox ID="txt_ApPj_Solution" runat="server" Columns="5" isAllowNull="false"
                                                DB_Length="500" Height="50px" TextMode="MultiLine" title="計畫限制條件及解決構想" Width="95%"></kd:StrTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailInsertContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <div style="margin-top: 2px; margin-left: 16px; position: absolute; float: left;
        background-color: White; padding-left: 2px;">
        <div style="float: left;">
            <img src="/CACI/image/blueBall.jpg" />
        </div>
        <div style="float: left; margin: 0px 3px 0px 3px;">
            過去三年接受補助情形
        </div>
    </div>
    <div class="table_detail_info">
        <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table class="table_lightblue" border="1" width="100%">
                        <tr>
                            <td class="lightblue_tb_title_more">
                                獲補助年度
                            </td>
                            <td class="lightblue_tb_text_more">
                                <kd:StrTextBox ID="txt_Aas_Year" runat="server" DB_Length="3" Width="95%" title="獲補助年度"
                                    ValGroup="grvQuery"></kd:StrTextBox>
                            </td>
                            <td class="lightblue_tb_title_more">
                                &nbsp;受補助計畫名稱
                            </td>
                            <td class="lightblue_tb_text_more">
                                <kd:StrTextBox ID="txt_Aas_PjName" runat="server" DB_Length="100" Width="95%" title="受補助計畫名稱"
                                    ValGroup="grvQuery"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lightblue_tb_title_more">
                                補助機關
                            </td>
                            <td class="lightblue_tb_text_more">
                                <kd:StrTextBox ID="txt_Aas_PjUnit" runat="server" DB_Length="100" Width="95%" title="補助機關"
                                    ValGroup="grvQuery"></kd:StrTextBox>
                            </td>
                            <td class="lightblue_tb_title_more">
                                補助金額
                            </td>
                            <td class="lightblue_tb_text_more">
                                <kd:StrTextBox ID="txt_Aas_Amount" runat="server" DB_Length="50" title="補助金額" isAllowNull="true"
                                    ValGroup="grvQuery"></kd:StrTextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txt_Aas_Amount"
                                    DisplayMoney="None" MaskType="Number" Mask="9,999,999">
                                </asp:MaskedEditExtender>
                                千元
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailControlContent" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <!-- Detail按鈕區 -->
                <asp:ImageButton ID="btnDTL_INSERT" runat="server" ImageUrl="~/image/dtl_Insert.png" />
                &nbsp;
                <asp:ImageButton ID="btnDTL_UPDATE" runat="server" ImageUrl="~/image/dtl_Update.png" />
                &nbsp;
                <asp:ImageButton ID="btnDTL_CLEAR" runat="server" ImageUrl="~/image/dtl_Clear.png" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="DetailDataContent" runat="Server">
    <!-- Detail 資料列式區 -->
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
                                    其他團隊成員資料
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
                            <asp:Panel ID="pnlGridView" runat="server" Width="100%">
                                <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="Aas_PjName" CssClass="table_lightblue" 
                                    Style="margin-top: 10px" onrowdatabound="grvQuery_RowDataBound">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                                            <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                            <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="Aas_Year" HeaderStyle-Font-Bold="false" HeaderText="獲補助年度"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Aas_PjName" HeaderStyle-Font-Bold="false" HeaderText="受補助計畫名稱"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Aas_PjUnit" HeaderStyle-Font-Bold="false" HeaderText="補助機關"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Aas_Amount" HeaderStyle-Font-Bold="false" HeaderText="補助金額"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsExists" HeaderStyle-Font-Bold="false" HeaderText="是否存在"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <table>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
