using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QApractice1019.SystemAdmin
{
    public partial class OutputCSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["ID"] != null)
            {
                string qaidtxt = this.Request.QueryString["ID"].ToString();
                int qaid = int.Parse(qaidtxt);

                var list = RespondentInfoManager.GetResponsesList(qaid);

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
            else
            {
                Response.Redirect("QAList.aspx");
            }
        }        
        protected void output_btn_Click(object sender, EventArgs e)
        {
            string qaidtxt = this.Request.QueryString["ID"].ToString();
            int qaid = int.Parse(qaidtxt);
            string filepath = $@"D:\Practice\QApractice1019\QApractice1019\QA_answerData{qaidtxt}_{DateTime.Now.ToString()}.csv";

            var list = RespondentInfoManager.GetAllResponsesbyQAID(qaid);
            CSVGenerator<All_Answer_View>(true, filepath, list);
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
        private List<CSVOutput_View> GetPagedDataTable(List<CSVOutput_View> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }
        void CSVGenerator<T>(bool genColumn, string FilePath, List<T> data)
        {
            using (var file = new StreamWriter(FilePath))
            {
                Type t = typeof(T);
                PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                //是否要輸出屬性名稱
                if (genColumn)
                {
                    file.WriteLineAsync(string.Join(",", propInfos.Select(i => i.Name)));
                }
                foreach (var item in data)
                {
                    file.WriteLineAsync(string.Join(",", propInfos.Select(i => i.GetValue(item))));
                }
            }
        }
    }
}