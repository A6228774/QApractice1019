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
    }
}
