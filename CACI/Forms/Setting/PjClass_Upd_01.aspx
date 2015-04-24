<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="PjClass_Upd_01.aspx.cs" Inherits="PjClass_Upd_01" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <div style="margin-top: 2px; margin-left: 16px; position: absolute; float: left;
        background-color: White; padding-left: 2px;">
        <div style="float: left;">
            <img src="/CACI/image/blueBall.jpg" />
        </div>
        <div style="float: left; margin: 0px 3px 0px 3px;">
            專案計畫類別及申請頁資料設定
        </div>
    </div>
    <div class="table_detail_info">
        <table class="table_lightblue" cellpadding="0" cellspacing="0" border="1" width="100%">
            <tr>
                <td class="lightblue_tb_title_more"><font style="color: Red">*</font>&nbsp;專案計畫代號:
                </td>
                <td class="lightblue_tb_text_more">
                    <kd:StrTextBox ID="txt_PjCs_Code" runat="server" DB_Length="2" title="專案計畫代號"></kd:StrTextBox>
                </td>
                <td class="lightblue_tb_title_more"><font style="color: Red">*</font>&nbsp;專案計畫名稱:
                </td>
                <td class="lightblue_tb_text_more">
                    <kd:StrTextBox ID="txt_PjCs_Name" runat="server" DB_Length="50" title="專案計畫名稱"></kd:StrTextBox>
                </td>
            </tr>
            <tr>
                <td class="lightblue_tb_title_more"><font style="color: Red">*</font>&nbsp;申請頁路徑:
                </td>
                <td class="lightblue_tb_text_more" colspan="3">
                    <kd:StrTextBox ID="txt_PjCs_Path" runat="server" DB_Length="200" title="申請頁路徑" Width="300"
                        isAllowNull="false" ValGroup="grvQuery"></kd:StrTextBox>
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <!-- Detail按鈕區 -->
                <asp:ImageButton ID="btnDTL_INSERT" runat="server" ImageUrl="~/image/dtl_Insert.png" />
                &nbsp;
                <asp:ImageButton ID="btnDTL_UPDATE" runat="server" ImageUrl="~/image/dtl_Update.png" />
                &nbsp;
                <asp:ImageButton ID="btnDTL_CLEAR" runat="server" ImageUrl="~/image/dtl_Clear.png" />
            </td>
        </tr>
    </table>
    <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                    <tr>
                        <td>
                            <div style="margin-top: 10px; margin-left: 16px; position: absolute; float: left;
                                background-color: White; padding-left: 2px;">
                                <div style="float: left;">
                                    <img src="/CACI/image/blueBall.jpg" />
                                </div>
                                <div style="float: left; margin: 0px 3px 0px 3px;">
                                    專案計畫類別及申請頁資料設定資料
                                </div>
                            </div>
                            <div style="float: left; position: absolute; margin: 0px 10px 0px 680px; background-color: white;
                                padding: 10px 0px 0px 6px;">
                                <asp:Label ID="lblPage" runat="server"></asp:Label>&nbsp;&nbsp;
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
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" CssClass="detail_result">
                                <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="PjCs_Code" CssClass="table_lightblue" OnRowDataBound="grvQuery_RowDataBound">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                            <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="PjCs_Code" HeaderStyle-Font-Bold="false" HeaderText="專案計畫代號"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PjCs_Name" HeaderStyle-Font-Bold="false" HeaderText="專案計畫名稱"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PjCs_Path" HeaderStyle-Font-Bold="false" HeaderText="計畫申請表路徑"
                                            HeaderStyle-VerticalAlign="Bottom" SortExpression="Ski_Name">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
                &nbsp;
                <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
                &nbsp;
                <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailInsertContent" runat="Server">
    <!-- 資料列式及查詢區 -->
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailControlContent" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="DetailDataContent" runat="Server">
    <!-- Detail 資料列式區 -->
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
</asp:Content>