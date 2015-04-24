<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="Allowance_Upd_05.aspx.cs" Inherits="Allowance_Upd_05" %>

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
    <table class="table_gray" style="background: white" cellpadding="0" cellspacing="0"
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        獎補助專案查詢</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
                        <tr>
                            <td class="title_2c">
                                專案名稱:
                            </td>
                            <td class="text_2c">
                                <asp:DropDownList ID="ddl_Pj_Name" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Pj_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="title_2c">
                                階段:
                            </td>
                            <td class="text_2c">
                                <asp:UpdatePanel ID="up_Stage_Name" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_Stage_Name" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Stage_Name_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                階段狀態:
                            </td>
                            <td class="text_2c">
                                <asp:UpdatePanel ID="up_AwSg_Verify" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_AwSg_Verify" runat="server" Enabled="true">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Query" runat="server" ImageUrl="~/image/btn_Query.jpg" />
    &nbsp;
    <asp:ImageButton ID="btn_Mark" runat="server" ImageUrl="~/image/btn_Insert.jpg" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.jpg" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" OnClick="btn_Back_Click" />
    <br />
    <table class="table_gray" style="background: white" cellpadding="0" cellspacing="0"
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        設定內容</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
                        <tr>
                            <td class="title_2c">
                                起始時間
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="up_Stage_Date" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Stage_Date" runat="server" Text=""></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                階段內容說明
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="up_Stage_Text" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Stage_Text" runat="server" Text=""></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c" rowspan="2">
                                階段紀錄
                            </td>
                            <td class="text_2c">
                                <kd:DateTextBox ID="txt_AwSg_Date" runat="server" Width="60" DateType="Taiwan" title="執行日期"></kd:DateTextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_AwSg_Date"
                                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                                </asp:CalendarExtender>
                            </td>
                            <td class="title_2c">
                                審核結果
                            </td>
                            <td class="text_2c">
                                <asp:DropDownList ID="ddl_AwSg_Verify2" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_AwSg_Text" runat="server" DB_Length="500" title="審核結果" Width="95%"
                                    TextMode="MultiLine" Columns="5" Height="50px"></kd:StrTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="QueryControlContent" runat="Server">
    <table cellpadding="0" cellspacing="0" align="left" style="background-color: transparent">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="background-color: white;">
                    <tr>
                        <td style="padding-left: 6px;">
                            <img src="/CACI/image/search_Result.png" />
                        </td>
                        <td style="padding-right: 6px;">
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
            AutoGenerateColumns="False" Width="100%" BorderWidth="1px" CellPadding="0" BorderColor="#3C6ED4"
            DataKeyNames="Aow_Code,Pj_Code,Stage_Index" AllowSorting="True">
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
                        <asp:CheckBox runat="server" ID="cbItem"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ApPj_Name" HeaderStyle-Font-Bold="false" HeaderText="申請計畫名稱">
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="單位(公司)名稱">
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Stage_Name" HeaderStyle-Font-Bold="false" HeaderText="階段名稱">
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="AwSg_Verify" HeaderStyle-Font-Bold="false" HeaderText="階段狀態">
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="table_data_blue_head" Font-Bold="false" />
            <RowStyle CssClass="table_data_blue_data" HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="OtherContentPlace" runat="Server">
    <div id="Pnl_Unit" name="Pnl_Unit" style="z-index: 1000; float: left; position: absolute;
        background-color: #ffffff; display: none; width: 600px; height: 480px;">
        Iframe
    </div>
</asp:Content>
