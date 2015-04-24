<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="PjJudge_Upd_01.aspx.cs" Inherits="PjJudge_Upd_01" %>

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
            <td class="title_2c">專案名稱: </td>
            <td class="text_2c" style="color: Red">
                <asp:Label ID="lbl_Pj_Name" runat="server" Text=""></asp:Label>
                <asp:HiddenField ID="hid_Pj_Code" runat="server" />
            </td>
            <td class="title_2c">專案啟動日期: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_StartDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">承辦人: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_User_Code" runat="server" Text=""></asp:Label>
            </td>
            <td class="title_2c">專案經費: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Pj_Fund" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">專案簡介: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Pj_PjIntro" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">專案註記要點: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Pj_PjNote" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailInsertContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <div style="float: left; margin-top: 2px; margin-left: 16px; position: absolute;
        float: left; background-color: White; padding-left: 2px;">
        <div style="float: left;">
            <img src="/CACI/image/blueBall.jpg" />
        </div>
        <div style="float: left; margin: 0px 3px 0px 3px;">
            顧問/委員 查詢
        </div>
    </div>
    <div class="table_detail_info">
        <table class="table_lightblue" cellpadding="0" cellspacing="0" border="1" width="100%">
            <tr>
                <td class="lightblue_tb_title_more">姓名: </td>
                <td class="lightblue_tb_text_more">
                    <kd:StrTextBox ID="txt_Comm_Name" runat="server" title="顧問/委員 姓名" DB_Length="50"
                        ValGroup="grvQuery"></kd:StrTextBox>
                </td>
                <td class="lightblue_tb_title_more">單位(公司)名稱</td>
                <td class="lightblue_tb_text_more">
                    <kd:StrTextBox ID="txt_Comm_ComName" runat="server" title="顧問/委員 單位(公司)名稱" DB_Length="50"
                        ValGroup="grvQuery"></kd:StrTextBox>
                </td>
            </tr>
            <tr>
                <td class="lightblue_tb_title_more">專長領域: </td>
                <td class="lightblue_tb_text_more" colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <contenttemplate>
                            <kd:CoDropDownList ID="ddl_Ski_Type" runat="server" title="專長領域分類" 
                                DataTextField="Sys_CdText" DataValueField="Sys_CdCode" AutoPostBack="true" 
                                onselectedindexchanged="ddl_Ski_Type_SelectedIndexChanged" isAllowNull="true" ValGroup="grvQuery">
                            </kd:CoDropDownList>
                            <kd:CoDropDownList ID="ddl_Ski_Num" runat="server" title="專長領域" DataTextField="Ski_Name" DataValueField="Ski_Num" isAllowNull="true" ValGroup="grvQuery">
                            </kd:CoDropDownList>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="lightyellow_tb_title_more">組別設定: </td>
                <td class="lightyellow_tb_text_more">
                    <asp:DropDownList ID="ddl_CmGp_Code" runat="server" DataTextField="CmGp_Name" DataValueField="CmGp_Num">
                    </asp:DropDownList>
                </td>
                <td class="lightyellow_tb_title_more">身份設定: </td>
                <td class="lightyellow_tb_text_more">
                    <asp:DropDownList ID="ddl_Pj_Identity" runat="server" DataTextField="Sys_CdText" DataValueField="Sys_CdCode">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailControlContent" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <!-- Detail按鈕區 -->
                <asp:ImageButton ID="btnDTL_Query" runat="server" ImageUrl="~/image/dtl_Query.png" />
                &nbsp;
                <asp:ImageButton ID="btnDTL_INSERT" runat="server" ImageUrl="~/image/dtl_Insert.png" />
                &nbsp;
                <asp:ImageButton ID="btnDTL_CLEAR" runat="server" ImageUrl="~/image/dtl_Clear.png" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="DetailDataContent" runat="Server">
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
                                        <div style="margin-top: 10px; margin-left: 16px; position: absolute; float: left;
                                            background-color: White; padding-left: 2px;">
                                            <div style="float: left;">
                                                <img src="/CACI/image/blueBall.jpg" />
                                            </div>
                                            <div style="float: left; margin: 0px 3px 0px 3px;">
                                                顧問/委員 查詢列表
                                            </div>
                                        </div>
                                        <div style="float: left; position: absolute; margin: 0px 10px 0px 680px; background-color: white;
                                            padding: 10px 0px 0px 6px;">
                                            <asp:Label ID="lblQueryPage" runat="server"></asp:Label>&nbsp;&nbsp;
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
                <!-- QueryDataGridView -->
                <table cellpadding="0px" cellspacing="1px" width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlQueryGridView" runat="server" Width="737px" class="detail_result">
                                <asp:GridView ID="grvQueryData" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="Comm_Code" CssClass="table_lightblue">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server"/>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_all" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Comm_Name" HeaderStyle-Font-Bold="false" HeaderText="姓名"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Comm_ComName" HeaderStyle-Font-Bold="false" HeaderText="單位(公司)名稱"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CommSKill" HeaderStyle-Font-Bold="false" HeaderText="專長領域"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                    <tr>
                        <td>
                            <table cellpadding="0" width="100%" cellspacing="0">
                                <tr>
                                    <td>
                                        <div style="margin-top: 25px; margin-left: 16px; position: absolute; float: left;
                                            background-color: White; padding-left: 2px;">
                                            <div style="float: left;">
                                                <img src="/CACI/image/blueBall.jpg" />
                                            </div>
                                            <div style="float: left; margin: 0px 3px 0px 3px;">
                                                顧問/委員 設定列表
                                            </div>
                                        </div>
                                        <div style="float: left; position: absolute; margin: 15px 10px 0px 680px; background-color: white;
                                            padding: 10px 0px 0px 6px;">
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
                <table cellpadding="0px" cellspacing="1px" width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" class="detail_result_more">
                                <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="Comm_Code" CssClass="table_lightblue" 
                                    onrowdatabound="grvQuery_RowDataBound">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_all" />
                                            <ItemStyle HorizontalAlign="left" Width="55px" CssClass="table_data_white_data" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="Comm_Name" HeaderStyle-Font-Bold="false" HeaderText="姓名"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Comm_ComName" HeaderStyle-Font-Bold="false" HeaderText="單位(公司)名稱"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CommSKill" HeaderStyle-Font-Bold="false" HeaderText="專長領域"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Pj_IdentityText" HeaderStyle-Font-Bold="false" HeaderText="身分"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Pj_Identity" HeaderStyle-Font-Bold="false" HeaderText="身分"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CmGp_Name" HeaderStyle-Font-Bold="false" HeaderText="評選組別"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CmGp_Code" HeaderStyle-Font-Bold="false" HeaderText="評選組別"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
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
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>