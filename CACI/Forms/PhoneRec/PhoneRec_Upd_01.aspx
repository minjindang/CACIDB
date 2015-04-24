<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="PhoneRec_Upd_01.aspx.cs" Inherits="PhoneRec_Upd_01" %>

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
                    <td class="text_2c" style="color:Red">
                        <asp:HiddenField ID="hid_PhRec_ComCode" runat="server" Value="N" />
                        <asp:HiddenField ID="hid_PhRec_Code" runat="server" Value="N" />
                        <asp:HiddenField ID="hid_PRcRp_Code" runat="server" Value="N" />
                        <kd:StrTextBox ID="txt_PhRec_ComName" runat="server" DB_Length="100" title="單位(公司)名稱" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                    <td class="title_2c">稅編/身份證字號/立案號: </td>
                    <td class="text_2c">
                        <kd:IDNumTextBox ID="txt_PhRec_Tonum" runat="server" DB_Length="30" title="稅編/身份證字號/立案號" 
                            ValGroup="grvQuery" ontextchanged="txt_PhRec_Tonum_TextChanged" 
                            AutoPostBack="True"></kd:IDNumTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title_2c">聯絡人姓名: </td>
                    <td class="text_2c">
                         <kd:StrTextBox ID="txt_PhRec_CtName" runat="server" DB_Length="50" title="聯絡人姓名" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                    <td class="title_2c">聯絡人電話: </td>
                    <td class="text_2c">
                        <kd:StrTextBox ID="txt_PhRec_CtTel" runat="server" DB_Length="10" title="聯絡人電話" isAllowNull="false" ></kd:StrTextBox>
                    </td>
                </tr>


                <tr>
                    <td class="title_2c">聯絡人 E-mail :</td>
                    <td class="text_2c" colspan="3">
                        <kd:StrTextBox ID="txt_PhRec_CtMail" runat="server" DB_Length="100" title="聯絡人 E-mail " isAllowNull="false" ></kd:StrTextBox>
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
                        提問內容</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">提問類別:</td>
                            <td class="text_2c" colspan="3">
                                <asp:DropDownList ID="ddl_CntClass_Code" runat="server">
                                </asp:DropDownList>(諮詢類別)
                            </td>

                        </tr>
                        <tr>
                            <td class="title_2c">提問內容:</td>
                            <td class="text_more" colspan="3" >
                                <kd:StrTextBox ID="txt_PhRec_Question" runat="server" Width="95%" DB_Length="500"   title="提問內容" isAllowNull="false" ></kd:StrTextBox>
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
                        諮詢回覆</div>
                </div>
                <div style="width: 100%">
                    <table class="table_gray" cellpadding="0" cellspacing="0" border="1" width="97%"
                        style="margin: 15px 10px 10px 10px;">
                        <tr>
                            <td class="title_2c">時間:</td>
                            <td class="text_2c" colspan="3" >
                                <kd:DateTextBox runat="server" ID="txt_PRcRp_Date" title="時間" DateType="Taiwan"> </kd:DateTextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_PRcRp_Date"
                                    Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">回覆內容:</td>
                            <td class="text_2c" colspan="3">
                                <kd:StrTextBox ID="txt_PRcRp_Text" runat="server" DB_Length="50" title="回覆內容"
                                    isAllowNull="false" Width="95%"></kd:StrTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_2c">處理方式:</td>
                            <td class="text_more" colspan="3">
                                <asp:DropDownList ID="ddl_PRcRp_Handle" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
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
    &nbsp;
    <asp:ImageButton ID="btn_Archive" runat="server" 
        ImageUrl="~/image/btn_Update.png" onclick="btn_Archive_Click" />
    
</asp:Content>
