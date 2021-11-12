<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="QApractice1019.Statistics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
</head>
<body>
    <h2>
        <asp:Literal ID="ltl_QAtitle" runat="server"></asp:Literal></h2>
    <form id="form1" runat="server">
        <div id="chart_div">

        </div>
        <asp:PlaceHolder ID="ph_question" runat="server"></asp:PlaceHolder>
        </br>
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal></br>
        <asp:Button ID="return_btn" runat="server" Text="返回" OnClick="return_btn_Click" />
    </form>
</body>
</html>
