namespace LigaManagement.Web.Classes
{
    using System;
    using System.Globalization;
    using System.IO;

    using System.Text;
    public class ErrorLogger
    {

        public static void WriteToErrorLog(string msg, string stkTrace, string title)
        {
            string StartupPath = Directory.GetCurrentDirectory();

            if (!(Directory.Exists(StartupPath + "\\Errors\\")))
                Directory.CreateDirectory(StartupPath + "\\Errors\\");
            
            FileStream fs = new FileStream(StartupPath + "\\Errors\\errlog " + DateTime.Now.Date.ToShortDateString() + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamWriter s = new StreamWriter(fs);

            s.Close();

            fs.Close();

            FileStream fs1 = new FileStream(StartupPath + "\\Errors\\errlog " + DateTime.Now.Date.ToShortDateString() + ".txt", FileMode.Append, FileAccess.Write);

            StreamWriter s1 = new StreamWriter(fs1);

            s1.Write("Titel: " + title + Environment.NewLine);

            s1.Write("Nachricht: " + msg + Environment.NewLine);

            s1.Write("StackTrace: " + stkTrace + Environment.NewLine);

            s1.Write("Datum/Uhrzeit: " + DateTime.Now.ToString() + Environment.NewLine);

            s1.Write("============================================" + Environment.NewLine);

            s1.Close();

            fs1.Close();

        }

    }
}
