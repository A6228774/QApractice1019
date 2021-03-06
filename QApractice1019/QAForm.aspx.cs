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
                var qaInfo = QAsManager.GetQADetail(qaid);
                DateTime today = DateTime.Today;

                this.ltl_QAtitle.Text = qaInfo.Title;
                this.lb_summary.Text = qaInfo.Summary;

                var qadesign = QAsManager.GetQAForm(qaid); //取得該問卷中的問題

                if (today < qaInfo.StartDate || today > qaInfo.EndDate)
                {
                    this.submit_btn.Enabled = false;
                }
                else
                {
                    if (this.Session["return_info"] != null && this.Session["return_answer"] != null)
                    {
                        RespondentInfo info = new RespondentInfo();
                        info = (RespondentInfo)HttpContext.Current.Session["QA_respodent"];
                        this.tbx_name.Text = info.Name;
                        this.tbx_email.Text = info.Email;
                        this.tbx_phone.Text = info.Phone;
                        this.tbx_age.Text = info.Age.ToString();

                        List<ConfirmModel> confirm = new List<ConfirmModel>();
                        confirm = (List<ConfirmModel>)HttpContext.Current.Session["QA_respodent_answer"];

                        foreach (ConfirmModel item in confirm)
                        {
                            Panel pnl_question = new Panel();
                            int qid = int.Parse(item.QID.ToString());
                            var question = QuestionsManager.GetQuestionDetail(qid);
                            bool mustkey = QAsManager.CheckMustkey(qaid, qid);

                            Label mustltl = new Label();
                            if (mustkey)
                            {
                                mustltl.Text = "<span style='color:red'>必填</span>";
                                pnl_question.Controls.Add(mustltl);
                            }

                            if (item.Type == "TB")
                            {
                                Literal title = new Literal();
                                title.Text = item.Title + "</br>";

                                TextBox tbx_ans = new TextBox();
                                tbx_ans.ID = "tbx_ans" + item.QID;
                                tbx_ans.Text = item.Answer;

                                pnl_question.Controls.Add(title);
                                pnl_question.Controls.Add(tbx_ans);
                                pn_allquestions.Controls.Add(pnl_question);
                            }
                            else if (item.Type == "RB")
                            {
                                Literal title = new Literal();
                                title.Text = item.Title + "</br>";
                                RadioButtonList rb_ans = new RadioButtonList();
                                rb_ans.ID = "rb_ans" + item.QID;

                                List<string> list = Getchoicelist(question);
                                rb_ans.DataSource = list;
                                rb_ans.DataBind();

                                rb_ans.SelectedValue = item.Answer;

                                pnl_question.Controls.Add(title);
                                pnl_question.Controls.Add(rb_ans);
                                pn_allquestions.Controls.Add(pnl_question);
                            }
                            else if (item.Type == "CB")
                            {
                                Literal title = new Literal();
                                title.Text = item.Title + "</br>";
                                CheckBoxList cbx_ans = new CheckBoxList();
                                cbx_ans.ID = "cbx_ans" + item.QID;

                                string answertxt = item.Answer;
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
                                pn_allquestions.Controls.Add(pnl_question);
                            }
                        }

                    }
                    else
                    {
                        foreach (var item in qadesign)
                        {
                            Panel pnl_question = new Panel();
                            int qid = int.Parse(item.QuestionID.ToString());

                            var question = QuestionsManager.GetQuestionDetail(qid);
                            bool mustkey = QAsManager.CheckMustkey(qaid, qid);

                            Label mustltl = new Label();
                            if (mustkey)
                            {
                                mustltl.Text = "<span style='color:red'>必填</span>";
                                pnl_question.Controls.Add(mustltl);
                            }

                            if (question.QuestionType.ToString() == "TB")
                            {
                                Literal title = new Literal();
                                title.Text = question.QuestionTitle + "</br>";
                                TextBox tbx_ans = new TextBox();
                                tbx_ans.ID = "tbx_ans" + item.QuestionID;


                                pnl_question.Controls.Add(title);
                                pnl_question.Controls.Add(tbx_ans);
                                this.pn_allquestions.Controls.Add(pnl_question);
                            }
                            else if (question.QuestionType.ToString() == "RB")
                            {
                                Literal title = new Literal();
                                title.Text = question.QuestionTitle + "</br>";
                                RadioButtonList rb_ans = new RadioButtonList();
                                rb_ans.ID = "rb_ans" + item.QuestionID;

                                List<string> list = Getchoicelist(question);
                                rb_ans.DataSource = list;
                                rb_ans.DataBind();

                                pnl_question.Controls.Add(title);
                                pnl_question.Controls.Add(rb_ans);
                                this.pn_allquestions.Controls.Add(pnl_question);
                            }
                            else if (question.QuestionType.ToString() == "CB")
                            {
                                Literal title = new Literal();
                                title.Text = question.QuestionTitle + "</br>";
                                CheckBoxList cbx_ans = new CheckBoxList();
                                cbx_ans.ID = "cbx_ans" + item.QuestionID;

                                List<string> list = Getchoicelist(question);
                                cbx_ans.DataSource = list;
                                cbx_ans.DataBind();

                                pnl_question.Controls.Add(title);
                                pnl_question.Controls.Add(cbx_ans);
                                this.pn_allquestions.Controls.Add(pnl_question);
                            }
                        }
                    }
                }
            }
        }
        protected void submit_btn_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltlMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            string qaidtxt = this.Request.QueryString["ID"].ToString();
            int qaid = int.Parse(qaidtxt);

            string nametxt = this.tbx_name.Text;
            string emailtxt = this.tbx_email.Text;
            string phonetxt = this.tbx_phone.Text;
            int age = int.Parse(this.tbx_age.Text.ToString());

            var qadesign = QAsManager.GetQAForm(qaid);

            if ((RespondentInfoManager.CheckRespodentByEmail(emailtxt) && RespondentInfoManager.CheckRespodentByName(nametxt)))
            {
                var user = RespondentInfoManager.GetRespodentInfo(nametxt, emailtxt);
                Guid rid = user.RespondentID;

                if (RespondentInfoManager.CheckRepeatResponse(rid, qaid))
                {
                    this.ltlMsg.Visible = true;
                    this.ltlMsg.Text = "<span style='color:red'>此用戶已經回答過本問卷</span>";
                }
                else
                {
                    HttpContext.Current.Session["QA_respodent"] = user;

                    List<ConfirmModel> resplist = new List<ConfirmModel>();

                    foreach (var item in qadesign)
                    {
                        ConfirmModel resp = new ConfirmModel();
                        int qid = int.Parse(item.QuestionID.ToString());
                        var q = QuestionsManager.GetQuestionDetail(qid);

                        resp.QID = qid;
                        resp.Title = q.QuestionTitle;
                        resp.Type = q.QuestionType;

                        if (q.QuestionType.ToString() == "TB")
                        {
                            TextBox txtbox = (TextBox)this.pn_allquestions.FindControl("tbx_ans" + item.QuestionID);
                            if (txtbox != null)
                            {
                                if (txtbox.Text == string.Empty && QAsManager.CheckMustkey(qaid, qid))
                                {
                                    this.ltlMsg.Text += $"<span style='color:red'>題目：{q.QuestionTitle}。項目為必填</span>";
                                    return;
                                }
                                else
                                {
                                    resp.Answer = txtbox.Text;
                                }
                            }
                        }
                        else if (q.QuestionType.ToString() == "RB")
                        {
                            RadioButtonList rblist = (RadioButtonList)pn_allquestions.FindControl("rb_ans" + item.QuestionID);
                            if (q.ChoiceID != null)
                            {
                                resp.CID = (int)q.ChoiceID;
                            }

                            if (rblist != null)
                            {
                                if (QAsManager.CheckMustkey(qaid, qid) && (rblist.SelectedValue == null))
                                {
                                    this.ltlMsg.Text += $"<span style='color:red'>題目：{q.QuestionTitle}。項目為必填</span>";
                                    return;
                                }
                                else
                                {
                                    resp.Answer = rblist.SelectedValue.ToString();
                                }
                            }
                        }
                        else if (q.QuestionType.ToString() == "CB")
                        {
                            List<string> anslist = new List<String>(); ;
                            CheckBoxList cbxlist = (CheckBoxList)pn_allquestions.FindControl("cbx_ans" + item.QuestionID);
                            if (q.ChoiceID != null)
                            {
                                resp.CID = (int)q.ChoiceID;
                            }
                            if (cbxlist != null)
                            {
                                foreach (ListItem li in cbxlist.Items)
                                {
                                    if (li.Selected)
                                    {
                                        anslist.Add(li.Value);
                                        resp.Answer = string.Join(";", anslist);
                                    }
                                }
                                if (resp.Answer == string.Empty && QAsManager.CheckMustkey(qaid, qid))
                                {
                                    this.ltlMsg.Text += $"<span style='color:red'>題目：{q.QuestionTitle}。項目為必填</span>";
                                    return;
                                }
                            }
                        }
                        resplist.Add(resp);
                    }
                    HttpContext.Current.Session["QA_respodent_answer"] = resplist;
                    Response.Redirect("QAConfirmPage.aspx?ID=" + qaidtxt);
                }
            }
            else
            {
                #region RespondentInfo   

                RespondentInfo info = new RespondentInfo();

                Guid guid = Guid.NewGuid();
                info.RespondentID = guid;

                info.Name = nametxt;
                info.Email = emailtxt;
                info.Phone = phonetxt;
                info.Age = age;
                #endregion

                List<ConfirmModel> resplist = new List<ConfirmModel>();

                foreach (var item in qadesign)
                {
                    ConfirmModel resp = new ConfirmModel();
                    int qid = int.Parse(item.QuestionID.ToString());
                    var q = QuestionsManager.GetQuestionDetail(qid);

                    resp.QID = qid;
                    resp.Title = q.QuestionTitle;
                    resp.Type = q.QuestionType;

                    if (q.QuestionType.ToString() == "TB")
                    {
                        TextBox txtbox = (TextBox)this.pn_allquestions.FindControl("tbx_ans" + item.QuestionID);
                        if (txtbox != null)
                        {
                            if (txtbox.Text == string.Empty && QAsManager.CheckMustkey(qaid, qid))
                            {
                                this.ltlMsg.Text += $"<span style='color:red'>題目：{q.QuestionTitle}。項目為必填</span>";
                                return;
                            }
                            else
                            {
                                resp.Answer = txtbox.Text;
                            }
                        }
                    }
                    else if (q.QuestionType.ToString() == "RB")
                    {
                        RadioButtonList rblist = (RadioButtonList)pn_allquestions.FindControl("rb_ans" + item.QuestionID);
                        if (q.ChoiceID != null)
                        {
                            resp.CID = (int)q.ChoiceID;
                        }

                        if (rblist != null)
                        {
                            if (QAsManager.CheckMustkey(qaid, qid) && (rblist.SelectedValue == null))
                            {
                                this.ltlMsg.Text += $"<span style='color:red'>題目：{q.QuestionTitle}。項目為必填</span>";
                                return;
                            }
                            else
                            {
                                resp.Answer = rblist.SelectedValue.ToString();
                            }
                        }
                    }
                    else if (q.QuestionType.ToString() == "CB")
                    {
                        List<string> anslist = new List<String>(); ;
                        CheckBoxList cbxlist = (CheckBoxList)pn_allquestions.FindControl("cbx_ans" + item.QuestionID);
                        if (q.ChoiceID != null)
                        {
                            resp.CID = (int)q.ChoiceID;
                        }
                        if (cbxlist != null)
                        {
                            foreach (ListItem li in cbxlist.Items)
                            {
                                if (li.Selected)
                                {
                                    anslist.Add(li.Value);
                                    resp.Answer = string.Join(";", anslist);
                                }
                            }
                            if (resp.Answer == string.Empty && QAsManager.CheckMustkey(qaid, qid))
                            {
                                this.ltlMsg.Text += $"<span style='color:red'>題目：{q.QuestionTitle}。項目為必填</span>";
                                return;
                            }
                        }
                    }
                    resplist.Add(resp);
                }

                HttpContext.Current.Session["QA_respodent"] = info;
                HttpContext.Current.Session["QA_respodent_answer"] = resplist;

                Response.Redirect("QAConfirmPage.aspx?ID=" + qaidtxt);
            }
        }
        protected void return_btn_Click(object sender, EventArgs e)
        {
            this.Session.Clear();
            Response.Redirect("Index.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msglist = new List<string>();

            if (string.IsNullOrWhiteSpace(this.tbx_name.Text) || string.IsNullOrEmpty(this.tbx_name.Text))
                msglist.Add("<span style='color:red'>姓名為必填</span>");

            else if (string.IsNullOrWhiteSpace(this.tbx_age.Text) || string.IsNullOrEmpty(this.tbx_age.Text))
                msglist.Add("<span style='color:red'>年齡為必填</span>");
            else if ((int.Parse(this.tbx_age.Text)) > 100)
                msglist.Add("<span style='color:red'>年齡需少於100</span>");

            else if (string.IsNullOrWhiteSpace(this.tbx_phone.Text) || string.IsNullOrEmpty(this.tbx_phone.Text))
                msglist.Add("<span style='color:red'>電話為必填</span>");
            else if (this.tbx_phone.Text.Length != 10)
                msglist.Add("<span style='color:red'>電話長度應為10碼</span>");

            else if (string.IsNullOrWhiteSpace(this.tbx_email.Text) || string.IsNullOrEmpty(this.tbx_email.Text))
                msglist.Add("<span style='color:red'>信箱為必填</span>");

            errorMsgList = msglist;

            if (msglist.Count == 0)
                return true;
            else
                return false;
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