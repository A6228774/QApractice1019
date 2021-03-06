<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="QApractice1019.Index" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }
    </style>
</head>
<body>
    <h2>簡易問卷大全</h2>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="login_btn" runat="server" Text="登入" align="right" OnClick="login_btn_Click" />
            <table border="1">
                <tr>
                    <td>問卷名稱：</td>
                    <td>
                        <asp:TextBox ID="tbx_keyword" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style1">開始/結束：</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="start_d" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:TextBox ID="end_d" runat="server" TextMode="Date"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="search_btn" runat="server" Text="檢索" OnClick="search_btn_Click" />
                        <asp:Button ID="clear_btn" runat="server" Text="清空" OnClick="clear_btn_Click" />
                    </td>
                </tr>
            </table>
            <asp:Literal ID="ltlMsg" runat="server" Visible="False"></asp:Literal>
            <asp:GridView ID="gv_QAList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" AllowPaging="True" OnPageIndexChanging="gv_QAList_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="#" DataField="QAID" />
                    <asp:TemplateField HeaderText="問卷名稱">
                        <ItemTemplate>
                            <asp:HyperLink ID="qalink" runat="server" NavigateUrl='<%# string.Format("QAForm.aspx?ID={0}",HttpUtility.UrlEncode(Eval("QAID").ToString())) %>'><%# Eval("QATitle") %></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="狀態" DataField="Status" />
                    <asp:BoundField DataField="StartDate" HeaderText="開始日期" DataFormatString="{0:d}" />
                    <asp:BoundField HeaderText="結束日期" DataField="EndDate" DataFormatString="{0:d}" NullDisplayText="-" />
                    <asp:TemplateField HeaderText="觀看統計">
                        <ItemTemplate>
                            <a href="Statistics.aspx?ID=<%# Eval("QAID") %>">前往</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>
            <asp:Literal ID="ltl_NoData" runat="server" Text="沒有任何問卷" Visible="False"></asp:Literal>
            <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="Index.aspx" />
        </div>
    </form>
</body>
</html>
