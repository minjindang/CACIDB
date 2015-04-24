<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="CoachKind_Upd_01.aspx.cs" Inherits="CoachKind_Upd_01" %>

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
        輔導類別設定
    </div>
    </div>
    <div class="table_detail_info" >
        <table class="table_lightblue" cellpadding="0" cellspacing="0" border="1" width="100%">
            <tr>
                <td class="lightblue_tb_title_more" ><font style="color: Red">*</font>&nbsp;類別代號: </td>
                <td class="lightblue_tb_text_more">
                    <asp:HiddenField ID="hid_IsNew" runat="server" />
                    <kd:StrTextBox ID="txt_ChKd_Code" runat="server" DB_Length="3" title="類別代號" Width="30" isAllowNull="false" ValGroup="grvQuery"></kd:StrTextBox>
                </td>
                <td class="lightblue_tb_title_more" ><font style="color: Red">*</font>&nbsp;類別主從: </td>
                <td class="lightblue_tb_text_more">
                    <kd:CoDropDownList ID="ddl_ChKd_Order" runat="server" DataTextField="Sys_CdText" 
                    DataValueField="Sys_CdCode" title="類別主從" isAllowNull="false" ValGroup="grvQuery" >
                    </kd:CoDropDownList>
                </td>
            </tr>
            <tr>
                <td class="lightblue_tb_title_more" ><font style="color: Red">*</font>&nbsp;類別名稱: </td>
                <td class="lightblue_tb_text_more" colspan="3">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <kd:StrTextBox ID="txt_ChKd_Name" runat="server" DB_Length="100" title="類別名稱" Width="95%" isAllowNull="false" ValGroup="grvQuery"></kd:StrTextBox>
                </td>
            </tr>
            <tr>
                <td class="lightblue_tb_title_more" >備註: </td>
                <td class="lightblue_tb_text_more" colspan="3">
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <kd:StrTextBox ID="txt_ChKd_Note" runat="server" DB_Length="200" title="備註" Width="95%" isAllowNull="true" ValGroup="grvQuery"></kd:StrTextBox>
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
                                    輔導類別
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
                                    BorderWidth="1px" DataKeyNames="ChKd_Code" 
                                    CssClass="table_lightblue" onrowdatabound="grvQuery_RowDataBound" >
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                            <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ChKd_Name" HeaderStyle-Font-Bold="false" HeaderText="類別名稱"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ChKd_Code" HeaderStyle-Font-Bold="false" HeaderText="類別代號"
                                            HeaderStyle-VerticalAlign="Bottom" >
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ChKd_OrderName" HeaderStyle-Font-Bold="false" HeaderText="類別主從"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="ChKd_Order" HeaderStyle-Font-Bold="false" HeaderText="類別主從"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>

                                       <asp:BoundField DataField="ChKd_Note" HeaderStyle-Font-Bold="false" HeaderText="備註"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsNew" HeaderStyle-Font-Bold="false" HeaderText="是否為新值"
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
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>