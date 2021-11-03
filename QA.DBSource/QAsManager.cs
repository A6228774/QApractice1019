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
        public static List<CustomizeQA> GetQAList()
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.CustomizeQA
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
        public static List<CustomizeQA> GetQAListbyKeyword(string keyword)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.CustomizeQA
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
        public static List<CustomizeQA> GetOrdersByDate(DateTime start_t, DateTime end_t)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    DateTime newend_d = end_t.AddDays(1);

                    var query = (from item in context.CustomizeQA
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
        public static CustomizeQA GetQADetail(int qaid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.CustomizeQA
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
        public static void CreateCustomizeQA(CustomizeQA QAInfo)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.CustomizeQA.Add(QAInfo);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static void InsertQuestions(QADesign design)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.QADesign.Add(design);
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
        public static List<QuestionsTable> GetQuestionsListbyQAID(int qaid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QuestionsTable
                                 join design in context.QADesign on item.QuestionID equals design.QuestionID
                                 where design.QAID == qaid
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
        public static List<QADesign> GetQAForm(int qaid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.QADesign
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
        public static void DeleteQA(int qaid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = context.CustomizeQA.Where(o => o.QAID == qaid).FirstOrDefault();
                    var obj2 = context.QADesign.Where(o => o.QAID == qaid);

                    foreach (var item in obj2)
                    {
                        context.QADesign.Remove(item);
                    }

                    if (obj != null)
                    {
                        context.CustomizeQA.Remove(obj);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
    }
}
