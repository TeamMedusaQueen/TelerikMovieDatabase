namespace TMDB.ExcelOperations
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;

    using TMDB.Data;
    using TMDB.Models;

    public class ExcelManager
    {
        public static void InsertInExcelFile(int reportId, string title, int budget)
        {
            OleDbConnection excelConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\..\\..\\Databases\\SQLite\\MovieBudgetReportList.xlsx;Extended Properties=Excel 12.0;");
            excelConnection.Open();

            using (excelConnection)
            {
                OleDbCommand insertCommand = new OleDbCommand("INSERT INTO [Sheet1$] (ReportID, Title, Budget) VALUES (@reportId, @title, @budget)", excelConnection);
                insertCommand.Parameters.AddWithValue("@ReportID", reportId);
                insertCommand.Parameters.AddWithValue("@Title", title);
                insertCommand.Parameters.AddWithValue("@Budget", budget);

                insertCommand.ExecuteNonQuery();
            }
        }

        public static void InsertInSqlDB(TmdbContext db)
        {
            const string ReportsPath = "..\\..\\..\\XLSFilesWorking\\XLSData";
            const string directoryPathForArchive = "..\\..\\..\\XLSFilesWorking\\Reports";
            const string zipFilePath = "..\\..\\..\\XLSFilesWorking";
            const string zipFileName = "ReportsArchive.zip";

            if (!Directory.Exists(ReportsPath))
            {
                Directory.CreateDirectory(ReportsPath);
            }

            List<string> files = new List<string>();
            var directories = Directory.GetDirectories(ReportsPath);

            foreach (var folder in directories)
            {
                files.AddRange(Directory.GetFiles(folder));
            }

            foreach (var file in files)
            {
                OleDbConnection xlsConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");

                xlsConn.Open();

                using (db)
                {
                    using (xlsConn)
                    {
                        OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", xlsConn);
                        OleDbDataReader reader = command.ExecuteReader();

                        using (reader)
                        {
                            while (reader.Read())
                            {
                                double weeks = (double)reader["Title"];
                                string score = (string)reader["Budget"];
                                Console.WriteLine("{0} - {1}", weeks, score);

                                db.BoxOfficeEntries.Add(
                                    new BoxOfficeEntry
                                    {
                                        Weeks = (int)weeks,
                                        GeneratedWeekendIncome = decimal.Parse(score)
                                    });

                                db.SaveChanges();
                            }

                            foreach (var item in db.BoxOfficeEntries)
                            {
                                Console.WriteLine(item.ID + " " + item.Weeks + " - > " + item.GeneratedWeekendIncome);
                            }
                        }
                    }
                }
            }
        }
    }
}
