<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master" AutoEventWireup="true" CodeFile="Allowance_Qry_01.aspx.cs" Inherits="Allowance_Qry_01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" Runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="QueryConditionContent" Runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">申請案號: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Aow_Code" runat="server" DB_Length="50" title="申請案號" ></kd:StrTextBox>
            </td>
            <td class="title_2c">專案名稱: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Pj_Name" runat="server" DB_Length="50" title="專案名稱" ></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">申請單位(公司): </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="50" title="申請單位" ></kd:StrTextBox>
            </td>
            <td class="title_2c">案件狀態: </td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_Aow_Status" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">專案執行期間: </td>
            <td class="text_2c">
                <kd:DateTextBox ID="txt_Pj_StartDate" runat="server" Width="60" DateType="Taiwan" title="專案執行期間(起)" ></kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_Pj_StartDate" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
                ~
                <kd:DateTextBox ID="txt_Pj_EndDate" runat="server" Width="60" DateType="Taiwan" title="專案執行期間(迄)" ></kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_Pj_EndDate" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
            </td>
            <td class="title_2c">案件階段: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Stage_Name" runat="server" DB_Length="50" title="案件階段" ></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">每頁筆數:</td>
            <td class="text_2c">
                <asp:DropDownList ID="ddlPage" runat="server">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="title_2c">&nbsp;</td>
            <td class="text_2c">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Query" runat="server" ImageUrl="~/image/btn_Query.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:Button ID="btnBatch" runat="server" Text="批次設定" onclick="btnBatch_Click" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="QueryControlContent" Runat="Server">
    <table cellpadding="0" cellspacing="0" align="right" width="100%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="background-color: white;">
                    <tr>
                        <td style="padding-left:6px;">
                            <img src="/CACI/image/search_Result.png" />
                        </td>
                        <td style="padding-right:6px;">
                            查詢結果&nbsp; 
                            <asp:Label ID="lblPageCount" runat="server"></asp:Label>&nbsp;&nbsp; 
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background-color: transparent; width: 380px">
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
<asp:Content ID="Content5" ContentPlaceHolderID="QueryResultContent" Runat="Server">
    <asp:Panel ID="pnlGridView" runat="server" ScrollBars="Auto" class="search_result">
        <asp:GridView ID="grvQuery" runat="server" AllowPaging="True" PagerSettings-Visible="false"
            AutoGenerateColumns="False" PageSize="10" Width="100%" BorderWidth="1px" CellPadding="0"
            DataKeyNames="Aow_Code,Com_Code,Pj_Code" AllowSorting="true" 
            onrowdatabound="grvQuery_RowDataBound">
            <PagerSettings Visible="False" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="mod" ImageUrl="~/image/btn_Update.png">
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                    <ItemStyle HorizontalAlign="right" Width="55px" />
                </asp:ButtonField>
                <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_mid" />
                    <ItemStyle HorizontalAlign="right" Width="55px" />
                </asp:ButtonField>
                <asp:ButtonField ButtonType="Image" CommandName="show" ImageUrl="~/image/btn_Detail.png">
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                    <ItemStyle HorizontalAlign="right" Width="55px" />
                </asp:ButtonField>
                <asp:BoundField DataField="SerialNo" HeaderStyle-Font-Bold="false" HeaderText="序號" SortExpression="Serial" >
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Pj_Name" HeaderStyle-Font-Bold="false" HeaderText="專案名稱" >
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_Name" HeaderStyle-Font-Bold="false" HeaderText="計畫名稱" >
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Pj_StartDate" HeaderStyle-Font-Bold="false" HeaderText="專案啟動日期" >
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Pj_PjFill" HeaderStyle-Font-Bold="false" HeaderText="樣式" >
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:ButtonField ButtonType="Button" CommandName="maintain" Text="申請流程維護"  >
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                    <ItemStyle HorizontalAlign="right" Width="55px" />
                </asp:ButtonField>
            </Columns>
            <AlternatingRowStyle BackColor="#DEE9FC" />
            <HeaderStyle Font-Bold="false" />
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>

