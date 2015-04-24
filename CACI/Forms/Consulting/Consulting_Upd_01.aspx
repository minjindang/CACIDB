<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Consulting_Upd_01.aspx.cs" Inherits="Consulting_Upd_01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" style="background: white; margin-top: 10px" cellpadding="0"
        cellspacing="0" border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        單位(公司)基本資料</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                單位(公司)名稱:
                            </td>
                            <td class="text_2c" style="color: Red">
                                <asp:HiddenField ID="hid_Com_Code" runat="server" />
                                <asp:Label ID="lbl_Com_Name" runat="server"></asp:Label>
                            </td>
                            <td class="title_2c">
                                統編/身份證字號/立案號:
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
                            <td class="title_2c">
                                聯絡人姓名:
                            </td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Com_CttName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                聯絡人公司電話:
                            </td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Com_CttTel" runat="server"></asp:Label>
                            </td>
                            <td class="title_2c">
                                聯絡人e-mail:
                            </td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Com_CttMail" runat="server"></asp:Label>
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
                        諮詢內容</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                申請諮詢時間:
                            </td>
                            <td class="text_2c">
                                <asp:HiddenField ID="hid_Cnst_Code" runat="server" />
                                <asp:Label ID="lbl_twRec_Info" runat="server"></asp:Label>
                            </td>
                            <td class="title_2c">
                                詢問方式:
                            </td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Cnst_CntWay" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                詢問類別:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:RadioButtonList ID="ckl_CntClass_Code" runat="server" title="詢問類別" RepeatColumns="5"
                                    RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                詢問內容:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Cnst_CntText" runat="server" DB_Length="50" title="詢問內容" isAllowNull="false"
                                    TextMode="MultiLine" Width="95%" Height="50px"></kd:StrTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <table class="table_gray" style="background: white; margin-top: 10px" cellpadding="0"
        cellspacing="0" border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        諮詢回覆</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                時間:
                            </td>
                            <td class="text_2c">
                                <kd:DateTextBox runat="server" ID="txt_CtRepl_Date" title="時間" DateType="Taiwan"
                                    isAllowNull="false"> </kd:DateTextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_CtRepl_Date"
                                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                                </asp:CalendarExtender>
                            </td>
                            <td class="title_2c">
                                回覆方式:
                            </td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_CtRepl_RpWay" runat="server" title="回覆方式" RepeatDirection="Horizontal"
                                    isAllowNull="false">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                回覆內容:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_CtRepl_RpText" runat="server" DB_Length="1000" title="回覆內容"
                                    isAllowNull="false" TextMode="MultiLine" Width="95%" Height="50px" ValGroup="grvQuery"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                處理結果:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:CoDropDownList ID="ddl_CtRepl_RpResult" runat="server" title="處理結果" RepeatDirection="Horizontal"
                                    isAllowNull="true">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <asp:UpdatePanel ID="up_Tab" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="諮詢回覆歷程">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="up_Tab1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                                   
                                    <tr>
                                        <td align="center">
                                            <!-- Detail按鈕區 -->
                                            <asp:ImageButton ID="btnDTL_INSERT" runat="server" Visible="false" ImageUrl="~/image/dtl_Insert.png" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnDTL_UPDATE" runat="server" Visible="false" ImageUrl="~/image/dtl_Update.png" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnDTL_CLEAR" runat="server" Visible="false" ImageUrl="~/image/dtl_Clear.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="br">
                                        </td>
                                    </tr>
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
                                                                                        諮詢回覆歷程
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
                                                                        <kd:MDGridView ID="grv_CntReply" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                            BorderWidth="1px" DataKeyNames="CtRepl_Code" CssClass="table_lightblue" Style="margin-top: 10px"
                                                                            CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime"
                                                                            ControlColumnWidth="39px">
                                                                            <Columns>
                                                                                <kd:ChildGridViewColumn>
                                                                                </kd:ChildGridViewColumn>
                                                                                <asp:BoundField DataField="CtRepl_Date" HeaderText="回覆時間">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="CtRepl_RpWay" HeaderText="回覆方式">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle BackColor="#DEE9FC" />
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
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="電話紀錄歷程">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="up_Tab3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center">
                                            <!-- Detail按鈕區 -->
                                            <asp:ImageButton ID="btnDTL_INSERT2" runat="server" Visible="false" ImageUrl="~/image/dtl_Insert.png" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnDTL_UPDATE2" runat="server" Visible="false" ImageUrl="~/image/dtl_Update.png" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnDTL_CLEAR2" runat="server" Visible="false" ImageUrl="~/image/dtl_Clear.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="br">
                                        </td>
                                    </tr>
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
                                                                                        電話紀錄歷程
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
                                                                        <kd:MDGridView ID="grv_Phone" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                            BorderWidth="1px" DataKeyNames="Case_Code,PhRec_Code" CssClass="table_lightblue"
                                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                                            <Columns>
                                                                                <kd:ChildGridViewColumn>
                                                                                </kd:ChildGridViewColumn>
                                                                                <asp:BoundField DataField="PRcRp_Date" HeaderStyle-Font-Bold="false" HeaderText="紀錄時間"
                                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="CntClass_Code" HeaderStyle-Font-Bold="false" HeaderText="提問類別"
                                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PRcRp_Handle" HeaderStyle-Font-Bold="false" HeaderText="處理方式"
                                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle BackColor="#DEE9FC" />
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
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
