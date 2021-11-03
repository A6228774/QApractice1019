using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
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

        }        
        protected void search_btn_Click(object sender, EventArgs e)
        {
            if (tbx_keyword.Text != string.Empty)
            {
                string keyword = this.tbx_keyword.Text;

                QAsManager.GetQAListbyKeyword(keyword);

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
                DateTime start_d = DateTime.Parse(starttxt);
                DateTime end_d = DateTime.Parse(endtxt);

                if (end_d <= start_d)
                {
                    this.ltlMsg.Visible = true;
                    this.ltlMsg.Text = "<span style='color:red'>結束日期必須大於起始日期，請重新選取日期</span>";
                }

                var list = QAsManager.GetOrdersByDate(start_d, end_d);

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
        private List<CustomizeQA> GetPagedDataTable(List<CustomizeQA> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }

    }
}