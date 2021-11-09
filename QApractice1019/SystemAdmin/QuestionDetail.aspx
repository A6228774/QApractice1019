<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QuestionDetail.aspx.cs" Inherits="QApractice1019.SystemAdmin.QuestionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>問題：</td>
            <td>
                <asp:Literal ID="ltl_title" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td>問題種類：</td>
            <td>
                <asp:Literal ID="ltl_type" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td>選項：</td>
            <td>
                <asp:Literal ID="ltl_choices" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td>加入常用問題庫：</td>
            <td>
                <asp:CheckBox ID="cbx_common" runat="server"/>
        </tr>
    </table>
    <asp:Button ID="btn_cancel" runat="server" Text="取消" OnClick="btn_cancel_Click" /><asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
</asp:Content>
