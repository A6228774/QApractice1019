<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminStatistics.aspx.cs" Inherits="QApractice1019.SystemAdmin.AdminStatistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <asp:Literal ID="ltl_QAtitle" runat="server"></asp:Literal></h2>
        <asp:PlaceHolder ID="ph_question" runat="server">
        </asp:PlaceHolder>
        </br>
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal></br>
    <asp:Button ID="return_btn" runat="server" Text="返回" OnClick="return_btn_Click" />
</asp:Content>
