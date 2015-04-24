<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Consulting_Lis_01.aspx.cs" Inherits="Consulting_Lis_01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="text_2c" colspan="4">單位(公司)基本資料</td>
        </tr>
        <tr>
            <td class="title_2c">單位(公司)名稱: </td>
            <td class="text_2c" style="color:Red">
                <asp:HiddenField ID="hid_Com_Code" runat="server" Value="N" />
                <asp:Label ID="lbl_Com_Name" runat="server" DB_Length="100" title="單位(公司)名稱"></asp:Label>
            </td>
            <td class="title_2c">統編/身份證字號/立案號: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Tonum" runat="server" DB_Length="100" title="統編/身份證字號/立案號" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">單位(公司)負責人: </td>
            <td class="text_2c">
                
                 <asp:Label ID="lbl_Com_Boss" runat="server" DB_Length="100" title="單位(公司)負責人" ></asp:Label>
            </td>
            <td class="title_2c">成立時間: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_SetupTime" runat="server" DB_Length="100" title="成立時間" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">資本額: </td>
            <td class="text_2c" >
                  <asp:Label ID="lbl_Com_Capital" runat="server" DB_Length="100" title="資本額"></asp:Label>
            </td>
            <td class="title_2c">員工數: </td>
            <td class="text_2c" >
                 <asp:Label ID="lbl_Com_EmpNum" runat="server" DB_Length="100" title="員工數"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">最近一年營業額:</td>
            <td class="text_2c">
                 <asp:Label ID="lbl_Com_LTover" runat="server" DB_Length="100" title="員工數" ></asp:Label>
            </td>
            <td class="title_2c">產業類別: </td>
            <td class="text_2c" >
                <KD:CoDropDownList ID="ddl_Com_MnSectors" runat="server" title="產業類別"
                    RepeatDirection="Horizontal" Enabled="false">
                </KD:CoDropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">營運模式:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Com_OPMode" runat="server" DB_Length="100" title="營運模式"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">公司發展現況:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Com_OPStatus" runat="server" DB_Length="100" title="公司發展現況"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">主要產品及服務:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Com_MnProduct" runat="server" DB_Length="100" title="主要產品及服務" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text_2c" colspan="4">單位(公司)聯絡資訊</td>
        </tr>
        <tr>
            <td class="title_2c">單位(公司)電話:</td>
            <td class="text_2c">
                 <asp:Label ID="lbl_Com_Tel" runat="server" DB_Length="100" title="單位(公司)電話" ></asp:Label>
            </td>
            <td class="title_2c">單位(公司)傳真: </td>
            <td class="text_2c" >
                  <asp:Label ID="lbl_Com_Fax" runat="server" DB_Length="100" title="單位(公司)電話" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">單位(公司)地址:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Com_OPAddr" runat="server" DB_Length="100" title="單位(公司)地址" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">單位(公司)網址:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Com_Url" runat="server" DB_Length="100" title="單位(公司)網址" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">單位(公司)e-mail:</td>
            <td class="text_2c" colspan="3">
                 <asp:Label ID="lbl_Com_Email" runat="server" DB_Length="100" title="單位(公司)e-mail" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text_2c" colspan="4">聯絡人資訊</td>
        </tr>
        <tr>
            <td class="title_2c">聯絡人姓名:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Com_CttName" runat="server" DB_Length="100" title="聯絡人姓名" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">聯絡人公司電話:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Com_CttTel" runat="server" DB_Length="100" title="聯絡人公司電話" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">聯絡人e-mail:</td>
            <td class="text_2c" colspan="3">
                   <asp:Label ID="lbl_Com_CttMail" runat="server" DB_Length="100" title="聯絡人e-mail" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text_2c" colspan="4">諮詢內容</td>
        </tr>
        <tr>
            <td class="title_2c">詢問方式:</td>
            <td class="text_2c" colspan="3">
                <asp:HiddenField ID="hid_Cnst_Code" runat="server" Value="N" />
                <KD:CoDropDownList ID="ddl_Cnst_CntWay" runat="server" title="詢問方式"
                    RepeatDirection="Horizontal" Enabled="false">
                </KD:CoDropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">詢問類別:</td>
            <td class="text_2c" colspan="3">
                <asp:RadioButtonList ID="ckl_CntClass_Code" runat="server" title="詢問類別" RepeatColumns="5"
                    RepeatDirection="Horizontal" Enabled="false">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">詢問內容:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Cnst_CntText" runat="server" DB_Length="100" title="詢問內容" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">案件狀態:</td>
            <td class="text_2c" colspan="3">
                <KD:CoDropDownList ID="ddl_Cnst_Status" runat="server" title="案件狀態"
                    RepeatDirection="Horizontal" isAllowNull="false" Enabled="false">
                </KD:CoDropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="諮詢回覆歷程">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <!-- Detail 資料列式區 -->
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
                                                                        諮詢回覆歷程
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; margin-top: -10px; position: absolute; float: left;
                                                                    background-color: White;">
                                                                    <asp:Label ID="lblPage" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                                    <asp:Panel ID="pnlGridView" runat="server" Width="100%" CssClass="inScroll">
                                                        <kd:MDGridView ID="grv_CntReply" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="CtRepl_Code" CssClass="table_lightblue" Style="margin-top: 10px"
                                                            CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png" LoadControlMode="UserControl"
                                                            TemplateCachingBase="Tablename" TemplateDataMode="RunTime" 
                                                            ControlColumnWidth="39px">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="CtRepl_Date" HeaderText="回覆時間">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CtRepl_RpWay" HeaderText="回覆方式">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <AlternatingRowStyle BackColor="#DEE9FC" />
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
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="電話紀錄歷程">
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
                                                                        電話紀錄歷程
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: -60px; position: absolute; float: left; background-color: White;">
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
                                                    <asp:Panel ID="pnlGridView2" runat="server" Width="100%">
                                                        <kd:MDGridView ID="grv_Phone" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Case_Code,PhRec_Code" CssClass="table_lightblue"
                                                            Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                                            LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime">
                                                            <Columns>
                                                                <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                                                <asp:BoundField DataField="PRcRp_Date" HeaderStyle-Font-Bold="false" HeaderText="紀錄時間"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CntClass_Code" HeaderStyle-Font-Bold="false" HeaderText="提問類別"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PRcRp_Handle" HeaderStyle-Font-Bold="false" HeaderText="處理方式"
                                                                    HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <AlternatingRowStyle BackColor="#DEE9FC" />
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
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
