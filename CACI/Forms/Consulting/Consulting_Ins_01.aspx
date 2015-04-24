<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Consulting_Ins_01.aspx.cs" Inherits="Consulting_Ins_01" %>

<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageDataContent" runat="Server">
    <asp:Label ID="lblProg" runat="server"></asp:Label>&nbsp;
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InsertContent" runat="Server">
    <table class="table_gray" style="background: white; margin-top:10px" cellpadding="0" cellspacing="0"
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        單位(公司)基本資料</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">單位(公司)名稱: </td>
                            <td class="text_2c" style="color: Red">
                                <asp:HiddenField ID="hid_Com_Code" runat="server" />
                                <kd:StrTextBox ID="txt_Com_Name" runat="server" DB_Length="100" title="單位(公司)名稱"
                                    isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">統編/身份證字號/立案號: </td>
                            <td class="text_2c">
                                <kd:IDNumTextBox ID="txt_Com_Tonum" runat="server" DB_Length="30" title="統編/身份證字號/立案號"
                                    ValGroup="grvQuery" OnTextChanged="txt_Com_Tonum_TextChanged" isAllowNull="false" AutoPostBack="True"></kd:IDNumTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">單位(公司)負責人: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Boss" runat="server" DB_Length="50" title="單位(公司)負責人"
                                    isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">成立時間: </td>
                            <td class="text_2c">
                                <kd:DateTextBox runat="server" ID="txt_Com_SetupTime" title="成立時間" DateType="Taiwan"> </kd:DateTextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Com_SetupTime"
                                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">資本額: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Capital" runat="server" DB_Length="20" title="資本額" isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">員工數: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_EmpNum" runat="server" DB_Length="10" title="員工數" isAllowNull="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">最近一年營業額:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_LTover" runat="server" DB_Length="10" title="最近一年營業額"
                                    isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c"></td>
                            <td class="text_2c"></td>
                        </tr>
                        <tr>
                            <td class="title_2c">產業類別: </td>
                            <td class="text_more" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <kd:CoDropDownList ID="ddl_Com_MnSectors" runat="server" title="產業類別" OnSelectedIndexChanged="ddl_Com_MnSectors_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </kd:CoDropDownList>
                                                </td>
                                                <td>
                                                    <kd:StrTextBox ID="txt_Com_MnOther" runat="server" DB_Length="100" title="產業其他類別"
                                                        Enabled="false"></kd:StrTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">營運模式:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_OPMode" runat="server" DB_Length="500" title="營運模式" isAllowNull="false"
                                    TextMode="MultiLine" Height="50" Width="95%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">公司發展現況:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_OPStatus" runat="server" DB_Length="500" title="公司發展現況"
                                    isAllowNull="false" TextMode="MultiLine" Height="50" Width="95%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">主要產品及服務:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_MnProduct" runat="server" DB_Length="500" title="主要產品及服務"
                                    isAllowNull="false" TextMode="MultiLine" Height="50" Width="95%"></kd:StrTextBox>
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
                        單位(公司)聯絡資訊</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">單位(公司)電話:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Tel" runat="server" DB_Length="20" title="單位(公司)電話" isAllowNull="false"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">單位(公司)傳真: </td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Fax" runat="server" DB_Length="20" title="單位(公司)傳真" isAllowNull="false"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">單位(公司)地址:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_OPAddr" runat="server" DB_Length="200" title="單位(公司)地址"
                                    isAllowNull="false" Width="95%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">單位(公司)網址:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Url" runat="server" DB_Length="200" title="單位(公司)網址" isAllowNull="false"
                                    Width="90%"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">單位(公司)e-mail:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_Email" runat="server" DB_Length="100" title="單位(公司)e-mail"
                                    isAllowNull="false" Width="90%"></kd:StrTextBox>
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
                        聯絡人資訊</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">聯絡人姓名:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttName" runat="server" DB_Length="50" title="聯絡人姓名" isAllowNull="false"
                                    Width="90%"></kd:StrTextBox>
                            </td>
                            <td class="title_2c">聯絡人公司電話:</td>
                            <td class="text_2c">
                                <kd:StrTextBox ID="txt_Com_CttTel" runat="server" DB_Length="50" title="聯絡人公司電話"
                                    isAllowNull="false" Width="90%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">聯絡人e-mail:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Com_CttMail" runat="server" DB_Length="50" title="聯絡人e-mail"
                                    isAllowNull="false" Width="95%"></kd:StrTextBox>
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
                        諮詢內容</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">詢問方式:</td>
                            <td class="text_more" colspan="3">
                                <asp:HiddenField ID="hid_Cnst_Code" runat="server" Value="N" />
                                <kd:CoDropDownList ID="ddl_Cnst_CntWay" runat="server" title="詢問方式" RepeatDirection="Horizontal"
                                    isAllowNull="false">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">詢問類別:</td>
                            <td class="text_more" colspan="3">
                                <asp:RadioButtonList ID="ckl_CntClass_Code" runat="server" title="詢問類別" RepeatColumns="5"
                                    RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">詢問內容:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_Cnst_CntText" runat="server" DB_Length="50" title="詢問內容" isAllowNull="false"
                                    TextMode="MultiLine" Width="95%" Height="50px"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">案件狀態:</td>
                            <td class="text_more" colspan="3">
                                <kd:CoDropDownList ID="ddl_Cnst_Status" runat="server" title="案件狀態" RepeatDirection="Horizontal"
                                    isAllowNull="false">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <table class="table_gray" style="background: white; margin-top:10px" cellpadding="0" cellspacing="0" 
        border="1" width="100%">
        <tr>
            <td>
                <div style="margin-top: -12px; margin-left: 15px; position: absolute; float: left;
                    background-color: White;">
                    <div style="float: left; padding-top: 2px; padding-left: 3px;">
                        <img src="/CACI/image/blueBall.jpg" />
                    </div>
                    <div style="float: left; padding-left: 3px;">
                        諮詢回覆</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">時間:</td>
                            <td class="text_2c">
                                <kd:DateTextBox runat="server" ID="txt_CtRepl_Date" title="時間" DateType="Taiwan"> </kd:DateTextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_CtRepl_Date"
                                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                                </asp:CalendarExtender>
                            </td>
                            <td class="title_2c">回覆方式:</td>
                            <td class="text_2c">
                                <kd:CoDropDownList ID="ddl_CtRepl_RpWay" runat="server" title="回覆方式" isAllowNull="true" RepeatDirection="Horizontal">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">回覆內容:</td>
                            <td class="text_more" colspan="3">
                                <kd:StrTextBox ID="txt_CtRepl_RpText" runat="server" DB_Length="1000" title="回覆內容"
                                    isAllowNull="false" TextMode="MultiLine" ValGroup="grvQuery" Width="95%" Height="50px"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">處理結果:</td>
                            <td class="text_more" colspan="3">
                                <kd:CoDropDownList ID="ddl_CtRepl_RpResult" runat="server" title="處理結果" RepeatDirection="Horizontal"
                                    ValGroup="grvQuery" isAllowNull="true">
                                </kd:CoDropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ControlButtonContent" runat="Server">
    <asp:ImageButton ID="btn_Insert" runat="server" ImageUrl="~/image/btn_Insert.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/image/btn_Clear.png" />
    &nbsp;
    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/image/btn_Back.png" />
</asp:Content>
