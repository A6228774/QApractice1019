﻿using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QApractice1019.SystemAdmin
{
    public partial class OutputCSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var list = RespondentInfoManager.GetQAList();

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
        private List<Respondent_answer> GetPagedDataTable(List<Respondent_answer> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }
    }
}