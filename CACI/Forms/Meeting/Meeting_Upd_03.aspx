<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterMultiDetailPage.master"
    AutoEventWireup="true" CodeFile="Meeting_Upd_03.aspx.cs" Inherits="Meeting_Upd_03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterInsertContent" runat="Server">
    <table class="table_gray" style="background: white" cellpadding="0" cellspacing="0"
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        專案資料</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                專案類型:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:Label ID="lbl_Pj_Kind" runat="server" Visible="false" ></asp:Label>
                                <asp:Label ID="lbl_Pj_Kind_Name" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                專案名稱:
                            </td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Pj_Name" runat="server"></asp:Label>
                            </td>
                            <td class="title_2c">
                                承辦人員:
                            </td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Pj_User_Code" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                專案簡介:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:Label ID="lbl_Pj_PjIntro" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                專案註記要點:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:Label ID="lbl_Pj_PjNote" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <table class="table_gray" style="background: white" cellpadding="0" cellspacing="0"
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        會議設定資料</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">
                                會議名稱:
                            </td>
                            <td class="text_more" colspan="3">
                                <asp:Label ID="lbl_Meeting_Name" runat="server"></asp:Label>
                                <asp:Label ID="lbl_Meeting_Code" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                承辦人:
                            </td>
                            <td class="text_more" colspan="3">
                                <kd:CoDropDownList ID="ddl_UserDep" runat="server" title="承辦人單位" DataTextField="UsDp_Name"
                                    DataValueField="UsDp_Code" isAllowNull="false" Enabled="false">
                                </kd:CoDropDownList>
                                &nbsp;
                                <kd:CoDropDownList ID="ddl_Meeting_User_Code" runat="server" title="承辦人" DataTextField="User_Name"
                                    DataValueField="User_Code" isAllowNull="false" Enabled="false">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">
                                會議開始時間:
                            </td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Meeting_BgnTime" runat="server"></asp:Label>
                            </td>
                            <td class="title_2c">
                                會議結束時間:
                            </td>
                            <td class="text_2c">
                                <asp:Label ID="lbl_Meeting_EndTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailMaintainContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="會議內容記錄">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                                <tr>
                                    <td>
                                        <div style="margin-left: 5px; float: left; background-color: White;">
                                            <div style="float: left; padding-left: 3px;">
                                                <img src="/CACI/image/yellow_ball.png" /></div>
                                            <div style="float: left; padding-left: 3px; font-weight: bold">
                                                場次資料
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
                                        <asp:Panel ID="pnlGridView" runat="server" Width="100%">
                                            <kd:MDGridView ID="grv_MtgTimes1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                BorderWidth="1px" DataKeyNames="Meeting_Code,Meeting_Index" CssClass="table_lightblue"
                                                Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                                LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="Table"
                                                ControlColumnWidth="39px">
                                                <Columns>
                                                    <kd:ChildGridViewColumn>
                                                    </kd:ChildGridViewColumn>
                                                    <asp:BoundField DataField="Meeting_Index" HeaderText="會議場次順序">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_Place" HeaderText="場次地點">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_Bgn" HeaderText="場次開始時間">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_End" HeaderText="場次結束時間">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </kd:MDGridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="委員紀錄">
            <ContentTemplate>
                <table class="tb_size" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table cellpadding="0px" cellspacing="0px" align="right" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" width="100%" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <div style="margin-left: 5px; float: left; background-color: White;">
                                                        <div style="float: left; padding-left: 3px;">
                                                            <img src="/CACI/image/yellow_ball.png" /></div>
                                                        <div style="float: left; padding-left: 3px; font-weight: bold">
                                                            場次資料
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-left: -60px; position: absolute; float: left; background-color: White;">
                                                        <asp:Label ID="lblPage3" runat="server"></asp:Label>&nbsp;&nbsp;
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
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
                                        <asp:Panel ID="pnlGridView3" runat="server" Width="100%" CssClass="inScroll">
                                            <%--<asp:GridView ID="grv_MtgTimes2" runat="server" AutoGenerateColumns="False" Width="100%"
                                                BorderWidth="1px" DataKeyNames="Meeting_Index" CssClass="table_lightblue" Style="margin-top: 10px">
                                                <Columns>
                                                    <asp:BoundField DataField="Meeting_Index" HeaderText="會議場次順序">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_Place" HeaderText="場次地點">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_Bgn" HeaderText="場次開始時間">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_End" HeaderText="場次結束時間">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>--%>
                                            <kd:MDGridView ID="grv_MtgTimes2" runat="server" AutoGenerateColumns="False" Width="100%"
                                                BorderWidth="1px" DataKeyNames="Meeting_Code,Meeting_Index" CssClass="table_lightblue"
                                                Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                                LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="Table"
                                                ControlColumnWidth="39px">
                                                <Columns>
                                                    <kd:ChildGridViewColumn>
                                                    </kd:ChildGridViewColumn>
                                                    <asp:BoundField DataField="Meeting_Index" HeaderText="會議場次順序">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_Place" HeaderText="場次地點">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_Bgn" HeaderText="場次開始時間">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Times_End" HeaderText="場次結束時間">
                                                        <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
                                                        <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </kd:MDGridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:Button ID="btn_Save" runat="server" Text="儲存" OnClick="btn_Save_Click" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
