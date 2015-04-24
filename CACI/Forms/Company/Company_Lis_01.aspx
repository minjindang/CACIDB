<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Company_Lis_01.aspx.cs" Inherits="Company_Lis_01" %>

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
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="業者基本資料">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail1 資料列式區 -->
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
                                                                        業者基本資料
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
                                                                                <asp:Label ID="lbl_Com_Name" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">單位統編: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Tonum" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">單位負責人: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Boss" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">負責人身份證字號: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_BsIDNO" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">組織型式: </td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <kd:CoDropDownList ID="ddl_Com_OrgForm" runat="server" title="組織型式" 
                                                                                    DataTextField="Sys_CdText" DataValueField="Sys_CdCode" isAllowNull="True" 
                                                                                    NoMk="False" Enabled="False">
                                                                                </kd:CoDropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">單位資本額: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Capital" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">單位員工數: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_EmpNum" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">主要產業別: </td>
                                                                            <td class="text_2c">
                                                                                <kd:CoDropDownList ID="ddl_Com_MnSectors" runat="server" title="主要產業別"
                                                                                    DataTextField="Sys_CdText" isAllowNull="True" DataValueField="Sys_CdCode" 
                                                                                    NoMk="False" Enabled="False">
                                                                                </kd:CoDropDownList>
                                                                                <asp:Label ID="lbl_Com_MnOther" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">次要產業別: </td>
                                                                            <td class="text_2c">
                                                                                <kd:CoDropDownList ID="ddl_Com_SbSectors" runat="server" title="次要產業別"
                                                                                    DataTextField="Sys_CdText" isAllowNull="True" DataValueField="Sys_CdCode" 
                                                                                    NoMk="False" Enabled="False">
                                                                                </kd:CoDropDownList>
                                                                                <asp:Label ID="lbl_Com_SbOther" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">主要產品及服務: </td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_MnProduct" runat="server"></asp:Label>
                                                                            </td>            
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">主要產品及服務<br>附件上傳: </td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:HyperLink ID="hpl_ComMnPd_file1" runat="server"></asp:HyperLink>
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
                                                                                <asp:Label ID="lbl_Com_Tel" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">單位傳真: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_Fax" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">單位地址:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_OPAddr" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">單位網址:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_Url" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">單位E-mail:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_Email" runat="server"></asp:Label>
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
                                                                                <asp:Label ID="lbl_Com_CttName" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="title_2c">聯絡人手機: </td>
                                                                            <td class="text_2c">
                                                                                <asp:Label ID="lbl_Com_CttCell" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">聯絡人單位電話:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_CttTel" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">聯絡人E-mail:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:Label ID="lbl_Com_CttMail" runat="server"></asp:Label>
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
                                                                                <asp:HyperLink ID="hpl_ComAtt_file2" runat="server"></asp:HyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">完稅資料影本:</td>
                                                                            <td class="text_2c" colspan="3">
                                                                                <asp:HyperLink ID="hpl_ComAtt_file3" runat="server"></asp:HyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="title_2c">檢附其他文件l:</td>
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
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="申請補助案歷程">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail2 資料列式區 -->
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
                                                                        申請補助案歷程
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
                                                    <asp:Panel ID="pnl_Aow" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_Aow" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Com_Code,Aow_Code,Pj_Code" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn>
                                                                </kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="List" HeaderStyle-Font-Bold="false" HeaderText="序號"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Pj_Class_Text" HeaderStyle-Font-Bold="false" HeaderText="專案種類"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Pj_Name" HeaderStyle-Font-Bold="false" HeaderText="計畫名稱"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Aow_Date" HeaderStyle-Font-Bold="false" HeaderText="申請日期"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Aow_Code" HeaderStyle-Font-Bold="false" HeaderText="申請編號"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Aow_Status_Text" HeaderStyle-Font-Bold="false" HeaderText="審核狀態"
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
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="諮詢歷程">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail3 資料列式區 -->
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
                                                                        諮詢歷程
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
                                                    <asp:Panel ID="pnl_Coach1" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_Coach1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Com_Code,Cnst_Code" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn>
                                                                </kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="List" HeaderStyle-Font-Bold="false" HeaderText="序號"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cnst" HeaderStyle-Font-Bold="false" HeaderText="類別"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                 <asp:BoundField DataField="Cnst_CntWay_Text" HeaderStyle-Font-Bold="false" HeaderText="諮詢方式"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cnst_CntDate" HeaderStyle-Font-Bold="false" HeaderText="諮詢日期"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                 <asp:BoundField DataField="CntClass_Code_Text" HeaderStyle-Font-Bold="false" HeaderText="諮詢類別"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cnst_Status_Text" HeaderStyle-Font-Bold="false" HeaderText="諮詢結果"
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
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="輔導歷程">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail4 資料列式區 -->
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
                                                                        輔導歷程
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="lblPage4" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                    <asp:Panel ID="pnl_Coach2" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_Coach2" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Com_Code,Coach_Code" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn>
                                                                </kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="List" HeaderStyle-Font-Bold="false" HeaderText="序號"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Coach_Date" HeaderStyle-Font-Bold="false" HeaderText="輔導日期"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ChKd_Name" HeaderStyle-Font-Bold="false" HeaderText="輔導項目"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Coach_Status_Text" HeaderStyle-Font-Bold="false" HeaderText="輔導結果"
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
        <%--<!--申請投融資歷程，先不寫-->
        <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="申請投融資歷程">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail5 資料列式區 -->
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
                                                                        申請投融資歷程
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="lblPage5" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                    <asp:Panel ID="pnl_Money" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_Money" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Com_Code" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <asp:BoundField DataField="List" HeaderStyle-Font-Bold="false" HeaderText="序號"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IndustryID" HeaderStyle-Font-Bold="false" HeaderText="投融資專案名稱"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="AreaID" HeaderStyle-Font-Bold="false" HeaderText="申請類別"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Date" HeaderStyle-Font-Bold="false" HeaderText="申請日期"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ApplyLoan" HeaderStyle-Font-Bold="false" HeaderText="申請金額"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Status_Text" HeaderStyle-Font-Bold="false" HeaderText="申請狀態"
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
        --%>
        <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="電話記錄">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail6 資料列式區 -->
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
                                                                        電話記錄
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="lblPage6" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                    <asp:Panel ID="pnl_Phone" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_Phone" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Com_Code,PhRec_Code" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn>
                                                                </kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="List" HeaderStyle-Font-Bold="false" HeaderText="序號"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PhRec_ComCode" HeaderStyle-Font-Bold="false" HeaderText="提問人"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CntClass_Code_Text" HeaderStyle-Font-Bold="false" HeaderText="提問類別"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PRcRp_Handle_Text" HeaderStyle-Font-Bold="false" HeaderText="處理方式"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PRcRp_Text" HeaderStyle-Font-Bold="false" HeaderText="回覆內容"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
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
        <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="管考記錄">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail7 資料列式區 -->
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
                                                                        管考記錄
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; position: absolute; float: left; background-color: White;">
                                                                    <asp:Label ID="lblPage7" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                    <asp:Panel ID="pnl_Evaluations" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_Evaluations" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Com_Code,Aow_Code,Meeting_Code,Meeting_Index" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_exit.jpg"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn>
                                                                </kd:ChildGridViewColumn>
                                                                <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="List" HeaderStyle-Font-Bold="false" HeaderText="序號"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Pj_Name" HeaderStyle-Font-Bold="false" HeaderText="專案名稱"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                 <asp:BoundField DataField="ApPj_Name" HeaderStyle-Font-Bold="false" HeaderText="計畫名稱"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Mety_Name" HeaderStyle-Font-Bold="false" HeaderText="管考記錄類別"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                 <asp:BoundField DataField="Times_Bgn" HeaderStyle-Font-Bold="false" HeaderText="開始時間"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Times_End" HeaderStyle-Font-Bold="false" HeaderText="結束時間"
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
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
</asp:Content>
