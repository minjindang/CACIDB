<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="Allowance_Upd_04.aspx.cs" Inherits="Allowance_Upd_04" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
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
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="text_more" colspan="4">
                單位(公司)基本資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>申請人:
            </td>
            <td class="text_2c">
                        <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="50" title="申請人" isAllowNull="false"></kd:StrTextBox>
                        <asp:Label ID="lbl_Com_Code" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_Aow_Code" runat="server" Visible="false"></asp:Label>
            </td>
            <td class="title_2c">
                <span class="style1">*</span>身份字號:
            </td>
            <td class="text_2c">
                        <kd:IDNumTextBox ID="txt_Com_Tonum" runat="server" title="身分證字號"  isAllowNull="false"></kd:IDNumTextBox>

            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>團體名稱:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Aow_GPName" runat="server" DB_Length="50" title="團體名稱" isAllowNull="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                <span class="style1">*</span>團體立案字號:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Aow_RegNum" runat="server" DB_Length="30" title="團體立案字號" isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                代表人:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Aow_FMan" runat="server" DB_Length="50" title="代表人"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                代表身份證字號:
            </td>
            <td class="text_2c">
                <kd:IDNumTextBox ID="txt_Aow_FMIDNO" runat="server" title="代表身份證字號"></kd:IDNumTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                產業別:
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
                <kd:StrTextBox ID="txt_Com_Account" runat="server" DB_Length="20" Enabled="false" title="登入帳號"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                登入密碼:
            </td>
            <td class="text_2c">
                <asp:TextBox ID="txt_Com_Pass" runat="server" TextMode="Password" Enabled="false" title="登入密碼"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text_more" colspan="4">
                計劃人資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>計劃主持人:
            </td>
            <td class="text_more">
                <kd:StrTextBox ID="txt_Aow_PJPM" runat="server" DB_Length="50" title="計劃主持人" isAllowNull="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                <span class="style1">*</span>計劃主持人電話:
            </td>
            <td class="text_more">
                <kd:StrTextBox ID="txt_Aow_PMTel" runat="server" DB_Length="20" title="計劃主持人電話" isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>計劃聯絡人:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_CttName" runat="server" DB_Length="50" title="計劃聯絡人" isAllowNull="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                <span class="style1">*</span>計劃聯絡人電話:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_CttTel" runat="server" DB_Length="20" title="計劃聯絡人電話" isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                計劃聯絡人傳真:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Fax" runat="server" DB_Length="20" title="計劃聯絡人傳真"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                <span class="style1">*</span>計劃聯絡人EMAIL:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_CttMail" runat="server" DB_Length="20" title="計劃聯絡人EMAIL" isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>地址:
            </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_Com_OPAddr" runat="server" DB_Length="200" Width="95%" title="地址" isAllowNull="false"></kd:StrTextBox>
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
            <td class="text_more" colspan="4">
                計劃資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>計劃名稱:
            </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_ApPj_Name" runat="server" DB_Length="200" Width="95%" title="計畫名稱" isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>計劃摘要:
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
                            <kd:StrTextBox ID="txt_ApPj_Goal" runat="server" DB_Length="1000" title="計畫目標" Width="95%"
                                TextMode="MultiLine" Columns="5" Height="50px" isAllowNull="false"></kd:StrTextBox>
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
                            <kd:StrTextBox ID="txt_ApPj_Profit" runat="server" Columns="5" DB_Length="500" Height="50px"
                                TextMode="MultiLine" title="預期效益及成果評估指標" Width="95%" isAllowNull="false"></kd:StrTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            四、計畫限制條件及解決構想
                        </td>
                    </tr>
                    <tr>
                        <td class="text_more">
                            <kd:StrTextBox ID="txt_ApPj_Solution" runat="server" Columns="5" DB_Length="500"
                                Height="50px" TextMode="MultiLine" title="計畫限制條件及解決構想" Width="95%" isAllowNull="false"></kd:StrTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>總經費:
            </td>
            <td class="text_2c">
                <kd:NumTextBox ID="txt_ApPj_TotAmt" runat="server" DB_IntLength="10" DB_DecLeng="0" isAllowNull="false"
                    title="總經費"></kd:NumTextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_ApPj_TotAmt" DisplayMoney="None" MaskType="Number" Mask="9,999,999" >
                </asp:MaskedEditExtender>
                千元
            </td>
            <td class="title_2c">
                <span class="style1">*</span>補助經費:
            </td>
            <td class="text_2c">
                <kd:NumTextBox ID="txt_ApPj_AowAmt" runat="server" DB_IntLength="10" DB_DecLeng="0" isAllowNull="false"
                    title="補助經費"></kd:NumTextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_ApPj_AowAmt" DisplayMoney="None" MaskType="Number" Mask="9,999,999" >
                </asp:MaskedEditExtender>
                千元
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                其他經費來源:
            </td>
            <td class="text_more" colspan="3">
                <kd:NumTextBox ID="txt_ApPj_OthAmt" runat="server" DB_IntLength="10" DB_DecLeng="0" isAllowNull="true"
                    title="其他經費來源"></kd:NumTextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_ApPj_OthAmt" DisplayMoney="None" MaskType="Number" Mask="9,999,999" >
                </asp:MaskedEditExtender>
                千元
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailInsertContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <div style="float: left; margin-top: 2px; margin-left: 16px; position: absolute;
        float: left; background-color: White; padding-left: 2px;">
        <div style="float: left;">
            <img src="/CACI/image/blueBall.jpg" />
        </div>
        <div style="float: left; margin: 0px 3px 0px 3px;">
            其他團隊成員資料
        </div>
    </div>
    <div class="table_detail_info">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table class="table_lightblue" border="1" width="100%">
                                <tr>
                                    <td class="lightblue_tb_title_more">
                                        受補助計畫名稱
                                    </td>
                                    <td class="lightblue_tb_text_more" colspan="3">
                                        <kd:StrTextBox ID="txt_Aas_PjName" title="受補助計畫名稱" runat="server" DB_Length="100" Width="95%" ValGroup="grvQuery"></kd:StrTextBox>
                                        <asp:Label ID="lbl_IsExists" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Com_Account" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Com_Pass" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Com_Code2" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lightblue_tb_title_more">
                                        姓名
                                    </td>
                                    <td class="lightblue_tb_text_more">
                                        <kd:StrTextBox ID="txt_Com_Name2" runat="server" DB_Length="100" Width="95%" title="姓名"
                                            ValGroup="grvQuery"></kd:StrTextBox>
                                    </td>
                                    <td class="lightblue_tb_title_more">
                                        身份證字號
                                    </td>
                                    <td class="lightblue_tb_text_more">
                                        <kd:IDNumTextBox ID="txt_Com_Tonum2" runat="server" ValGroup="grvQuery" title="身份證字號"
                                            AutoPostBack="true" ontextchanged="txt_Com_Tonum2_TextChanged"></kd:IDNumTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lightblue_tb_title_more">
                                        性別
                                    </td>
                                    <td class="lightblue_tb_text_more">
                                        <asp:RadioButtonList ID="rbl_Com_BsGender" runat="server" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="lightblue_tb_title_more">
                                        聯絡電話
                                    </td>
                                    <td class="lightblue_tb_text_more">
                                        <kd:StrTextBox ID="txt_Com_BsTel" runat="server" DB_Length="20" title="聯絡電話" Width="95%" ValGroup="grvQuery"></kd:StrTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lightblue_tb_title_more">
                                        地址
                                    </td>
                                    <td class="lightblue_tb_text_more" colspan="3">
                                        <kd:StrTextBox ID="txt_Com_OPAddr2" runat="server" title="地址" DB_Length="200" Width="95%" ValGroup="grvQuery"></kd:StrTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lightblue_tb_title_more">
                                        身分證影本
                                    </td>
                                    <td class="lightblue_tb_text_more" colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                                    BorderWidth="1px" DataKeyNames="Com_Tonum" CssClass="table_lightblue" Style="margin-top: 10px">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                                            <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                            <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="Aas_PjName" HeaderStyle-Font-Bold="false" HeaderText="受補助計畫名稱"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="姓名"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Com_Tonum" HeaderStyle-Font-Bold="false" HeaderText="身份證字號"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Com_BsGender" HeaderStyle-Font-Bold="false" HeaderText="性別"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Com_BsTel" HeaderStyle-Font-Bold="false" HeaderText="聯絡電話"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Com_OPAddr" HeaderStyle-Font-Bold="false" HeaderText="地址"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Com_Code" HeaderStyle-Font-Bold="false" HeaderText="Com_Code"
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
