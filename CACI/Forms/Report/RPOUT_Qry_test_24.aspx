<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Qry_test_24.aspx.cs" Inherits="RPOUT_Qry_test_24" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl" TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="QueryConditionContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">
                獎補助申請案件之編號 :
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Aow_Code" runat="server" DB_Length="11" title="獎補助申請案件之編號" isAllowNull="false"></kd:StrTextBox>
                <font color = "red">*</font>輸入AP00000027或直接點擊查看列印皆能產出報表
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_PrintToExl" runat="server" 
        ImageUrl="~/image/btn_exExcel.png" onclick="btn_PrintToExl_Click" />
    <asp:ImageButton ID="btn_PrintToXML" runat="server" 
        ImageUrl="~/image/btn_exXML.png" onclick="btn_PrintToXML_Click" />
    <asp:ImageButton ID="btn_PrintToPDF" runat="server" 
        ImageUrl="~/image/btn_broPrint.png" onclick="btn_PrintToPDF_Click" />
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" Visible="False" />
    <asp:ImageButton ID="btn_Query" runat="server" ImageUrl="~/image/btn_Query.png" Visible="False" />
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" Visible="False" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="QueryControlContent" runat="Server">
    <table cellpadding="0" cellspacing="0" align="left" style="background-color: transparent">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="background-color: white;">
                    <tr>
                        <td style="padding-left:6px;">
                            <img src="/CACI/image/search_Result.png" />
                        </td>
                        <td style="padding-right:6px;">
                            查詢結果&nbsp;
                            <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background-color: transparent; width: 388px">
                &nbsp;
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" visible="false" width="170px">
                    <tr>
                        <td nowrap="nowrap" style="background-color: White;" align="right">
                            <asp:Panel ID="pnlPageInfo" runat="server" Visible="false" class="tb_btn">
                                <asp:LinkButton ID="lnkPageUP" runat="server">上一頁</asp:LinkButton>/第&nbsp;
                                <asp:DropDownList ID="ddlPageNum" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                頁/<asp:LinkButton ID="lnkPageDown" runat="server">下一頁</asp:LinkButton>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="QueryResultContent" runat="Server">
    <asp:Panel ID="pnlGridView" runat="server" ScrollBars="None" CssClass="search_result">
        <asp:GridView ID="grvQuery" runat="server" AllowPaging="True" PagerSettings-Visible="false"
            AutoGenerateColumns="False" Width="100%" BorderWidth="1px" CellPadding="0" 
            BorderColor="#3C6ED4" DataKeyNames="Aow_Code" AllowSorting="True">
            <PagerSettings Visible="False" />
            <Columns>

            </Columns>
            <HeaderStyle CssClass="table_data_blue_head" Font-Bold="false" />
            <RowStyle CssClass="table_data_blue_data" HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="OtherContentPlace" runat="Server">
  <div id="Pnl_Unit" name="Pnl_Unit" style="z-index: 1000; float:left; position:absolute; background-color: #ffffff; display: none; width: 600px; height: 480px;">
        Iframe
  </div>
</asp:Content>
