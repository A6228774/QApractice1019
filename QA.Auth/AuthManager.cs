using QA.DBSource;
using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QA.Auth
{
    public class AuthManager
    {
        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(account) ||
                string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "<span style='color:red'>請輸入帳號/密碼</span>";
                return false;
            }

            var userInfo = UserInfoManager.GetUserInfoByAccount(account);

            if (userInfo == null)
            {
                errorMsg = $"<span style='color:red'>帳號: {account} 輸入錯誤</span>";
                return false;
            }

            if (string.Compare(userInfo.Account, account, true) == 0 &&
                string.Compare(userInfo.Password, pwd, false) == 0)
            {
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "<span style='color:red'>登入失敗，請重新確認帳號/密碼</span>";
                return false;
            }
        }
        public static bool Islogined()
        {
            if (HttpContext.Current.Session["Login"] == null)
                return false;
            else
                return true;
        }

    }
}
