﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master" AutoEventWireup="true" CodeFile="Consulting_Qry_01.aspx.cs" Inherits="Consulting_Qry_01" %>

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
            <td class="title_2c">
                申請編號:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Cnst_Code" runat="server" DB_Length="10" title="申請編號"></kd:StrTextBox>
            </td>
            <td class="title_2c">每頁筆數 : </td>
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
                單位(公司)名稱 :
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="50" title="單位(公司)名稱"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                申請諮詢時間 :
            </td>
            <td class="text_2c">
                <kd:DateTextBox runat="server" DateType="Taiwan" ID="txt_Cnst_CntDateS" title="申請諮詢時間起" >
                </kd:DateTextBox>
                <asp:CalendarExtender runat="server" TargetControlID="txt_Cnst_CntDateS" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd"  ></asp:CalendarExtender>-
                             <kd:DateTextBox runat="server" DateType="Taiwan" ID="txt_Cnst_CntDateE" title="申請諮詢時間迄" >
                </kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Cnst_CntDateE" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd"  ></asp:CalendarExtender>
            </td>
        </tr>
        <tr>
             <td class="title_2c">
                處理結果:
            </td>
            <td class="text_more" colspan="3">
                <asp:CheckBoxList ID="ckl_Cnst_Stat" runat="server" 
                    RepeatDirection="Horizontal" >
                </asp:CheckBoxList>          
            </td>
        </tr>
        <tr>
             <td class="title_2c">
                詢問類別:
            </td>
            <td class="text_more" colspan="3">
                <asp:CheckBoxList ID="ckl_CntClass_Code" runat="server" 
                    RepeatDirection="Horizontal" >
                </asp:CheckBoxList>          
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
            DataKeyNames="Cnst_Code" AllowSorting="true" onRowDataBound="grvQuery_RowDataBound" >
            <PagerSettings Visible="False" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="mod" ImageUrl="~/image/btn_Reply.png">
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
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="公司名稱">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
             
                </asp:BoundField>
                <asp:BoundField DataField="twRec_Info" HeaderStyle-Font-Bold="false" HeaderText="申請諮詢時間">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
            
                </asp:BoundField>
                <asp:BoundField DataField="Cnst_StatusName" HeaderStyle-Font-Bold="false" HeaderText="處理結果">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
                <asp:BoundField DataField="Cnst_Status" HeaderStyle-Font-Bold="false" HeaderText="處理結果">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                </asp:BoundField>
            </Columns>
            <AlternatingRowStyle BackColor="#DEE9FC" />
            <HeaderStyle Font-Bold="false" />
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>

