using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QApractice1019
{
    public partial class QAForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["ID"] == null)
            {
                Response.Redirect("Index.aspx");
            }
            else
            {
                string qaidtxt = this.Request.QueryString["ID"].ToString();
                int qaid = int.Parse(qaidtxt);
                var qaformInfo = QAsManager.GetQADetail(qaid);

                this.ltl_QAtitle.Text = qaformInfo.Title;
                this.lb_summary.Text = qaformInfo.Summary;

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
        }
        protected void submit_btn_Click(object sender, EventArgs e)
        {
            string qaidtxt = this.Request.QueryString["ID"].ToString();
            int qaid = int.Parse(qaidtxt);

            string nametxt = this.tbx_name.Text;
            string emailtxt = this.tbx_email.Text;
            string phonetxt = this.tbx_phone.Text;
            int age = int.Parse(this.tbx_age.Text.ToString());

            var qadesign = QAsManager.GetQAForm(qaid);

            if (!(RespondentInfoManager.GetRespodentByEmail(emailtxt) && RespondentInfoManager.GetRespodentByName(nametxt)))
            {
                RespondentInfo info = new RespondentInfo();

                Guid guid = Guid.NewGuid();
                info.RespondentID = guid;

                info.Name = nametxt;
                info.Email = emailtxt;
                info.Phone = phonetxt;
                info.Age = age;
                RespondentInfoManager.CreateRespodent(info);

                Respondent_answer ans = new Respondent_answer();

                foreach (var item in qadesign)
                {
                    ans.RespondentID = guid;
                    ans.QAID = qaid;
                    ans.QuestionID = item.QuestionID;

                    int qid = int.Parse(item.QuestionID.ToString());
                    var question = QAsManager.GetQuestionDetail(qid);

                    if (question.QuestionType.ToString() == "TB")
                    {
                        TextBox txtbox = (TextBox)ph_question.FindControl("tbx_ans" + item.QuestionID);
                        if (txtbox != null)
                        {
                            ans.Answer = txtbox.Text;
                        }
                    }
                    else if (question.QuestionType.ToString() == "RB")
                    {
                        RadioButtonList rblist = (RadioButtonList)ph_question.FindControl("rb_ans" + item.QuestionID);
                        ans.ChoiceID = question.ChoiceID;

                        if (rblist != null)
                        {
                            ans.Answer = rblist.SelectedValue.ToString();
                        }
                    }
                    else if (question.QuestionType.ToString() == "CB")
                    {
                        CheckBoxList cbxlist = (CheckBoxList)ph_question.FindControl("cbx_ans" + item.QuestionID);
                        ans.ChoiceID = question.ChoiceID;

                        if (cbxlist != null)
                        {
                            foreach (ListItem li in cbxlist.Items)
                            {
                                if (li.Selected)
                                {
                                    ans.Answer += li.Value.ToString() + ";";
                                }
                            }
                        }
                    }

                    AnswerManager.CreateRespodent_answer(ans);
                }
                Response.Redirect("Index.aspx");
            }
            else
            {
                var respondent = RespondentInfoManager.GetRespodentInfo(nametxt, emailtxt);

                Respondent_answer ans = new Respondent_answer();

                Save_answer(qaid, qadesign, respondent, ans);
                Response.Redirect("Index.aspx");
            }

        }

        private void Save_answer(int qaid, List<QADesign> qadesign, RespondentInfo respondent, Respondent_answer ans)
        {
            foreach (var item in qadesign)
            {
                ans.RespondentID = respondent.RespondentID;
                ans.QAID = qaid;
                ans.QuestionID = item.QuestionID;

                int qid = int.Parse(item.QuestionID.ToString());
                var question = QAsManager.GetQuestionDetail(qid);

                if (question.QuestionType.ToString() == "TB")
                {
                    TextBox txtbox = (TextBox)ph_question.FindControl("tbx_ans" + item.QuestionID);
                    if (txtbox != null)
                    {
                        ans.Answer = txtbox.Text;
                    }
                }
                else if (question.QuestionType.ToString() == "RB")
                {
                    RadioButtonList rblist = (RadioButtonList)ph_question.FindControl("rb_ans" + item.QuestionID);
                    ans.ChoiceID = question.ChoiceID;

                    if (rblist != null)
                    {
                        ans.Answer = rblist.SelectedValue.ToString();
                    }
                }
                else if (question.QuestionType.ToString() == "CB")
                {
                    CheckBoxList cbxlist = (CheckBoxList)ph_question.FindControl("cbx_ans" + item.QuestionID);
                    ans.ChoiceID = question.ChoiceID;

                    if (cbxlist != null)
                    {
                        foreach (ListItem li in cbxlist.Items)
                        {
                            if (li.Selected)
                            {
                                ans.Answer += li.Value.ToString() + ";";
                            }
                        }
                    }
                }

                AnswerManager.CreateRespodent_answer(ans);
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