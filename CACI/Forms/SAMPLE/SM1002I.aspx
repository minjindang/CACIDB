<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="SM1002I.aspx.cs" Inherits="SM1002I" %>

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
            <td class="text_2c" style="color: Red">(自動編號) </td>
            <td class="title_2c">Mcol_2 :</td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Mcol_2" runat="server" DB_Length="10" title="Mcol_2" Width="80" isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">Mcol_3 :</td>
            <td class="text_2c">
                <kd:NumTextBox ID="txt_Mcol_3" runat="server" title="Mcol_3" DB_IntLength="10" DB_DecLength="0"
                    Width="20" isAllowNull="false"></kd:NumTextBox>
            </td>
            <td class="title_2c"></td>
            <td class="text_2c">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailInsertContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <div style=" float:left; margin-top: 2px; margin-left:16px; position: absolute; float:left; background-color:White; padding-left:2px; ">
        <div style=" float:left;">
            <img src="/CACI/image/blueBall.jpg" />
        </div>
        <div style=" float:left; margin:0px 3px 0px 3px; ">
            Detail資訊
        </div>
    </div>
    <div class="table_detail_info" >
        <table class="table_lightblue" cellpadding="0" cellspacing="0" border="1" width="100%">
            <tr>
                <td class="lightblue_tb_title_more" >Dcol_2
                    <font style="color: Red">*</font>&nbsp; 
                </td>
                <td class="lightblue_tb_text_more">
                    <asp:HiddenField ID="hid_Dcol_1" runat="server" Value="N" />
                    <kd:StrTextBox ID="txt_Dcol_2" runat="server" title="Dcol_2" DB_Length="10" isAllowNull="false" ValGroup="grvQuery"></kd:StrTextBox>
                </td>
                <td class="lightblue_tb_title_more" >Dcol_3</td>
                <td class="lightblue_tb_text_more">
                    <kd:StrTextBox ID="txt_Dcol_3" runat="server" title="Dcol_3" DB_Length="10" ValGroup="grvQuery"></kd:StrTextBox>
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
                                    聯絡資訊列表
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
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" class="detail_result">
                                <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="Dcol_1" CssClass="table_lightblue" >
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
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
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <table>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>