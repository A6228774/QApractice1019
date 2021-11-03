<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AnswerDetail.aspx.cs" Inherits="QApractice1019.SystemAdmin.AnswerDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnl_respondentInfo" runat="server" BackColor="#999999">
            <table>
                <tr>
                    <td>您的姓名</td>
                    <td>
                        <asp:TextBox ID="tbx_name" runat="server">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox ID="tbx_email" runat="server">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td>電話</td>
                    <td>
                        <asp:TextBox ID="tbx_phone" runat="server">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td>年齡</td>
                    <td>
                        <asp:TextBox ID="tbx_age" runat="server">
                        </asp:TextBox></td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>
