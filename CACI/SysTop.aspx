<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysTop.aspx.cs" Inherits="SysTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body
        {
        	border:0px;
        	margin:0 0 0 0 ;
        }
        .banner_left
        {
        	background-image:url(image/CACI_Blue_Banner_Left.jpg);
            width:690px;
            height:80px;
            top:0px;
            background-repeat:no-repeat;
            padding: 0px 0 0;
        }
        .banner_Mid
        {
        	background-image:url(image/CACI_Blue_Banner_Mid.jpg);
            height:80px;
            top:0px;
            background-repeat:repeat-x;
            padding: 0px 0 0;
        }
        .banner_Right
        {
        	background-image:url(image/CACI_Blue_Banner_Right.jpg);
            width:216px;
            height:80px;
            top:0px;
            background-repeat:repeat-x;
            padding: 0px 0 0;
        }
    </style>
</head>
<body>
    <table style="width:100%" cellspacing="0" cellpadding="0"  >
        <tr>
            <td class="banner_left" style=" width:690px; height:80px;"></td>
            <td class="banner_Mid" style=" height:80px;">&nbsp;</td>
            <td class="banner_Right" style=" width:216px; height:80px; text-align:right; padding-right:10px;">
                <table cellpadding="0" cellspacing="1" style="color:White; font-size:12px; font-weight:bold;">
                    <tr>
                        <td style="text-align:right">單位名稱：</td>
                        <td style="text-align:left"><asp:Label ID="lbl_Unit_Name" runat="server" ></asp:Label>
                            <asp:Label ID="lbl_Prog" runat="server" Visible="false" ></asp:Label>
                            <asp:Label ID="lbl_Msg" runat="server" Visible="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right">用戶名稱：</td>
                        <td style="text-align:left"><asp:Label ID="lbl_User_Name" runat="server" ></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align:right">使用權限：</td>
                        <td style="text-align:left"><asp:Label ID="lbl_Permission" runat="server" ></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
