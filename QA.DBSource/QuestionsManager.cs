using QA.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.DBSource
{
    public class QuestionsManager
    {
        public static QuestionsTable GetQuestionDetail(int qid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QuestionsTable
                                 where item.QuestionID == qid
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
        public static void CreateQuestions(QuestionsTable questions)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.QuestionsTable.Add(questions);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static void InsertQuestions(QA_Question design)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.QA_Question.Add(design);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static void InsertCommonQuestions(QA_Question common)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.QA_Question.Add(common);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static void CreateChoices(ChoiceTable choices)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.ChoiceTable.Add(choices);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static bool UpdateQuestion(int qid, QuestionsTable questions)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = context.QuestionsTable.Where(o => o.QuestionID == qid).FirstOrDefault();

                    if(obj != null)
                    {

                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        public static bool UpdateCommonQuestion(int qid, bool onoff)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = context.QuestionsTable.Where(o => o.QuestionID == qid).FirstOrDefault();

                        obj.CommonQuestion = onoff;

                        context.SaveChanges();
                        return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        public static List<QuestionsTable> GetQuestionsList()
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QuestionsTable
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
        public static List<QA_Question_View> GetQuestionsListbyQAID(int qaid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QA_Question_View
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
        public static List<QuestionsTable> GetQuestionsListbyQID(int qid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QuestionsTable
                                 where item.QuestionID == qid
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
        public static ChoiceTable GetChoiceList(int cid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.ChoiceTable
                                 where item.ChoiceID == cid
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
        public static QuestionsTable GetFinalQID()
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QuestionsTable
                                 orderby item.QuestionID descending
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
    }
}
