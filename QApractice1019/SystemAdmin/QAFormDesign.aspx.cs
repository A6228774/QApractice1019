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
    public partial class QAFormDesign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qaidtxt = this.Request.QueryString["ID"].ToString();
                int qaid = int.Parse(qaidtxt);
                var list = QAsManager.GetQuestionsListbyQAID(qaid);

                if (list.Count == 0)
                {
                    this.ltl_NoQuestion.Visible = true;
                    this.ltl_NoQuestion.Text = "問卷尚未有任何問題";
                }
                else
                {
                    this.gv_QuestionList.DataSource = list;
                    this.gv_QuestionList.DataBind();
                }
            }
        }

        protected void cancel_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("QAList.aspx");
        }

        protected void add_btn_Click(object sender, EventArgs e)
        {
            int qid = new int();
            int newQ = int.Parse(this.ddl_question.SelectedValue);
            string type = this.ddl_type.SelectedValue.ToString();
            string choicetxt = this.choice_txb.Text;

            QuestionsTable questions = new QuestionsTable();

            questions.QuestionTitle = this.title_tbx.Text;
            questions.QuestionType = type;

            if (this.must_tbx.Checked)
            {
                questions.MustKey = true;
            }
            else
            {
                questions.MustKey = false;
            }
            if (type != "TB")
            {
                int choiceid = new int();
                char sperator = char.Parse(";");
                string[] choicearray = choicetxt.Split(sperator);
                int choice_cnt = choicearray.Count();

                if (choice_cnt > 6)
                {
                    this.ltl_errorMsg.Text = "選項最多6個項目";
                    return;
                }

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
                QAsManager.CreateChoices(choicelist);
            }
            else
            {
                questions.ChoiceID = null;
            }

            QAsManager.CreateQuestions(questions);

            QADesign design = new QADesign();

            if (this.Request.QueryString["ID"] != null)
            {
                design.QAID = int.Parse(this.Request.QueryString["ID"].ToString());
                design.QuestionID = questions.QuestionID;
            }
            else
            {
                //HttpContext.Current.Session["newQADesign"] = this.gv_QuestionList.
            }

            QAsManager.InsertQuestions(design);
            Response.Redirect(Request.RawUrl);
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
            }
        }

        protected void ddl_common_SelectedIndexChanged(object sender, EventArgs e)
        {
            string qidtxt = this.ddl_common.SelectedValue.ToString();
            int qid = int.Parse(qidtxt);

            var QuestionInfo = QAsManager.GetQuestionDetail(qid);

            this.title_tbx.Text = QuestionInfo.QuestionTitle;
            this.ddl_type.SelectedValue = QuestionInfo.QuestionType;
            this.must_tbx.Checked = QuestionInfo.MustKey;

        }
        protected void save_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("QAList.aspx");
        }
    }
}