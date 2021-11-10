using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QApractice1019.SystemAdmin
{
    public partial class QAFormDesign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.Request.QueryString["ID"] == null)
                {
                    Response.Redirect("QAList.aspx");
                }
                else
                {
                    string qaidtxt = this.Request.QueryString["ID"].ToString();
                    int qaid = int.Parse(qaidtxt);
                    var list = QuestionsManager.GetQuestionsListbyQAID(qaid);

                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[4]
                    { new DataColumn("QuestionID"), new DataColumn("QuestionTitle"), new DataColumn("QuestionType"), new DataColumn("MustKey")});

                    foreach (var row in list)
                    {
                        int dt_qid = row.QuestionID;
                        string dt_title = row.QuestionTitle;
                        string dt_type = row.QuestionType;
                        bool dt_must = row.MustKey;

                        dt.Rows.Add(dt_qid, dt_title, dt_type, dt_must);
                    }

                    this.gv_QuestionList.DataSource = dt;
                    this.gv_QuestionList.DataBind();
                    HttpContext.Current.Session["Tempdt"] = dt;
                }
            }
        }
        protected void cancel_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("QAList.aspx");
        }
        protected void add_btn_Click(object sender, EventArgs e)
        {
            string qaidtxt = this.Request.QueryString["ID"].ToString();
            int qaid = int.Parse(qaidtxt);

            if (this.ddl_question.SelectedValue == "1")
            {
                QuestionsTable questions = new QuestionsTable();
                List<QA_Question> commonlist = new List<QA_Question>();
                QA_Question common = new QA_Question();

                common.QAID = qaid;

                string qidtxt = this.ddl_common.SelectedValue.ToString();
                int qid = int.Parse(qidtxt);
                common.QuestionID = qid;

                var QuestionInfo = QuestionsManager.GetQuestionDetail(qid);

                questions.QuestionTitle = this.title_tbx.Text;
                questions.QuestionType = this.ddl_type.SelectedValue.ToString();

                if (questions.QuestionType != "TB")
                {
                    int cid = int.Parse(QuestionInfo.ChoiceID.ToString());
                    questions.ChoiceID = cid;
                }
                if (this.must_tbx.Checked)
                {
                    common.MustKey = true;
                }
                else
                {
                    common.MustKey = false;
                }
                commonlist.Add(common);

                HttpContext.Current.Session["TempCommonQ"] = commonlist;
                //QuestionsManager.InsertCommonQuestions(common);

                DataTable dt = (DataTable)HttpContext.Current.Session["Tempdt"];
                dt.Rows.Add(qid, questions.QuestionTitle, questions.QuestionType, common.MustKey);
                this.gv_QuestionList.DataSource = dt;
                this.gv_QuestionList.DataBind();
            }
            else
            {
                List<QuestionsTable> newQlist = new List<QuestionsTable>();
                QuestionsTable questions = new QuestionsTable();
                List<QA_Question> QAq = new List<QA_Question>();
                QA_Question newq = new QA_Question();

                var obj = QuestionsManager.GetFinalQID();
                int lastqid = int.Parse(obj.QuestionID.ToString());
                int qid = lastqid + 1;

                string title = this.title_tbx.Text;
                string type = this.ddl_type.SelectedValue.ToString();

                questions.QuestionTitle = title;
                questions.QuestionType = type;

                if (type != "TB")
                {
                    int choiceid = new int();
                    string choicetxt = this.choice_txb.Text;
                    char sperator = char.Parse(";");
                    string[] choicearray = choicetxt.Split(sperator);
                    int choice_cnt = choicearray.Count();

                    if (choice_cnt > 6)
                    {
                        this.ltl_errorMsg.Text = "選項最多6個項目";
                        return;
                    }

                    List<ChoiceTable> clist = new List<ChoiceTable>();
                    ChoiceTable choicelist = new ChoiceTable();
                    choicelist.ChoiceCount = choice_cnt;
                    questions.ChoiceID = choiceid;

                    if (choice_cnt == 1)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                    }
                    else if (choice_cnt == 2)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                    }
                    else if (choice_cnt == 3)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                        choicelist.ThirdChoice = choicearray[2].ToString();
                    }
                    else if (choice_cnt == 4)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                        choicelist.ThirdChoice = choicearray[2].ToString();
                        choicelist.ForthChoice = choicearray[3].ToString();
                    }
                    else if (choice_cnt == 5)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                        choicelist.ThirdChoice = choicearray[2].ToString();
                        choicelist.ForthChoice = choicearray[3].ToString();
                        choicelist.FifthChoice = choicearray[4].ToString();
                    }
                    else if (choice_cnt == 6)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                        choicelist.ThirdChoice = choicearray[2].ToString();
                        choicelist.ForthChoice = choicearray[3].ToString();
                        choicelist.FifthChoice = choicearray[4].ToString();
                        choicelist.SixthChoice = choicearray[5].ToString();
                    }

                    clist.Add(choicelist);
                    HttpContext.Current.Session["TempChoice"] = clist;
                }
                else
                {
                    questions.ChoiceID = null;
                }

                newQlist.Add(questions);
                HttpContext.Current.Session["TempNewQ"] = newQlist;

                newq.QAID = qaid;
                newq.QuestionID = qid;
                if (this.must_tbx.Checked)
                {
                    newq.MustKey = true;
                }
                else
                {
                    newq.MustKey = false;
                }
                QAq.Add(newq);

                HttpContext.Current.Session["TempAddQ"] = QAq;

                DataTable dt = (DataTable)HttpContext.Current.Session["Tempdt"];
                dt.Rows.Add(qid, title, type, newq.MustKey);
                this.gv_QuestionList.DataSource = dt;
                this.gv_QuestionList.DataBind();
                HttpContext.Current.Session["Tempdt"] = dt;
            }

            this.ddl_question.SelectedValue = "0";
            this.ddl_common.Visible = false;
            this.title_tbx.Text = string.Empty;
            this.ddl_type.SelectedValue = "0";
            this.choice_txb.Text = string.Empty;
        }
        protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddl_type.SelectedValue != "TB")
            {
                this.choice_txb.Enabled = true;
            }
            else
                this.choice_txb.Enabled = false;
        }
        protected void ddl_question_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddl_question.SelectedValue == "1")
            {
                this.ddl_common.Visible = true;
                this.title_tbx.Text = string.Empty;
            }
            else
            {
                this.ddl_common.Visible = false;
                this.title_tbx.Text = string.Empty;
                this.title_tbx.Enabled = true;
                this.ddl_type.Enabled = true;
                this.choice_txb.Text = string.Empty;
                this.ddl_type.SelectedValue = "0";
            }
        }
        protected void ddl_common_SelectedIndexChanged(object sender, EventArgs e)
        {
            string qidtxt = this.ddl_common.SelectedValue.ToString();
            int qid = int.Parse(qidtxt);

            var QuestionInfo = QuestionsManager.GetQuestionDetail(qid);

            if (QuestionInfo.QuestionType != "TB")
            {
                int cid = int.Parse(QuestionInfo.ChoiceID.ToString());
                var item = QuestionsManager.GetChoiceList(cid);

                string choices = string.Join(";", item.FirstChoice, item.SecondChoice, item.ThirdChoice, item.ForthChoice, item.FifthChoice, item.SixthChoice);
                this.choice_txb.Text = choices;
                this.choice_txb.Enabled = false;
            }
            else
            {
                this.choice_txb.Text = string.Empty;
            }
            this.title_tbx.Text = QuestionInfo.QuestionTitle;
            this.title_tbx.Enabled = false;

            this.ddl_type.SelectedValue = QuestionInfo.QuestionType;
            this.ddl_type.Enabled = false;
        }
        protected void save_btn_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["TempCommonQ"] == null && HttpContext.Current.Session["TempAddQ"] == null)
            {
                Response.Redirect("QAList.aspx");
            }
            else
            {
                if (HttpContext.Current.Session["TempCommonQ"] != null && HttpContext.Current.Session["TempAddQ"] == null)
                {
                    List<QA_Question> commonlist = (List<QA_Question>)HttpContext.Current.Session["TempCommonQ"];

                    foreach (QA_Question common in commonlist)
                    {
                        QuestionsManager.InsertCommonQuestions(common);
                    }
                }
                else if (HttpContext.Current.Session["TempCommonQ"] == null && HttpContext.Current.Session["TempAddQ"] != null)
                {
                    if (HttpContext.Current.Session["TempChoice"] != null)
                    {
                        List<ChoiceTable> clist = (List<ChoiceTable>)HttpContext.Current.Session["TempChoice"];
                        foreach (ChoiceTable ct in clist)
                        {
                            QuestionsManager.CreateChoices(ct);
                        }
                    }

                    List<QuestionsTable> newQlist = (List<QuestionsTable>)HttpContext.Current.Session["TempNewQ"];
                    foreach (QuestionsTable newQ in newQlist)
                    {
                        QuestionsManager.CreateQuestions(newQ);
                    }

                    List<QA_Question> QA_qlist = (List<QA_Question>)HttpContext.Current.Session["TempAddQ"];
                    foreach (QA_Question newQ in QA_qlist)
                    {
                        QuestionsManager.InsertQuestions(newQ);
                    }
                }
                else
                {
                    List<QA_Question> commonlist = (List<QA_Question>)HttpContext.Current.Session["TempCommonQ"];
                    foreach (QA_Question common in commonlist)
                    {
                        QuestionsManager.InsertCommonQuestions(common);
                    }

                    if (HttpContext.Current.Session["TempChoice"] != null)
                    {
                        List<ChoiceTable> clist = (List<ChoiceTable>)HttpContext.Current.Session["TempChoice"];
                        foreach (ChoiceTable ct in clist)
                        {
                            QuestionsManager.CreateChoices(ct);
                        }
                    }

                    List<QuestionsTable> newQlist = (List<QuestionsTable>)HttpContext.Current.Session["TempNewQ"];
                    foreach (QuestionsTable newQ in newQlist)
                    {
                        QuestionsManager.CreateQuestions(newQ);
                    }

                    List<QA_Question> QA_qlist = (List<QA_Question>)HttpContext.Current.Session["TempAddQ"];
                    foreach (QA_Question newQ in QA_qlist)
                    {
                        QuestionsManager.InsertQuestions(newQ);
                    }
                }
            }
            Response.Redirect("QAList.aspx");
        }
        protected void edit_btn_Click(object sender, EventArgs e)
        {
            QuestionsTable qt = (QuestionsTable)HttpContext.Current.Session["TempQchange"];

            if (qt != null)
            {
                int qid = qt.QuestionID;
                qt.QuestionTitle = this.title_tbx.Text;
                qt.QuestionType = this.ddl_type.SelectedValue.ToString();

                if (qt.QuestionType != "TB")
                {
                    ChoiceTable choicelist = new ChoiceTable();

                    int cid = int.Parse(qt.ChoiceID.ToString());
                    string choicetxt = this.choice_txb.Text;
                    char sperator = char.Parse(";");
                    string[] choicearray = choicetxt.Split(sperator);
                    int choice_cnt = choicearray.Count();

                    if (choice_cnt == 1)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                    }
                    else if (choice_cnt == 2)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                    }
                    else if (choice_cnt == 3)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                        choicelist.ThirdChoice = choicearray[2].ToString();
                    }
                    else if (choice_cnt == 4)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                        choicelist.ThirdChoice = choicearray[2].ToString();
                        choicelist.ForthChoice = choicearray[3].ToString();
                    }
                    else if (choice_cnt == 5)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                        choicelist.ThirdChoice = choicearray[2].ToString();
                        choicelist.ForthChoice = choicearray[3].ToString();
                        choicelist.FifthChoice = choicearray[4].ToString();
                    }
                    else if (choice_cnt == 6)
                    {
                        choicelist.FirstChoice = choicearray[0].ToString();
                        choicelist.SecondChoice = choicearray[1].ToString();
                        choicelist.ThirdChoice = choicearray[2].ToString();
                        choicelist.ForthChoice = choicearray[3].ToString();
                        choicelist.FifthChoice = choicearray[4].ToString();
                        choicelist.SixthChoice = choicearray[5].ToString();
                    }
                    choicelist.ChoiceCount = choice_cnt;

                    QuestionsManager.UpdateChoices(cid, choicelist);
                }
                else
                {
                    qt.ChoiceID = null;
                }

                QuestionsManager.UpdateQuestion(qid, qt);
            }
            else
            {
                return;
            }

            this.ddl_question.SelectedValue = "0";
            this.ddl_common.Visible = false;
            this.title_tbx.Text = string.Empty;
            this.ddl_type.SelectedValue = "0";
            this.choice_txb.Text = string.Empty;
            this.edit_btn.Visible = false;
            this.add_btn.Visible = true;
            Response.Redirect(Request.RawUrl);
        }
        protected void gv_QuestionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = (e.Row.FindControl("gvddl_type") as DropDownList);
                int qid = int.Parse(e.Row.Cells[0].Text);

                var list = QuestionsManager.GetQuestionsListbyQID(qid);
                ddl.DataSource = list;
                ddl.DataBind();

                if (ddl.SelectedValue == "TB")
                {
                    ddl.SelectedItem.Text = "文字方塊";
                }
                else if (ddl.SelectedValue == "RB")
                {
                    ddl.SelectedItem.Text = "單選方塊";
                }
                else if (ddl.SelectedValue == "CB")
                {
                    ddl.SelectedItem.Text = "複選方塊";
                }
            }
        }
        protected void gv_QuestionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Q_edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gv_QuestionList.Rows[rowIndex];

                int qid = int.Parse(row.Cells[0].Text);
                var detail = QuestionsManager.GetQuestionDetail(qid);

                if (detail != null)
                {
                    this.title_tbx.Text = detail.QuestionTitle;
                    this.ddl_type.SelectedValue = detail.QuestionType.ToString();
                    this.add_btn.Visible = false;
                    this.edit_btn.Visible = true;

                    if (detail.QuestionType != "TB")
                    {
                        int cid = int.Parse(detail.ChoiceID.ToString());
                        var item = QuestionsManager.GetChoiceList(cid);

                        string choices = string.Join(";", item.FirstChoice, item.SecondChoice, item.ThirdChoice, item.ForthChoice, item.FifthChoice, item.SixthChoice);
                        this.choice_txb.Text = choices;
                        this.choice_txb.Enabled = true;
                    }
                    HttpContext.Current.Session["TempQchange"] = detail;
                }
            }
            if (e.CommandName == "deleteQrow")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gv_QuestionList.Rows[rowIndex];

                string qaidtxt = this.Request.QueryString["ID"].ToString();
                int qaid = int.Parse(qaidtxt);
                int qid = int.Parse(row.Cells[0].Text);

                if (QAsManager.CheckQuestionInQA(qaid, qid))
                {
                    QAsManager.DeleteQuestion(qaid, qid);
                }
                else
                {
                    DataTable dt = (DataTable)HttpContext.Current.Session["Tempdt"];
                    dt.Rows[rowIndex].Delete();
                    HttpContext.Current.Session["Tempdt"] = dt;
                    gv_QuestionList.DataSource = dt;
                    gv_QuestionList.DataBind();
                }
                Response.Redirect(Request.RawUrl);
            }

        }
    }
}