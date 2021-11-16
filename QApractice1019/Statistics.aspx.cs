using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace QApractice1019
{
    public partial class Statistics : System.Web.UI.Page
    {
        public class ChartData
        {
            public string Choice { get; set; }
            public int Cnt { get; set; }
        }

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

                        DataTable chartdt = new DataTable();
                        chartdt.Columns.Add(new DataColumn("Choicetxt", typeof(string)));
                        chartdt.Columns.Add(new DataColumn("Cnt", typeof(int)));

                        foreach (string li in list)
                        {
                            Literal choice = new Literal();
                            choice.Text = li + "：";

                            var dt = AnswerManager.CountRBanswer(qaid, qid, li);
                            Literal cnt = new Literal();
                            cnt.Text = $"({dt.Count().ToString()})" + "</br>";

                            pnl_question.Controls.Add(choice);
                            pnl_question.Controls.Add(cnt);
                            chartdt.Rows.Add(li, dt.Count());
                        }
                        Chart chart = DrawPieChart(chartdt, title.Text);

                        this.ph_question.Controls.Add(pnl_question);
                        this.ph_question.Controls.Add(chart);
                        this.ph_question.Controls.Add(linebreak);
                    }
                    else if (question.QuestionType.ToString() == "CB")
                    {
                        Literal title = new Literal();
                        title.Text = question.QuestionTitle + "</br>";
                        pnl_question.Controls.Add(title);

                        List<string> list = Getchoicelist(question);

                        DataTable chartdt = new DataTable();
                        chartdt.Columns.Add(new DataColumn("Choicetxt", typeof(string)));
                        chartdt.Columns.Add(new DataColumn("Cnt", typeof(int)));

                        foreach (string li in list)
                        {
                            Literal choice = new Literal();
                            choice.Text = li + "：";

                            var dt = AnswerManager.CountCBanswer(qaid, qid, li);
                            Literal cnt = new Literal();
                            cnt.Text = $"({dt.Count().ToString()})</br>";

                            pnl_question.Controls.Add(choice);
                            pnl_question.Controls.Add(cnt);
                            chartdt.Rows.Add(li, dt.Count());
                        }
                        Chart chart = DrawPieChart(chartdt, title.Text);

                        this.ph_question.Controls.Add(pnl_question);
                        this.ph_question.Controls.Add(chart);
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
                Response.Redirect("Index.aspx");
            }
        }
        protected void return_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
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
        private Chart DrawPieChart(DataTable dt, string title)
        {
            Chart chart = new Chart();
            chart.DataSource = dt;

            chart.Series.Add(new Series("SeriesName"));
            chart.Series["SeriesName"].ChartType = SeriesChartType.Pie;
            chart.Series["SeriesName"].XValueMember = "Choicetxt";
            chart.Series["SeriesName"].YValueMembers = "Cnt";
            chart.Series["SeriesName"].Label = "#PERCENT{P2}";
            chart.Series["SeriesName"].LegendText = "#VALX";
            chart.Series["SeriesName"].LabelForeColor = Color.White;

            chart.Legends.Add(new Legend("LegendName"));
            chart.Legends["LegendName"].LegendStyle = LegendStyle.Column;
            chart.Legends["LegendName"].Docking = Docking.Right;
            chart.Legends["LegendName"].Alignment = System.Drawing.StringAlignment.Center;

            chart.ChartAreas.Add(new ChartArea("ChartName"));
            chart.ChartAreas["ChartName"].AxisX.Title = title;

            chart.DataBind();
            return chart;
        }
    }
}