﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Qry_23.aspx.cs" Inherits="RPOUT_Qry_23"  %>

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
        .style2
        {
            color: #000000;
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
            <td class="title_2c">申請之案件編號: </td>
            <td class="text_2c">
                <asp:TextBox ID="Aow_Code" runat="server"></asp:TextBox>
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
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title"></td>
            
            
        </tr>
        
        
        
    </table>
    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="Server">    
    <asp:imageButton ID="btn_PrintExcel" runat="server" ImageUrl="~/image/btn_exExcel.png" OnClick="btn_PrintToExl_Click"/>
    &nbsp;
    <asp:imageButton ID="btn_PrintXml" runat="server" ImageUrl="~/image/btn_exXML.png" OnClick="btn_PrintToXML_Click"/>
    &nbsp;
    <asp:imageButton ID="btn_PrintPdf" runat="server" ImageUrl="~/image/btn_broPrint.png" OnClick=" btn_PrintToPDF_Click"/>
    &nbsp;
    <asp:imageButton ID="btn_Query" runat="server" ImageUrl="~/image/btn_Query.png"/>
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
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
            BorderColor="#3C6ED4" DataKeyNames="Com_Code" AllowSorting="True" 
            onselectedindexchanged="grvQuery_SelectedIndexChanged" >
            <PagerSettings Visible="False" />
            <Columns>
                <asp:TemplateField>
                     <HeaderTemplate>
                      <asp:CheckBox runat="server" ID="cbh" OnCheckedChanged="SelectAll" AutoPostBack="true">
                      </asp:CheckBox>
                   </HeaderTemplate>                 
                    <ItemTemplate>                        
                        <asp:CheckBox runat="server" ID="chk" OnCheckedChanged="Button_OnOff" AutoPostBack="true">
                      </asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="單位名稱">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_CttName" HeaderStyle-Font-Bold="false" HeaderText="聯絡人">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Tel" HeaderStyle-Font-Bold="false" HeaderText="電話">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Coach_Date" HeaderStyle-Font-Bold="false" HeaderText="申請日期">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Pj_Name" HeaderStyle-Font-Bold="false" HeaderText="輔導服務">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Meeting_BgnTime" HeaderStyle-Font-Bold="false" HeaderText="輔導時間">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="ChKd_Name" HeaderStyle-Font-Bold="false" HeaderText="輔導面向(小類)">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>             
                 <asp:BoundField DataField="Record_Text" HeaderStyle-Font-Bold="false" HeaderText="輔導會議摘要">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                   <asp:BoundField DataField="status" HeaderStyle-Font-Bold="false" HeaderText="後續進度">
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