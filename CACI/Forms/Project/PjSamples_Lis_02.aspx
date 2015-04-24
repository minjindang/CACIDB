<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="PjSamples_Lis_02.aspx.cs" Inherits="PjSamples_Lis_02" %>

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
            <td class="title_2c">範本代號: </td>
            <td class="text_2c" style="color: Red">
                <asp:Label ID="lbl_PjSp_Code" runat="server"></asp:Label>
            </td>
            <td class="title_2c">範本類別: </td>
            <td class="text_2c">輔導專案 </td>
        </tr>
        <tr>
            <td class="title_2c">範本名稱: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_PjSp_Name" runat="server"></asp:Label>
            </td>
            <td class="title_2c">是否提供轉件: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_PjSp_Trans" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">承辦人: </td>
            <td class="text_2c">
                <asp:Label ID="lbl_UserAcc" runat="server"></asp:Label>
            </td>
            <td class="title_2c"></td>
            <td class="text_2c"></td>
        </tr>
        <tr>
            <td class="title_2c">外網專案說明: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_PjSp_WebExp" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">專案簡介: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_PjSp_PjIntro" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">專案註記要點: </td>
            <td class="text_more" colspan="3">
                <asp:Label ID="lbl_PjSp_PjNote" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">上傳附件: </td>
            <td class="text_more" colspan="3"></td>
        </tr>
        
    </table>
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
                                    流程資訊列表
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
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" CssClass="detail_result">
                                <kd:MDGridView ID="grv_SmpStage" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="PjSp_Code,SpStage_Index" CssClass="table_lightblue"
                                    Style="margin-top: 10px" CloseIconPath="~/image/btn_Expand.png" OpenIconPath="~/image/btn_Collapse.png"
                                    LoadControlMode="UserControl" TemplateCachingBase="Tablename" TemplateDataMode="RunTime" OnRowDataBound="grv_SmpStage_RowDataBound">
                                    <Columns>
                                        <kd:ChildGridViewColumn></kd:ChildGridViewColumn>
                                        <asp:BoundField DataField="SpStage_Index" HeaderStyle-Font-Bold="false" HeaderText="階段順序"
                                            ItemStyle-Wrap="true" HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpStage_Name" HeaderStyle-Font-Bold="false" HeaderText="階段名稱"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpStage_Kind" HeaderStyle-Font-Bold="false" HeaderText="階段類型"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
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
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>