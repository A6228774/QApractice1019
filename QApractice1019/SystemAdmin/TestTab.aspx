<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestTab.aspx.cs" Inherits="QApractice1019.SystemAdmin.TestTab" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Menu runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="false" OnMenuItemClick="Unnamed_MenuItemClick" RenderingMode="Table" ID="tab_menu">
            <Items>
                <asp:MenuItem Text="問卷資料" Value="0"></asp:MenuItem>
                <asp:MenuItem Text="問題設計" Value="1"></asp:MenuItem>
                <asp:MenuItem Text="輸出CSV" Value="2"></asp:MenuItem>
                <asp:MenuItem Text="查看統計" Value="3"></asp:MenuItem>
            </Items>
        </asp:Menu>

        <asp:MultiView runat="server" ActiveViewIndex="0" ID="multi">
            <asp:View ID="Info_tab" runat="server">
                <table>
                    <tr valign="top">
                        <td class="TabArea">
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
                                    <td>
                                        <asp:TextBox ID="tbx_end" runat="server" TextMode="Date"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="cbx_enable" runat="server" Text="已啟用" Checked="true" /></td>
                                </tr>
                            </table>
                            <asp:Button ID="build_btn" runat="server" Text="保存" OnClick="build_btn_Click" Style="height: 27px" />
                        </td>
                    </tr>
                </table>
            </asp:View>

            <asp:View ID="Design_tab" runat="server">
                <table width="578" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td class="TabArea">
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
                                        <asp:Button ID="edit_btn" runat="server" Text="保存" Visible="False" OnClick="edit_btn_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Literal ID="ltl_errorMsg" runat="server"></asp:Literal>
                            <br />
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
                                            </asp:DropDownList></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CheckBoxField HeaderText="必填" ReadOnly="True" DataField="MustKey" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="edit_btn" runat="server" Text="編輯" CommandName="Q_edit" CommandArgument="<%# Container.DataItemIndex %>" /></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="deleteQ_btn" runat="server" Text="刪除" OnClientClick="return confirm('確認要從本問卷中移除此問題?問卷資料將無法復原');" CommandName="deleteQrow" CommandArgument="<%# Container.DataItemIndex %>" /></ItemTemplate>
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
                            <asp:Literal ID="ltl_NoQuestion" runat="server" Visible="False"></asp:Literal>
                            <br />
                            <asp:Button ID="cancel_btn" runat="server" Text="取消" OnClick="cancel_btn_Click" />
                            <asp:Button ID="save_btn" runat="server" Text="送出" OnClick="save_btn_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>

            <asp:View ID="OutputCSV_tab" runat="server">
                <table width="578" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td class="TabArea">
                            <asp:Button ID="output_btn" runat="server" Text="匯出" OnClick="output_btn_Click" /><asp:Literal ID="ltl_Msg" runat="server" Visible="False"></asp:Literal>
                            <br />
                            <asp:GridView ID="gv_QAList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="姓名" />
                                    <asp:BoundField DataField="AnswerDate" DataFormatString="{0:d}" HeaderText="填寫時間" />
                                    <asp:TemplateField HeaderText="觀看細節">
                                        <ItemTemplate><a href='AnswerDetail.aspx?ID=<%#Eval("RespondentID") %>&amp;QAID=<%# Eval("QAID") %>'>前往</a></ItemTemplate>
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
                            <uc1:ucPager ID="ucPager" runat="server" CurrentPage="1" PageSize="10" TotalSize="10" Url="OutputCSV.aspx" />
                            <asp:Button ID="return_btn" runat="server" OnClick="return_btn_Click" Text="返回" />
                        </td>
                    </tr>
                </table>
            </asp:View>

            <asp:View ID="statistic_tab" runat="server">
                <table width="578" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td class="TabArea">
                            <h2>
                                <asp:Literal ID="ltl_QAtitle" runat="server"></asp:Literal></h2>
                            <asp:Panel ID="pnl_statistic" runat="server"></asp:Panel>
                            <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="返回" OnClick="return_btn_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
