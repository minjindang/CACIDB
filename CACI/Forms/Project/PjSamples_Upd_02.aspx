﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDetailPage.master"
    AutoEventWireup="true" CodeFile="PjSamples_Upd_02.aspx.cs" Inherits="PjSamples_Upd_02" %>

<%@ Register Assembly="CuteWebUI.AjaxUploader" Namespace="CuteWebUI" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            <td class="text_more" style="color: Red" colspan="3">
                <asp:Label ID="lbl_PjSp_Code" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="title_2c">範本類別: </td>
            <td class="text_2c">
                輔導專案
            </td>
            <td class="title_2c">範本名稱: </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_PjSp_Name" runat="server" DB_Length="50" title="範本名稱" Width="90%"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">承辦人: </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_UserDep" runat="server" title="承辦人單位" AutoPostBack="true"
                    DataTextField="UsDp_Name" DataValueField="UsDp_Code" isAllowNull="false" 
                    onselectedindexchanged="ddl_UserDep_SelectedIndexChanged">
                </kd:CoDropDownList>
                &nbsp;
                <kd:CoDropDownList ID="ddl_UserAcc" runat="server" title="承辦人" DataTextField="User_Name" DataValueField="User_Code" isAllowNull="false">
                </kd:CoDropDownList>
            </td>
            <td class="title_2c">是否提供轉件: </td>
            <td class="text_2c">
                <kd:CoDropDownList ID="ddl_PjSp_Trans" runat="server" title="是否提供轉件" isAllowNull="false">
                    <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="否" Value="N"></asp:ListItem>
                </kd:CoDropDownList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">外網專案說明: </td>
            <td class="text_more" colspan="3">
                <cc1:Editor ID="Editor1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="title_2c">專案簡介: </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_PjSp_PjIntro" runat="server" DB_Length="1000" 
                    title="專案簡介" Width="95%" TextMode="MultiLine" Columns="5" Height="50px"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">專案註記要點: </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_PjSp_PjNote" runat="server" DB_Length="1000" 
                    title="專案註記要點" Width="95%" TextMode="MultiLine" Columns="5" Height="50px"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">附件上傳: </td>
            <td class="text_more" colspan="3">
                <asp:LinkButton ID="lbtn_PjSp_PjFile" runat="server" 
                    OnCommand="lbtn_PjSp_PjFile_Command"></asp:LinkButton>
<%--                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" 
                    onfileuploaded="RadAsyncUpload1_FileUploaded" 
                    UploadedFilesRendering="AboveFileInput" AutoAddFileInputs="false" 
                    Skin="Default" >
                    <Localization Cancel="取消" Remove="移除" Select="選擇檔案" />
                </telerik:RadAsyncUpload>--%>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="ComAtt_Code1" runat="server" Value="N" />
                        <cc2:UploadAttachments runat="server" ID="Uploader" InsertText="選擇檔案" MultipleFilesUpload="false"
                            ShowTableHeader="false" OnFileUploaded="Uploader_FileUploaded" ShowCheckBoxes="false" 
                            onattachmentremoveclicked="Uploader_AttachmentRemoveClicked" >
                        </cc2:UploadAttachments>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DetailInsertContent" runat="Server">
    <!-- 資料列式及查詢區 -->
    <div style=" float:left; margin-top: 2px; margin-left:16px; position: absolute; float:left; background-color:White; padding-left:2px; ">
        <div style=" float:left;">
            <img src="/CACI/image/blueBall.jpg" />
        </div>
        <div style=" float:left; margin:0px 3px 0px 3px; ">
            流程階段設定
        </div>
    </div>
    <div class="table_detail_info" >
        <table class="table_lightblue" cellpadding="0" cellspacing="0" border="1" width="100%">
            <tr>
                <td class="lightblue_tb_title_more">階段名稱<font style="color: Red">*</font>&nbsp;
                </td>
                <td class="lightblue_tb_text_more">
                    <kd:StrTextBox ID="txt_SpStage_Name" runat="server" title="階段名稱" DB_Length="100" isAllowNull="false"
                        ValGroup="grv_SmpStage" Width="90%"></kd:StrTextBox>
                </td>
                <td class="lightblue_tb_title_more">階段順序<font style="color: Red">*</font>&nbsp;
                </td>
                <td class="lightblue_tb_text_more" >
                    <asp:HiddenField ID="Old_SpStage_Index" runat="server" />
                    <kd:CoDropDownList ID="ddl_SpStage_Index" runat="server" title="階段順序" isAllowNull="false" ValGroup="grv_SmpStage">
                        <%--<asp:ListItem Text="1" Value="1" ></asp:ListItem>--%>
                    </kd:CoDropDownList>
                </td>
            </tr>
            <tr>
                <td class="lightblue_tb_title_more">階段類型</td>
                <td class="lightblue_tb_text_more" colspan="3">
                    <table cellpadding="0" cellspacing="0" >
                        <tr>
                            <td>
                                <asp:RadioButton ID="rad_SpStage_Kind_1" Text="起始階段" runat="server" GroupName="rad_SpStage_Kind" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rad_SpStage_Kind_2" Text="與前階段日差" runat="server" GroupName="rad_SpStage_Kind" />
                            </td>
                            <td>
                                <kd:NumTextBox ID="txt_SpStage_Days" runat="server" DB_IntLength="2" DB_DecLength="0" title="與前階段日差" Width="30"></kd:NumTextBox>
                            </td>
                            <td>
                                天
                            </td>
                            <td>
                                <asp:RadioButton ID="rad_SpStage_Kind_3" Text="結束階段" runat="server" GroupName="rad_SpStage_Kind" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="lightblue_tb_title_more">階段內容說明<font style="color: Red">*</font>&nbsp;
                </td>
                <td class="lightblue_tb_text_more" colspan="3">
                    <kd:StrTextBox ID="txt_SpStage_Text" runat="server" title="階段內容說明" DB_Length="1000" isAllowNull="false"
                        Width="95%" TextMode="MultiLine" Columns="5" ValGroup="grv_SmpStage"></kd:StrTextBox>
                </td>
            </tr>
            <tr>
                <td class="lightblue_tb_title_more">是否提醒</td>
                <td class="lightblue_tb_text_more">
                    <asp:DropDownList ID="ddl_SpStage_RmFlag" runat="server" >
                        <asp:ListItem Text="否" Value="N" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="是" Value="Y" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="lightblue_tb_title_more">提前提醒天數</td>
                <td class="lightblue_tb_text_more">
                    <kd:NumTextBox ID="txt_SpStage_RmDays" runat="server" DB_IntLength="2" DB_DecLength="0" title="提前提醒天數" Width="30px" ValGroup="grv_SmpStage"></kd:NumTextBox>
                </td>
            </tr>
            <tr>
                    
                <td class="lightblue_tb_title_more">提醒人員
                </td>
                <td class="lightblue_tb_text_more" colspan="3">
                    <asp:CheckBoxList ID="chl_SpStage_RmEmpl" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="申請單位" Value="1"></asp:ListItem>
                        <asp:ListItem Text="承辦人" Value="2"></asp:ListItem>
                        <asp:ListItem Text="顧問人員" Value="3"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="lightblue_tb_title_more">提醒文稿</td>
                <td class="lightblue_tb_text_more" colspan="3">
                    <kd:StrTextBox ID="txt_SpStage_RmText" runat="server" DB_Length="100" title="是否文稿" ValGroup="grv_SmpStage" Width="90%" TextMode="MultiLine" Columns="3"></kd:StrTextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="DetailControlContent" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" >
        <tr>
            <td align="center">
                 <!-- Detail按鈕區 -->
                <asp:ImageButton ID="btnDTL_INSERT" runat="server" ImageUrl="~/image/dtl_Insert.png" />
                &nbsp;
                <asp:ImageButton ID="btnDTL_UPDATE" runat="server" ImageUrl="~/image/dtl_Update.png" />
                &nbsp;
                <asp:ImageButton ID="btnDTL_CLEAR" runat="server" ImageUrl="~/image/dtl_Clear.png" />
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
                                    流程資訊列表
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
                            <asp:Panel ID="pnlGridView" runat="server" Width="737px" class="detail_result">
                                <asp:GridView ID="grv_SmpStage" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" DataKeyNames="SpStage_Index" CssClass="table_lightblue">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="select" ImageUrl="~/image/btn_Detail.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_left" />
                                            <ItemStyle HorizontalAlign="right" Width="55px" CssClass="yellow_grv_row1" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="del" ImageUrl="~/image/btn_Delete.png">
                                            <HeaderStyle CssClass="table_data_lightblue_head_fun_right" />
                                            <ItemStyle HorizontalAlign="left" Width="55px" CssClass="yellow_grv_row2" />
                                        </asp:ButtonField>
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
                                        <asp:BoundField DataField="SpStage_Days" HeaderStyle-Font-Bold="false" HeaderText="階段日差"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpStage_Text" HeaderStyle-Font-Bold="false" HeaderText="階段內容說明"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpStage_Name" HeaderStyle-Font-Bold="false" HeaderText="階段名稱"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpStage_RmFlag" HeaderStyle-Font-Bold="false" HeaderText="是否提醒"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpStage_RmEmpl" HeaderStyle-Font-Bold="false" HeaderText="提醒人員"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpStage_RmDays" HeaderStyle-Font-Bold="false" HeaderText="提醒天數"
                                            HeaderStyle-VerticalAlign="Bottom">
                                            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" />
                                            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpStage_RmText" HeaderStyle-Font-Bold="false" HeaderText="提醒文稿"
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
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>