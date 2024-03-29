﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true"
    CodeFile="Allowance_Upd_03.aspx.cs" Inherits="Allowance_Upd_03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InsertContent" runat="Server">
    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td class="text_more" colspan="4">
                單位(公司)基本資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>申請人:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" title="申請人" isAllowNull="false"></kd:StrTextBox>
                <asp:Label ID="lbl_Com_Code" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbl_Aow_Code" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="title_2c">
                性別:
            </td>
            <td class="text_2c">
                <asp:RadioButtonList ID="rbl_Com_BsGender" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>身份證字號:
            </td>
            <td class="text_2c" colspan="3">
                <kd:IDNumTextBox ID="txt_Com_Tonum" runat="server" title="身份證字號" isAllowNull="false"></kd:IDNumTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                聯絡電話(日):
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_BsTel" runat="server" DB_Length="20" title="聯絡電話(日)"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                聯絡電話(夜):
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_BsNightTel" runat="server" DB_Length="20" title="聯絡電話(夜)"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>手機:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_BsCell" runat="server" DB_Length="10" title="手機" isAllowNull="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                傳真號碼:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Fax" runat="server" DB_Length="20" title="傳真號碼"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                電子信箱:
            </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_Com_Email" runat="server" DB_Length="100" Width="95%" title="Email"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>通訊地址:
            </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_Com_OPAddr" runat="server" DB_Length="200" isAllowNull="false"
                    Width="95%" title="地址"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>產業別:
            </td>
            <td class="text_more" colspan="3">
                <asp:RadioButtonList ID="rbl_Com_MnSectors" RepeatColumns="2" RepeatDirection="Horizontal"
                    runat="server">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                登入帳號:
            </td>
            <td class="text_2c">
                <kd:StrTextBox ID="txt_Com_Account" runat="server" DB_Length="20" title="登入帳號" isAllowNull="true"
                    Enabled="false"></kd:StrTextBox>
            </td>
            <td class="title_2c">
                登入密碼:
            </td>
            <td class="text_2c">
                <asp:TextBox ID="txt_Com_Pass" runat="server" TextMode="Password" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text_more" colspan="4">
                計劃資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>計畫名稱:
            </td>
            <td class="text_more" colspan="3">
                <kd:StrTextBox ID="txt_ApPj_Name" runat="server" DB_Length="200" Width="95%" title="計畫名稱"
                    isAllowNull="false"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                <span class="style1">*</span>計畫摘要:
            </td>
            <td class="text_more" colspan="3">
                <table width="100%">
                    <tr>
                        <td>
                            一、計畫目標
                        </td>
                    </tr>
                    <tr>
                        <td class="text_more">
                            <kd:StrTextBox ID="txt_ApPj_Goal" runat="server" DB_Length="1000" title="計畫目標" Width="95%"
                                TextMode="MultiLine" Columns="5" Height="50px" isAllowNull="false"></kd:StrTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            二、實施策略及方法
                        </td>
                    </tr>
                    <tr>
                        <td class="text_more">
                            <kd:StrTextBox ID="txt_ApPj_Policies" runat="server" Columns="5" DB_Length="1000"
                                Height="50px" TextMode="MultiLine" title="實施策略及方法" Width="95%" isAllowNull="false"></kd:StrTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            三、預期效益及成果評估指標
                        </td>
                    </tr>
                    <tr>
                        <td class="text_more">
                            <kd:StrTextBox ID="txt_ApPj_Profit" runat="server" Columns="5" DB_Length="500" Height="50px"
                                TextMode="MultiLine" title="預期效益及成果評估指標" Width="95%" isAllowNull="false"></kd:StrTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            四、計畫限制條件及解決構想
                        </td>
                    </tr>
                    <tr>
                        <td class="text_more">
                            <kd:StrTextBox ID="txt_ApPj_Solution" runat="server" Columns="5" DB_Length="500"
                                Height="50px" TextMode="MultiLine" title="計畫限制條件及解決構想" Width="95%" isAllowNull="false"></kd:StrTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                總經費:
            </td>
            <td class="text_2c">
                <kd:NumTextBox ID="txt_ApPj_TotAmt" runat="server" DB_IntLength="10" DB_DecLeng="0"
                    title="總經費"></kd:NumTextBox>千元
            </td>
            <td class="title_2c">
                補助經費:
            </td>
            <td class="text_2c">
                <kd:NumTextBox ID="txt_ApPj_AowAmt" runat="server" DB_IntLength="10" DB_DecLeng="0"
                    title="補助經費"></kd:NumTextBox>千元
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                其他經費來源:
            </td>
            <td class="text_2c" colspan="3">
                <kd:StrTextBox ID="txt_ApPj_OthAmt" runat="server" DB_Length="100" title="其他經費來源"></kd:StrTextBox>
            </td>
        </tr>
        <tr>
            <td class="text_more" colspan="4">
                檢具資料
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                個人身份證影本(正)
            </td>
            <td class="text_more" colspan="3">
                <asp:HiddenField ID="ComAtt_Code1" runat="server" Value="N" />
                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" OnFileUploaded="RadAsyncUpload1_FileUploaded"
                    UploadedFilesRendering="AboveFileInput" AutoAddFileInputs="false" Skin="Default">
                    <Localization Cancel="取消" Remove="移除" Select="上傳附件" />
                </telerik:RadAsyncUpload>
            </td>
        </tr>
        <tr>
            <td class="title_2c">
                個人身份證影本(反)
            </td>
            <td class="text_more" colspan="3">
                <asp:HiddenField ID="ComAtt_Code2" runat="server" Value="N" />
                <telerik:RadAsyncUpload ID="RadAsyncUpload2" runat="server" OnFileUploaded="RadAsyncUpload2_FileUploaded"
                    UploadedFilesRendering="AboveFileInput" AutoAddFileInputs="false" Skin="Default">
                    <Localization Cancel="取消" Remove="移除" Select="上傳附件" />
                </telerik:RadAsyncUpload>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ControlButtonContent" runat="server">
    <asp:ImageButton ID="btn_Update" runat="server" ImageUrl="~/image/btn_Update.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
