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
    public partial class AnswerDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string qaidtxt = this.Request.QueryString["ID"].ToString();
            int qaid = int.Parse(qaidtxt);
            var qaformInfo = QAsManager.GetQADetail(qaid);
            DateTime today = DateTime.Today;

            var qadesign = QAsManager.GetQAForm(qaid); //取得該問卷中的問題

            foreach (var item in qadesign)
            {
                Panel pnl_question = new Panel();
                int qid = int.Parse(item.QuestionID.ToString());

                var question = QAsManager.GetQuestionDetail(qid);

                if (question.QuestionType.ToString() == "TB")
                {
                    Literal title = new Literal();
                    title.Text = question.QuestionTitle;
                    TextBox tbx_ans = new TextBox();
                    tbx_ans.ID = "tbx_ans" + item.QuestionID;

                    pnl_question.Controls.Add(title);
                    pnl_question.Controls.Add(tbx_ans);
                    ph_question.Controls.Add(pnl_question);
                }
                else if (question.QuestionType.ToString() == "RB")
                {
                    Literal title = new Literal();
                    title.Text = question.QuestionTitle;
                    RadioButtonList rb_ans = new RadioButtonList();
                    rb_ans.ID = "rb_ans" + item.QuestionID;

                    List<string> list = Getchoicelist(question);
                    rb_ans.DataSource = list;
                    rb_ans.DataBind();

                    pnl_question.Controls.Add(title);
                    pnl_question.Controls.Add(rb_ans);
                    ph_question.Controls.Add(pnl_question);
                }
                else if (question.QuestionType.ToString() == "CB")
                {
                    Literal title = new Literal();
                    title.Text = question.QuestionTitle;
                    CheckBoxList cbx_ans = new CheckBoxList();
                    cbx_ans.ID = "cbx_ans" + item.QuestionID;

                    List<string> list = Getchoicelist(question);
                    cbx_ans.DataSource = list;
                    cbx_ans.DataBind();

                    pnl_question.Controls.Add(title);
                    pnl_question.Controls.Add(cbx_ans);
                    ph_question.Controls.Add(pnl_question);
                }
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