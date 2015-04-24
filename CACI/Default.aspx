<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="80,*" cols="1024" frameborder="no" border="0" framespacing="0" style="z-Index:1">
	<frame src="SysTop.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" style="z-Index:1"/>
	<frameset cols="250px,7px,*" frameborder="no" border="0" framespacing="0" name="omniMain" style="z-Index:1">
		<frame src="SysTree.aspx" name="leftFrame" scrolling="auto" noresize="noresize" id="leftFrame" style="z-Index:1"/>
		<frame src="bar.htm" name="midFrame" scrolling="no" noresize="noresize" id="midFrame" style="z-Index:1" />
		<frame src="Forms/Report/RPOUT_Qry_04.aspx" name="mainFrame" id="mainFrame" scrolling="auto" style="z-Index:1"/>
		<%--<frame src="Forms/Setting/Announcement_Lis_02.aspx" name="mainFrame" id="mainFrame" scrolling="auto" style="z-Index:1"/>--%>
	</frameset>
</frameset>
</html>
