using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.DBSource
{
    public class RespondentInfoManager
    {
        public static bool GetRespodentByEmail(string emailtxt)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.RespondentInfo
                         where item.Email == emailtxt
                         select item);

                    var obj = query.FirstOrDefault();
                    if (obj == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return true;
            }
        }
        public static bool GetRespodentByName(string nametxt)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.RespondentInfo
                         where item.Name == nametxt
                         select item);

                    var obj = query.FirstOrDefault();
                    if (obj == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return true;
            }
        }
        public static void CreateRespodent(RespondentInfo info)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.RespondentInfo.Add(info);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static RespondentInfo GetRespodentInfo(string name, string email)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.RespondentInfo
                         where item.Name == name && item.Email == email
                         select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
    }
}
