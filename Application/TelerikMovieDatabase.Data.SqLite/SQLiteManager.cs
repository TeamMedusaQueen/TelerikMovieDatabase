namespace TelerikMovieDatabase.Data.SqLite
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
	using System.Linq;
	using TelerikMovieDatabase.Data.SqLite.Models;

    public class SqLiteManager
    {
        private IList<MovieBudget> GetDataFromSqLiteDatabase()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source = ..\\..\\..\\..\\Databases\\SQLite\\MovieBudget.db");
            sqliteConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM MovieBudgetReports", sqliteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            var data = new List<MovieBudget>();
            using (reader)
            {
                while (reader.Read())
                {
                    int reportId = Convert.ToInt32(reader["ReportID"]);
                    string title = (string)reader["Title"];
                    int budget = Convert.ToInt32(reader["Budget"]);
                    MovieBudget report = new MovieBudget(reportId, title, budget);
                    data.Add(report);
                }
            }
            
            return data;
        }
    }
}