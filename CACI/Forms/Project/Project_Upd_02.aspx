<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="Project_Upd_02.aspx.cs" Inherits="Project_Upd_02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CuteWebUI.AjaxUploader" Namespace="CuteWebUI" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c"><font color = "red">*</font>專案名稱: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Pj_Name" runat="server" DB_Length="50" title="專案名稱" isAllowNull="false" ></kd:StrTextBox>
                <asp:Label ID="lbl_Pj_Code" runat="server" Visible="false">
                    <asp:HiddenField ID="hid_From_Page" runat="server" />
                </asp:Label>
            </td>
            <td class="title_2c"><font color = "red">*</font>網路申請開放時間: </td>
            <td class="text_2c">
                <kd:DateTextBox ID="txt_Pj_BgnDate" runat="server" Width="60" DateType="Taiwan" title="網路申請開放日期(起)" isAllowNull="false"></kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Pj_BgnDate"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
                ~
                <kd:DateTextBox ID="txt_Pj_EndDate" runat="server" Width="60" DateType="Taiwan" title="網路申請開放日期(迄)" isAllowNull="false"></kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_Pj_EndDate"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="title_2c"><font color = "red">*</font>專案啟動日期: </td>
            <td class="text_2c">
                <kd:DateTextBox ID="txt_Pj_StartDate" runat="server" Width="60" DateType="Taiwan"
                    title="專案啟動日期" isAllowNull="false"></kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_Pj_StartDate"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
            </td>
            <td class="title_2c"><font color = "red">*</font>是否提供轉件: </td>
            <td class="title_2c">
                是否提供轉件:
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_Pj_Trans" runat="server" title="是否提供轉件" isAllowNull="false">
                    <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="否" Value="N"></asp:ListItem>
                </kd:CoDropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c"><font color = "red">*</font>承辦人: </td>
            <td class="text_2c">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <kd:CoDropDownList ID="ddl_UserDep" runat="server" title="承辦人單位" isAllowNull="false" AutoPostBack="true"
                            DataTextField="UsDp_Name" DataValueField="UsDp_Code" OnSelectedIndexChanged="ddl_UserDep_SelectedIndexChanged">
                        </kd:CoDropDownList>
                        &nbsp;
                        <kd:CoDropDownList ID="ddl_UserAcc" runat="server" title="承辦人" DataTextField="User_Name"
                            DataValueField="User_Code" isAllowNull="false">
                        </kd:CoDropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_UserDep" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="title_2c">
                專案經費:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Pj_Fund" runat="server" DB_Length="50" title="專案經費"></kd:StrTextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_Pj_Fund"
                    DisplayMoney="None" MaskType="Number" Mask="9,999,999">
                </asp:MaskedEditExtender>
                千元
            </td>
        </tr>
        <tr>
            <td class="title_2c"><font color = "red">*</font>外網專案說明: </td>
            <td class="text_more" colspan="3">
                <cc1:Editor ID="Editor1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="title_2c"><font color = "red">*</font>專案簡介: </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_Pj_PjIntro" runat="server" DB_Length="1000" title="專案簡介" isAllowNull="false" Width="95%"
                    TextMode="MultiLine" Columns="5" Height="50px"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                專案註記要點:
            </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_Pj_PjNote" runat="server" DB_Length="1000" title="專案註記要點"
                    Width="95%" TextMode="MultiLine" Columns="5" Height="50px"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                附件上傳:
            </td>
            <td class="text_more" colspan="3">
                <cc1:UploadAttachments runat="server" ID="Upload1" InsertText="選擇檔案" MultipleFilesUpload="false"
                    ShowTableHeader="false" OnFileUploaded="Uploader_1_FileUploaded" ShowCheckBoxes="false"
                    OnAttachmentRemoveClicked="Uploader_1_AttachmentRemoveClicked">
                </cc1:UploadAttachments>
            </td>
        </tr>
        <%--<tr>
            <td class="title_2c">
                申請表單樣式:
            </td>
            <td class="text_more" colspan="3">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rad_Pj_PjFill_1" runat="server" Text="樣式1" GroupName="Pj_PjFill" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rad_Pj_PjFill_2" runat="server" Text="樣式2" GroupName="Pj_PjFill" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rad_Pj_PjFill_3" runat="server" Text="樣式3" GroupName="Pj_PjFill" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rad_Pj_PjFill_4" runat="server" Text="樣式4" GroupName="Pj_PjFill" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
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
            流程階段設定
        </div>
    </div>
    <div class="table_detail_info">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="table_lightblue" style="" cellpadding="0" cellspacing="0" border="1"
                                            width="100%">
                                            <tr>
                                                <td class="lightblue_tb_title_more">
                                                    階段名稱<font style="color: Red">*</font>&nbsp;
                                                </td>
                                                <td class="lightblue_tb_text_more">
                                                    <kd:StrTextBox ID="txt_Stage_Name" runat="server" title="階段名稱" DB_Length="100" isAllowNull="false"
                                                        ValGroup="grvQuery" Width="90%"></kd:StrTextBox>
                                                </td>
                                                <td class="lightblue_tb_title_more">
                                                    階段順序<font style="color: Red">*</font>&nbsp;
                                                </td>
                                                <td class="lightblue_tb_text_more">
                                                    <asp:HiddenField ID="Old_Stage_Index" runat="server" />
                                                    <kd:CoDropDownList ID="ddl_Stage_Index" runat="server" title="階段順序" isAllowNull="false"
                                                        ValGroup="grvQuery">
                                                        <%--<asp:ListItem Text="1" Value="1"></asp:ListItem>--%>
                                                    </kd:CoDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lightblue_tb_title_more">
                                                    執行日期
                                                </td>
                                                <td class="lightblue_tb_text_more" colspan="3">
                                                    <kd:DateTextBox ID="txt_Stage_Date" runat="server" Width="60" DateType="Taiwan" title="執行日期"></kd:DateTextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_Stage_Date"
                                                        Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lightblue_tb_title_more">
                                                    階段類型
                                                </td>
                                                <td class="lightblue_tb_text_more" colspan="3">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="rad_Stage_Kind_1" Text="起始階段" runat="server" GroupName="rad_Stage_Kind" />
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="rad_Stage_Kind_2" Text="中間階段(執行日期)" runat="server" GroupName="rad_Stage_Kind" />
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="rad_Stage_Kind_3" Text="結束階段" runat="server" GroupName="rad_Stage_Kind" />
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="rad_Stage_Kind_4" Text="管考階段" runat="server" GroupName="rad_Stage_Kind" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lightblue_tb_title_more">
                                                    階段內容說明<font style="color: Red">*</font>&nbsp;
                                                </td>
                                                <td class="lightblue_tb_text_more" colspan="3">
                                                    <kd:StrTextBox ID="txt_Stage_Text" runat="server" title="階段內容說明" DB_Length="1000"
                                                        isAllowNull="false" Width="95%" TextMode="MultiLine" Columns="5" ValGroup="grvQuery"></kd:StrTextBox>
                                                </td>
                                            </tr>
                                            <%--
                                            <tr>
                                                <td class="lightblue_tb_title_more">
                                                    是否召開會議
                                                </td>
                                                <td class="lightblue_tb_text_more">
                                                    <asp:DropDownList ID="ddl_Stage_IsMeeting" runat="server">
                                                        <asp:ListItem Text="否" Value="N" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="lightblue_tb_title_more">
                                                    會議性質
                                                </td>
                                                <td class="lightblue_tb_text_more">
                                                    <kd:CoDropDownList ID="ddl_Stage_MtKind" runat="server" title="會議性質" DataTextField="Mety_Name"
                                                        DataValueField="Mety_Code" isAllowNull="true" ValGroup="grvQuery">
                                                    </kd:CoDropDownList>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="lightblue_tb_title_more">
                                                    是否提醒
                                                </td>
                                                <td class="lightblue_tb_text_more">
                                                    <asp:DropDownList ID="ddl_Stage_RmFlag" runat="server">
                                                        <asp:ListItem Text="否" Value="N" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="lightblue_tb_title_more">
                                                    提醒人員
                                                </td>
                                                <td class="lightblue_tb_text_more">
                                                    <asp:CheckBoxList ID="chl_Stage_RmEmpl" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="申請單位" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="承辦人" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="顧問人員" Value="3"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lightblue_tb_title_more">
                                                    提前提醒天數
                                                </td>
                                                <td class="lightblue_tb_text_more" colspan="3">
                                                    <kd:NumTextBox ID="txt_Stage_RmDays" runat="server" DB_IntLength="2" DB_DecLength="0"
                                                        title="提前提醒天數" Width="30px" ValGroup="grvQuery"></kd:NumTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lightblue_tb_title_more">
                                                    提醒文稿
                                                </td>
                                                <td class="lightblue_tb_text_more" colspan="3">
                                                    <kd:StrTextBox ID="txt_Stage_RmText" runat="server" DB_Length="100" title="是否文稿"
                                                        ValGroup="grvQuery" Width="90%" TextMode="MultiLine" Columns="3"></kd:StrTextBox>
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
                                    流程資訊列表
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
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlGridView" runat="server" Width="100%" class="detail_result">
                                        <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                            BorderWidth="1px" DataKeyNames="Stage_Index" CssClass="table_lightblue" OnRowDataBound="pnlGridView_RowDataBound">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                                                    <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                                    <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                                    <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                                </asp:ButtonField>
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
                                                <asp:BoundField DataField="Stage_Date" HeaderStyle-Font-Bold="false" HeaderText="執行日期"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stage_Text" HeaderStyle-Font-Bold="false" HeaderText="階段內容說明"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stage_RmFlag" HeaderStyle-Font-Bold="false" HeaderText="是否提醒"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stage_RmEmpl" HeaderStyle-Font-Bold="false" HeaderText="提醒人員"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stage_RmDays" HeaderStyle-Font-Bold="false" HeaderText="提醒天數"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stage_RmText" HeaderStyle-Font-Bold="false" HeaderText="提醒文稿"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stage_RmEmpl" HeaderStyle-Font-Bold="false" HeaderText="提醒人員代碼"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stage_Kind" HeaderStyle-Font-Bold="false" HeaderText="階段類型代碼"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stage_Days" HeaderStyle-Font-Bold="false" HeaderText="Stage_Days"
                                                    HeaderStyle-VerticalAlign="Bottom">
                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
