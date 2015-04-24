<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Qry_12.aspx.cs" Inherits="RPOUT_Qry_12"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .title
        {
            background-color: #FCF1FC;
        }
        .style1
        {
            color: #FF0000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="QueryConditionContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
       <%-- <tr>
            <td class="title_2c"><font color = "red">*</font>報告名稱：</td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_reportname" runat="server">
                    <asp:ListItem Text="諮詢紀錄匯整-一般表" Value="諮詢紀錄匯整-一般表"></asp:ListItem>
                </asp:DropDownList>      
            </td>         
        </tr>--%>
      
        <tr>
            <td class="title_2c">詢問日期：</td>
            <td class="text_2c">                
                <kd:DateTextBox ID="dtb_Cnst_CntDate_Bgn" runat="server" title="詢問日期區間起" DateType="Taiwan" ></kd:DateTextBox>~
                 <asp:CalendarExtender TargetControlID="dtb_Cnst_CntDate_Bgn" ID="CalendarExtender1" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" runat="server">
                 </asp:CalendarExtender>
                <kd:DateTextBox ID="dtb_Cnst_CntDate_End" runat="server" title="詢問日期區間迄" DateType="Taiwan" ></kd:DateTextBox>
                 <asp:CalendarExtender TargetControlID="dtb_Cnst_CntDate_End" ID="CalendarExtender2" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" runat="server">
                 </asp:CalendarExtender>
            </td>
            <td class="title_2c">詢問人/公司：</td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="10" title="詢問人/公司：" ></kd:StrTextBox>                
            </td>
        </tr>

        <tr>
            <td class="title_2c">問題類型：</td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_CntClass_Code" runat="server"></asp:DropDownList>            
            </td>
            <td class="title_2c">每頁筆數：</td>
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

<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_PrintToExl" runat="server" 
        ImageUrl="~/image/btn_exExcel.png" onclick="btn_PrintToExl_Click" />
    <asp:ImageButton ID="btn_PrintToXML" runat="server" 
        ImageUrl="~/image/btn_exXML.png" onclick="btn_PrintToXML_Click" />
    <asp:ImageButton ID="btn_PrintToPDF" runat="server" 
        ImageUrl="~/image/btn_broPrint.png" onclick="btn_PrintToPDF_Click" />
    <asp:ImageButton ID="btn_Query" runat="server"
        ImageUrl="~/image/btn_Query.png" />
    <asp:ImageButton ID="btn_Clear" runat="server"
        ImageUrl="~/image/btn_Clear.png" />
            &nbsp;
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" Visible="false" />
</asp:Content>      

<asp:Content ID="Content5" ContentPlaceHolderID="QueryControlContent" runat="Server">
    <table cellpadding="0" cellspacing="0" align="left" style="background-color: transparent">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="background-color: white;">
                    <tr>
                        <td style="padding-left:6px;">
                            <image src="/CACI/image/search_Result.png" />
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
            BorderColor="#3C6ED4" DataKeyNames="Cnst_CntDate" AllowSorting="True" 
            onrowdatabound="grvQuery_RowDataBound" >
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
                      <asp:CheckBox runat="server" ID="cbItem" OnCheckedChanged="Button_OnOff" AutoPostBack="true"></asp:CheckBox>
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Cnst_CntDate" HeaderStyle-Font-Bold="false" HeaderText="詢問日期">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CntWay" HeaderStyle-Font-Bold="false" HeaderText="詢問方式">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="詢問人/公司">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Cnst_CntText" HeaderStyle-Font-Bold="false" HeaderText="諮詢/需求內容摘要">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="qType" HeaderStyle-Font-Bold="false" HeaderText="問題類型">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>               
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
