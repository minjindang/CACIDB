<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Coach_Lis_01.aspx.cs" Inherits="Coach_Lis_01" %>

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
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
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
                            <td class="text_2c" colspan="1">
                                <asp:HiddenField ID="hid_BAcc_Code" runat="server" Value="N" />
                                <asp:DropDownList ID="ddl_BAcc_Bankno" runat="server" DB_Length="20" title="�Ȧ�" isAllowNull="false" Enabled="false" ></asp:DropDownList>�Ȧ�
                            </td>
                            <td class="text_2c" colspan="2">
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
                        �ӽл��ɶ���</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">�ӽл��ɶ���: </td>
                            <td class="text_2c" colspan="3" >
                                <asp:Label ID="lbl_ChKd_Name" runat="server" ></asp:Label>
                            </td>
                        </tr>
                         
                        <tr>
                            <td class="title_2c">�л����ӽж��ز{���q�ҹJ�쪺���D: </td>
                            <td class="text_2c" colspan="3" >
                            <asp:HiddenField ID="hid_Coach_Code" runat="server" Value="N" />
                            <asp:Label ID="lbl_Coach_QstText" runat="server" ></asp:Label>
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
                                <asp:Button ID="btn_ComAtt_file1" runat="server" Text="�s��" 
                                    onclick="btn_ComAtt_file1_Click" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="���ɬy�{���q����">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail ��ƦC���� -->
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
                                                                        ���ɬy�{���q����
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="lbl_CoachStage" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                    <asp:Panel ID="pnl_CoachStage" runat="server" Width="100%" CssClass="inScroll">
                                                        <kd:MDGridView ID="grv_CoachStage" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Coach_Code,Pj_Code,Stage_Index" CssClass="table_lightblue" Style="margin-top: 10px"
                                                            CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png" LoadControlMode="UserControl"
                                                            TemplateCachingBase="Tablename" TemplateDataMode="RunTime" ControlColumnWidth="39px">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="ChSg_Date" HeaderText="������">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ChSg_Verify" HeaderText="�f�ֵ��G">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
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
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="�|ĳ�ѻP����">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail2 ��ƦC���� -->
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
                                                                        �|ĳ�ѻP����
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; position: absolute; float: left; background-color: White;">
                                                                    <asp:Label ID="lbl_Meeting" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                    <asp:Panel ID="pnl_Meeting" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_Meeting" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="MtgCom" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="Meeting_Name" HeaderStyle-Font-Bold="false" HeaderText="�|ĳ�W��"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Meeting_BgnTime" HeaderStyle-Font-Bold="false" HeaderText="�|ĳ�ɶ�"
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
