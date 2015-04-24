<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Coach_Upd_01.aspx.cs" Inherits="Coach_Upd_01" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/UserControl/ral_Chkd_Code.ascx" tagname="CoachKindRadioButtonList" tagprefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                        <asp:HiddenField ID="hid_Com_Code" runat="server" />
                        <asp:Label ID="lbl_Com_Name" runat="server" ></asp:Label>
                    </td>
                    <td class="title_2c">�|�s/�����Ҧr��/�߮׸�: </td>
                    <td class="text_2c">
                        <asp:Label ID="lbl_Com_Tonum" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">���(���q)�t�d�H: </td>
                    <td class="text_2c">
                         <asp:Label ID="lbl_Com_Boss" runat="server" ></asp:Label>
                    </td>
                    <td class="title_2c">�t�d�H�����Ҧr��: </td>
                    <td class="text_2c">
                         <asp:Label ID="lbl_Com_BsIDNO" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�t�d�H�ʧO: </td>
                    <td class="text_2c">
                        <asp:RadioButtonList ID="rbl_Com_BsGender" runat="server" Enabled="false" RepeatDirection="Horizontal" >
                        </asp:RadioButtonList>
                
                    </td>
                    <td class="title_2c">�t�d�H���: </td>
                    <td class="text_2c">
                        <asp:Label ID="lbl_Com_BsCell" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�t�d�H�~�ּh: </td>
                    <td class="text_2c" colspan="3">
                        <asp:RadioButtonList ID="rbl_Com_BsAgeRange" runat="server" Enabled="false">
                        </asp:RadioButtonList>
                
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�t�d�H�̰��Ǿ�: </td>
                    <td class="text_2c" colspan="3">
                        <asp:RadioButtonList ID="rbl_Com_BsDegree" runat="server" Enabled="false">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�n�O����: </td>
                    <td class="text_2c" >
                         <asp:DropDownList ID="ddl_Com_OrgForm" runat="server" DB_Length="20" title="�n�O����" isAllowNull="false" enabled="false" ></asp:DropDownList>
                    </td>
                    <td class="title_2c">���߮ɶ�: </td>
                    <td class="text_2c" >
                        <asp:Label ID="lbl_Com_SetupTime" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�ꥻ�B: </td>
                    <td class="text_2c" >
                         <asp:Label ID="lbl_Com_Capital" runat="server" ></asp:Label>(��)
                    </td>
                    <td class="title_2c">���u��: </td>
                    <td class="text_2c" >
                          <asp:Label ID="lbl_Com_EmpNum" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�̪�@�~��~�B:</td>
                    <td class="text_2c" colspan="3">
                        <asp:Label ID="lbl_Com_LTover" runat="server" ></asp:Label>(��)
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">�D�n���~�ΪA��:</td>
                    <td class="text_2c" colspan="3">
                        <asp:Label ID="lbl_Com_MnProduct" runat="server" ></asp:Label>
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
                                <asp:DropDownList ID="ddl_BAcc_Bankno" runat="server" DB_Length="20" title="�Ȧ�" isAllowNull="false" Enabled="false" ></asp:DropDownList>�Ȧ�
                            </td>
                            <td class="text_2c" colspan="1">
                                <asp:Label ID="lbl_BAcc_Accno" runat="server" DB_Length="20" title="����" isAllowNull="false"></asp:Label>����
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�ūH(���~):</td>
                            <td class="text_more" >
                                <asp:RadioButtonList ID="rbl_BAcc_Mcredit" runat="server" Enabled="false">
                                </asp:RadioButtonList>
                            </td>
                           <td class="title_2c">�ūH(�t�d�H/�t��):</td>
                            <td class="text_more" >
                                <asp:RadioButtonList ID="rbl_BAcc_Scredit" runat="server" Enabled="false">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">���H(���~):</td>
                            <td class="text_2c">
                                <asp:RadioButtonList ID="rbl_BAcc_MCheck" runat="server" Enabled="false">
                                </asp:RadioButtonList>
                            </td>
                            <td class="title_2c">���H(�t�d�H/�t��):</td>
                            <td class="text_2c">
                                <asp:RadioButtonList ID="rbl_BAcc_SCheck" runat="server" Enabled="false">
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
                                <asp:Label ID="lbl_Com_CttName" runat="server" ></asp:Label>
                            </td>
                            <td class="title_2c">�p���H���:</td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Com_CttCell" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�p���H���q�q��:</td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Com_CttTel" runat="server" ></asp:Label>
                            </td>
                            <td class="title_2c">�ǯu:</td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Com_Fax" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�p���He-mail:</td>
                            <td class="text_more" colspan="3">
                                <asp:Label ID="lbl_Com_CttMail" runat="server" ></asp:Label>
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
                            <td class="text_2c" colspan="3" >
                            <asp:HiddenField ID="hid_Coach_Code" runat="server" Value="N" />
                            <kd:StrTextBox ID="txt_Coach_QstText" runat="server" DB_Length="50" title="�л����ӽж��ز{���q�ҹJ�쪺���D"
                                    isAllowNull="false" Width="95%" Height="60" ></kd:StrTextBox>
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
                               <asp:HiddenField ID="ComAtt_Path1" runat="server" Value="N" />
                                <asp:Button ID="btn_ComAtt_file1" runat="server" Text="�s��" />
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�ӷ~�Τ��q�n�O�v��:</td>
                            <td class="text_2c"  colspan="3">
                                <asp:HiddenField ID="ComAtt_Path2" runat="server" Value="N" />
                                <asp:Button ID="btn_ComAtt_file2" runat="server" Text="�s��" />
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�̪�@�~�]�ȷ|�p��������v������~�|�ӳ��Ѽv��:</td>
                            <td class="text_more" colspan="3">
                                <asp:HiddenField ID="ComAtt_Path3" runat="server" Value="N" />
                                <asp:Button ID="btn_ComAtt_file3" runat="server" Text="�s��" />
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">�˪���L����:</td>
                            <td class="text_more" colspan="3">
                                <asp:Button ID="btn_ComAtt_file4" runat="server" Text="�s��" />
                                <asp:HiddenField ID="ComAtt_Path4" runat="server" Value="N" />
                                
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
