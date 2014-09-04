namespace TelerikMovieDatabase.Data.SqLite
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
	using System.Linq;

    public class SqLiteManager
    {
        private IList<MovieBudgetReport> GetDataFromSqLiteDatabase()
        {
            var db = new MovieBudgetContext();
            var data = db.MovieBudgetReports.ToList();
            return data;
        }

        private static void InitializeSQLiteDb()
        {

            using (var db = new MovieBudgetContext())
            {
                db.MovieBudgetReports.Add(new MovieBudgetReport { Title = "Titanic", Budget = 200000000 });
                db.MovieBudgetReports.Add(new MovieBudgetReport { Title = "The Shawnshank redemption", Budget = 25000000 });
                db.MovieBudgetReports.Add(new MovieBudgetReport { Title = "The Godfather", Budget = 6000000 });
                db.SaveChanges();
            }
        }
    }
}