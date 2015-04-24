<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Statics_Qry_03P3.aspx.cs" Inherits="RPOUT_Statics_Qry_03"  %>

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
        <tr>
            <td class="title_2c">分析向度:</td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_Pj_Name" runat="server" title="Pj_Name" 
                    AutoPostBack="True" ClientIDMode="AutoID" 
                    onselectedindexchanged="ddl_Mcol_1_SelectedIndexChanged">
                    <asp:ListItem Value="RPOUT_Statics_Qry_03.aspx">產業類別分析</asp:ListItem>
                    <asp:ListItem Value="RPOUT_Statics_Qry_03P2.aspx">公司登記類型分析</asp:ListItem>
                    <asp:ListItem Value="RPOUT_Statics_Qry_03P3.aspx">登記資本額分析</asp:ListItem>
                    <asp:ListItem Value="RPOUT_Statics_Qry_03P4.aspx">輔導面向分析</asp:ListItem>
                </kd:CoDropDownList>      
            </td>         
        </tr>
        <tr>
            <td class="title_2c"><span class="style1">*</span>年度 :</td>
            <td class="text_2c">
            <kd:StrTextBox ID="Year"  runat="server" DB_Length="10" title="年度" MaxLength="3" Size="3" isAllowNull="false"></kd:StrTextBox>
                
                <span class="style1">請輸入格式ex:099</span></td>
            <td class="title_2c">&nbsp;</td>
            <td class="text_2c"> 
                 <asp:HiddenField ID="yearCountTotal" runat="server" />
                <asp:HiddenField ID="accuCountTotal" runat="server" />
            </td>
        </tr>
         <asp:DropDownList ID="ddlPage" runat="server" Visible="false">
                    <asp:ListItem Text="20" Value="20"></asp:ListItem>                   
                </asp:DropDownList> 
    </table>
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">    
    <asp:imageButton ID="btn_PrintExcel" runat="server" ImageUrl="~/image/btn_exExcel.png" OnClick="btn_PrintToExl_Click"/>
    &nbsp;
    <asp:imageButton ID="btn_PrintXml" runat="server" ImageUrl="~/image/btn_exXML.png" OnClick="btn_PrintToXML_Click"/>
    &nbsp;
    <asp:imageButton ID="btn_PrintPdf" runat="server" ImageUrl="~/image/btn_broPrint.png" OnClick="btn_PrintToPDF_Click"/>
    &nbsp;
    <asp:imageButton ID="btn_Query" runat="server" ImageUrl="~/image/btn_Query.png"/>
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
    <asp:Panel ID="pnlGridView" runat="server" ScrollBars="None" CssClass="search_result">
        <asp:GridView ID="grvQuery" runat="server" AllowPaging="True" PagerSettings-Visible="false"
            AutoGenerateColumns="False" Width="100%" BorderWidth="1px" CellPadding="0" 
            BorderColor="#3C6ED4" DataKeyNames="capRangeName" AllowSorting="True" 
            onselectedindexchanged="grvQuery_SelectedIndexChanged" ShowFooter="true" 
            onrowdatabound="grvQuery_RowDataBound" PageSize="50" >
            <PagerSettings Visible="False" />
            <Columns>
                <asp:BoundField DataField="capRangeName" HeaderStyle-Font-Bold="false" HeaderText="產業別/年度">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="capRangeCount" HeaderStyle-Font-Bold="false" HeaderText="當年度個數">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="capRangeCount" HeaderStyle-Font-Bold="false" HeaderText="當年度百分比">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="accuCapRangeCount" HeaderStyle-Font-Bold="false" HeaderText="累計年度個數">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="accuCapRangeCount" HeaderStyle-Font-Bold="false" HeaderText="累計百分比">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
            </Columns>
            <FooterStyle HorizontalAlign="Center" />
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
