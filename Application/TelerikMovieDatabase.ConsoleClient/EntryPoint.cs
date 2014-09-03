namespace TelerikMovieDatabase.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Globalization;
    using System.Linq;

    using TelerikMovieDatabase.Data.MongoDb;
    using TelerikMovieDatabase.Data.MsSql;
    using TelerikMovieDatabase.Data.Xml;
    using TelerikMovieDatabase.Data.Excel;
    using TelerikMovieDatabase.Utils;

    internal class EntryPoint
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            // To test xml report
            // var test = new XmlManager();
            // string tableName = "Movies";
            // List<string> columnNames = new List<string>() { "Title", "RunningTime", "Rating" };
            // test.ExportFromMovieToXml(tableName, columnNames);
           
            //zip xls file to archive, extracting and import to sqlDB
            TelerikMovieDatabaseMsSqlContext db = new TelerikMovieDatabaseMsSqlContext();
            using (db)
            {
                ZipManager.AddFileToZipArchive("..\\..\\..\\..\\Databases\\XLS\\XLSData\\BoxOffice-week1-September.xls", File.GetCreationTime("..\\..\\..\\..\\Databases\\XLS\\XLSData\\BoxOffice-week1-September.xls").ToString("dd-MMM-yyyy"), "..\\..\\..\\..\\Databases\\XLS\\Reports.zip");

                ZipManager.ExtractFiles("..\\..\\..\\..\\Databases\\XLS\\Reports.zip", "..\\..\\..\\..\\Databases\\XLS\\Reports\\");

                ExcelManager.ImportInSqlDb(db, "..\\..\\..\\..\\Databases\\XLS\\Reports\\"); //If you want to add in database first uncoment the code inside this method

            }

            // Prepare Initial Data
            // Step 1
            //new MongoDbInitializer().Init();
            // Step 2
            //new ExcelZipFileInitializer().Init();
            // Step 3
            //Create SqLite Database and fill data ?
            // Step 4
            //Create MySql Database and fill data ?

            // Step 5
            // Migrate Data From MongoDb To MsSql
            //MigrateDataFromMongoDbToMsSql();
        }

        private static void MigrateDataFromMongoDbToMsSql()
        {
            using (var dbContext = new TelerikMovieDatabaseMsSqlContext())
            {
                // TODO: This condition should be removed, when UI is ready the migration process will be invoked from button
                if (!dbContext.Movies.Any())
                {
                    var movies = new MongoDbToMsSqlMigration().GetMovies();
                    foreach (var movie in movies)
                    {
                        dbContext.Movies.Add(movie);
                    }

                    dbContext.SaveChanges();
                }
            }
        }
    }
}