<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Company_Upd_01.aspx.cs" Inherits="Company_Upd_01" %>

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
                        基本資料
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">單位名稱: </td>
                            <td class="text_2c" style="color: Red">
                                <asp:Label ID="lblComm_Code" runat="server" Visible="false"></asp:Label>
                                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" title="公司名稱" isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">單位統編: </td>
                            <td class="text_2c">
                                <kd:CoIDNumTextBox ID="txt_Com_Tonum" runat="server" title="公司統編" isAllowNull="false"></kd:CoIDNumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">單位負責人: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Boss" runat="server" DB_Length="50" title="公司負責人" isAllowNull="true"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">負責人身份證字號: </td>
                            <td class="text_2c">
                                <kd:IDNumTextBox ID="txt_Com_BsIDNO" runat="server" title="負責人身份證字號" isAllowNull="true"></kd:IDNumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">組織型式: </td>
                            <td class="text_2c" colspan="3">
                                <kd:CoDropDownList ID="ddl_Com_OrgForm" runat="server" title="組織型式" 
                                    DataTextField="Sys_CdText" DataValueField="Sys_CdCode" isAllowNull="true">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">單位資本額: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Capital" runat="server" DB_Length="20" title="公司資本額" isAllowNull="true"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">單位員工數: </td>
                            <td class="text_2c">
                                <kd:NumTextBox ID="txt_Com_EmpNum" runat="server" DB_IntLength="10" DB_DecLength="0" title="公司員工數" isAllowNull="true"></kd:NumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">主要產業別: </td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_Com_MnSectors" runat="server" title="主要產業別"
                                    DataTextField="Sys_CdText" isAllowNull="true" DataValueField="Sys_CdCode" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddl_Com_MnSectors_SelectedIndexChanged">
                                </kd:CoDropDownList>
                                <kd:StrTextBox ID="txt_Com_MnOther" runat="server" DB_Length="100" title="主要產業別" Visible="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">次要產業別: </td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_Com_SbSectors" runat="server" title="次要產業別"
                                    DataTextField="Sys_CdText" isAllowNull="true" DataValueField="Sys_CdCode" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddl_Com_SbSectors_SelectedIndexChanged">
                                </kd:CoDropDownList>
                                <kd:StrTextBox ID="txt_Com_SbOther" runat="server" DB_Length="100" title="次要產業別" Visible="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">主要產品及服務: </td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_Com_MnProduct" runat="server" DB_Length="500" title="主要產品及服務" Width="95%" isAllowNull="true"></kd:StrTextBox>
                            </td>            
                        </tr>
                        <tr>
                            <td class="title_2c">主要產品及服務<br />附件上傳: </td>
                            <td class="text_2c" colspan="3">
                                <asp:HyperLink ID="hpl_ComMnPd_file1" runat="server"></asp:HyperLink>
                                <cc1:UploadAttachments runat="server" ID="Uploader_Product" InsertText="選擇檔案" MultipleFilesUpload="false"
                                    ShowTableHeader="false" OnFileUploaded="Uploader_Product_FileUploaded" ShowCheckBoxes="false" 
                                    onattachmentremoveclicked="Uploader_Product_AttachmentRemoveClicked" >
                                </cc1:UploadAttachments>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
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
                        單位聯絡資訊
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">單位電話: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Tel" runat="server" DB_Length="20" title="公司電話" isAllowNull="true"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">單位傳真: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Fax" runat="server" DB_Length="20" title="公司傳真" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">單位地址:</td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_Com_OPAddr" runat="server" DB_Length="200" title="公司地址" Width="95%" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">單位網址:</td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_Com_Url" runat="server" DB_Length="200" title="公司網址" Width="95%" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">單位E-mail:</td>
                            <td class="text_2c" colspan="3">
                                <kd:EmailTextBox ID="txt_Com_Email" runat="server" title="公司E-mail" Width="95%" isAllowNull="true"></kd:EmailTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
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
                        聯絡人資訊
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">聯絡人姓名: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttName" runat="server" DB_Length="50" title="聯絡人姓名" isAllowNull="true"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">聯絡人手機: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttCell" runat="server" DB_Length="10" title="聯絡人手機" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">聯絡人單位電話:</td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_Com_CttTel" runat="server" DB_Length="20" title="聯絡人公司電話" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">聯絡人E-mail:</td>
                            <td class="text_2c" colspan="3">
                                <kd:EmailTextBox ID="txt_Com_CttMail" runat="server" title="聯絡人E-mail" Width="95%" isAllowNull="true"></kd:EmailTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
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
                        應檢附資料上傳
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">    
                        <tr>
                            <td class="title_2c">單位設立登記影本:</td>
                            <td class="text_2c" colspan="3">
                                <asp:Label ID="lbl_ComAtt_Code2" runat="server" Visible="false"></asp:Label>
                                <asp:HyperLink ID="hpl_ComAtt_file2" runat="server"></asp:HyperLink>
                                <cc1:UploadAttachments runat="server" ID="Upload_RP" InsertText="選擇檔案" MultipleFilesUpload="false"
                                    ShowTableHeader="false" OnFileUploaded="Uploader_RP_FileUploaded" ShowCheckBoxes="false" 
                                    onattachmentremoveclicked="Uploader_RP_AttachmentRemoveClicked" >
                                </cc1:UploadAttachments>                                
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">完稅資料影本:</td>
                            <td class="text_2c" colspan="3">
                                <asp:Label ID="lbl_ComAtt_Code3" runat="server" Visible="false"></asp:Label>
                                <asp:HyperLink ID="hpl_ComAtt_file3" runat="server"></asp:HyperLink>
                                <cc1:UploadAttachments runat="server" ID="Upload_FA" InsertText="選擇檔案" MultipleFilesUpload="false"
                                    ShowTableHeader="false" OnFileUploaded="Uploader_FA_FileUploaded" ShowCheckBoxes="false" 
                                    onattachmentremoveclicked="Uploader_FA_AttachmentRemoveClicked" >
                                </cc1:UploadAttachments>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">檢附其他文件l:</td>
                            <td class="text_2c" colspan="3">
                                <asp:Label ID="lbl_ComAtt_Code4" runat="server" Visible="false"></asp:Label>
                                <asp:HyperLink ID="hpl_ComAtt_file4" runat="server"></asp:HyperLink>
                                <cc1:UploadAttachments runat="server" ID="Upload_OT" InsertText="選擇檔案" MultipleFilesUpload="false"
                                    ShowTableHeader="false" OnFileUploaded="Uploader_OT_FileUploaded" ShowCheckBoxes="false" 
                                    onattachmentremoveclicked="Uploader_OT_AttachmentRemoveClicked" >
                                </cc1:UploadAttachments>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
