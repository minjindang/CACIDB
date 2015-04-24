<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="PhoneRec_Upd_02.aspx.cs" Inherits="PhoneRec_Upd_02" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="�~�̰򥻸��">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:HiddenField ID="hid_PhRec_ComCode" runat="server" Value="N" />
                            <asp:HiddenField ID="hid_PhRec_Code" runat="server" Value="N" />
                            <asp:HiddenField ID="hid_PRcRp_Code" runat="server" Value="N" />
                            <!-- Detail1 ��ƦC���� -->
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
                                                                        �~�̰򥻸��
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                                        �򥻸��
                                                                    </div>
                                                                </div>
                                                                <div style="width: 100%">
                                                                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                                                                        style="margin: 15px 10px 10px 10px;">
                                                                        <tr>
                                                                            <td class="title_2c">���W��: </td>
                                                                            <td class="text_2c" style="color: Red">
                                                                                <asp:Label ID="lbl_Com_Name" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">���νs: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Tonum" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">���t�d�H: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Boss" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">�t�d�H�����Ҧr��: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_BsIDNO" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">��´����: </td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <kd:CoDropDownList ID="ddl_Com_OrgForm" runat="server" title="��´����" DataTextField="Sys_CdText"
                                                                                    DataValueField="Sys_CdCode" isAllowNull="True" NoMk="False" Enabled="False">
                                                                                </kd:CoDropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">���ꥻ�B: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Capital" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">�����u��: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_EmpNum" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">�D�n���~�O: </td>
                                                                            <td class="text_2c">
                                                                                <kd:CoDropDownList ID="ddl_Com_MnSectors" runat="server" title="�D�n���~�O" DataTextField="Sys_CdText"
                                                                                    isAllowNull="True" DataValueField="Sys_CdCode" NoMk="False" Enabled="False">
                                                                                </kd:CoDropDownList>
                                                                                <asp:Label ID="lbl_Com_MnOther" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">���n���~�O: </td>
                                                                            <td class="text_2c">
                                                                                <kd:CoDropDownList ID="ddl_Com_SbSectors" runat="server" title="���n���~�O" DataTextField="Sys_CdText"
                                                                                    isAllowNull="True" DataValueField="Sys_CdCode" NoMk="False" Enabled="False">
                                                                                </kd:CoDropDownList>
                                                                                <asp:Label ID="lbl_Com_SbOther" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">�D�n���~�ΪA��: </td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_MnProduct" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">�D�n���~�ΪA��<br>
                                                                                ����W��: </td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:HyperLink ID="hpl_ComMnPd_file1" runat="server"></asp:HyperLink>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                                                        ����p����T
                                                                    </div>
                                                                </div>
                                                                <div style="width: 100%">
                                                                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                                                                        style="margin: 15px 10px 10px 10px;">
                                                                        <tr>
                                                                            <td class="title_2c">���q��: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Tel" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">���ǯu: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Fax" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">���a�}:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_OPAddr" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">�����}:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_Url" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">���E-mail:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_Email" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                                                        �p���H��T
                                                                    </div>
                                                                </div>
                                                                <div style="width: 100%">
                                                                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                                                                        style="margin: 15px 10px 10px 10px;">
                                                                        <tr>
                                                                            <td class="title_2c">�p���H�m�W: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_CttName" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">�p���H���: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_CttCell" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">�p���H���q��:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_CttTel" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">�p���HE-mail:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_CttMail" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                                                        ���˪���ƤW��
                                                                    </div>
                                                                </div>
                                                                <div style="width: 100%">
                                                                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                                                                        style="margin: 15px 10px 10px 10px;">
                                                                        <tr>
                                                                            <td class="title_2c">���]�ߵn�O�v��:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:HyperLink ID="hpl_ComAtt_file2" runat="server"></asp:HyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">���|��Ƽv��:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:HyperLink ID="hpl_ComAtt_file3" runat="server"></asp:HyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">�˪���L���l:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:HyperLink ID="hpl_ComAtt_file4" runat="server"></asp:HyperLink>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
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
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="�ӽиɧU�׾��{">
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
                                                                        �ӽиɧU�׾��{
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="lblPage1" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                                <asp:Panel ID="pnl_grv_Aow" runat="server" Width="100%">
                                                                    <asp:GridView ID="grv_Aow" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        BorderWidth="1px" DataKeyNames="Aow_Code" CssClass="table_lightblue" Style="margin-top: 10px">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderStyle CssClass="table_data_lightblue_head" />
                                                                                <HeaderStyle Font-Bold="False" />
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox runat="server" ID="cbHead" OnCheckedChanged="Aow_SelectAll" AutoPostBack="true">
                                                                                    </asp:CheckBox></HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox Style="text-align:center;" runat="server" ID="cbItem"></asp:CheckBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Aas_Year" HeaderText="��ɧU�~��">
                                                                                <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Aas_PjName" HeaderText="���ɧU�p�e�W��">
                                                                                <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Aas_PjUnit" HeaderText="�ɧU����">
                                                                                <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Aas_Amount" HeaderText="�ɧU���B">
                                                                                <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Aas_Content" HeaderText="�p�e���e�Φ��G">
                                                                                <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                                <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Aas_Relation" HeaderText="�P�����ӽЭp�e���e���p�ΰϹj����">
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
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="�Ը߾��{">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail3 ��ƦC���� -->
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
                                                                        �Ը߾��{
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
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
                                                    <asp:Panel ID="pnl_grv_Consulting" runat="server" Width="737px" CssClass="detail_result">
                                                        <kd:MDGridView ID="grv_Consulting" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Cnst_Code" CssClass="table_lightblue" Style="margin-top: 10px"
                                                            CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn>
                                                                </kd:ChildGridViewColumn>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate >
                                                                        <asp:CheckBox runat="server" ID="cbHead" OnCheckedChanged="Consulting_SelectAll"
                                                                            AutoPostBack="true"></asp:CheckBox></HeaderTemplate>
                                                                    <ItemTemplate >
                                                                        <asp:CheckBox Style="text-align:center;" runat="server" ID="cbItem"></asp:CheckBox>
                                                                         <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Cnst_CntWayName" HeaderStyle-Font-Bold="false" HeaderText="�Ըߤ覡"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cnst_twCntDate" HeaderStyle-Font-Bold="false" HeaderText="�Ըߤ��"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CntClass_CodeName" HeaderStyle-Font-Bold="false" HeaderText="�Ը����O"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cnst_StatusName" HeaderStyle-Font-Bold="false" HeaderText="�Ը߳B�z���G"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="table_data_lightblue_head" />
                                                            <HeaderStyle Font-Bold="False" />
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
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="���ɾ��{">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail4 ��ƦC���� -->
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
                                                                        ���ɾ��{
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="lblPage3" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                    <asp:Panel ID="pnl_grv_Coach" runat="server" Width="737px" CssClass="detail_result">
                                                        <kd:MDGridView ID="grv_Coach" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Coach_Code" CssClass="table_lightblue" Style="margin-top: 10px"
                                                            CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn>
                                                                </kd:ChildGridViewColumn>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox runat="server" ID="cbHead" OnCheckedChanged="Coach_SelectAll" AutoPostBack="true">
                                                                        </asp:CheckBox>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox  runat="server" ID="cbItem"></asp:CheckBox>
                                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Coach_Date" HeaderStyle-Font-Bold="false" HeaderText="�ӽл��ɤ��"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ChKd_Name" HeaderStyle-Font-Bold="false" HeaderText="���ɶ���"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Coach_Status" HeaderStyle-Font-Bold="false" HeaderText="���ɼf�w���G"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="table_data_lightblue_head" />
                                                            <HeaderStyle Font-Bold="False" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
    <asp:ImageButton ID="btn_Archive" runat="server" ImageUrl="~/image/btn_Update.png"
        OnClick="btn_Archive_Click" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
</asp:Content>
