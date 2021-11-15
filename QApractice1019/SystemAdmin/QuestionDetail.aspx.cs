using QA.Auth;
using QA.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QApractice1019.SystemAdmin
{
    public partial class QuestionDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManager.Islogined())
            {
                Response.Redirect("QAList.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (this.Request.QueryString["QID"] == null)
                {
                    Response.Redirect("QuestionsBank.aspx");
                }
                else
                {
                    string qidtxt = this.Request.QueryString["QID"].ToString();
                    int qid = int.Parse(qidtxt);
                    var question = QuestionsManager.GetQuestionDetail(qid);

                    this.ltl_title.Text = question.QuestionTitle;

                    if (question.QuestionType == "TB")
                    {
                        this.ltl_type.Text = "文字方塊";
                    }
                    else if (question.QuestionType == "RB")
                    {
                        this.ltl_type.Text = "單選方塊";
                    }
                    else if (question.QuestionType == "CB")
                    {
                        this.ltl_type.Text = "複選方塊";
                    }

                    if (question.ChoiceID != null)
                    {
                        int cid = int.Parse(question.ChoiceID.ToString());
                        var choice = QuestionsManager.GetChoiceList(cid);

                        this.ltl_choices.Text = string.Join(";", choice.FirstChoice, choice.SecondChoice, choice.ThirdChoice, choice.ForthChoice, choice.FifthChoice, choice.SixthChoice);
                    }
                    else
                    {
                        this.ltl_choices.Text = "-";
                    }

                    if (question.CommonQuestion != null)
                    {
                        this.cbx_common.Checked = question.CommonQuestion.Value;
                    }
                }
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuestionsBank.aspx");
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            string qidtxt = this.Request.QueryString["QID"].ToString();
            int qid = int.Parse(qidtxt);

            bool onoff = cbx_common_CheckedChanged();

            QuestionsManager.UpdateCommonQuestion(qid, onoff);
            Response.Redirect("QuestionsBank.aspx");
        }

        private bool cbx_common_CheckedChanged()
        {
            bool onoff;

            if (this.cbx_common.Checked)
            {
                onoff = true;
            }
            else
            {
                onoff = false;
            }
            return onoff;
        }
    }
}