namespace TMDB.DatabaseDataGet
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;

    class SQLiteManager
    {
        private IList<SqLiteReturnObject> GetDataFromSqLiteDatabase()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source = ..\\..\\..\\..\\Databases\\SQLite\\MovieBudget.db");
            sqliteConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM MovieBudgetReports", sqliteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            var data = new List<SqLiteReturnObject>();
            using (reader)
            {
                while (reader.Read())
                {
                    int reportId = Convert.ToInt32(reader["ReportID"]);
                    string title = (string)reader["Title"];
                    int budget = Convert.ToInt32(reader["Budget"]);
                    SqLiteReturnObject report = new SqLiteReturnObject(reportId, title, budget);
                    data.Add(report);
                }
            }
            
            return data;
        }
    }
}