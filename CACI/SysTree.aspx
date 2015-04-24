<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysTree.aspx.cs" Inherits="SysTree" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            scrollbar-face-color: #FFFFFF;
            scrollbar-highlight-color: #FFFFFF;
            scrollbar-shadow-color: #CCCCCC;
            scrollbar-darkshadow-color: #FFFFFF;
            scrollbar-3dlight-color: #CCCCCC;
            scrollbar-arrow-color: #000000;
            scrollbar-track-color: #CCCCCC;
        }
        
        .menu_d {/*定選單寬度*/
         margin-top: 0px;
         margin-right: 0px;
         margin-left: 0px;
         margin-bottom: 0px;
        }

        .menu {
         font-size: 12px;
        }

        .menu a {
         display: block;
         color: #333333;
         text-decoration: none;
         font-size: 12px;
         padding: 3px;
        }
        .menu td.mainnav {/*主選單*/
         border-top-width: 1px;
         border-bottom-width: 1px;
         border-top-style: solid;
         border-bottom-style: solid;
         border-top-color: #FFFFFF;
         border-bottom-color: #B5F91E;
         background-color:#E7F9BD;
        }
        .menu td.subnav {/*次選單*/
         text-indent: 0px;
         background-repeat: no-repeat;
         border-bottom-width: 1px;
         border-bottom-style: solid;
         border-bottom-color:#B5F91E;
         background-color:#E7F9BD;
        }
        .menu td.itemav {/*次選單*/
         text-indent: 0px;
         background-repeat: repeat-x;
         border-bottom-width: 1px;
         border-bottom-style: solid;
         border-bottom-color:#1A75BB;
        }
        .menu a:hover {
         color: #FFFFFF;
         background-color: ActiveCaption;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="menu_d">
            <asp:TreeView ID="treeMenu" runat="server" NodeIndent="15" Height="100%" Width="230px"
                CssClass="menu" ExpandDepth="1" ShowExpandCollapse="true" >
                <NodeStyle ForeColor="Black" />
                <SelectedNodeStyle BackColor="White" Font-Underline="False" BorderWidth="1px" BorderColor="Red" />
                <RootNodeStyle Width="250px" />
                <ParentNodeStyle Width="250px" />
                <LeafNodeStyle Width="250px" />
                <LevelStyles>
                    <asp:TreeNodeStyle CssClass="mainnav" Font-Underline="False" />
                    <asp:TreeNodeStyle CssClass="subnav" Font-Underline="False" />
                    <asp:TreeNodeStyle CssClass="itemav" Font-Underline="False" />
                </LevelStyles>
            </asp:TreeView>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
