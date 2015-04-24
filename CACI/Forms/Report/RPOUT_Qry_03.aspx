<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Qry_03.aspx.cs" Inherits="RPOUT_Qry_03" %>

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
        <%--
        <tr>
            <td class="title_2c" style="width:20%">
                報表名稱 :
            </td>
            <td class="text_2c" colspan="3">
                <kd:CoDropDownList ID="CoDropDownList1" runat="server" title="報表名稱" DataTextField="Stage_Name" DataValueField="Stage_Index" isAllowNull="false"/>
            </td>
        </tr>
        --%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <tr>
                    <td class="title_2c" style="width:20%">
                        <font color="red">*</font>年度 :
                    </td>
                    <td class="text_2c" style="width:30%">
                        <kd:StrTextBox ID="txt_Pj_StartDate" runat="server" AutoPostBack="true" DB_Length="3" size="5" title="年度" OnTextChanged="txt_Pj_StartDate_TextChanged" isAllowNull="false"/>
                    </td>
                    <td class="title_2c" style="width:20%">
                        <font color="red">*</font>專案名稱 :
                    </td>
                    <td class="text_2c" style="width:30%">
                        <kd:CoDropDownList ID="sel_Pj_Name" runat="server" title="專案名稱" DataTextField="Pj_Name" DataValueField="Pj_Code" AutoPostBack="true" OnSelectedIndexChanged="sel_Pj_Name_SelectedIndexChanged" isAllowNull="false"/>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">
                        會議日期區間 :
                    </td>
                    <td class="text_2c">
                        <kd:DateTextBox runat="server" DateType="Taiwan" ID="txt_Times_Bgn" title="會議日期區間起">
                        </kd:DateTextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Times_Bgn"
                            Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                        </asp:CalendarExtender>
                        -
                        <kd:DateTextBox runat="server" DateType="Taiwan" ID="txt_Times_End" title="會議日期區間迄">
                        </kd:DateTextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_Times_End"
                            Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                        </asp:CalendarExtender>
                    </td>
                    <td class="title_2c">
                        <font color="red">*</font>階段名稱 :
                    </td>
                    <td class="text_2c">
                        <kd:CoDropDownList ID="sel_Pj_Stage" runat="server" title="階段名稱" DataTextField="Stage_Name" DataValueField="Stage_Index" isAllowNull="false"/>
                    </td>
                </tr>
            </ContentTemplate>
        </asp:UpdatePanel>
        <tr>
            <td class="title_2c">每頁筆數 : </td>
            <td class="text_2c">
                <asp:DropDownList ID="ddlPage" runat="server">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_PrintToExl" runat="server" 
        ImageUrl="~/image/btn_exExcel.png" onclick="btn_PrintExcel_Click" />
    &nbsp;
    <asp:ImageButton ID="btn_PrintToXML" runat="server" 
        ImageUrl="~/image/btn_exXML.png" onclick="btn_PrintXml_Click" />
    &nbsp;
    <asp:ImageButton ID="btn_PrintToPDF" runat="server" 
        ImageUrl="~/image/btn_broPrint.png" onclick="btn_PrintPdf_Click" />
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
            AutoGenerateColumns="False" Width="100%" BorderWidth="1px" CellPadding="0"
            DataKeyNames="Com_Name" AllowSorting="True" >
            <PagerSettings Visible="False" />
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="cbHead" OnCheckedChanged="SelectAll" AutoPostBack="true">
                        </asp:CheckBox>
                    </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cbItem" OnCheckedChanged="SelectItem" AutoPostBack="true"></asp:CheckBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="申請單位/進駐單位">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_Msectors" HeaderStyle-Font-Bold="false" HeaderText="產業別">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_Name" HeaderStyle-Font-Bold="false" HeaderText="計畫名稱">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Eval_TotScore" HeaderStyle-Font-Bold="false" HeaderText="分數">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
            </Columns>
            <AlternatingRowStyle BackColor="#DEE9FC" />
            <HeaderStyle Font-Bold="false" />
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
