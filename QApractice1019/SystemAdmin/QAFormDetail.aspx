<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QAFormDetail.aspx.cs" Inherits="QApractice1019.SystemAdmin.QAFormDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>問卷標題：</td>
            <td>
                <asp:TextBox ID="tbx_title" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>問卷描述：</td>
            <td>
                <asp:TextBox ID="tbx_summary" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>開始日期：</td>
            <td>
                <asp:TextBox ID="tbx_start" runat="server" TextMode="Date"></asp:TextBox></td>
        </tr>
        <tr>
            <td>結束日期：</td>
            <td><asp:TextBox ID="tbx_end" runat="server" TextMode="Date"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><asp:CheckBox ID="cbx_enable" runat="server" Text="已啟用" checked="true"/></td>
        </tr>
        </table>
    <asp:Literal ID="ltl_Msg" runat="server"></asp:Literal></br>
    <asp:Button ID="cancel_btn" runat="server" Text="取消" OnClick="cancel_btn_Click" /><asp:Button ID="build_btn" runat="server" Text="下一步" OnClick="design_btn_Click" />
</asp:Content>
