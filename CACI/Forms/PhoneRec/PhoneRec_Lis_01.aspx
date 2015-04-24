<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="PhoneRec_Lis_01.aspx.cs" Inherits="PhoneRec_Lis_01" %>

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
            <td class="title_2c">稅編/身份證字號/立案號: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Com_Tonum" runat="server" DB_Length="100" title="單位(公司)名稱" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">聯絡人姓名:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Com_CttName" runat="server" DB_Length="100" title="聯絡人姓名" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">聯絡人e-mail:</td>
            <td class="text_2c" colspan="3">
                   <asp:Label ID="lbl_Com_CttMail" runat="server" DB_Length="100" title="聯絡人e-mail" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text_2c" colspan="4">提問內容</td>
        </tr>
        <tr>
            <td class="title_2c">提問內容:</td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_PhRec_Question" runat="server" DB_Length="100" title="詢問內容" ></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
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
