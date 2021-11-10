using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.DBSource
{
    public class QAsManager
    {
        public static List<QAInfo> GetQAList()
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QAInfo
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
        public static List<QAInfo> GetQAListbyKeyword(string keyword)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QAInfo
                                 where item.Title.Contains(keyword)
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
        public static List<QAInfo> GetQAsByDate(DateTime start_t, DateTime end_t)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    DateTime newend_d = end_t.AddDays(1);

                    var query = (from item in context.QAInfo
                                 where item.EndDate <= newend_d && item.StartDate >= start_t
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
        public static QAInfo GetQADetail(int qaid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QAInfo
                                 where item.QAID == qaid
                                 select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                    return null;
                }
            }
        }
        public static void CreateQAInfo(QAInfo QAInfo)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.QAInfo.Add(QAInfo);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static List<QA_Question> GetQAForm(int qaid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QA_Question
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
        public static bool CheckMustkey(int qaid, int qid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.QA_Question
                         where item.QAID == qaid && item.QuestionID == qid
                         select item.MustKey);

                    var obj = query.FirstOrDefault();
                    if (obj == true)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        public static void DeleteQA(int qaid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = context.QAInfo.Where(o => o.QAID == qaid).FirstOrDefault();
                    var obj2 = context.QA_Question.Where(o => o.QAID == qaid);

                    foreach (var item in obj2)
                    {
                        context.QA_Question.Remove(item);
                    }

                    if (obj != null)
                    {
                        context.QAInfo.Remove(obj);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static void DeleteQuestionFromQA(int qaid, int qid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = context.QA_Question.Where(o => o.QAID == qaid && o.QuestionID == qid).FirstOrDefault();

                    if (obj != null)
                    {
                        context.QA_Question.Remove(obj);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static bool CheckQuestionInQA(int qaid, int qid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.QA_Question
                         where item.QAID == qaid && item.QuestionID == qid
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
    }
}
