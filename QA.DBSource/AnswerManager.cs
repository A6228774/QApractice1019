using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.DBSource
{
    public class AnswerManager
    {
        public static void CreateRespodent_answer(Respondent_answer ans)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Respondent_answer.Add(ans);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static List<Respondent_answer> CountRBanswer(int qaid, int qid, string ans)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Respondent_answer
                         where item.QAID == qaid && item.QuestionID == qid && item.Answer == ans
                         select item);

                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        public static List<Respondent_answer> CountCBanswer(int qaid, int qid, string ans)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Respondent_answer
                         where item.QAID == qaid && item.QuestionID == qid && item.Answer.Contains(ans)
                         select item);

                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        public static int CountTotalanswerbyQAID(int qaid, int qid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Respondent_answer
                         where item.QAID == qaid && item.QuestionID == qid
                         select item);

                    int cnt = query.Count();
                    return cnt;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return 0;
            }
        }
    }
}
