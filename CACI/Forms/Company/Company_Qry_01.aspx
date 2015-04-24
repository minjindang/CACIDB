<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="Company_Qry_01.aspx.cs" Inherits="Company_Qry_01" %>

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
                專案年度:
            </td>
            <td class="text_2c">
                <kd:NumTextBox ID="txt_Pj_StartDate_Year" runat="server" DB_IntLength="3" DB_DecLength="0" title="專案年度"></kd:NumTextBox>
            </td>
            <td class="title_2c">
                每頁筆數 :
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
                專案類別:
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_Pj_Kind" runat="server" title="專案類別" isAllowNull="true" DataTextField="Sys_CdText" DataValueField="Sys_CdCode">
                </kd:CoDropDownList>
            </td>
            <td class="title_2c">
                成效亮點分級:
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_Aow_Class" runat="server" title="成效亮點分級" isAllowNull="true" DataTextField="Sys_CdText" DataValueField="Sys_CdCode">
                </kd:CoDropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                公司名稱:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" title="公司名稱" Width="95%"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                統編/身分證字號/立案號 :
            </td>
            <td class="text_2c">
                <kd:CoIDNumTextBox ID="txt_Com_Tonum" runat="server" title="統編/身分證字號/立案號"></kd:CoIDNumTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" />
    &nbsp;
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
    <asp:Panel ID="pnlGridView" runat="server" ScrollBars="None" class="search_result">
        <asp:GridView ID="grvQuery" runat="server" AllowPaging="True" PagerSettings-Visible="false"
            AutoGenerateColumns="False" BorderWidth="1px" CellPadding="0" Width="100%" BorderColor="White"
            DataKeyNames="Com_Code" AllowSorting="True" EnableViewState="true">
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
                <asp:BoundField DataField="Serial" HeaderStyle-Font-Bold="false" HeaderText="序號"
                    SortExpression="Serial">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="公司名稱"
                    SortExpression="Mcol_1">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Tonum" HeaderStyle-Font-Bold="false" HeaderText="公司統編">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Boss" HeaderStyle-Font-Bold="false" HeaderText="負責人">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_MnSectors" HeaderStyle-Font-Bold="false" HeaderText="主要產業別">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
            </Columns>
            <AlternatingRowStyle BackColor="#DEE9FC" />
            <HeaderStyle Font-Bold="false" />
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="OtherContentPlace" runat="Server">
</asp:Content>
