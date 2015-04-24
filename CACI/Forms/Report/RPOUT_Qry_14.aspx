<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Qry_14.aspx.cs" Inherits="RPOUT_Qry_14"  %>

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
            <td class="title_2c">編號/身分證字號/立案號 :</td>
            <td class="text_2c">
                <kd:StrTextBox ID="Com_Tonum" runat="server" DB_Length="10" title="單位代號" ></kd:StrTextBox>
            
             
            </td>
            <td class="title_2c">單位名稱: </td>
            <td class="text_2c">
                <asp:TextBox ID="Com_Name" runat="server"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="title_2c">申請日期區間 :</td>
            <td class="text_2c">                
                <kd:DateTextBox ID="Coach_DateS" runat="server" title="詢問日期區間" DateType="Taiwan" ></kd:DateTextBox>~
                 <asp:CalendarExtender TargetControlID="Coach_DateS" ID="CalendarExtender1" TodaysDateFormat="yyyy/MM/dd" runat="server">
                </asp:CalendarExtender>
                 <kd:DateTextBox ID="Coach_DateE" runat="server" title="詢問日期區間" DateType="Taiwan" ></kd:DateTextBox>
                <asp:CalendarExtender TargetControlID="Coach_DateE" ID="CalendarExtender2" TodaysDateFormat="yyyy/MM/dd" runat="server">
                </asp:CalendarExtender>
            </td>
            <td class="title_2c">專案名稱:</td>
            <td class="text_2c">              
                 <asp:TextBox ID="Pj_Name" runat="server"></asp:TextBox>                          
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
            BorderColor="#3C6ED4" DataKeyNames="Com_Name" AllowSorting="True" 
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
                <asp:BoundField DataField="Com_Tel" HeaderStyle-Font-Bold="false" HeaderText="單位電話">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_CttName" HeaderStyle-Font-Bold="false" HeaderText="聯絡人1">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Com_CttCell" HeaderStyle-Font-Bold="false" HeaderText="聯絡人1手機">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Com_CttMail" HeaderStyle-Font-Bold="false" HeaderText="聯絡人1電子郵件">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Com_CttName2" HeaderStyle-Font-Bold="false" HeaderText="聯絡人2">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Com_CttCell2" HeaderStyle-Font-Bold="false" HeaderText="聯絡人2手機">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Com_CttMail2" HeaderStyle-Font-Bold="false" HeaderText="聯絡人2電子郵件">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Com_OPAddr" HeaderStyle-Font-Bold="false" HeaderText="單位住址">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>                                                             
            </Columns>
            <HeaderStyle CssClass="table_data_blue_head" Font-Bold="false" />
            <RowStyle CssClass="table_data_blue_data" HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
