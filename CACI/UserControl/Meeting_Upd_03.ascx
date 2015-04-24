<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Meeting_Upd_03.ascx.cs"
    Inherits="Meeting_Upd_03" %>
<%@ Register Assembly="CuteWebUI.AjaxUploader" Namespace="CuteWebUI" TagPrefix="cc1" %>
<%@ Register Assembly="com.kangdainfo.online.WebControl" Namespace="com.kangdainfo.online.WebControl"
    TagPrefix="kd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:HiddenField ID="hid_tmpPath" runat="server" Value="N" />
<asp:GridView ID="grvQuery" runat="server" AutoGenerateColumns="False" Width="100%"
    BorderWidth="1px" DataKeyNames="Comm_Code,Meeting_Index" CssClass="table_lightblue" Style="margin-top: 10px" onrowdatabound="grvQuery_RowDataBound">
    <Columns>
        <asp:BoundField DataField="Comm_Name" HeaderText="評審委員">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="會議記錄內容">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
            <ItemTemplate>
                <kd:StrTextBox ID="txt_Record_Text" runat="server" DB_Length="1000" title="會議記錄內容"
                    isAllowNull="false" Width="95%" TextMode="MultiLine" Columns="5" Text='<%# Bind("Record_Text") %>'
                    Height="50px"></kd:StrTextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="會議紀錄上傳">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
            <ItemTemplate>
                <cc1:UploadAttachments runat="server" ID="Uploader" InsertText="選擇檔案" MultipleFilesUpload="false"
                    ShowTableHeader="false" OnFileUploaded="Uploader_FileUploaded" ShowCheckBoxes="false" 
                    onattachmentremoveclicked="Uploader_AttachmentRemoveClicked" >
                </cc1:UploadAttachments>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="IsNew" HeaderText="IsNew">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="IsNew" HeaderText="IsNew">
            <HeaderStyle CssClass="table_data_lightblue_head" Font-Bold="False" VerticalAlign="Bottom" />
            <ItemStyle CssClass="table_data_white_data" HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
