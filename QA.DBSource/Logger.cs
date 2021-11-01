using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.DBSource
{
    class Logger
    {
        public static void WriteLog(Exception ex)
        {
            string msg =
                $@" {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
                    {ex.ToString()}
                ";

            throw ex;
        }
    }
}
