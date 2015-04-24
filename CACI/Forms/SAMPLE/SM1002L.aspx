<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="SM1002L.aspx.cs" Inherits="SM1002L" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="title_2c">Mcol_1 :</td>
            <td class="text_2c" style="color: Red">
                <asp:Label ID="lbl_Mcol_1" runat="server" Text=""></asp:Label>
            </td>
            <td class="title_2c">Mcol_2 :</td>
            <td class="text_2c">
                <asp:Label ID="lbl_Mcol_2" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">Mcol_3 :</td>
            <td class="text_2c">
                <asp:Label ID="lbl_Mcol_3" runat="server" ></asp:Label>
            </td>
            <td class="title_2c"></td>
            <td class="text_2c">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="DetailDataContent" runat="Server">
    <!-- Detail 資料列式區 -->
    <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table cellpadding="0px" cellspacing="0px" align="right" width="100%" >
                    <tr> 
                        <td>
                            <div style=" margin-top: 10px; margin-left:16px; position: absolute; float:left; background-color:White; padding-left:2px; ">
                                <div style=" float:left; ">
                                    <img src="/CACI/image/blueBall.jpg" />
                                </div>
                                <div style=" float:left; margin:0px 3px 0px 3px; ">
                                    聯絡資訊列表
                                </div>
                            </div>
                            <div  style=" float:left; position: absolute; margin: 0px 10px 0px 680px; background-color:white; padding:10px 0px 0px 6px;" >
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
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" CssClass="detail_result">
                                <asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="Dcol_1" CssClass="table_lightblue">
                                    <Columns>
                                        <asp:BoundField DataField="Dcol_2" HeaderStyle-Font-Bold="false" HeaderText="Dcol_2"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dcol_3" HeaderStyle-Font-Bold="false" HeaderText="Dcol_3"
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
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>