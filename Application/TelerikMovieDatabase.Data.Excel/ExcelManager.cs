namespace TelerikMovieDatabase.Data.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TelerikMovieDatabase.Data.MsSql;
    using TelerikMovieDatabase.Models;

    public class ExcelManager
    {
        public static void InsertInExcelFile(int reportId, string title, int profits)
        {
            OleDbConnection excelConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\..\\..\\Databases\\SQLite\\MovieBudgetReportList.xlsx;Extended Properties=Excel 12.0;");
            excelConnection.Open();

            using (excelConnection)
            {
                OleDbCommand insertCommand = new OleDbCommand("INSERT INTO [Sheet1$] (ReportID, Title, Profits) VALUES (@reportId, @title, @profits)", excelConnection);
                insertCommand.Parameters.AddWithValue("@ReportID", reportId);
                insertCommand.Parameters.AddWithValue("@Title", title);
                insertCommand.Parameters.AddWithValue("@Profits", profits);

                insertCommand.ExecuteNonQuery();
            }
        }

        public static void ImportInSqlDb(TelerikMovieDatabaseMsSqlContext db, string reportsZipPath)
        {
            if (!Directory.Exists(reportsZipPath))
            {
                Directory.CreateDirectory(reportsZipPath);
            }

            List<string> files = new List<string>();
            var directories = Directory.GetDirectories(reportsZipPath);

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
                        var movies = from movie in db.Movies select movie.Title;
                        Dictionary<string, string[]> reports = new Dictionary<string, string[]>();

                        using (reader)
                        {
                            while (reader.Read())
                            {
                                string weeks = (string)reader["Weeks"];
                                string score = (string)reader["Gross"];
                                string movie = (string)reader["Movie"];

                                reports.Add(movie, new string[] { weeks, score });
                            }

                           // foreach (var movie in movies)
                           // {
                           //     db.BoxOfficeEntries.Add(
                           //         new BoxOfficeEntry
                           //         {
                           //             Weeks = int.Parse(reports[movie][0]),
                           //             GeneratedWeekendIncome = decimal.Parse(reports[movie][1])
                           //         });
                           // }
                           // db.SaveChanges();

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
