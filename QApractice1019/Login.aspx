<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QApractice1019.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <h2>登入
    </h2>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>帳號：
                    </td>
                    <td>
                        <asp:TextBox ID="account_tbx" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>密碼：</td>
                    <td>
                        <asp:TextBox ID="pw_tbx" runat="server" TextMode="Password"></asp:TextBox></td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="login_btn" runat="server" Text="登入" OnClick="login_btn_Click" />
                        <asp:Button ID="return_btn" runat="server" Text="返回" OnClick="return_btn_Click" />
                    </td>
                </tr>
            </table>
            <asp:Literal ID="ltlMsg" runat="server" Visible="False"></asp:Literal>
        </div>
    </form>
</body>
</html>
