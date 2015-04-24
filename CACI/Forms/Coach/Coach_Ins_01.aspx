<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Coach_Ins_01.aspx.cs" Inherits="Coach_Ins_01" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/UserControl/ral_Chkd_Code.ascx" tagname="CoachKindRadioButtonList" tagprefix="uc1" %>
<%@ Register Assembly="CuteWebUI.AjaxUploader" Namespace="CuteWebUI" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InsertContent" runat="Server">
    <table class="table_gray" style="background: white; margin-top:10px" cellpadding="0" cellspacing="0"
        border="1" width="100%">
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
                    <td class="title_2c">單位(公司)名稱: </td>
                    <td class="text_2c" style="color:Red">
                        <asp:HiddenField ID="hid_Com_Code" runat="server" Value="N" />
                        <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" title="單位(公司)名稱" isAllowNull="false" ></kd:StrTextBox>
                        <asp:Label ID="lbl_Pj_Code" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td class="title_2c"><span style="color:Red;">*</span>統編/身份證字號/立案號: </td>
                    <td class="text_2c">
                        <kd:IDNumTextBox ID="txt_Com_Tonum" runat="server" DB_Length="30" title="統編/身份證字號/立案號" 
                           ontextchanged="txt_Com_Tonum_TextChanged" 
                            AutoPostBack="True" isAllowNull="false" ></kd:IDNumTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">單位(公司)負責人: </td>
                    <td class="text_2c">
                         <kd:StrTextBox ID="txt_Com_Boss" runat="server" DB_Length="50" title="單位(公司)負責人" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                    <td class="title_2c">負責人身份證字號: </td>
                    <td class="text_2c">
                        <kd:StrTextBox ID="txt_Com_BsIDNO" runat="server" DB_Length="50" title="負責人身份證字號" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">負責人性別: </td>
                    <td class="text_2c">
                        <asp:RadioButtonList ID="rbl_Com_BsGender" runat="server">
                        </asp:RadioButtonList>
                
                    </td>
                    <td class="title_2c">負責人手機: </td>
                    <td class="text_2c">
                        <kd:StrTextBox ID="txt_Com_BsCell" runat="server" DB_Length="50" title="負責人手機" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">負責人年齡層: </td>
                    <td class="text_2c" colspan="3">
                        <asp:RadioButtonList ID="rbl_Com_BsAgeRange" runat="server">
                        </asp:RadioButtonList>
                
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">負責人最高學歷: </td>
                    <td class="text_2c" colspan="3">
                        <asp:RadioButtonList ID="rbl_Com_BsDegree" runat="server">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">登記類型: </td>
                    <td class="text_2c" >
                         <asp:DropDownList ID="ddl_Com_OrgForm" runat="server" DB_Length="20" title="登記類型" isAllowNull="false" ></asp:DropDownList>
                    </td>
                    <td class="title_2c">成立時間: </td>
                    <td class="text_2c" >
                                             <kd:DateTextBox runat="server"  ID="txt_Com_SetupTime" title="成立時間" DateType="Taiwan" isAllowNull="false" > </kd:DateTextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Com_SetupTime" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd"  ></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">資本額: </td>
                    <td class="text_2c" >
                         <kd:StrTextBox ID="txt_Com_Capital" runat="server" DB_Length="20" title="資本額" isAllowNull="false" ></kd:StrTextBox>(元)
                    </td>
                    <td class="title_2c">員工數: </td>
                    <td class="text_2c" >
                         <kd:StrTextBox ID="txt_Com_EmpNum" runat="server" DB_Length="10" title="員工數" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">最近一年營業額:</td>
                    <td class="text_2c" colspan="3">
                        <kd:StrTextBox ID="txt_Com_LTover" runat="server" DB_Length="10" title="最近一年營業額" isAllowNull="false" ></kd:StrTextBox>(元)
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">主要產品及服務:</td>
                    <td class="text_2c" colspan="3">
                        <kd:StrTextBox ID="txt_Com_MnProduct" runat="server" DB_Length="500" title="主要產品及服務" isAllowNull="false" TextMode="MultiLine" Height="100" Width="95%" ></kd:StrTextBox>
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
                        銀行資料</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">往來銀行:</td>
                            <td class="text_2c" colspan="2">
                                 <asp:HiddenField ID="hid_BAcc_Code" runat="server" Value="N" />
                                <%--<asp:DropDownList ID="ddl_BAcc_Bankno" runat="server" DB_Length="20" title="銀行" isAllowNull="false"></asp:DropDownList>--%>
                                <kd:CoDropDownList ID="ddl_BAcc_Bankno" runat="server" title="銀行" isAllowNull="false">
                                </kd:CoDropDownList>
                                銀行
                            </td>
                            <td class="text_2c" colspan="1">
                                <kd:StrTextBox ID="txt_BAcc_Accno" runat="server" DB_Length="20" title="分行" isAllowNull="false"></kd:StrTextBox>分行
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">債信(企業):</td>
                            <td class="text_more" >
                                <asp:RadioButtonList ID="rbl_BAcc_Mcredit" runat="server">
                                </asp:RadioButtonList>
                            </td>
                           <td class="title_2c">債信(負責人/配偶):</td>
                            <td class="text_more" >
                                <asp:RadioButtonList ID="rbl_BAcc_Scredit" runat="server">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">票信(企業):</td>
                            <td class="text_2c">
                                <asp:RadioButtonList ID="rbl_BAcc_MCheck" runat="server">
                                </asp:RadioButtonList>
                            </td>
                            <td class="title_2c">票信(負責人/配偶):</td>
                            <td class="text_2c">
                                <asp:RadioButtonList ID="rbl_BAcc_SCheck" runat="server">
                                </asp:RadioButtonList>
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
                        聯絡資訊</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">聯絡人姓名:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttName" runat="server" DB_Length="50" title="聯絡人姓名" isAllowNull="false"
                                    Width="90%"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">聯絡人手機:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttCell" runat="server" DB_Length="50" title="聯絡人手機" isAllowNull="false"
                                    Width="90%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">聯絡人公司電話:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttTel" runat="server" DB_Length="50" title="聯絡人公司電話"
                                    isAllowNull="false" Width="90%"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">傳真:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Fax" runat="server" DB_Length="50" title="傳真"
                                     Width="90%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">聯絡人e-mail:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_CttMail" runat="server" DB_Length="50" title="聯絡人e-mail"
                                    isAllowNull="false" Width="95%"></kd:StrTextBox>
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
                        申請輔導項目(總計請勾選一項)</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                         <uc1:CoachKindRadioButtonList ID="CoachKindRadioButtonList1" runat="server" />
                        <tr>
                            <td class="title_2c">請說明申請項目現階段所遇到的問題: </td>
                            <td class="text_2c">
                            <asp:HiddenField ID="hid_Coach_Code" runat="server" Value="N" />
                            <kd:StrTextBox ID="txt_Coach_QstText" runat="server" DB_Length="50" title="請說明申請項目現階段所遇到的問題"
                                    Width="95%" Height="60" ></kd:StrTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <table class="table_gray" style="background: white; margin-top:10px" cellpadding="0" cellspacing="0" 
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        檢附資料上傳</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">公司簡介資料:</td>
                            <td class="text_2c" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="ComAtt_Code1" runat="server" Value="N" />
                                        <cc1:UploadAttachments runat="server" ID="Uploader_IT" InsertText="選擇檔案" MultipleFilesUpload="false"
                                            ShowTableHeader="false" OnFileUploaded="Uploader_IT_FileUploaded" ShowCheckBoxes="false" 
                                            onattachmentremoveclicked="Uploader_IT_AttachmentRemoveClicked" >
                                        </cc1:UploadAttachments>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">商業或公司登記影本:</td>
                            <td class="text_2c" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="ComAtt_Code2" runat="server" Value="N" />
                                        <cc1:UploadAttachments runat="server" ID="Uploader_RP" InsertText="選擇檔案" MultipleFilesUpload="false"
                                            ShowTableHeader="false" OnFileUploaded="Uploader_RP_FileUploaded" ShowCheckBoxes="false" 
                                            onattachmentremoveclicked="Uploader_RP_AttachmentRemoveClicked" >
                                        </cc1:UploadAttachments>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">最近一年財務會計相關報表影本及營業稅申報書影本:</td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="ComAtt_Code3" runat="server" Value="N" />
                                        <cc1:UploadAttachments runat="server" ID="Uploader_FA" InsertText="選擇檔案" MultipleFilesUpload="false"
                                            ShowTableHeader="false" OnFileUploaded="Uploader_FA_FileUploaded" ShowCheckBoxes="false" 
                                            onattachmentremoveclicked="Uploader_FA_AttachmentRemoveClicked" >
                                        </cc1:UploadAttachments>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">檢附其他附件:</td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="ComAtt_Code4" runat="server" Value="N" />
                                        <cc1:UploadAttachments runat="server" ID="Uploader_OT" InsertText="選擇檔案" MultipleFilesUpload="false"
                                            ShowTableHeader="false" OnFileUploaded="Uploader_OT_FileUploaded" ShowCheckBoxes="false" 
                                            onattachmentremoveclicked="Uploader_OT_AttachmentRemoveClicked" >
                                        </cc1:UploadAttachments>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
