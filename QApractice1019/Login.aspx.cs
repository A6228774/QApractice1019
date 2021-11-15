using QA.Auth;
using QA.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QApractice1019
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login_btn_Click(object sender, EventArgs e)
        {
            string accouttxt = this.account_tbx.Text;
            string pwtxt = this.pw_tbx.Text;

            string msg;
            if (!AuthManager.TryLogin(accouttxt, pwtxt, out msg))
            {

                this.ltlMsg.Visible = true;
                this.ltlMsg.Text = msg;
                return;
            }
            var userInfo = UserInfoManager.GetUserInfoByAccount(accouttxt);

            if(userInfo != null)
            {
                HttpContext.Current.Session["Login"] = userInfo;
                Response.Redirect("/SystemAdmin/QAList.aspx");
            }
            else
            {
                this.ltlMsg.Text = "<span style='color:red'>沒有該帳號</span>";
                return;
            }

        }
        protected void return_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}