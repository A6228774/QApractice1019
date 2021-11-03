<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QAForm.aspx.cs" Inherits="QApractice1019.QAForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <h2>
        <asp:Literal ID="ltl_QAtitle" runat="server"></asp:Literal></h2>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lb_date" runat="server"></asp:Label></br>
            <asp:Label ID="lb_summary" runat="server"></asp:Label>
        </div>
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
            <asp:Literal ID="ltl_date" runat="server"></asp:Literal>
        </asp:Panel>
        <asp:PlaceHolder ID="ph_question" runat="server"></asp:PlaceHolder>
        </br>
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal></br>
        <asp:Button ID="submit_btn" runat="server" Text="提交" OnClick="submit_btn_Click" />
    </form>
</body>
</html>
