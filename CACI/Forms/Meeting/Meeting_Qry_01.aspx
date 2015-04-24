<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="Meeting_Qry_01.aspx.cs" Inherits="Meeting_Qry_01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="QueryConditionContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">
                專案名稱:
            </td>
            <td class="text_2c" >
                <kd:StrTextBox ID="txt_Pj_Name" runat="server" DB_Length="100" title="會議名稱"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                每頁筆數:
            </td>
            <td class="text_2c">
                <asp:DropDownList ID="ddlPage" runat="server">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                會議名稱:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Meeting_Name" runat="server" DB_Length="50" title="會議名稱"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                會議類型:
            </td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_Meeting_Class" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                承辦人員:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Meeting_User_Code" runat="server" DB_Length="50" title="承辦人員"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                會議開始時間:
            </td>
            <td class="text_2c">
                <kd:DateTextBox ID="txt_Meeting_BgnTime" runat="server" Width="60" DateType="Taiwan"
                    title="會議開始時間"></kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Meeting_BgnTime"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
                <asp:DropDownList ID="ddl_BgnHour" runat="server">
                </asp:DropDownList>
                :<asp:DropDownList ID="ddl_BgnMin" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                會議結束時間:
            </td>
            <td class="text_2c" colspan="3">
                <kd:DateTextBox ID="txt_Meeting_EndTime" runat="server" Width="60" DateType="Taiwan"
                    title="會議結束時間"></kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_Meeting_EndTime"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
                <asp:DropDownList ID="ddl_EndHour" runat="server">
                </asp:DropDownList>
                :<asp:DropDownList ID="ddl_EndMin" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Query" runat="server" ImageUrl="~/image/btn_Query.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="QueryControlContent" runat="Server">
    <table cellpadding="0" cellspacing="0" align="right" width="100%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="background-color: white;">
                    <tr>
                        <td style="padding-left: 6px;">
                            <img src="/CACI/image/search_Result.png" />
                        </td>
                        <td style="padding-right: 6px;">
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
<asp:Content ID="Content5" ContentPlaceHolderID="QueryResultContent" runat="Server">
    <asp:Panel ID="pnlGridView" runat="server" ScrollBars="Auto" class="search_result">
        <asp:GridView ID="grvQuery" runat="server" AllowPaging="True" PagerSettings-Visible="false"
            AutoGenerateColumns="False" PageSize="10" Width="100%" BorderWidth="1px" CellPadding="0"
            DataKeyNames="Meeting_Code" AllowSorting="true" OnRowDataBound="grvQuery_RowDataBound">
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
                <asp:ButtonField ButtonType="Button" CommandName="config" Text="會議紀錄設定">
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                    <ItemStyle HorizontalAlign="right" Width="55px" />
                </asp:ButtonField>
                <asp:BoundField DataField="SerialNo" HeaderStyle-Font-Bold="false" HeaderText="序號"
                    SortExpression="Serial">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Pj_Name" HeaderStyle-Font-Bold="false" HeaderText="專案名稱">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Meeting_Name" HeaderStyle-Font-Bold="false" HeaderText="會議名稱">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Mety_Name" HeaderStyle-Font-Bold="false" HeaderText="會議類別">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Meeting_BgnTime" HeaderStyle-Font-Bold="false" HeaderText="會議開始時間">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Mety_Code" HeaderStyle-Font-Bold="false" HeaderText="會議類別代碼">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
            </Columns>
            <AlternatingRowStyle BackColor="#DEE9FC" />
            <HeaderStyle Font-Bold="false" />
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
