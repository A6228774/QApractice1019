<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AnswerDetail.aspx.cs" Inherits="QApractice1019.SystemAdmin.AnswerDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <asp:Literal ID="ltl_QAtitle" runat="server"></asp:Literal></h2>
    <asp:Panel ID="pnl_respondentInfo" runat="server" BackColor="#999999">
        <table>
            <tr>
                <td>您的姓名</td>
                <td>
                    <asp:Literal ID="ltl_name" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <asp:Literal ID="ltl_email" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>電話</td>
                <td>
                    <asp:Literal ID="ltl_phone" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>年齡</td>
                <td>
                    <asp:Literal ID="ltl_age" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Literal ID="ltlMsg" runat="server" Visible="False"></asp:Literal></br>
    <asp:PlaceHolder ID="ph_question" runat="server"></asp:PlaceHolder></br>
    <asp:Button ID="return_btn" runat="server" Text="返回" OnClick="return_btn_Click" />
</asp:Content>
