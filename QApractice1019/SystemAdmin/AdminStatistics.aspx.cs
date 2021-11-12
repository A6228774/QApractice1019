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
    public partial class AdminStatistics : System.Web.UI.Page
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
                    Literal linebreak = new Literal();
                    linebreak.Text = "<br/>";

                    var question = QuestionsManager.GetQuestionDetail(qid);

                    if (question.QuestionType.ToString() == "RB")
                    {
                        Literal title = new Literal();
                        title.Text = question.QuestionTitle + "</br>";
                        pnl_question.Controls.Add(title);

                        List<string> list = Getchoicelist(question);
                        foreach (string li in list)
                        {
                            Literal choice = new Literal();
                            choice.Text = li + "：";

                            var dt = AnswerManager.CountRBanswer(qaid, qid, li);
                            Literal cnt = new Literal();
                            cnt.Text = $"({dt.Count().ToString()})</br>";

                            Literal percent = new Literal();
                            float total = AnswerManager.CountTotalanswerbyQAID(qaid, qid);
                            float percentage = dt.Count() / total;
                            if (total != 0)
                            {
                                percent.Text = $"{percentage.ToString("P2")}";
                            }

                            pnl_question.Controls.Add(choice);
                            pnl_question.Controls.Add(percent);
                            pnl_question.Controls.Add(cnt);
                        }
                        this.ph_question.Controls.Add(pnl_question);
                        this.ph_question.Controls.Add(linebreak);
                    }
                    else if (question.QuestionType.ToString() == "CB")
                    {
                        Literal title = new Literal();
                        title.Text = question.QuestionTitle + "</br>";
                        pnl_question.Controls.Add(title);

                        List<string> list = Getchoicelist(question);
                        foreach (string li in list)
                        {
                            Literal choice = new Literal();
                            choice.Text = li + "：";

                            var dt = AnswerManager.CountCBanswer(qaid, qid, li);
                            Literal cnt = new Literal();
                            cnt.Text = $"({dt.Count().ToString()})</br>";

                            Literal percent = new Literal();
                            float total = AnswerManager.CountTotalanswerbyQAID(qaid, qid);
                            float percentage = dt.Count() / total;
                            if (total != 0)
                            {
                                percent.Text = $"{percentage.ToString("P2")}";
                            }

                            pnl_question.Controls.Add(choice);
                            pnl_question.Controls.Add(percent);
                            pnl_question.Controls.Add(cnt);
                        }
                        this.ph_question.Controls.Add(pnl_question);
                        this.ph_question.Controls.Add(linebreak);
                    }
                    else
                    {
                        Literal title = new Literal();
                        title.Text = question.QuestionTitle + "</br>" + "<span style='color:red'>文字方塊不作統計</span>" + "</br></br>";
                        pnl_question.Controls.Add(title);
                        this.ph_question.Controls.Add(pnl_question);
                        this.ph_question.Controls.Add(linebreak);
                    }
                }
            }
            else
            {
                Response.Redirect("QAList.aspx");
            }
        }
        protected void return_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("QAList.aspx");
        }

        private static List<string> Getchoicelist(QuestionsTable question)
        {
            int cid = int.Parse(question.ChoiceID.ToString());
            ChoiceTable choices = new ChoiceTable();
            choices = QuestionsManager.GetChoiceList(cid);

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