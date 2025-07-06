namespace LigaManagement.Web.Classes
{
    using System;
    using System.Globalization;
    using System.IO;

    using System.Text;
    public static class ErrorLogger
    {

        public static void WriteToErrorLog(string msg, string stkTrace, string title)
        {
            return; // Temporarily disable error logging


            //string StartupPath = Directory.GetCurrentDirectory();

            //if (!(Directory.Exists(AppContext.BaseDirectory + "\\Errors\\")))
            //    Directory.CreateDirectory(AppContext.BaseDirectory + "\\Errors\\");

            //var path = Path.Combine(AppContext.BaseDirectory, "Errors", $"errlog {DateTime.Now:MM-dd-yyyy}.txt");
            ////FileStream fs = new FileStream(StartupPath + "\\Errors\\errlog " + DateTime.Now.Date.ToShortDateString() + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //StreamWriter s = new StreamWriter(fs);

            //s.Close();

            //fs.Close();

            //FileStream fs1 = new FileStream(StartupPath + "\\Errors\\errlog " + DateTime.Now.Date.ToShortDateString() + ".txt", FileMode.Append, FileAccess.Write);

            //StreamWriter s1 = new StreamWriter(fs1);

            //s1.Write("Titel: " + title + Environment.NewLine);

            //s1.Write("Nachricht: " + msg + Environment.NewLine);

            //s1.Write("StackTrace: " + stkTrace + Environment.NewLine);

            //s1.Write("Datum/Uhrzeit: " + DateTime.Now.ToString() + Environment.NewLine);

            //s1.Write("============================================" + Environment.NewLine);

            //s1.Close();

            //fs1.Close();

        }

    }
}
