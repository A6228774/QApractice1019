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
            string qaidtxt = this.Request.QueryString["QAID"].ToString();
            int qaid = int.Parse(qaidtxt);
            var qaformInfo = QAsManager.GetQADetail(qaid);
            this.ltl_QAtitle.Text = qaformInfo.Title;

            string respodentidtxt = this.Request.QueryString["ID"].ToString();
            Guid respodentid = Guid.Parse(respodentidtxt);

            var qadesign = QAsManager.GetQAForm(qaid); //取得該問卷中的問題
            var respodentinfo = RespondentInfoManager.GetRespodentInfobyID(respodentid);

            this.ltl_name.Text = respodentinfo.Name;
            this.ltl_email.Text = respodentinfo.Email;
            this.ltl_phone.Text = respodentinfo.Phone;
            this.ltl_age.Text = respodentinfo.Age.ToString();

            foreach (var item in qadesign)
            {
                Panel pnl_question = new Panel();
                int qid = int.Parse(item.QuestionID.ToString());

                var question = QAsManager.GetQuestionDetail(qid);
                var answer = RespondentInfoManager.GetRespodent_question_answer(respodentid, qaid, qid);

                if (question.QuestionType.ToString() == "TB")
                {
                    Literal title = new Literal();
                    title.Text = question.QuestionTitle;
                    TextBox tbx_ans = new TextBox();
                    tbx_ans.ID = "tbx_ans" + item.QuestionID;
                    tbx_ans.Enabled = false;
                    tbx_ans.Text = answer.Answer;

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
                    rb_ans.Enabled = false;

                    List<string> list = Getchoicelist(question);
                    rb_ans.DataSource = list;
                    rb_ans.DataBind();

                    rb_ans.SelectedValue = answer.Answer;

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
                    cbx_ans.Enabled = false;

                    string answertxt = answer.Answer;
                    char sperator = char.Parse(";");
                    string[] answer_choice = answertxt.Split(sperator);

                    List<string> list = Getchoicelist(question);
                    cbx_ans.DataSource = list;
                    cbx_ans.DataBind();

                    foreach (var c in answer_choice)
                    {
                        if (c != "")
                        {
                            cbx_ans.Items.FindByValue(c).Selected = true;
                        }
                    }

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