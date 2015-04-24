<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Qry_07.aspx.cs" Inherits="RPOUT_Qry_07" %>

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
            <td class="title_2c" style="width:20%">
                <font color="red">*</font>專案名稱 :
            </td>
            <td class="text_2c" style="width:30%">
                <kd:CoDropDownList ID="ddl_Pj_Name" runat="server" AutoPostBack="True" title="專案名稱"
                    onselectedindexchanged="ddl_Pj_Name_ddlChanged" isAllowNull="false" />
            </td>
            <td class="title_2c" style="width:20%">
                提案組別 :
            </td>
            <td class="text_2c" style="width:30%">
                <kd:CoDropDownList ID="ddl_ApPj_ApGroup" runat="server" title="提案組別" isAllowNull="true"/>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <font color="red">*</font>階段名稱 :
            </td>
            <td class="text_2c" colspan="3">
                <kd:CoDropDownList ID="ddl_Pj_Stage" runat="server" title="階段名稱" isAllowNull="false"/>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                審核結果 :
            </td>
            <td class="text_2c">
                <asp:DropDownList ID="sel_AwSg_Verify" runat="server">
                    <asp:ListItem Value="" Text="請選擇"></asp:ListItem>
                    <asp:ListItem Value="Y" Text="通過"></asp:ListItem>
                    <asp:ListItem Value="N" Text="不通過"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="title_2c">
                公司統編 :
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Tonum" runat="server" DB_Length="10" size="10" title="公司統編"/>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                公司名稱 :
            </td>
            <td class="text_2c" colspan="3">
                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" size="30" title="公司名稱"/>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                產業別 :
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="dll_ApPj_Msectors" runat="server" title="產業別" isAllowNull="true" />
            </td>
            <td class="title_2c">
                資本額 :
            </td>
            <td class="text_2c">
                <asp:TextBox ID="txt_Com_CapitalS" Width="65" runat="server"></asp:TextBox>元
                ~
                <asp:TextBox ID="txt_Com_CapitalE" Width="65" runat="server"></asp:TextBox>元
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_Com_CapitalS" 
	            ControlToValidate = "txt_Com_CapitalE" ErrorMessage="資本額起必須大於資本額迄" Type="Integer" Operator="GreaterThan" 
	            Display="Dynamic" SetFocusOnError="True">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                是否得獎 :
            </td>
            <td class="text_2c">
                <asp:DropDownList ID="sel_CompanyPrice" runat="server">
                    <asp:ListItem Value="" Text="請選擇"></asp:ListItem>
                    <asp:ListItem Value="Y" Text="是"></asp:ListItem>
                    <asp:ListItem Value="N" Text="否"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="title_2c">
                <font color="red">*</font>申請日期區間 :
            </td>
            <td class="text_2c">
                <kd:DateTextBox runat="server" DateType="Taiwan" ID="txt_Aow_DateS" title="申請日期區間起" isAllowNull="false">
                </kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_Aow_DateS"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
                -
                <kd:DateTextBox runat="server" DateType="Taiwan" ID="txt_Aow_DateE" title="申請日期區間迄" isAllowNull="false">
                </kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_Aow_DateE"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="title_2c">每頁筆數 : </td>
            <td class="text_2c" colspan="3">
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
            DataKeyNames="Com_Name" AllowSorting="True" OnRowDataBound="grvQuery_RowDataBound"  >
            <PagerSettings Visible="False" />
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="cbHead" OnCheckedChanged="SelectAll" AutoPostBack="true"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="cbItem" OnCheckedChanged="SelectItem" AutoPostBack="true"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="公司名稱">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_Name" HeaderStyle-Font-Bold="false" HeaderText="計畫名稱">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_ApGroup" HeaderStyle-Font-Bold="false" HeaderText="提案組別">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_CttName" HeaderStyle-Font-Bold="false" HeaderText="聯絡人">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_TotAmt" HeaderStyle-Font-Bold="false" HeaderText="總經費">
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
