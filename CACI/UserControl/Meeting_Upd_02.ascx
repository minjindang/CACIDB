<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Meeting_Upd_02.ascx.cs"
    Inherits="Meeting_Upd_02" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
    BorderWidth="1px" DataKeyNames="Comm_Code" CssClass="table_lightblue" 
    Style="margin-top: 10px" onrowdatabound="grvQuery_RowDataBound" >
    <Columns>
        <asp:BoundField DataField="Comm_Name" HeaderText="評審委員">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="綜合意見">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
            <ItemTemplate>
                <kd:StrTextBox ID="txt_Eval_Note" runat="server" DB_Length="1000" title="綜合意見"
                    isAllowNull="false" Width="95%" TextMode="MultiLine" Columns="5" Text='<%# Bind("Eval_Note") %>' 
                    Height="50px" ontextchanged="txt_Eval_Note_TextChanged"></kd:StrTextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="處理情形">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lbl_Eval_Status" runat="server" Text='<%# Bind("Eval_Status") %>' Visible="false"></asp:Label>
                <%--<asp:DropDownList ID="ddl_Eval_Status" runat="server">
                </asp:DropDownList>--%>
                <kd:CoDropDownList ID="ddl_Eval_Status" runat="server" title="處理情形">
                </kd:CoDropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="IsNew" HeaderText="IsNew">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
