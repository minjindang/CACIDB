<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebControlSample.ascx.cs" Inherits="WebControlSample" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txt_usr_1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_usr_2" runat="server" Text="測試" onclick="btn_usr_2_Click" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

