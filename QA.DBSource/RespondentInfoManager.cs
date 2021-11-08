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
        public static bool CheckRespodentByEmail(string emailtxt)
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
        public static bool CheckRespodentByName(string nametxt)
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
        public static bool CheckRepeatAnswer(Guid respondentid, int qaid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.ResponseTable
                         where item.RespodentID == respondentid && item.QAID == qaid
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
        public static void CreateResponse(ResponseTable rt)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.ResponseTable.Add(rt);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static RespondentInfo GetRespodentInfobyID(Guid guid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.RespondentInfo
                         where item.RespondentID == guid
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
        public static List<ResponseTable> GetAllAnswerList(int qaid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.ResponseTable
                                 where item.QAID == qaid
                                 select item);

                    var list = query.ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                    return null;
                }
            }
        }
        public static Respondent_answer GetRespodent_question_answer(Guid respondentid, int qaid, int qid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Respondent_answer
                         where item.RespondentID == respondentid && item.QAID == qaid && item.QuestionID == qid
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
