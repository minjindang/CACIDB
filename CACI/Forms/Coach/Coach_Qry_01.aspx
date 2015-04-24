<%@ Page Title="" Language="C#" MasterPageFile="~/Master/QueryMasterPage.master" AutoEventWireup="true" CodeFile="Coach_Qry_01.aspx.cs" Inherits="Coach_Qry_01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>

<%@ Register src="~/UserControl/cbl_Chkd_Code.ascx" tagname="CoachKindCheckboxList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" Runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="QueryConditionContent" Runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c"></td>
            <td class="text_2c"></td>
            <td class="title_2c">
                �C������ :
            </td>
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
                �ӽнs��:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Coach_Code" runat="server" DB_Length="10" title="�ӽнs��"></kd:StrTextBox>
            </td>
            
            <td class="title_2c">
                �M�צW�� :
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Pj_Name" runat="server" DB_Length="50" title="�M�צW��"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
           <td class="title_2c">
                ���(���q)�W�� :
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="50" title="���(���q)�W��"></kd:StrTextBox>
            </td>
             <td class="title_2c">
                ���(���q)�t�d�H :
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Boss" runat="server" DB_Length="50" title="���(���q)�t�d�H"></kd:StrTextBox>
            </td>

        </tr>
        <tr>
            <td class="title_2c">
                �Τ@�s��/�����Ҧr��/�߮׮׸� :
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Tonum" runat="server" DB_Length="50" title="�Τ@�s��/�����Ҧr��/�߮׮׸�"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                �ӽл��ɮɶ� :
            </td>
            <td class="text_2c">
                <kd:DateTextBox runat="server" DateType="Taiwan" ID="txt_Coach_DateS" title="�ӽл��ɮɶ��_" >
                </kd:DateTextBox>
                <asp:CalendarExtender runat="server" TargetControlID="txt_Coach_DateS" Format="yyy/MM/dd" TodaysDateFormat="yyy/MM/dd"  ></asp:CalendarExtender>-
                 <kd:DateTextBox runat="server" DateType="Taiwan" ID="txt_Coach_DateE" title="�ӽл��ɮɶ���" >
                </kd:DateTextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Coach_DateE" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd"  ></asp:CalendarExtender>
            </td>
        </tr>

        <tr>
             <td class="title_2c">
                ���ɶ���:
            </td>
            <td class="text_2c" colspan="3">
               
                    <uc1:CoachKindCheckboxList ID="CoachKindCheckboxList1" runat="server" />
                    
               
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
                            �d�ߵ��G&nbsp; 
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
                                <asp:LinkButton ID="lnkPageUP" runat="server">�W�@��</asp:LinkButton>/��&nbsp;
                                <asp:DropDownList ID="ddlPageNum" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                ��/<asp:LinkButton ID="lnkPageDown" runat="server">�U�@��</asp:LinkButton>
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
            DataKeyNames="Coach_Code,Pj_Code" AllowSorting="true" onRowDataBound="grvQuery_RowDataBound" >
            <PagerSettings Visible="False" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="mod" ImageUrl="~/image/btn_Update.png">
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
                <asp:ButtonField ButtonType="Button" CommandName="maintain" Text="�f�d"  >
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                    <ItemStyle HorizontalAlign="right" Width="55px" />
                </asp:ButtonField>
               <asp:BoundField DataField="Pj_Name" HeaderStyle-Font-Bold="false" HeaderText="�M�צW��">
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Com_Name" HeaderStyle-Font-Bold="false" HeaderText="���q�W��">
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField DataField="twCoach_Date" HeaderStyle-Font-Bold="false" HeaderText="�ӽл��ɮɶ�">
                    <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                    <HeaderStyle Font-Bold="False" />
                </asp:BoundField>

            </Columns>
            <AlternatingRowStyle BackColor="#DEE9FC" />
            <HeaderStyle Font-Bold="false" />
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>

