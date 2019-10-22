using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob.Utility
{
   public class LogWriter
    {
        private string exePath = string.Empty;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
           // ErrorLogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
            exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }
       
        public void ErrorLogWrite(string logMessage)
        {
            exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(exePath + "\\" + "log.txt"))
                {
                    ErrorLog(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
        public void ErrorLog(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\n Error Log Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }


    }
}
