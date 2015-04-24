<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Meeting_Upd_01.aspx.cs" Inherits="Meeting_Upd_01" %>

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
                        會議類別設定及專案查詢</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                會議類別:
                            </td>
                            <td class="text_2c">
                                <asp:UpdatePanel ID="up_Meeting_Class" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_Meeting_Class" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:Label ID="lbl_Meeting_Code" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td class="title_2c">
                                專案名稱:
                            </td>
                            <td class="text_2c">
                                <asp:UpdatePanel ID="up_Pj_Code" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_Pj_Code" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                        專案資料</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                專案類型:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="up_Pj_Kind" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Pj_Kind" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Pj_Kind_Name" runat="server" ></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                專案名稱:
                            </td>
                            <td class="text_2c">
                                <asp:UpdatePanel ID="up_Pj_Name" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Pj_Name" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="title_2c">
                                承辦人員:
                            </td>
                            <td class="text_2c">
                                <asp:UpdatePanel ID="up_Pj_User_Code" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Pj_User_Code" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                專案簡介:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="up_Pj_PjIntro" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Pj_PjIntro" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                專案註記要點:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="up_Pj_PjNote" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Pj_PjNote" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                        會議設定資料</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                會議名稱:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Meeting_Name" runat="server" DB_Length="100" title="會議名稱"
                                    isAllowNull="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                承辦人:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <kd:CoDropDownList ID="ddl_UserDep" runat="server" title="承辦人單位" AutoPostBack="true"
                                            DataTextField="UsDp_Name" DataValueField="UsDp_Code" isAllowNull="false" OnSelectedIndexChanged="ddl_UserDep_SelectedIndexChanged">
                                        </kd:CoDropDownList>
                                        &nbsp;
                                        <kd:CoDropDownList ID="ddl_Meeting_User_Code" runat="server" title="承辦人" DataTextField="User_Name"
                                            DataValueField="User_Code" isAllowNull="false">
                                        </kd:CoDropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_UserDep" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
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
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged"
                AutoPostBack="true">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="場次設定">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="up_Tab1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <div style="margin-top: 12px; margin-left: 5px; position: absolute; float: left;
                                                            background-color: White;">
                                                            <div style="float: left; padding-top: 2.5px; padding-left: 3px;">
                                                                <img src="/CACI/image/yellow_ball.png" /></div>
                                                            <div style="float: left; padding-left: 3px; padding-top: 3px; font-weight: bold">
                                                                場次設定
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <table class="table_lightblue" style="margin-top: 35px;" cellpadding="0" cellspacing="0"
                                                            border="1" width="100%">
                                                            <tr>
                                                                <td class="lightblue_tb_title_more">
                                                                    會議地點<font style="color: Red">*</font>&nbsp;
                                                                </td>
                                                                <td class="lightblue_tb_text_more" colspan="3">
                                                                    <kd:StrTextBox ID="txt_Times_Place" runat="server" title="會議地點" DB_Length="200" isAllowNull="False"
                                                                        Width="90%" ValGroup="grv_MtgTimes" NoMk="False"></kd:StrTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lightblue_tb_title_more">
                                                                    會議開始時間
                                                                </td>
                                                                <td class="lightblue_tb_text_more">
                                                                    <kd:DateTextBox ID="txt_Times_Bgn" runat="server" Width="60px" DateType="Taiwan"
                                                                        title="會議開始時間" isAllowNull="True" NoMk="False"></kd:DateTextBox><asp:CalendarExtender
                                                                            ID="CalendarExtender1" runat="server" TargetControlID="txt_Times_Bgn" Format="yyyy/MM/dd"
                                                                            TodaysDateFormat="yyyy/MM/dd" Enabled="True">
                                                                        </asp:CalendarExtender>
                                                                    <asp:DropDownList ID="ddl_BgnHour" runat="server">
                                                                    </asp:DropDownList>
                                                                    :<asp:DropDownList ID="ddl_BgnMin" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="lightblue_tb_title_more">
                                                                    會議結束時間
                                                                </td>
                                                                <td class="lightblue_tb_text_more">
                                                                    <kd:DateTextBox ID="txt_Times_End" runat="server" Width="60px" DateType="Taiwan"
                                                                        title="會議開始時間" isAllowNull="True" NoMk="False"></kd:DateTextBox><asp:CalendarExtender
                                                                            ID="CalendarExtender2" runat="server" TargetControlID="txt_Times_End" Format="yyyy/MM/dd"
                                                                            TodaysDateFormat="yyyy/MM/dd" Enabled="True">
                                                                        </asp:CalendarExtender>
                                                                    <asp:DropDownList ID="ddl_EndHour" runat="server">
                                                                    </asp:DropDownList>
                                                                    :<asp:DropDownList ID="ddl_EndMin" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="br">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="btnDTL_INSERT" runat="server" ImageUrl="~/image/dtl_Insert.png" />&#160;&nbsp;<asp:ImageButton
                                                ID="btnDTL_UPDATE" runat="server" ImageUrl="~/image/dtl_Update.png" />&#160;&nbsp;<asp:ImageButton
                                                    ID="btnDTL_CLEAR" runat="server" ImageUrl="~/image/dtl_Clear.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="br">
                                        </td>
                                    </tr>
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
                                                                                        場次設定資料列表
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
                                                                        <asp:GridView ID="grv_MtgTimes" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                            BorderWidth="1px" DataKeyNames="Meeting_Index" CssClass="table_lightblue" Style="margin-top: 10px">
                                                                            <Columns>
                                                                                <asp:ButtonField ButtonType="Image" CommandName="mod" ImageUrl="~/image/btn_Detail.png">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                                                                                    <ItemStyle HorizontalAlign="Right" Width="55px" CssClass="yellow_grv_row1" />
                                                                                </asp:ButtonField>
                                                                                <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="55px" CssClass="yellow_grv_row2" />
                                                                                </asp:ButtonField>
                                                                                <asp:BoundField DataField="Meeting_Index" HeaderText="會議場次順序">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Times_Place" HeaderText="場次地點">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Times_Bgn" HeaderText="場次開始時間">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Times_End" HeaderText="場次結束時間">
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
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="顧問委員">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="up_Tab2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <div style="margin-top: 12px; margin-left: 5px; position: absolute; float: left;
                                                            background-color: White;">
                                                            <div style="float: left; padding-top: 2.5px; padding-left: 3px;">
                                                                <img src="/CACI/image/yellow_ball.png" />
                                                            </div>
                                                            <div style="float: left; padding-left: 3px; padding-top: 3px; font-weight: bold">
                                                                顧問委員
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <table class="table_lightblue" style="margin-top: 35px;" cellpadding="0" cellspacing="0"
                                                            border="1" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table class="table_lightblue" style="margin-top: 35px;" cellpadding="0" cellspacing="0"
                                                                        border="1" width="100%">
                                                                        <tr>
                                                                            <td class="lightblue_tb_title_more">
                                                                                場次設定<font style="color: Red">*</font>&nbsp;
                                                                            </td>
                                                                            <td class="lightblue_tb_text_more" colspan="3">
                                                                                <kd:CoDropDownList ID="ddl_Meeting_Index" runat="server" title="場次設定" ValGroup="grv_Committee">
                                                                                </kd:CoDropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lightblue_tb_title_more" colspan="4">
                                                                                顧問/委員查詢
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lightblue_tb_title_more">
                                                                                姓名
                                                                            </td>
                                                                            <td class="lightblue_tb_text_more">
                                                                                <kd:StrTextBox ID="txt_Comm_Name" runat="server" title="姓名" DB_Length="50" Width="90%"
                                                                                    ValGroup="grv_Committee" NoMk="False"></kd:StrTextBox>
                                                                            </td>
                                                                            <td class="lightblue_tb_title_more">
                                                                                單位名稱
                                                                            </td>
                                                                            <td class="lightblue_tb_text_more">
                                                                                <kd:StrTextBox ID="txt_Comm_ComName" runat="server" title="單位名稱" DB_Length="50" Width="90%"
                                                                                    ValGroup="grv_Committee" NoMk="False"></kd:StrTextBox>
                                                                            </td>
                                                                            <%--<tr>
                                                                                <td class="lightblue_tb_title_more">
                                                                                    輔導方式
                                                                                </td>
                                                                                <td class="lightblue_tb_text_more" colspan="3">
                                                                                    <asp:CheckBoxList ID="ckl_Comm_CoachWay" runat="server" RepeatDirection="Horizontal"
                                                                                        RepeatColumns="3">
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                            </tr>--%>
                                                                            <tr>
                                                                                <td class="lightblue_tb_title_more">
                                                                                    輔導項目
                                                                                </td>
                                                                                <td class="lightblue_tb_text_more" colspan="3">
                                                                                    <asp:CheckBoxList ID="ckl_Comm_CoTerms" runat="server" RepeatDirection="Horizontal"
                                                                                        DataTextField="Sys_CdText" DataValueField="Sys_CdCode" ValGroup="grv_Committee">
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lightblue_tb_title_more">
                                                                                    專長領域
                                                                                </td>
                                                                                <td class="lightblue_tb_text_more" colspan="3">
                                                                                    <kd:CoDropDownList ID="ddl_Skill" runat="server" DataTextField="Ski_Name" DataValueField="Ski_Num"
                                                                                        title="專長領域" ValGroup="grv_Committee" isAllowNull='true'>
                                                                                    </kd:CoDropDownList>
                                                                                </td>
                                                                            </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="br">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <!-- Detail按鈕區 -->
                                            <asp:ImageButton ID="btnDTL_QUERY2" runat="server" ImageUrl="~/image/dtl_Query.png" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnDTL_INSERT2" runat="server" ImageUrl="~/image/dtl_Insert.png" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnDTL_CLEAR2" runat="server" ImageUrl="~/image/dtl_Clear.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="br">
                                        </td>
                                    </tr>
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
                                                                            顧問/委員篩選資訊列表
                                                                        </div>
                                                                        <div style="float: left; padding-left: 520px;">
                                                                            <asp:Label ID="lblQueryPage2" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                        </div>
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
                                            <!-- QueryDataGridView -->
                                            <table cellpadding="0px" cellspacing="1px" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlQueryGridView2" runat="server" Width="100%" CssClass="inScroll">
                                                            <asp:GridView ID="grv_SearchCommittee" runat="server" AllowPaging="false" PagerSettings-Visible="false"
                                                                AutoGenerateColumns="False" Width="100%" BorderWidth="1px" CellPadding="0" BorderColor="#3C6ED4"
                                                                DataKeyNames="Comm_Code" AllowSorting="True">
                                                                <PagerSettings Visible="False" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderStyle CssClass="table_data_lightblue_head" />
                                                                        <HeaderStyle Font-Bold="False" />
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox runat="server" ID="cbHead" OnCheckedChanged="Committee_SelectAll" AutoPostBack="true">
                                                                            </asp:CheckBox></HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ID="cbItem"></asp:CheckBox></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Comm_Name" HeaderStyle-Font-Bold="false" HeaderText="姓名">
                                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Ski_Name" HeaderStyle-Font-Bold="false" HeaderText="專長領域">
                                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Comm_Title" HeaderStyle-Font-Bold="false" HeaderText="職銜">
                                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Comm_ComName" HeaderText="單位(公司)名稱">
                                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="table_data_blue_head" Font-Bold="false" />
                                                                <RowStyle CssClass="table_data_blue_data" HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
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
                                                                                <div style="margin-left: 5px; margin-top: 10px; float: left; background-color: White;">
                                                                                    <div style="float: left; padding-left: 3px;">
                                                                                        <img src="/CACI/image/yellow_ball.png" />
                                                                                    </div>
                                                                                    <div style="float: left; padding-left: 3px; font-weight: bold">
                                                                                        顧問委員設定資料列表
                                                                                    </div>
                                                                                    <div style="float: left; padding-left: 530px;">
                                                                                        <asp:Label ID="lblPage2" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                    </div>
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
                                                                    <asp:Panel ID="pnlGridView2" runat="server" Width="100%" CssClass="inScroll">
                                                                        <asp:GridView ID="grv_Committee" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                            BorderWidth="1px" DataKeyNames="Comm_Code,Meeting_Index" CssClass="table_lightblue"
                                                                            Style="margin-top: 10px">
                                                                            <Columns>
                                                                                <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_delete.png">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="55px" CssClass="yellow_grv_row2" />
                                                                                </asp:ButtonField>
                                                                                <asp:BoundField DataField="Meeting_Index" HeaderText="場次">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Comm_Name" HeaderText="姓名">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Comm_ComName" HeaderText="單位(公司)名稱">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Ski_Name" HeaderText="專長領域">
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
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="申請單位">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="up_Tab3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <div style="margin-top: 12px; margin-left: 5px; position: absolute; float: left;
                                                            background-color: White;">
                                                            <div style="float: left; padding-top: 2.5px; padding-left: 3px;">
                                                                <img src="/CACI/image/yellow_ball.png" />
                                                            </div>
                                                            <div style="float: left; padding-left: 3px; padding-top: 3px; font-weight: bold">
                                                                申請單位
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <table class="table_lightblue" style="margin-top: 35px;" cellpadding="0" cellspacing="0"
                                                            border="1" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table class="table_lightblue" style="margin-top: 35px;" cellpadding="0" cellspacing="0"
                                                                        border="1" width="100%">
                                                                        <tr>
                                                                            <td class="lightblue_tb_title_more">
                                                                                場次設定<font style="color: Red">*</font>&nbsp;
                                                                            </td>
                                                                            <td class="lightblue_tb_text_more">
                                                                                <kd:CoDropDownList ID="ddl_Meeting_Index2" runat="server" title="場次設定" ValGroup="grv_Company">
                                                                                </kd:CoDropDownList>
                                                                            </td>
                                                                            <td class="lightblue_tb_title_more">
                                                                                顧問委員<font style="color: Red">*</font>&nbsp;
                                                                            </td>
                                                                            <td class="lightblue_tb_text_more">
                                                                                <kd:CoDropDownList ID="ddl_Committee" runat="server" title="顧問委員" ValGroup="grv_Company">
                                                                                </kd:CoDropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lightblue_tb_title_more" colspan="4">
                                                                                單位(公司)查詢
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lightblue_tb_title_more">
                                                                                統編/身份證字號/立案字號
                                                                            </td>
                                                                            <td class="lightblue_tb_text_more">
                                                                                <kd:StrTextBox ID="txt_Com_Tonum" runat="server" title="統編/身份證字號/立案字號" DB_Length="30"
                                                                                    Width="90%" ValGroup="grv_Company" NoMk="False"></kd:StrTextBox>
                                                                            </td>
                                                                            <td class="lightblue_tb_title_more">
                                                                                單位(公司)名稱
                                                                            </td>
                                                                            <td class="lightblue_tb_text_more">
                                                                                <kd:StrTextBox ID="txt_Com_Name" runat="server" title="單位(公司)名稱" DB_Length="100"
                                                                                    Width="90%" ValGroup="grv_Company" NoMk="False"></kd:StrTextBox>
                                                                            </td>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="br">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <!-- Detail按鈕區 -->
                                            <asp:ImageButton ID="btnDTL_QUERY3" runat="server" ImageUrl="~/image/dtl_Query.png" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnDTL_INSERT3" runat="server" ImageUrl="~/image/dtl_Insert.png" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnDTL_CLEAR3" runat="server" ImageUrl="~/image/dtl_Clear.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="br">
                                        </td>
                                    </tr>
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
                                                                            單位(公司)篩選資訊列表
                                                                        </div>
                                                                        <div style="float: left; padding-left: 520px;">
                                                                            <asp:Label ID="lblQueryPage3" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                        </div>
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
                                            <!-- QueryDataGridView -->
                                            <table cellpadding="0px" cellspacing="1px" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlQueryGridView3" runat="server" Width="100%" CssClass="inScroll">
                                                            <asp:GridView ID="grv_SearchCompany" runat="server" AllowPaging="false" PagerSettings-Visible="false"
                                                                AutoGenerateColumns="False" Width="100%" BorderWidth="1px" CellPadding="0" BorderColor="#3C6ED4"
                                                                DataKeyNames="Com_Code" AllowSorting="True">
                                                                <PagerSettings Visible="False" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderStyle CssClass="table_data_lightblue_head" />
                                                                        <HeaderStyle Font-Bold="False" />
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox runat="server" ID="cbHead" OnCheckedChanged="Company_SelectAll" AutoPostBack="true">
                                                                            </asp:CheckBox></HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ID="cbItem"></asp:CheckBox></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="單位(公司)名稱">
                                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ChKd_Name" HeaderStyle-Font-Bold="false" HeaderText="輔導項目">
                                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="table_data_blue_head" Font-Bold="false" />
                                                                <RowStyle CssClass="table_data_blue_data" HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
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
                                                        ~/Bin/com.kangdainfo.online.WebBase.dll ~/Bin/com.kangdainfo.online.WebControl.dll
                                                        <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" width="100%" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <div style="margin-left: 5px; margin-top: 10px; float: left; background-color: White;">
                                                                                    <div style="float: left; padding-left: 3px;">
                                                                                        <img src="/CACI/image/yellow_ball.png" />
                                                                                    </div>
                                                                                    <div style="float: left; padding-left: 3px; font-weight: bold">
                                                                                        單位(公司)設定資料列表
                                                                                    </div>
                                                                                    <div style="float: left; padding-left: 530px;">
                                                                                        <asp:Label ID="lblPage3" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                    </div>
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
                                                                    <asp:Panel ID="pnlGridView3" runat="server" Width="100%" CssClass="inScroll">
                                                                        <asp:GridView ID="grv_Company" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                            BorderWidth="1px" DataKeyNames="Com_Code,Meeting_Index" CssClass="table_lightblue"
                                                                            Style="margin-top: 10px" OnRowDataBound="grv_Company_RowDataBound">
                                                                            <Columns>
                                                                                <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_delete.png">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="55px" CssClass="yellow_grv_row2" />
                                                                                </asp:ButtonField>
                                                                                <asp:BoundField DataField="Meeting_Index" HeaderText="場次">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Comm_Name" HeaderText="顧問委員">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Com_Name" HeaderText="單位(公司)名稱">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ChKd_Name" HeaderText="輔導項目">
                                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Comm_Code" HeaderText="顧問委員代碼">
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
