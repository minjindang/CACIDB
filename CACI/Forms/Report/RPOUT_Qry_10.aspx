<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Qry_10.aspx.cs" Inherits="RPOUT_Qry_10" %>

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
    <table class="table_gray" cellpadding="1" cellspacing="0" border="1" width="50%">
       <%-- <tr>
            <td class="title_2c">
                <font color = "red">*</font>報表名稱 ：
            </td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_reportname" runat="server">
                    <asp:ListItem Text="獎補助縣市政府專案綜合資料表" Value="獎補助縣市政府專案綜合資料表"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>
    </table>
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">
                <font color = "red">*</font>年度：
            </td>   
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Pj_StartDate" runat="server" AutoPostBack="True"  title="年度" DB_Length="3"
                    Width="30px" ontextchanged="txt_Pj_StartDate_TextChanged" isAllowNull="false" ></kd:StrTextBox>
                    <font color = "red">請輸入民國年ex：099</font>
            </td>
            <td class="title_2c" align="right"><font color = "red">*</font>專案名稱：</td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_Pj_Name" runat="server" AutoPostBack="True" title="專案名稱" 
                    onselectedindexchanged="ddl_Pj_Name_SelectedIndexChanged" isAllowNull="false"></kd:CoDropDownList>
            </td>
        </tr>
    </table>
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c"><font color = "red">*</font>初審階段：</td>
            <td class="text_2c">
                 <kd:CoDropDownList ID="ddl_Stage_Name_Bgn" runat="server" AutoPostBack="True" title="初審階段"
                  isAllowNull="false"></kd:CoDropDownList>
            </td>
            <td class="title_2c"><font color = "red">*</font>最終審查階段：</td>
            <td class="text_2c">               
                 <kd:CoDropDownList ID="ddl_Stage_Name_End" runat="server" AutoPostBack="True" title="最終審查階段"
                 isAllowNull="false"></kd:CoDropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">初審審查結果：</td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_Sys_CdText_Bgn" runat="server"></asp:DropDownList>
            </td>
            <td class="title_2c">最終審查結果：</td>
            <td class="text_2c">
                <asp:DropDownList ID="ddl_Sys_CdText_End" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
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
            BorderColor="#3C6ED4" DataKeyNames="Aow_Code" AllowSorting="True" >
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
                <asp:BoundField DataField="Aow_Code" HeaderStyle-Font-Bold="false" HeaderText="申請案號">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="申請單位">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Imple" HeaderStyle-Font-Bold="false" HeaderText="執行單位">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_Name" HeaderStyle-Font-Bold="false" HeaderText="計畫名稱">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="firstResult" HeaderStyle-Font-Bold="false" HeaderText="初審審查結果">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="finalResult" HeaderStyle-Font-Bold="false" HeaderText="決審審查結果">
                <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="finalFee" HeaderStyle-Font-Bold="false" HeaderText="核定經費">
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
