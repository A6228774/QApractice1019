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
    public partial class QAFormDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.Request.QueryString["ID"] == null)
                {
                    this.tbx_start.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
                else
                {
                    string qaidtxt = this.Request.QueryString["ID"];
                    int qaid = int.Parse(qaidtxt);

                    var QAInfo = QAsManager.GetQADetail(qaid);

                    this.tbx_title.Text = QAInfo.Title;
                    this.tbx_summary.Text = QAInfo.Summary;
                    this.tbx_start.Text = QAInfo.StartDate.ToString("yyyy-MM-dd");
                    this.tbx_end.Text = QAInfo.EndDate?.ToString("yyyy-MM-dd");
                    this.cbx_enable.Checked = QAInfo.IsEnabled;

                }
            }
        }

        protected void design_btn_Click(object sender, EventArgs e)
        {
            QAInfo temp = new QAInfo();

            temp.QAID = new int();
            temp.Title = this.tbx_title.Text;
            temp.Summary = this.tbx_summary.Text;
            temp.StartDate = DateTime.Parse(this.tbx_start.Text);

            if (this.tbx_end.Text == string.Empty)
            {
                temp.EndDate = null;
            }
            else
            {
                temp.EndDate = DateTime.Parse(this.tbx_end.Text);
            }
            if (this.cbx_enable.Checked)
            {
                temp.IsEnabled = true;
            }
            else
            {
                temp.IsEnabled = false;
            }

            HttpContext.Current.Session["QADetail"] = temp;

            if (this.Request.QueryString["ID"] != null)
            {
                string qaidtxt = this.Request.QueryString["ID"];
                Response.Redirect("QAFormDesign.aspx?ID=" + qaidtxt);
            }
            else
            {
                QAsManager.CreateQAInfo(temp);
                Response.Redirect("QAFormDesign.aspx?ID=" + temp.QAID);

            }
        }

        protected void cancel_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("QAList.aspx");
        }
    }
}