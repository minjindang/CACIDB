<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master" AutoEventWireup="true" CodeFile="UserPwd_Qry_01.aspx.cs" Inherits="UserPwd_Qry_01"  %>
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
            <td class="title_2c"></td>
            <td class="text_2c">
            </td>
            <td class="title_2c">每頁筆數 :</td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_Page" runat="server">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">單位名稱:</td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Comm_ComName" runat="server" title="單位名稱" DB_Length="100" Width="80%"></kd:StrTextBox>
            </td>
            <td class="title_2c">姓名:</td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Comm_Name" runat="server" title="姓名" DB_Length="50" Width="80%"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">帳號:</td>
            <td class="text_2c">
                <%--<kd:CoIDNumTextBox ID="txt_Comm_Account" runat="server" title="帳號" Width="80%"></kd:CoIDNumTextBox>--%>
                <kd:StrTextBox ID="txt_Comm_Account" runat="server" title="帳號" DB_Length="50" Width="80%"></kd:StrTextBox>
            </td>
            <td class="title_2c">鎖定狀態:</td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_Comm_AcStatus" runat="server">
                    <asp:ListItem Text="請選擇" Value=""></asp:ListItem>
                    <asp:ListItem Text="鎖定" Value="L"></asp:ListItem>
                    <asp:ListItem Text="正常" Value="UnL"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <!--<asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" />
    &nbsp;--><!--20111223 Frank 本表無新增 -->
    <asp:ImageButton ID="btn_Query" runat="server" ImageUrl="~/image/btn_Query.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
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
    <asp:Panel ID="pnlGridView" runat="server" ScrollBars="Auto" CssClass="search_result">
        <asp:GridView ID="grvQuery" runat="server" AllowPaging="True" PagerSettings-Visible="false"
            AutoGenerateColumns="False" Width="100%" BorderWidth="1px" CellPadding="0"
            BorderColor="#3C6ED4" DataKeyNames="Comm_Code" AllowSorting="True" EnableViewState="true" >
            <PagerSettings Visible="False" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="mod" ImageUrl="~/image/btn_Update.png">
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                    <ItemStyle HorizontalAlign="right" Width="55px" CssClass="table_data_blue_data_fun_left" />
                </asp:ButtonField>
                <asp:BoundField DataField="Comm_ComName" HeaderStyle-Font-Bold="false" HeaderText="單位名稱">
                </asp:BoundField>
                <asp:BoundField DataField="Comm_Name" HeaderStyle-Font-Bold="false" HeaderText="姓名">
                </asp:BoundField>
                <asp:BoundField DataField="Comm_Account" HeaderStyle-Font-Bold="false" HeaderText="登入帳號">
                </asp:BoundField>
                <asp:BoundField DataField="Comm_AcStatus" HeaderStyle-Font-Bold="false" HeaderText="鎖定狀態">
                </asp:BoundField>
                <asp:BoundField DataField="Type" HeaderStyle-Font-Bold="false" HeaderText="資料表">
                </asp:BoundField>

            </Columns>
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="false" />
            <RowStyle HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#DEE9FC" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="OtherContentPlace" runat="Server">
</asp:Content>
