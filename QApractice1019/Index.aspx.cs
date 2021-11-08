using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace QApractice1019
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime today = DateTime.Now;

                List<QAInfo> list = new List<QAInfo>();
                list = QAsManager.GetQAList();

                DataTable dt = GridViewDataBind(today, list);

                this.gv_QAList.DataSource = dt;
                this.gv_QAList.DataBind();

                foreach (GridViewRow gvrow in this.gv_QAList.Rows)
                {
                    if (gvrow.Cells[2].Text != "開放中")
                    {
                        HyperLink link = (HyperLink)gvrow.Cells[1].FindControl("qalink");
                        link.Enabled = false;
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

                DataTable dt = GridViewDataBind(today, list);

                this.gv_QAList.DataSource = dt;
                this.gv_QAList.DataBind();

                foreach (GridViewRow gvrow in this.gv_QAList.Rows)
                {
                    if (gvrow.Cells[2].Text != "開放中")
                    {
                        HyperLink link = (HyperLink)gvrow.Cells[1].FindControl("qalink");
                        link.Enabled = false;
                    }
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
                DataTable dt = GridViewDataBind(today, list);

                this.gv_QAList.DataSource = dt;
                this.gv_QAList.DataBind();

                foreach (GridViewRow gvrow in this.gv_QAList.Rows)
                {
                    if (gvrow.Cells[2].Text != "開放中")
                    {
                        HyperLink link = (HyperLink)gvrow.Cells[1].FindControl("qalink");
                        link.Enabled = false;
                    }
                }
            }
        }
        protected void clear_btn_Click(object sender, EventArgs e)
        {
            this.start_d.Text = string.Empty;
            this.end_d.Text = string.Empty;
            this.tbx_keyword.Text = string.Empty;

            Response.Redirect("Index.aspx");
        }

        private static DataTable GridViewDataBind(DateTime today, List<QAInfo> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5]
            { new DataColumn("QAID"), new DataColumn("QATitle"), new DataColumn("Status"), new DataColumn("StartDate"), new DataColumn("EndDate")});

            foreach (var row in list)
            {
                int qaid = row.QAID;
                string title = row.Title;

                string status;
                if (today < row.StartDate)
                {
                    status = "未開始";
                }
                else if (today > row.EndDate)
                {
                    status = "已完結";
                }
                else
                {
                    status = "開放中";
                }

                string start_d = row.StartDate.ToString("d");
                string end_d = row.EndDate?.ToString("d");

                dt.Rows.Add(qaid, title, status, start_d, end_d);
            }

            return dt;
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