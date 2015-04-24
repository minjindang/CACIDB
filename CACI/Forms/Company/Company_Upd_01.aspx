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
                        �򥻸��
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">���W��: </td>
                            <td class="text_2c" style="color: Red">
                                <asp:Label ID="lblComm_Code" runat="server" Visible="false"></asp:Label>
                                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" title="���q�W��" isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">���νs: </td>
                            <td class="text_2c">
                                <kd:CoIDNumTextBox ID="txt_Com_Tonum" runat="server" title="���q�νs" isAllowNull="false"></kd:CoIDNumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">���t�d�H: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Boss" runat="server" DB_Length="50" title="���q�t�d�H" isAllowNull="true"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">�t�d�H�����Ҧr��: </td>
                            <td class="text_2c">
                                <kd:IDNumTextBox ID="txt_Com_BsIDNO" runat="server" title="�t�d�H�����Ҧr��" isAllowNull="true"></kd:IDNumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">��´����: </td>
                            <td class="text_2c" colspan="3">
                                <kd:CoDropDownList ID="ddl_Com_OrgForm" runat="server" title="��´����" 
                                    DataTextField="Sys_CdText" DataValueField="Sys_CdCode" isAllowNull="true">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">���ꥻ�B: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Capital" runat="server" DB_Length="20" title="���q�ꥻ�B" isAllowNull="true"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">�����u��: </td>
                            <td class="text_2c">
                                <kd:NumTextBox ID="txt_Com_EmpNum" runat="server" DB_IntLength="10" DB_DecLength="0" title="���q���u��" isAllowNull="true"></kd:NumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�D�n���~�O: </td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_Com_MnSectors" runat="server" title="�D�n���~�O"
                                    DataTextField="Sys_CdText" isAllowNull="true" DataValueField="Sys_CdCode" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddl_Com_MnSectors_SelectedIndexChanged">
                                </kd:CoDropDownList>
                                <kd:StrTextBox ID="txt_Com_MnOther" runat="server" DB_Length="100" title="�D�n���~�O" Visible="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">���n���~�O: </td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_Com_SbSectors" runat="server" title="���n���~�O"
                                    DataTextField="Sys_CdText" isAllowNull="true" DataValueField="Sys_CdCode" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddl_Com_SbSectors_SelectedIndexChanged">
                                </kd:CoDropDownList>
                                <kd:StrTextBox ID="txt_Com_SbOther" runat="server" DB_Length="100" title="���n���~�O" Visible="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�D�n���~�ΪA��: </td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_Com_MnProduct" runat="server" DB_Length="500" title="�D�n���~�ΪA��" Width="95%" isAllowNull="true"></kd:StrTextBox>
                            </td>            
                        </tr>
                        <tr>
                            <td class="title_2c">�D�n���~�ΪA��<br />����W��: </td>
                            <td class="text_2c" colspan="3">
                                <asp:HyperLink ID="hpl_ComMnPd_file1" runat="server"></asp:HyperLink>
                                <cc1:UploadAttachments runat="server" ID="Uploader_Product" InsertText="����ɮ�" MultipleFilesUpload="false"
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
                        ����p����T
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">���q��: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Tel" runat="server" DB_Length="20" title="���q�q��" isAllowNull="true"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">���ǯu: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Fax" runat="server" DB_Length="20" title="���q�ǯu" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">���a�}:</td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_Com_OPAddr" runat="server" DB_Length="200" title="���q�a�}" Width="95%" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�����}:</td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_Com_Url" runat="server" DB_Length="200" title="���q���}" Width="95%" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">���E-mail:</td>
                            <td class="text_2c" colspan="3">
                                <kd:EmailTextBox ID="txt_Com_Email" runat="server" title="���qE-mail" Width="95%" isAllowNull="true"></kd:EmailTextBox>
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
                        �p���H��T
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">�p���H�m�W: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttName" runat="server" DB_Length="50" title="�p���H�m�W" isAllowNull="true"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">�p���H���: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttCell" runat="server" DB_Length="10" title="�p���H���" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�p���H���q��:</td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_Com_CttTel" runat="server" DB_Length="20" title="�p���H���q�q��" isAllowNull="true"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�p���HE-mail:</td>
                            <td class="text_2c" colspan="3">
                                <kd:EmailTextBox ID="txt_Com_CttMail" runat="server" title="�p���HE-mail" Width="95%" isAllowNull="true"></kd:EmailTextBox>
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
                        ���˪���ƤW��
                    </div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">    
                        <tr>
                            <td class="title_2c">���]�ߵn�O�v��:</td>
                            <td class="text_2c" colspan="3">
                                <asp:Label ID="lbl_ComAtt_Code2" runat="server" Visible="false"></asp:Label>
                                <asp:HyperLink ID="hpl_ComAtt_file2" runat="server"></asp:HyperLink>
                                <cc1:UploadAttachments runat="server" ID="Upload_RP" InsertText="����ɮ�" MultipleFilesUpload="false"
                                    ShowTableHeader="false" OnFileUploaded="Uploader_RP_FileUploaded" ShowCheckBoxes="false" 
                                    onattachmentremoveclicked="Uploader_RP_AttachmentRemoveClicked" >
                                </cc1:UploadAttachments>                                
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">���|��Ƽv��:</td>
                            <td class="text_2c" colspan="3">
                                <asp:Label ID="lbl_ComAtt_Code3" runat="server" Visible="false"></asp:Label>
                                <asp:HyperLink ID="hpl_ComAtt_file3" runat="server"></asp:HyperLink>
                                <cc1:UploadAttachments runat="server" ID="Upload_FA" InsertText="����ɮ�" MultipleFilesUpload="false"
                                    ShowTableHeader="false" OnFileUploaded="Uploader_FA_FileUploaded" ShowCheckBoxes="false" 
                                    onattachmentremoveclicked="Uploader_FA_AttachmentRemoveClicked" >
                                </cc1:UploadAttachments>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�˪���L���l:</td>
                            <td class="text_2c" colspan="3">
                                <asp:Label ID="lbl_ComAtt_Code4" runat="server" Visible="false"></asp:Label>
                                <asp:HyperLink ID="hpl_ComAtt_file4" runat="server"></asp:HyperLink>
                                <cc1:UploadAttachments runat="server" ID="Upload_OT" InsertText="����ɮ�" MultipleFilesUpload="false"
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
