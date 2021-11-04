<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="OutputCSV.aspx.cs" Inherits="QApractice1019.SystemAdmin.OutputCSV" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="output_btn" runat="server" Text="匯出" OnClick="output_btn_Click"/></br>
    <asp:GridView ID="gv_QAList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
        <Columns>
            <asp:BoundField HeaderText="姓名" DataField="Name" />
            <asp:BoundField HeaderText="填寫時間" DataField="AnswerDate" DataFormatString="{0:d}" />
            <asp:TemplateField HeaderText="觀看細節">
                <Itemtemplate>
                   <a href="AnswerDetail.aspx?ID=<%#Eval("RespondentID") %>&QAID=<%# Eval("QAID") %>">前往</a>
                </Itemtemplate>
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
    <asp:Literal ID="ltl_NoData" runat="server"></asp:Literal>
    <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="OutputCSV.aspx"/>
</asp:Content>
