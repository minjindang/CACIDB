<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="Committee_Upd_01.aspx.cs" Inherits="Committee_Upd_01" %>

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
            <td class="text_2c" colspan="4">基本資料</td>
        </tr>
        <tr>
            <td class="title_2c">姓名: </td>
            <td class="text_2c" style="color: Red">
                <kd:StrTextBox ID="txt_Comm_Name" runat="server" DB_Length="10" title="姓名" isAllowNull="false"></kd:StrTextBox>
                <asp:Label ID="lblComm_Code" runat="server" Visible="false"></asp:Label>
            </td>
            <td class="title_2c">身分證字號: </td>
            <td class="text_2c">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <kd:IDNumTextBox ID="txt_Comm_IDNO" runat="server" title="身分證字號" AutoPostBack="true" OnTextChanged="txt_Comm_IDNO_TextChanged"></kd:IDNumTextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="title_2c">職銜: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Comm_Title" runat="server" DB_Length="50" title="職銜" isAllowNull="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">公司名稱: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Comm_ComName" runat="server" DB_Length="50" title="公司名稱" isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">聯絡電話: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Comm_Tel" runat="server" DB_Length="20" title="聯絡電話" isAllowNull="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">手機: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Comm_Cell" runat="server" DB_Length="10" title="手機" isAllowNull="true"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">電子信箱: </td>
            <td class="text_more" colspan="3">
                <kd:EmailTextBox ID="txt_Comm_Mail" runat="server" MaxLength="100" title="電子信箱" Width="95%"
                    isAllowNull="true"></kd:EmailTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">登入帳號: </td>
            <td class="text_2c">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hid_IsNewAccount" runat="server" />
                        <kd:StrTextBox ID="txt_Comm_Account" runat="server" DB_Length="20" title="登入帳號" isAllowNull="true"
                            Enabled="false"></kd:StrTextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="title_2c">登入密碼: </td>
            <td class="text_2c">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_Comm_Pass" runat="server" TextMode="Password" Enabled="false"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="text_2c" colspan="4">匯款資料</td>
        </tr>
        <tr>
            <td class="title_2c">銀行帳號: </td>
            <td class="text_2c" colspan="3">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <kd:NumTextBox ID="txt_Comm_Bank_Num" runat="server" DB_IntLength="3" DB_DecLength="0"
                                        title="銀行代碼" isAllowNull="false" Width="20px" AutoPostBack="true" OnTextChanged="txt_Comm_Bank_Num_TextChanged"></kd:NumTextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_Comm_Bank_Name" runat="server" Text="" Visible="false"></asp:Label></td>
                                <td>
                                    <kd:StrTextBox ID="txt_Comm_Bankno" runat="server" DB_Length="20" title="銀行帳號" isAllowNull="false"
                                        ValGroup="grvQuery"></kd:StrTextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="title_2c">戶名:</td>
            <td class="text_2c" colspan="3">
                <kd:StrTextBox ID="txt_Comm_BkName" runat="server" DB_Length="50" title="戶名" isAllowNull="false"
                    ValGroup="grvQuery"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">存摺掃描檔:</td>
            <td class="text_2c" colspan="3">
                <asp:LinkButton ID="lbtn_Comm_BkFile" runat="server" 
                    OnCommand="lbtn_Comm_BkFile_Command"></asp:LinkButton>
                <cc1:UploadAttachments runat="server" ID="Uploader_Comm_BkFile" InsertText="選擇檔案" MultipleFilesUpload="false"
                    ShowTableHeader="false" OnFileUploaded="Uploader_Comm_BkFile_FileUploaded" ShowCheckBoxes="false" 
                    onattachmentremoveclicked="Uploader_Comm_BkFile_AttachmentRemoveClicked" >
                </cc1:UploadAttachments>
            </td>
        </tr>
        <tr>
            <td class="text_2c" colspan="4">顧問/委員輔導資料</td>
        </tr>
        <tr>
            <td class="title_2c">輔導方式:</td>
            <td class="text_2c" colspan="3">
                <asp:CheckBoxList ID="ckl_Comm_CoachWay" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">輔導項目:</td>
            <td class="text_2c" colspan="3">
                <asp:CheckBoxList ID="ckl_Comm_CoTerms" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
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
            Detail資訊
        </div>
    </div>
    <div class="table_detail_info">
        <table class="table_lightblue" cellpadding="0" cellspacing="0" border="1" width="100%">
            <tr>
                <td class="lightblue_tb_title_more">專長領域: <font style="color: Red">*</font> </td>
                <td class="lightblue_tb_text_more">
                    <asp:DropDownList ID="ddl_Skill" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="lightblue_tb_title_more">備註:</td>
                <td class="lightblue_tb_text_more">
                    <kd:StrTextBox ID="txt_Skill_Note" runat="server" title="備註" DB_Length="200" ValGroup="grvQuery"></kd:StrTextBox>
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
                                    聯絡資訊列表
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
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" class="detail_result">
                                <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="Ski_Num" CssClass="table_lightblue">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                                            <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                            <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="Ski_Name" HeaderStyle-Font-Bold="false" HeaderText="領域"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Skill_Note" HeaderStyle-Font-Bold="false" HeaderText="備註"
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
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
