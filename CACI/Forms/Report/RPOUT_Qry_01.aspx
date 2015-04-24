<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master"
    AutoEventWireup="true" CodeFile="RPOUT_Qry_01.aspx.cs" Inherits="RPOUT_Qry_01" %>

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
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <kd:CoDropDownList ID="sel_Pj_name" runat="server" title="專案名稱" 
                            DataTextField="Pj_Name" DataValueField="Pj_Code" AutoPostBack="true" onselectedindexchanged="sel_Pj_name_SelectedIndexChanged" isAllowNull="false" />
                        </ContentTemplate>
                     </asp:UpdatePanel>
                    </td>
                    <td class="title_2c" style="width:25%">
                        申請單位 :
                    </td>
                    <td class="text_2c">
                       <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="50" title="申請單位"></kd:StrTextBox>
                    </td>
                </tr>
        <tr>
            <td class="title_2c">
                計畫名稱 :
            </td>
            <td class="text_2c">
               <kd:StrTextBox ID="txt_ApPj_Name" runat="server" DB_Length="50" title="計畫名稱"></kd:StrTextBox>      
            </td>
            <td class="title_2c">
                商品類別 :
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="sel_ApPj_ProdCls" runat="server" title="商品類別" DataTextField="ApPj_ProdCls" DataValueField="ApPj_ProdCls" isAllowNull="true"/>       
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                申請組別 :
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="sel_ApPj_ApGroup" runat="server" title="申請組別" DataTextField="ApPj_ApGroup" DataValueField="ApPj_ApGroup" isAllowNull="true"/>       
            </td>       
             <td class="title_2c" style="width: 143px">
                產業別 :
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="sel_ApPj_Msectors" runat="server" title="產業別" DataTextField="ApPj_Msectors" DataValueField="ApPj_Msectors" isAllowNull="true"/>       
            </td>         
        </tr>
        <tr>
            <td class="title_2c">
                公司登記所在地 :
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="sel_Com_PostCode" runat="server" title="公司登記所在地" DataTextField="Com_PostCode" DataValueField="Com_PostCode" isAllowNull="true"/>
                     
            </td>
             <td class="title_2c">
                <font color="red">*</font>初審/技審階段 :
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="sel_AwSg_Verify_1" runat="server" title="初審/技審階段" DataTextField="Stage_Name" DataValueField="Stage_Name" isAllowNull="true"/>       
            </td>  
        </tr>
        <tr>
            <td class="title_2c">
                <font color="red">*</font>複審/決審階段 :
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="sel_AwSg_Verify_2" runat="server" title="複審/決審階段" DataTextField="Stage_Name" DataValueField="Stage_Name" isAllowNull="true"/>       
            </td> 
            <td class="title_2c">
                申請日期區間 :
            </td>
            <td class="text_2c">
                <kd:DateTextBox runat="server" DateType="Taiwan" ID="Aow_SDate" title="申請日期起">
                </kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Aow_SDate"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
                -&nbsp;&nbsp;
                <kd:DateTextBox runat="server" DateType="Taiwan" ID="Aow_EDate" title="申請日期迄">
                </kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Aow_EDate"
                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                </asp:CalendarExtender>
            </td>
       </tr>
       <tr>
            <td class="title_2c">
                初審結果:
            </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="sel_First_Res" runat="server" title="階段名稱" DataTextField="sel_First_Res" DataValueField="sel_First_Res" isAllowNull="true"/>
            </td>
            <td class="title_2c" style="width: 145px">
                資本額區間(千元)：
            </td>
            <td class="text_2c">
                <asp:TextBox ID="txt_Com_CapitalS" runat="server" size="5"></asp:TextBox>~
                <asp:TextBox ID="txt_Com_CapitalE" runat="server" size="5"></asp:TextBox>
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
        ImageUrl="~/image/btn_exExcel.png" onclick="btn_PrintToExl_Click" />
    <asp:ImageButton ID="btn_PrintToXML" runat="server" 
        ImageUrl="~/image/btn_exXML.png" onclick="btn_PrintToXML_Click" />
    <asp:ImageButton ID="btn_PrintToPDF" runat="server" 
        ImageUrl="~/image/btn_broPrint.png" onclick="btn_PrintToPDF_Click" />
    <asp:ImageButton ID="btn_Query" runat="server" ImageUrl="~/image/btn_Query.png" />
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" Visible="false" />
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
                        <td style="padding-right: 6px;" width="100%">
                            查詢結果&nbsp;
                            <asp:Label ID="lblPageCount" runat="server"></asp:Label>&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background-color: transparent; width: 100px">
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
            DataKeyNames="Aow_Code" AllowSorting="True" PageSize="2" >
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
                      <asp:CheckBox runat="server" ID="cbItem" OnCheckedChanged="Button_OnOff" AutoPostBack="true">
                      </asp:CheckBox>
                   </ItemTemplate>
                </asp:TemplateField>
               
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="申請單位">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_Name" HeaderStyle-Font-Bold="false" HeaderText="計畫名稱">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_BEgnDate" HeaderStyle-Font-Bold="false" HeaderText="計畫期程">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_Msectors" HeaderStyle-Font-Bold="false" HeaderText="產業別">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPj_ApGroup" HeaderStyle-Font-Bold="false" HeaderText="申請組別">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="AwSg_Verify_1" HeaderStyle-Font-Bold="false" HeaderText="初審/技審結果">
                    <HeaderStyle CssClass="table_data_lightblue_head" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="AwSg_Verify_2" HeaderStyle-Font-Bold="false" HeaderText="複審/決審結果">
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
