<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoachStage_Upd_02.ascx.cs"
    Inherits="CoachStage_Upd_02" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:DataList ID="DataList1" runat="server">
    <HeaderTemplate>
        <table class="table_lightYellow" style="margin-top: 10px;" cellpadding="0" cellspacing="0"
            border="1" width="100%">
    </HeaderTemplate>
    <ItemTemplate>
         <tr>
            <td class="lightyellow_tb_title_more" colspan="2">
                <asp:Label ID="lbl_Score_Items" runat="server" Text='<%# Eval("Score_Items") %>' ></asp:Label>
                (0~ 
                <asp:Label ID="lbl_Score_Max" runat="server" Text='<%# Eval("Score_Max") %>' ></asp:Label>
                )
            </td>
        </tr>
        <tr>
            <td class="lightyellow_tb_title_more">
                評語
            </td>
            <td class="lightyellow_tb_text_more" >
                <asp:Label ID="lbl_Tail_Text" runat="server" Text='<%# Eval("Tail_Text") %>' ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="lightyellow_tb_title_more">
                分數
            </td>
            <td class="lightyellow_tb_text_more" >
                <asp:Label ID="lbl_Tail_Score" runat="server" Text='<%# Eval("Tail_Score") %>' ></asp:Label>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:DataList>
