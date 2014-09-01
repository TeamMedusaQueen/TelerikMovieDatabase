namespace TMDB.DatabaseDataGet
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;
    using TMDB.ExcelOperations;
    
    class SQLiteManager
    {
        private static void ManageSQLiteDataBaseExcelConnection()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source = ..\\..\\..\\..\\Databases\\SQLite\\MovieBudget.db");
            sqliteConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM MovieBudgetReports", sqliteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            var data = new List<object>();
            using (reader)
            {
                while (reader.Read())
                {
                    int reportId = Convert.ToInt32(reader["ReportID"]);
                    string title = (string)reader["Title"];
                    int budget = Convert.ToInt32(reader["Budget"]);
                    data.Add(reportId);
                    data.Add(title);
                    data.Add(budget);
                    ExcelManager.InsertInExcelFile(reportId, title, budget);
                }
            }
            sqliteConnection.Close();
            Console.WriteLine("Data inserted in Excel file successfully.");
        }
    }
}