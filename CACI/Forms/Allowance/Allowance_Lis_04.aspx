<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Allowance_Lis_04.aspx.cs" Inherits="Allowance_Lis_04" %>

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
            <td class="text_more" colspan="4">
                單位(公司)基本資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                申請人:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Name" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                身份字號:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Tonum" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                團體名稱:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Aow_GPName" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                團體立案字號:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Aow_RegNum" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                代表人:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Aow_FMan" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                代表身份證字號:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Aow_FMIDNO" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                產業別:
            </td>
            <td class="text_more" colspan="3">
                <asp:RadioButtonList ID="rbl_Com_MnSectors" RepeatColumns="2" RepeatDirection="Horizontal"
                    runat="server" Enabled="false">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                登入帳號:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Account" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                登入密碼:
            </td>
            <td class="text_2c">
                <asp:TextBox ID="txt_Com_Pass" runat="server" TextMode="Password" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text_more" colspan="4">
                計劃人資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                計劃主持人:
            </td>
            <td class="text_more">
                <asp:Label ID="lbl_Aow_PJPM" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                計劃主持人電話:
            </td>
            <td class="text_more">
                <asp:Label ID="lbl_Aow_PMTel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                計劃聯絡人:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_CttName" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                計劃聯絡人電話:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_CttTel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                計劃聯絡人傳真:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Fax" runat="server"></asp:Label>
            </td>
            <td class="title_2c">
                計劃聯絡人EMAIL:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_CttMail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                地址:
            </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Com_OPAddr" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                網址:
            </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Com_Url" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text_more" colspan="4">
                計劃資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                計劃名稱:
            </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_ApPj_Name" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                計劃摘要:
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
                            <asp:Label ID="lbl_ApPj_Goal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            二、實施策略及方法
                        </td>
                    </tr>
                    <tr>
                        <td class="text_more">
                            <asp:Label ID="lbl_ApPj_Policies" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            三、預期效益及成果評估指標
                        </td>
                    </tr>
                    <tr>
                        <td class="text_more">
                            <asp:Label ID="lbl_ApPj_Profit" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            四、計畫限制條件及解決構想
                        </td>
                    </tr>
                    <tr>
                        <td class="text_more">
                            <asp:Label ID="lbl_ApPj_Solution" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                總經費:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_ApPj_TotAmt" runat="server"></asp:Label>元
            </td>
            <td class="title_2c">
                補助經費:
            </td>
            <td class="text_2c">
                <asp:Label ID="lbl_ApPj_AowAmt" runat="server"></asp:Label>元
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                其他經費來源:
            </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_ApPj_OthAmt" runat="server"></asp:Label>
                元</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="其他團隊成員資料">
            <ContentTemplate>
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
                                                    <asp:BoundField DataField="Aas_PjName" HeaderText="受補助計畫名稱">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Com_Name" HeaderText="姓名">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Com_Tonum" HeaderText="身份證字號">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Com_BsGender" HeaderText="性別">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Com_BsTel" HeaderText="聯絡電話">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Com_OPAddr" HeaderText="地址">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Com_Code" HeaderText="Com_Code">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
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
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="流程資訊列表">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
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
                                                                        <img src="/CACI/image/yellow_ball.png" /></div>
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
                                                    <asp:Panel ID="pnlGridView2" runat="server" Width="737px" CssClass="detail_result">
                                                        <kd:MDGridView ID="grv_AowStage" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Pj_Code,Aow_Code,Stage_Index" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn>
                                                                </kd:ChildGridViewColumn>
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
                                                                <asp:BoundField DataField="Stage_Date" HeaderStyle-Font-Bold="false" HeaderText="執行日期"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="AwSg_Verify" HeaderStyle-Font-Bold="false" HeaderText="階段狀態"
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
    </asp:TabContainer>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
