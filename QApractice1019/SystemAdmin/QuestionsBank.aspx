<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QuestionsBank.aspx.cs" Inherits="QApractice1019.SystemAdmin.QuestionsBank" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gv_questionsbank" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="gv_questionsbank_RowDataBound">
        <Columns>
            <asp:BoundField DataField="QuestionID" HeaderText="#" />
            <asp:BoundField DataField="QuestionTitle" HeaderText="問題" />
            <asp:TemplateField HeaderText="問題種類">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_type" runat="server" DataValueField="QuestionType" Enabled="False">
                        <asp:ListItem Value="TB">文字方塊</asp:ListItem>
                        <asp:ListItem Value="RB">單選方塊</asp:ListItem>
                        <asp:ListItem Value="CB">複選方塊</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="詳細設定">
                <ItemTemplate>
                    <a href="QuestionDetail.aspx?QID=<%# Eval("QuestionID") %>">前往
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="常用問題">
                <ItemTemplate>
                   <%# ConvertNullableBool(Eval("CommonQuestion")) %>
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
    <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="QuestionsBank.aspx" />
    <asp:Literal ID="ltl_NoQuestion" runat="server" Visible="False"></asp:Literal></br>
</asp:Content>
