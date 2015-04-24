<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true"
    CodeFile="Announcement_Lis_02.aspx.cs" Inherits="Announcement_Lis_02" %>

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
                                                    公告
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
                                                <div style="text-align: right; padding-right: 15px; margin-top: 6px; font-size: 12px;
                                                    color: Blue;">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" 
                                                        onclick="LinkButton1_Click" ForeColor="Blue">更多...</asp:LinkButton></div>
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
        <tr>
            <td>
                <asp:Panel ID="pnl_Stage" runat="server">
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
                                                    階段通知
                                                </div>
                                            </div>
                                            <div style="float: left; position: absolute; margin: 0px 10px 0px 680px; background-color: white; padding: 10px 0px 0px 6px;">
                                                <asp:Label ID="lbl_Stage" runat="server"></asp:Label>
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
                                            <asp:Panel ID="Panel1" runat="server" Width="737px" class="detail_result">
                                                <asp:GridView ID="grv_Stage" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    BorderWidth="1px" DataKeyNames="Pj_Code,Stage_Index" CssClass="table_lightblue" OnRowCommand="grv_Stage_RowCommand"
                                                    OnRowDataBound="grv_Stage_RowDataBound" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" />
                                                            <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="Subject" HeaderStyle-Font-Bold="false" HeaderText="公告主旨"
                                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BgnTime" HeaderStyle-Font-Bold="false" HeaderText="公告日期"
                                                            HeaderStyle-VerticalAlign="Bottom" HeaderStyle-Width="150px">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Pj_Kind" HeaderStyle-Font-Bold="false" HeaderText="專案類別"
                                                            HeaderStyle-VerticalAlign="Bottom">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div style="text-align: right; padding-right: 15px; margin-top: 6px; font-size: 12px;
                                                    color: Blue;">
                                                    <asp:LinkButton ID="LinkButton2" runat="server" 
                                                        onclick="LinkButton2_Click" ForeColor="Blue">更多...</asp:LinkButton>
                                                </div>
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
        <tr>
            <td>
                <asp:Panel ID="pnl_Metting" runat="server">
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
                                                    會議設定通知
                                                </div>
                                            </div>
                                            <div style="float: left; position: absolute; margin: 0px 10px 0px 680px; background-color: white; padding: 10px 0px 0px 6px;">
                                                <asp:Label ID="lbl_Metting" runat="server"></asp:Label>
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
                                            <asp:Panel ID="Panel4" runat="server" Width="737px" class="detail_result">
                                                <asp:GridView ID="grv_Metting" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    BorderWidth="1px" DataKeyNames="Meeting_Code" CssClass="table_lightblue" OnRowCommand="grv_Metting_RowCommand"
                                                    OnRowDataBound="grv_Metting_RowDataBound" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" />
                                                            <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="Meeting_Name" HeaderStyle-Font-Bold="false" HeaderText="會議名稱"
                                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Meeting_BgnTime" HeaderStyle-Font-Bold="false" HeaderText="會議開始時間"
                                                            HeaderStyle-VerticalAlign="Bottom" HeaderStyle-Width="150px">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div style="text-align: right; padding-right: 15px; margin-top: 6px; font-size: 12px;
                                                    color: Blue;">
                                                    <asp:LinkButton ID="LinkButton3" runat="server" 
                                                        onclick="LinkButton3_Click" ForeColor="Blue">更多...</asp:LinkButton>
                                                </div>
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
        <tr>
            <td>
                <asp:Panel ID="pnl_Judge" runat="server">
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
                                                    委員遴選通知
                                                </div>
                                            </div>
                                            <div style="float: left; position: absolute; margin: 0px 10px 0px 680px; background-color: white; padding: 10px 0px 0px 6px;">
                                                <asp:Label ID="lbl_Judge" runat="server"></asp:Label>
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
                                            <asp:Panel ID="Panel5" runat="server" Width="737px" class="detail_result">
                                                <asp:GridView ID="grv_Judge" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    BorderWidth="1px" DataKeyNames="Pj_Code" CssClass="table_lightblue" OnRowCommand="grv_Judge_RowCommand"
                                                    OnRowDataBound="grv_Judge_RowDataBound" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" />
                                                            <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="Pj_Name" HeaderStyle-Font-Bold="false" HeaderText="專案名稱"
                                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Pj_StartDate" HeaderStyle-Font-Bold="false" HeaderText="專案啟動日期"
                                                            HeaderStyle-VerticalAlign="Bottom" HeaderStyle-Width="150px">
                                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div style="text-align: right; padding-right: 15px; margin-top: 6px; font-size: 12px;
                                                    color: Blue;">
                                                    <asp:LinkButton ID="LinkButton4" runat="server" 
                                                        onclick="LinkButton4_Click" ForeColor="Blue">更多...</asp:LinkButton>
                                                </div>
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
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" Visible="false" />
</asp:Content>
