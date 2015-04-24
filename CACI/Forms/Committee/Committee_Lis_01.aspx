<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="Committee_Lis_01.aspx.cs" Inherits="Committee_Lis_01" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="text_2c" colspan="4">基本資料</td>
        </tr>
        <tr>
            <td class="title_2c">姓名: </td>
            <td class="text_2c" style="color: Red">
                <asp:Label ID="lbl_Comm_Name" runat="server"></asp:Label>
            </td>
            <td class="title_2c">身分證字號: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_IDNO" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">職銜: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_Title" runat="server"></asp:Label>
            </td>
            <td class="title_2c">公司名稱: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_ComName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">聯絡電話: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_Tel" runat="server"></asp:Label>
            </td>
            <td class="title_2c">手機: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_Cell" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">電子信箱: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_Comm_Mail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">登入帳號: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_Account" runat="server"></asp:Label>
            </td>
            <td class="title_2c">登入密碼: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_Comm_Pass" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text_2c" colspan="4">匯款資料</td>
        </tr>
        <tr>
            <td class="title_2c">銀行帳號: </td>
            <td class="text_2c" colspan="3">
                <asp:Label ID="lbl_Comm_Bank_Num" runat="server"></asp:Label>
                &nbsp;
                <asp:Label ID="lbl_Comm_Bank_Name" runat="server"></asp:Label>
                &nbsp;
                <asp:Label ID="lbl_Comm_Bankno" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">戶名:</td>
            <td class="text_2c" colspan="3">
                 <asp:Label ID="lbl_Comm_BkName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">存摺掃描檔:</td>
            <td class="text_2c" colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td class="text_2c" colspan="4">顧問/委員輔導資料</td>
        </tr>
        <tr>
            <td class="title_2c">輔導方式:</td>
            <td class="text_2c" colspan="3">
                <asp:CheckBoxList ID="ckl_Comm_CoachWay" runat="server" RepeatDirection="Horizontal" Enabled="false">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">輔導項目:</td>
            <td class="text_2c" colspan="3">
                <asp:CheckBoxList ID="ckl_Comm_CoTerms" runat="server" RepeatDirection="Horizontal" Enabled="false">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailInsertContent" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailControlContent" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="DetailDataContent" runat="Server">
    <!-- Detail 資料列式區 -->
    <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                    <tr>
                        <td>
                            <div style="margin-top: 10px; margin-left: 16px; position: absolute; float: left;
                                background-color: White; padding-left: 2px;">
                                <div style="float: left;">
                                    <img src="/CACI/image/blueBall.jpg" />
                                </div>
                                <div style="float: left; margin: 0px 3px 0px 3px;">
                                    聯絡資訊列表
                                </div>
                            </div>
                            <div style="float: left; position: absolute; margin: 0px 10px 0px 680px; background-color: white;
                                padding: 10px 0px 0px 6px;">
                                <asp:Label ID="lblPage" runat="server"></asp:Label>&nbsp;&nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0px" cellspacing="1px" width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" class="detail_result">
                                <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="Ski_Num" CssClass="table_lightblue">
                                    <Columns>
                                        <asp:BoundField DataField="Ski_Name" HeaderStyle-Font-Bold="false" HeaderText="領域"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Skill_Note" HeaderStyle-Font-Bold="false" HeaderText="備註"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <table>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
