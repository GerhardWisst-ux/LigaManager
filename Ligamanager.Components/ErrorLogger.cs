namespace LigaManagement.Web.Classes
{
    using System;
    using System.IO;

    using System.Text;
    public class ErrorLogger    {
   
        public static void WriteToErrorLog(string msg, string stkTrace, string title)

        {

            string StartupPath = System.IO.Directory.GetCurrentDirectory();

            if (!(System.IO.Directory.Exists(StartupPath + "\\Errors\\")))

            {

                System.IO.Directory.CreateDirectory(StartupPath + "\\Errors\\");

            }

            FileStream fs = new FileStream(StartupPath + "\\Errors\\errlog " + DateTime.Now.Date.ToShortDateString() + ".txt" , FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamWriter s = new StreamWriter(fs);

            s.Close();

            fs.Close();

            FileStream fs1 = new FileStream(StartupPath + "\\Errors\\errlog " + DateTime.Now.Date.ToShortDateString() + ".txt", FileMode.Append, FileAccess.Write);

            StreamWriter s1 = new StreamWriter(fs1);

            s1.Write("Title: " + title + Environment.NewLine);

            s1.Write("Message: " + msg + Environment.NewLine);

            s1.Write("StackTrace: " + stkTrace + Environment.NewLine);

            s1.Write("Date/Time: " + DateTime.Now.ToString() + Environment.NewLine);

            s1.Write("============================================" + Environment.NewLine);

            s1.Close();

            fs1.Close();

        }

    }
}
