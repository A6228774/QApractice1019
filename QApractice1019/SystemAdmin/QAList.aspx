<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QAList.aspx.cs" Inherits="QApractice1019.SystemAdmin.QAList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table border="1">
            <tr>
                <td>問卷名稱：</td>
                <td>
                    <asp:TextBox ID="tbx_keyword" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>開始/結束：</td>
                <td>
                    <asp:TextBox ID="start_d" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:TextBox ID="end_d" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="search_btn" runat="server" Text="檢索" OnClick="search_btn_Click" /></td>
            </tr>
        </table>
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal></br>
        <asp:Button ID="delete_btn" runat="server" Text="刪除" OnClientClick="return confirm('確認要刪除這筆訂單嗎?問卷資料將無法復原');" OnClick="delete_btn_Click" />
        <asp:Button ID="new_btn" runat="server" Text="新問卷" OnClick="new_btn_Click" />
        <asp:GridView ID="gv_QAList" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="delete_cbx" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="#" DataField="QAID" />
                <asp:TemplateField HeaderText="問卷">
                    <ItemTemplate>
                        <a href="QAFormDetail.aspx?ID=<%# Eval("QAID") %>"><%# Eval("Title") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="狀態">
                    <ItemTemplate>
                        <%# Boolean.Parse(Eval("IsEnabled").ToString()) ? "啟用" : "關閉" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="開始時間" DataField="StartDate" DataFormatString="{0:d}" />
                <asp:BoundField HeaderText="結束時間" DataField="EndDate" DataFormatString="{0:d}" />
                <asp:TemplateField HeaderText="回答資料">
                    <ItemTemplate>
                        <a href="OutputCSV.aspx">觀看</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="觀看統計">
                    <ItemTemplate>
                        <a href="../Statistics.aspx?ID=<%# Eval("QAID") %>">前往</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
        <asp:Literal ID="ltl_NoData" runat="server" Text="沒有任何問卷" Visible="False"></asp:Literal>
        <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="QAList.aspx" />
    </div>
</asp:Content>
