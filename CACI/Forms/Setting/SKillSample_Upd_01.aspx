﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="SKillSample_Upd_01.aspx.cs" Inherits="SKillSample_Upd_01" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailInsertContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <div style=" margin-top: 2px; margin-left:16px; position: absolute; float:left; background-color:White; padding-left:2px; ">
    <div style=" float:left;">
        <img src="/CACI/image/blueBall.jpg" />
    </div>
    <div style=" float:left; margin:0px 3px 0px 3px; "> 
        專長資料設定
    </div>
    </div>
    <div class="table_detail_info" >
        <table class="table_lightblue" cellpadding="0" cellspacing="0" border="1" width="100%">
            <tr>
                <td class="lightblue_tb_title_more" ><font style="color: Red">*</font>&nbsp;專長分類: </td>
                <td class="lightblue_tb_text_more">
                    <asp:DropDownList ID="ddl_Ski_Kind" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="lightblue_tb_title_more" ><font style="color: Red">*</font>&nbsp;專長名稱: </td>
                <td class="lightblue_tb_text_more">
                     <kd:StrTextBox ID="txt_Ski_Name" runat="server" DB_Length="100" title="專長名稱" Width="100" isAllowNull="false" ValGroup="grvQuery"></kd:StrTextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailControlContent" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" >
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
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="DetailDataContent" runat="Server">
    <!-- Detail 資料列式區 -->
    <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table cellpadding="0px" cellspacing="0px" align="right" width="100%" >
                    <tr> 
                        <td>
                            <div style=" margin-top: 10px; margin-left:16px; position: absolute; float:left; background-color:White; padding-left:2px; ">
                                <div style=" float:left; ">
                                    <img src="/CACI/image/blueBall.jpg" />
                                </div>
                                <div style=" float:left; margin:0px 3px 0px 3px; ">
                                    專長資料
                                </div>
                            </div>
                            <div  style=" float:left; position: absolute; margin: 0px 10px 0px 680px; background-color:white; padding:10px 0px 0px 6px;" >
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
                                    BorderWidth="1px" DataKeyNames="Ski_Num" CssClass="table_lightblue" 
                                    onrowdatabound="grvQuery_RowDataBound" >
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                            <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="Sys_CdText" HeaderStyle-Font-Bold="false" HeaderText="專長分類"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Ski_Name" HeaderStyle-Font-Bold="false" HeaderText="專長名稱"
                                            HeaderStyle-VerticalAlign="Bottom" SortExpression="Ski_Name">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Ski_Kind" >
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
    <table>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>