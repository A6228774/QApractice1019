using QA.Auth;
using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QApractice1019.SystemAdmin
{
    public partial class QuestionsBank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManager.Islogined())
            {
                Response.Redirect("QAList.aspx");
                return;
            }

            var list = QuestionsManager.GetQuestionsList();

            if (list.Count > 0)  // 檢查有無資料
            {
                var pagedList = this.GetPagedDataTable(list);

                this.gv_questionsbank.DataSource = pagedList;
                this.gv_questionsbank.DataBind();

                this.ucPager.TotalSize = list.Count();
                this.ucPager.Bind();
            }
            else
            {
                this.gv_questionsbank.Visible = false;
                this.ltl_NoQuestion.Visible = true;
            }

        }
        protected void gv_questionsbank_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = (e.Row.FindControl("ddl_type") as DropDownList);
                int qid = int.Parse(e.Row.Cells[0].Text);

                ddl.DataSource = QuestionsManager.GetQuestionsListbyQID(qid);
                ddl.DataBind();

                if(ddl.SelectedValue == "TB")
                {
                    ddl.SelectedItem.Text = "文字方塊";
                }
                else if(ddl.SelectedValue == "RB")
                {
                    ddl.SelectedItem.Text = "單選方塊";
                }
                else if (ddl.SelectedValue == "CB")
                {
                    ddl.SelectedItem.Text = "複選方塊";
                }
            }
        }

        public string ConvertNullableBool(object cq)
        {
            if (cq != null)
            {
                return (bool)cq ? "是" : "否";
            }
            else
            {
                return "-";
            }
        }
        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;
            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;
            if (intPage <= 0)
                return 1;
            return intPage;
        }
        private List<QuestionsTable> GetPagedDataTable(List<QuestionsTable> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }

    }
}