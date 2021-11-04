<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="QApractice1019.Statistics" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
        <h2>
        <asp:Literal ID="ltl_QAtitle" runat="server"></asp:Literal></h2>
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="ph_question" runat="server"></asp:PlaceHolder>
        </br>
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </form>
</body>
</html>
