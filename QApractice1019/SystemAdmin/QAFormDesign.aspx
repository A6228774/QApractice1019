<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QAFormDesign.aspx.cs" Inherits="QApractice1019.SystemAdmin.QAFormDesign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>問題種類：</td>
            <td>
                <asp:DropDownList ID="ddl_question" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_question_SelectedIndexChanged">
                    <asp:ListItem Value="0">自訂題目</asp:ListItem>
                    <asp:ListItem Value="1">問題庫</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>問題：</td>
            <td>
                <asp:TextBox ID="title_tbx" runat="server"></asp:TextBox>
                <asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_type_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="0">請選擇類別</asp:ListItem>
                    <asp:ListItem Value="TB">文字方塊</asp:ListItem>
                    <asp:ListItem Value="RB">單選方塊</asp:ListItem>
                    <asp:ListItem Value="CB">複選方塊</asp:ListItem>
                </asp:DropDownList>
                <asp:CheckBox ID="must_tbx" runat="server" Text="必填" />
            </td>
        </tr>
        <tr>
            <td>選項：</td>
            <td>
                <asp:TextBox ID="choice_txb" runat="server" Enabled="False"></asp:TextBox>
                <asp:Literal ID="Literal1" runat="server" Text="選項以；分隔，並最多6項"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>常用問題：
            </td>
            <td>
                <asp:DropDownList ID="ddl_common" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="QuestionTitle" DataValueField="QuestionID" OnSelectedIndexChanged="ddl_common_SelectedIndexChanged" Visible="False" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Value="0">無</asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnectionString %>" SelectCommand="SELECT * FROM [QuestionsTable] WHERE ([CommonQuestion] = @CommonQuestion)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="True" Name="CommonQuestion" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="add_btn" runat="server" Text="加入" OnClick="add_btn_Click" />
                <asp:Button ID="edit_btn" runat="server" Text="保存" Visible="False" OnClick="edit_btn_Click"/>
            </td>
        </tr>
    </table>
    <asp:Literal ID="ltl_errorMsg" runat="server"></asp:Literal></br>
    <asp:GridView ID="gv_QuestionList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="gv_QuestionList_RowDataBound" OnRowCommand="gv_QuestionList_RowCommand">
        <Columns>
            <asp:BoundField DataField="QuestionID" HeaderText="#" />
            <asp:BoundField DataField="QuestionTitle" HeaderText="問題" />
            <asp:TemplateField HeaderText="問題種類">
                <ItemTemplate>
                    <asp:DropDownList ID="gvddl_type" runat="server" Enabled="False" DataValueField="QuestionType">
                        <asp:ListItem Value="TB">文字方塊</asp:ListItem>
                        <asp:ListItem Value="RB">單選方塊</asp:ListItem>
                        <asp:ListItem Value="CB">複選方塊</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField HeaderText="必填" ReadOnly="True" DataField="MustKey" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" Text="編輯" CommandName="Q_edit" CommandArgument="<%# Container.DataItemIndex %>"/>                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="deleteQ_btn" runat="server" Text="刪除" OnClientClick="return confirm('確認要從本問卷中移除此問題?問卷資料將無法復原');" CommandName="deleteQrow" CommandArgument="<%# Container.DataItemIndex %>"/>
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
    <asp:Literal ID="ltl_NoQuestion" runat="server" Visible="False"></asp:Literal></br>
    <asp:Button ID="cancel_btn" runat="server" Text="取消" OnClick="cancel_btn_Click" />
    <asp:Button ID="save_btn" runat="server" Text="送出" OnClick="save_btn_Click" />
</asp:Content>
