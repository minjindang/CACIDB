<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true"
    CodeFile="Announcement_Lis_03.aspx.cs" Inherits="Announcement_Lis_03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InsertContent" runat="Server">
    <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnl_Announcement" runat="server">
                    <table cellpadding="0" cellspacing="0">
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
                                                    系統公告
                                                </div>
                                            </div>
                                            <div style="float: left; position: absolute; margin: 0px 10px 0px 680px; background-color: white; padding: 10px 0px 0px 6px;">
                                                <asp:Label ID="lbl_Announcement" runat="server"></asp:Label>&nbsp;&nbsp;
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
                                            <asp:Panel ID="Panel2" runat="server" Width="737px" class="detail_result">
                                                <asp:GridView ID="grv_Announcement" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    BorderWidth="1px" DataKeyNames="Ann_Code" CssClass="table_lightblue" OnRowCommand="grv_Announcement_RowCommand"
                                                    OnRowDataBound="grv_Announcement_RowDataBound" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" />
                                                            <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="Ann_Name" HeaderStyle-Font-Bold="false" HeaderText="公告主旨"
                                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Ann_BgnTime" HeaderStyle-Font-Bold="false" HeaderText="公告日期"
                                                            HeaderStyle-VerticalAlign="Bottom" HeaderStyle-Width="150px">
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
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
