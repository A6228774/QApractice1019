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
    public partial class QAConfirmPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            if (this.Request.QueryString["ID"] == null)
            {
                Response.Redirect("Index.aspx");
            }
            else
            {
                string qaidtxt = this.Request.QueryString["ID"].ToString(); //取得問卷資訊
                int qaid = int.Parse(qaidtxt);
                var qaInfo = QAsManager.GetQADetail(qaid);
                this.ltl_QAtitle.Text = qaInfo.Title;
                this.lb_summary.Text = qaInfo.Summary;

                RespondentInfo info = new RespondentInfo();
                info = (RespondentInfo)HttpContext.Current.Session["QA_respodent"];
                this.ltl_name.Text = info.Name;
                this.ltl_email.Text = info.Email;
                this.ltl_phone.Text = info.Phone;
                this.ltl_age.Text = info.Age.ToString();

                List<ConfirmModel> confirm = new List<ConfirmModel>();
                confirm = (List<ConfirmModel>)HttpContext.Current.Session["QA_respodent_answer"];

                foreach (ConfirmModel item in confirm)
                {
                    Panel pnl_question = new Panel();
                    int qid = int.Parse(item.QID.ToString());

                    if (item.Type == "TB")
                    {
                        Literal title = new Literal();
                        title.Text = item.Title + "</br>";
                        Label tbx_ans = new Label();
                        tbx_ans.ID = "tbx_ans" + item.QID;
                        tbx_ans.Text = item.Answer;

                        pnl_question.Controls.Add(title);
                        pnl_question.Controls.Add(tbx_ans);
                        ph_question.Controls.Add(pnl_question);
                    }
                    else if (item.Type == "RB")
                    {
                        Literal title = new Literal();
                        title.Text = item.Title + "</br>";
                        RadioButtonList rb_ans = new RadioButtonList();
                        rb_ans.ID = "rb_ans" + item.QID;
                        rb_ans.Enabled = false;

                        List<string> list = Getchoicelist(item);
                        rb_ans.DataSource = list;
                        rb_ans.DataBind();

                        rb_ans.SelectedValue = item.Answer;

                        pnl_question.Controls.Add(title);
                        pnl_question.Controls.Add(rb_ans);
                        ph_question.Controls.Add(pnl_question);
                    }
                    else if (item.Type == "CB")
                    {
                        Literal title = new Literal();
                        title.Text = item.Title + "</br>";
                        CheckBoxList cbx_ans = new CheckBoxList();
                        cbx_ans.ID = "cbx_ans" + item.QID;
                        cbx_ans.Enabled = false;

                        string answertxt = item.Answer;
                        char sperator = char.Parse(";");
                        string[] answer_choice = answertxt.Split(sperator);

                        List<string> list = Getchoicelist(item);
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
        }
        protected void submit_btn_Click(object sender, EventArgs e)
        {
            string qaidtxt = this.Request.QueryString["ID"].ToString();
            int qaid = int.Parse(qaidtxt);

            RespondentInfo info = new RespondentInfo();
            info = (RespondentInfo)HttpContext.Current.Session["QA_respodent"];
            string name = info.Name;
            string email = info.Email;

            ResponseTable rt = new ResponseTable();

            List<ConfirmModel> confirm = new List<ConfirmModel>();
            confirm = (List<ConfirmModel>)HttpContext.Current.Session["QA_respodent_answer"];

            if (!(RespondentInfoManager.CheckRespodentByEmail(email) && RespondentInfoManager.CheckRespodentByName(name)))
            {
                RespondentInfoManager.CreateRespodent(info);
                rt.QAID = qaid;
                rt.AnswerDate = DateTime.Today;
                rt.RespodentID = info.RespondentID;

                RespondentInfoManager.CreateResponse(rt);

                foreach (ConfirmModel item in confirm)
                {
                    Respondent_answer answer = new Respondent_answer();
                    answer.RespondentID = info.RespondentID;
                    answer.RID = rt.ResponseID;
                    answer.QAID = qaid;
                    answer.QuestionID = item.QID;

                    if (item.Type == "TB")
                    {
                        Label label = (Label)ph_question.FindControl("tbx_ans" + item.QID);
                        if (label != null)
                        {
                            answer.Answer = label.Text;
                        }
                    }
                    else if (item.Type == "RB")
                    {
                        RadioButtonList rblist = (RadioButtonList)ph_question.FindControl("rb_ans" + item.QID);
                        answer.ChoiceID = item.CID;

                        if (rblist != null)
                        {
                            answer.Answer = rblist.SelectedValue.ToString();
                        }
                    }
                    else if (item.Type == "CB")
                    {
                        CheckBoxList cbxlist = (CheckBoxList)ph_question.FindControl("cbx_ans" + item.QID);
                        answer.ChoiceID = item.CID;

                        if (cbxlist != null)
                        {
                            foreach (ListItem li in cbxlist.Items)
                            {
                                if (li.Selected)
                                {
                                    answer.Answer += li.Value.ToString() + ";";
                                }
                            }
                        }
                    }

                    AnswerManager.CreateRespodent_answer(answer);
                }
                Response.Redirect("Index.aspx");

            }
            else
            {
                var user = RespondentInfoManager.GetRespodentInfo(name, email);
                rt.QAID = qaid;
                rt.AnswerDate = DateTime.Today;
                rt.RespodentID = user.RespondentID;

                RespondentInfoManager.CreateResponse(rt);

                foreach (ConfirmModel item in confirm)
                {
                    Respondent_answer answer = new Respondent_answer();
                    answer.RespondentID = user.RespondentID;
                    answer.RID = rt.ResponseID;
                    answer.QAID = qaid;
                    answer.QuestionID = item.QID;

                    if (item.Type == "TB")
                    {
                        Label label = (Label)ph_question.FindControl("tbx_ans" + item.QID);
                        if (label != null)
                        {
                            answer.Answer = label.Text;
                        }
                    }
                    else if (item.Type == "RB")
                    {
                        RadioButtonList rblist = (RadioButtonList)ph_question.FindControl("rb_ans" + item.QID);
                        answer.ChoiceID = item.CID;

                        if (rblist != null)
                        {
                            answer.Answer = rblist.SelectedValue.ToString();
                        }
                    }
                    else if (item.Type == "CB")
                    {
                        CheckBoxList cbxlist = (CheckBoxList)ph_question.FindControl("cbx_ans" + item.QID);
                        answer.ChoiceID = item.CID;

                        if (cbxlist != null)
                        {
                            foreach (ListItem li in cbxlist.Items)
                            {
                                if (li.Selected)
                                {
                                    answer.Answer += li.Value.ToString() + ";";
                                }
                            }
                        }
                    }

                    AnswerManager.CreateRespodent_answer(answer);
                }
                Response.Redirect("Index.aspx");
            }
        }
        protected void return_btn_Click(object sender, EventArgs e)
        {
            RespondentInfo info = new RespondentInfo();
            info = (RespondentInfo)HttpContext.Current.Session["QA_respodent"];
            HttpContext.Current.Session["return_info"] = info;

            List<ConfirmModel> confirm = new List<ConfirmModel>();
            confirm = (List<ConfirmModel>)HttpContext.Current.Session["QA_respodent_answer"];
            HttpContext.Current.Session["return_answer"] = confirm;

            string qaidtxt = this.Request.QueryString["ID"].ToString();
            Response.Redirect("QAForm.aspx?ID=" + qaidtxt);
        }

        private static List<string> Getchoicelist(ConfirmModel question)
        {
            int cid = int.Parse(question.CID.ToString());
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