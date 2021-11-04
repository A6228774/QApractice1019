using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace QApractice1019
{
    public partial class Statistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["ID"] != null)
            {
                string qaidtxt = this.Request.QueryString["ID"].ToString();
                int qaid = int.Parse(qaidtxt);
                var qaformInfo = QAsManager.GetQADetail(qaid);
                this.ltl_QAtitle.Text = qaformInfo.Title;

                var qadesign = QAsManager.GetQAForm(qaid);

                foreach (var item in qadesign)
                {
                    Panel pnl_question = new Panel();
                    int qid = int.Parse(item.QuestionID.ToString());

                    var question = QAsManager.GetQuestionDetail(qid);

                    if (question.QuestionType.ToString() == "RB")
                    {
                        Literal title = new Literal();
                        title.Text = question.QuestionTitle;
                        Chart rb_chart = new Chart();

                        List<string> list = Getchoicelist(question);

                        pnl_question.Controls.Add(title);
                        pnl_question.Controls.Add(rb_chart);
                        ph_question.Controls.Add(pnl_question);
                    }
                    else if (question.QuestionType.ToString() == "CB")
                    {
                        Literal title = new Literal();
                        title.Text = question.QuestionTitle;
                        Chart cbx_chart = new Chart();

                        pnl_question.Controls.Add(title);
                        pnl_question.Controls.Add(cbx_chart);
                        ph_question.Controls.Add(pnl_question);
                    }
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }
        private static List<string> Getchoicelist(QuestionsTable question)
        {
            int cid = int.Parse(question.ChoiceID.ToString());
            ChoiceTable choices = new ChoiceTable();
            choices = QAsManager.GetChoiceList(cid);

            List<string> list = new List<string>();
            if (choices.ChoiceCount == 1)
            {
                list.Add(choices.FirstChoice);
            }
            else if (choices.ChoiceCount == 2)
            {
                list.Add(choices.FirstChoice);
                list.Add(choices.SecondChoice);
            }
            else if (choices.ChoiceCount == 3)
            {
                list.Add(choices.FirstChoice);
                list.Add(choices.SecondChoice);
                list.Add(choices.ThirdChoice);
            }
            else if (choices.ChoiceCount == 4)
            {
                list.Add(choices.FirstChoice);
                list.Add(choices.SecondChoice);
                list.Add(choices.ThirdChoice);
                list.Add(choices.ForthChoice);
            }
            else if (choices.ChoiceCount == 5)
            {
                list.Add(choices.FirstChoice);
                list.Add(choices.SecondChoice);
                list.Add(choices.ThirdChoice);
                list.Add(choices.ForthChoice);
                list.Add(choices.FifthChoice);
            }
            else if (choices.ChoiceCount == 6)
            {
                list.Add(choices.FirstChoice);
                list.Add(choices.SecondChoice);
                list.Add(choices.ThirdChoice);
                list.Add(choices.ForthChoice);
                list.Add(choices.FifthChoice);
                list.Add(choices.SixthChoice);
            }

            return list;
        }
    }
}