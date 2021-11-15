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
    public partial class QAList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManager.Islogined())
            {
                Response.Redirect("/Index.aspx");
                return;
            }

            var list = QAsManager.GetQAList();

            if (list.Count > 0)  // 檢查有無資料
            {
                var pagedList = this.GetPagedDataTable(list);

                this.gv_QAList.DataSource = pagedList;
                this.gv_QAList.DataBind();

                this.ucPager.TotalSize = list.Count();
                this.ucPager.Bind();
            }
            else
            {
                this.gv_QAList.Visible = false;
                this.ltl_NoData.Visible = true;
            }
        }
        protected void delete_btn_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_QAList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbx = (row.Cells[0].FindControl("delete_cbx") as CheckBox);
                    if (cbx.Checked)
                    {
                        int qaid = int.Parse(row.Cells[1].Text);
                        QAsManager.DeleteQA(qaid);
                    }
                }
            }
        }
        protected void search_btn_Click(object sender, EventArgs e)
        {
            if (tbx_keyword.Text != string.Empty)
            {
                string keyword = this.tbx_keyword.Text;
                DateTime today = DateTime.Now;
                var list = QAsManager.GetQAListbyKeyword(keyword);

                this.gv_QAList.DataSource = list;
                this.gv_QAList.DataBind();

                if (list.Count > 0)  // 檢查有無資料
                {
                    var pagedList = this.GetPagedDataTable(list);

                    this.gv_QAList.DataSource = pagedList;
                    this.gv_QAList.DataBind();

                    this.ucPager.TotalSize = list.Count();
                    this.ucPager.Bind();
                }
                else
                {
                    this.gv_QAList.Visible = false;
                    this.ltl_NoData.Visible = true;
                }
            }
            else if ((start_d.Text != string.Empty) && (end_d.Text != string.Empty))
            {
                string starttxt = this.start_d.Text;
                string endtxt = this.end_d.Text;

                if (string.IsNullOrWhiteSpace(starttxt) || string.IsNullOrEmpty(endtxt)) // 檢查有無輸入日期
                {
                    this.ltlMsg.Visible = true;
                    this.ltlMsg.Text = "<span style='color:red'>搜尋日期有錯誤，請重新選取日期</span>";
                    return;
                }
                try                                // 檢查是否符合DateTime格式(例外輸入狀況:年份五位數&無選取日期)
                {
                    DateTime.Parse(starttxt);
                    DateTime.Parse(endtxt);
                }
                catch (Exception)
                {
                    this.ltlMsg.Visible = true;
                    this.ltlMsg.Text = "<span style='color:red'>搜尋日期有錯誤，請重新選取日期</span>";
                    return;
                }

                DateTime start = DateTime.Parse(starttxt);
                DateTime end = DateTime.Parse(endtxt);
                DateTime today = DateTime.Today;

                if (end <= start)
                {
                    this.ltlMsg.Visible = true;
                    this.ltlMsg.Text = "<span style='color:red'>結束日期必須大於起始日期，請重新選取日期</span>";
                }

                var list = QAsManager.GetQAsByDate(start, end);
                this.gv_QAList.DataSource = list;
                this.gv_QAList.DataBind();

                if (list.Count > 0)  // 檢查有無資料
                {
                    var pagedList = this.GetPagedDataTable(list);

                    this.gv_QAList.DataSource = pagedList;
                    this.gv_QAList.DataBind();

                    this.ucPager.TotalSize = list.Count();
                    this.ucPager.Bind();
                }
                else
                {
                    this.gv_QAList.Visible = false;
                    this.ltl_NoData.Visible = true;
                }
            }
        }
        protected void new_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("QAFormDetail.aspx");
        }
        protected void clear_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void logout_btn_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            Response.Redirect("../Index.aspx");
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
        private List<QAInfo> GetPagedDataTable(List<QAInfo> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }
    }
}