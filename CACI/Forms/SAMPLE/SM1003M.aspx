<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="SM1003M.aspx.cs" Inherits="SM1003M" %>
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
            <td class="title_2c">Mcol_1 :</td>
            <td class="text_2c" style="color: Red">
                <asp:Label ID="lbl_Mcol_1" runat="server"></asp:Label>
            </td>
            <td class="title_2c">Mcol_2 :</td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Mcol_2" runat="server" DB_Length="10" title="Mcol_2" Width="80"
                    isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">Mcol_3 :</td>
            <td class="text_2c">
                <kd:NumTextBox ID="txt_Mcol_3" runat="server" title="Mcol_3" DB_IntLength="10" DB_DecLength="0"
                    Width="20" isAllowNull="false"></kd:NumTextBox>
            </td>
            <td class="title_2c"></td>
            <td class="text_2c"></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Detail資訊">
            <contenttemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <div style=" margin-top: 12px; margin-left:5px; position: absolute; float:left; background-color:White; ">
                                            <div style=" float:left; padding-top:2.5px; padding-left:3px;">
                                                <img src="/CACI/image/yellow_ball.png" />
                                            </div>
                                            <div style=" float:left; padding-left:3px; padding-top:3px; font-weight:bold ">
                                                Detail資訊
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="table_lightblue" style=" margin-top:35px;" cellpadding="0" cellspacing="0" border="1" width="100%">
                                            <tr>
                                                <td class="lightblue_tb_title_more">Dcol_2<font style="color: Red">*</font>&nbsp;
                                                </td>
                                                <td class="lightblue_tb_text_more">
                                                    <asp:HiddenField ID="hid_Dcol_1" runat="server" Value="N" />
                                                    <kd:StrTextBox ID="txt_Dcol_2" runat="server" title="Dcol_2" DB_Length="10" isAllowNull="false"
                                                        ValGroup="grvQuery"></kd:StrTextBox>
                                                </td>
                                                <td class="lightblue_tb_title_more">Dcol_3</td>
                                                <td class="lightblue_tb_text_more">
                                                    <kd:StrTextBox ID="txt_Dcol_3" runat="server" title="Dcol_3" DB_Length="10" ValGroup="grvQuery"></kd:StrTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="br"></td>
                    </tr>
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
                    <tr>
                        <td class="br"></td>
                    </tr>
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
                                                                <div style="margin-left:5px; margin-top:-10px; float:left; background-color:White; ">
                                                                    <div style=" float:left; padding-left:3px;">
                                                                        <img src="/CACI/image/yellow_ball.png" />
                                                                    </div>
                                                                    <div style=" float:left; padding-left:3px; font-weight:bold ">
                                                                       Detail 資訊列表
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left:300px; margin-top:-10px; position: absolute; float:left; background-color:White; ">
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
                                                        <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Dcol_1" CssClass="table_lightblue" style=" margin-top:10px">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="mod" ImageUrl="~/image/btn_Detail.png">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                                                                    <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                                                </asp:ButtonField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                                                    <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="Dcol_2" HeaderStyle-Font-Bold="false" HeaderText="Dcol_2"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Dcol_3" HeaderStyle-Font-Bold="false" HeaderText="Dcol_3"
                                                                    HeaderStyle-VerticalAlign="Bottom">
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
                        </td>
                    </tr>
                </table>
            </contenttemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Detail2資訊">
            <contenttemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <div style=" margin-top: 12px; margin-left:5px; position: absolute; float:left; background-color:White; ">
                                            <div style=" float:left; padding-top:2.5px; padding-left:3px;">
                                                <img src="/CACI/image/yellow_ball.png" />
                                            </div>
                                            <div style=" float:left; padding-left:3px; padding-top:3px; font-weight:bold ">
                                                Detail 2資訊 
                                            </div>
                                        </div>                                    
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="table_lightblue" style=" margin-top:35px;" cellpadding="0" cellspacing="0" border="1" width="100%">
                                            <tr>
                                                <td class="lightblue_tb_title_more">Dcol_22<font style="color: Red">*</font>&nbsp;
                                                </td>
                                                <td class="lightblue_tb_text_more">
                                                    <asp:HiddenField ID="hid_Dcol_21" runat="server" Value="N" />
                                                    <kd:StrTextBox ID="txt_Dcol_22" runat="server" title="Dcol_22" DB_Length="10" isAllowNull="false"
                                                        ValGroup="grvQuery2"></kd:StrTextBox>
                                                </td>
                                                <td class="lightblue_tb_title_more">Dcol_23</td>
                                                <td class="lightblue_tb_text_more">
                                                    <kd:StrTextBox ID="txt_Dcol_23" runat="server" title="Dcol_23" DB_Length="10" ValGroup="grvQuery2"></kd:StrTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="br"></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <!-- Detail按鈕曲 -->
                            <asp:ImageButton ID="btnDTL_INSERT2" runat="server" ImageUrl="~/image/dtl_Insert.png" />
                            &nbsp;
                            <asp:ImageButton ID="btnDTL_UPDATE2" runat="server" ImageUrl="~/image/dtl_Update.png" />
                            &nbsp;
                            <asp:ImageButton ID="btnDTL_CLEAR2" runat="server" ImageUrl="~/image/dtl_Clear.png" />
                        </td>
                    </tr>
                    <tr>
                        <td class="br"></td>
                    </tr>
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
                                                                <div style="margin-left:5px; margin-top:-10px; float:left; background-color:White; ">
                                                                    <div style=" float:left; padding-left:3px;">
                                                                        <img src="/CACI/image/yellow_ball.png" />
                                                                    </div>
                                                                    <div style=" float:left; padding-left:3px; font-weight:bold ">
                                                                       Detail2 資訊列表
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left:300px; margin-top:-10px; position: absolute; float:left; background-color:White; ">
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
                                                    <asp:Panel ID="pnlGridView2" runat="server" Width="100%" CssClass="inScroll">
                                                        <asp:GridView ID="grvQuery2" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="1px" DataKeyNames="Dcol_21" CssClass="table_lightblue" style=" margin-top:10px">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                                                                    <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                                                </asp:ButtonField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                                                    <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="Dcol_22" HeaderStyle-Font-Bold="false" HeaderText="Dcol_22"
                                                                    ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                                    <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                                    <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Dcol_23" HeaderStyle-Font-Bold="false" HeaderText="Dcol_23"
                                                                    HeaderStyle-VerticalAlign="Bottom">
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
                        </td>
                    </tr>
                </table>
            </contenttemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
