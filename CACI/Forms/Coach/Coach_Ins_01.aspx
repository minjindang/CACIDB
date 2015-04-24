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
                                ���(���q)�򥻸��</div>
                        </div>
                        <div style="width: 100%">
                            <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                                style="margin: 15px 10px 10px 10px;">
                <tr>
                    <td class="title_2c">���(���q)�W��: </td>
                    <td class="text_2c" style="color:Red">
                        <asp:HiddenField ID="hid_Com_Code" runat="server" Value="N" />
                        <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" title="���(���q)�W��" isAllowNull="false" ></kd:StrTextBox>
                        <asp:Label ID="lbl_Pj_Code" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td class="title_2c"><span style="color:Red;">*</span>�νs/�����Ҧr��/�߮׸�: </td>
                    <td class="text_2c">
                        <kd:IDNumTextBox ID="txt_Com_Tonum" runat="server" DB_Length="30" title="�νs/�����Ҧr��/�߮׸�" 
                           ontextchanged="txt_Com_Tonum_TextChanged" 
                            AutoPostBack="True" isAllowNull="false" ></kd:IDNumTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">���(���q)�t�d�H: </td>
                    <td class="text_2c">
                         <kd:StrTextBox ID="txt_Com_Boss" runat="server" DB_Length="50" title="���(���q)�t�d�H" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                    <td class="title_2c">�t�d�H�����Ҧr��: </td>
                    <td class="text_2c">
                        <kd:StrTextBox ID="txt_Com_BsIDNO" runat="server" DB_Length="50" title="�t�d�H�����Ҧr��" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�t�d�H�ʧO: </td>
                    <td class="text_2c">
                        <asp:RadioButtonList ID="rbl_Com_BsGender" runat="server">
                        </asp:RadioButtonList>
                
                    </td>
                    <td class="title_2c">�t�d�H���: </td>
                    <td class="text_2c">
                        <kd:StrTextBox ID="txt_Com_BsCell" runat="server" DB_Length="50" title="�t�d�H���" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�t�d�H�~�ּh: </td>
                    <td class="text_2c" colspan="3">
                        <asp:RadioButtonList ID="rbl_Com_BsAgeRange" runat="server">
                        </asp:RadioButtonList>
                
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�t�d�H�̰��Ǿ�: </td>
                    <td class="text_2c" colspan="3">
                        <asp:RadioButtonList ID="rbl_Com_BsDegree" runat="server">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�n�O����: </td>
                    <td class="text_2c" >
                         <asp:DropDownList ID="ddl_Com_OrgForm" runat="server" DB_Length="20" title="�n�O����" isAllowNull="false" ></asp:DropDownList>
                    </td>
                    <td class="title_2c">���߮ɶ�: </td>
                    <td class="text_2c" >
                                             <kd:DateTextBox runat="server"  ID="txt_Com_SetupTime" title="���߮ɶ�" DateType="Taiwan" isAllowNull="false" > </kd:DateTextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Com_SetupTime" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd"  ></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�ꥻ�B: </td>
                    <td class="text_2c" >
                         <kd:StrTextBox ID="txt_Com_Capital" runat="server" DB_Length="20" title="�ꥻ�B" isAllowNull="false" ></kd:StrTextBox>(��)
                    </td>
                    <td class="title_2c">���u��: </td>
                    <td class="text_2c" >
                         <kd:StrTextBox ID="txt_Com_EmpNum" runat="server" DB_Length="10" title="���u��" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�̪�@�~��~�B:</td>
                    <td class="text_2c" colspan="3">
                        <kd:StrTextBox ID="txt_Com_LTover" runat="server" DB_Length="10" title="�̪�@�~��~�B" isAllowNull="false" ></kd:StrTextBox>(��)
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�D�n���~�ΪA��:</td>
                    <td class="text_2c" colspan="3">
                        <kd:StrTextBox ID="txt_Com_MnProduct" runat="server" DB_Length="500" title="�D�n���~�ΪA��" isAllowNull="false" TextMode="MultiLine" Height="100" Width="95%" ></kd:StrTextBox>
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
                        �Ȧ���</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">���ӻȦ�:</td>
                            <td class="text_2c" colspan="2">
                                 <asp:HiddenField ID="hid_BAcc_Code" runat="server" Value="N" />
                                <%--<asp:DropDownList ID="ddl_BAcc_Bankno" runat="server" DB_Length="20" title="�Ȧ�" isAllowNull="false"></asp:DropDownList>--%>
                                <kd:CoDropDownList ID="ddl_BAcc_Bankno" runat="server" title="�Ȧ�" isAllowNull="false">
                                </kd:CoDropDownList>
                                �Ȧ�
                            </td>
                            <td class="text_2c" colspan="1">
                                <kd:StrTextBox ID="txt_BAcc_Accno" runat="server" DB_Length="20" title="����" isAllowNull="false"></kd:StrTextBox>����
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�ūH(���~):</td>
                            <td class="text_more" >
                                <asp:RadioButtonList ID="rbl_BAcc_Mcredit" runat="server">
                                </asp:RadioButtonList>
                            </td>
                           <td class="title_2c">�ūH(�t�d�H/�t��):</td>
                            <td class="text_more" >
                                <asp:RadioButtonList ID="rbl_BAcc_Scredit" runat="server">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">���H(���~):</td>
                            <td class="text_2c">
                                <asp:RadioButtonList ID="rbl_BAcc_MCheck" runat="server">
                                </asp:RadioButtonList>
                            </td>
                            <td class="title_2c">���H(�t�d�H/�t��):</td>
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
                        �p����T</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">�p���H�m�W:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttName" runat="server" DB_Length="50" title="�p���H�m�W" isAllowNull="false"
                                    Width="90%"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">�p���H���:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttCell" runat="server" DB_Length="50" title="�p���H���" isAllowNull="false"
                                    Width="90%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�p���H���q�q��:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttTel" runat="server" DB_Length="50" title="�p���H���q�q��"
                                    isAllowNull="false" Width="90%"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">�ǯu:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Fax" runat="server" DB_Length="50" title="�ǯu"
                                     Width="90%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�p���He-mail:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_CttMail" runat="server" DB_Length="50" title="�p���He-mail"
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
                        �ӽл��ɶ���(�`�p�ФĿ�@��)</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                         <uc1:CoachKindRadioButtonList ID="CoachKindRadioButtonList1" runat="server" />
                        <tr>
                            <td class="title_2c">�л����ӽж��ز{���q�ҹJ�쪺���D: </td>
                            <td class="text_2c">
                            <asp:HiddenField ID="hid_Coach_Code" runat="server" Value="N" />
                            <kd:StrTextBox ID="txt_Coach_QstText" runat="server" DB_Length="50" title="�л����ӽж��ز{���q�ҹJ�쪺���D"
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
                        �˪���ƤW��</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">���q²�����:</td>
                            <td class="text_2c" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="ComAtt_Code1" runat="server" Value="N" />
                                        <cc1:UploadAttachments runat="server" ID="Uploader_IT" InsertText="����ɮ�" MultipleFilesUpload="false"
                                            ShowTableHeader="false" OnFileUploaded="Uploader_IT_FileUploaded" ShowCheckBoxes="false" 
                                            onattachmentremoveclicked="Uploader_IT_AttachmentRemoveClicked" >
                                        </cc1:UploadAttachments>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�ӷ~�Τ��q�n�O�v��:</td>
                            <td class="text_2c" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="ComAtt_Code2" runat="server" Value="N" />
                                        <cc1:UploadAttachments runat="server" ID="Uploader_RP" InsertText="����ɮ�" MultipleFilesUpload="false"
                                            ShowTableHeader="false" OnFileUploaded="Uploader_RP_FileUploaded" ShowCheckBoxes="false" 
                                            onattachmentremoveclicked="Uploader_RP_AttachmentRemoveClicked" >
                                        </cc1:UploadAttachments>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�̪�@�~�]�ȷ|�p��������v������~�|�ӳ��Ѽv��:</td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="ComAtt_Code3" runat="server" Value="N" />
                                        <cc1:UploadAttachments runat="server" ID="Uploader_FA" InsertText="����ɮ�" MultipleFilesUpload="false"
                                            ShowTableHeader="false" OnFileUploaded="Uploader_FA_FileUploaded" ShowCheckBoxes="false" 
                                            onattachmentremoveclicked="Uploader_FA_AttachmentRemoveClicked" >
                                        </cc1:UploadAttachments>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�˪���L����:</td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="ComAtt_Code4" runat="server" Value="N" />
                                        <cc1:UploadAttachments runat="server" ID="Uploader_OT" InsertText="����ɮ�" MultipleFilesUpload="false"
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
