namespace TelerikMovieDatabase.ConsoleClient
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using TelerikMovieDatabase.Data.Excel;
	using TelerikMovieDatabase.Data.Imdb;
	using TelerikMovieDatabase.Data.MongoDb;
	using TelerikMovieDatabase.Data.MsSql;
	using TelerikMovieDatabase.Data.MsSql.Repositories;
	using TelerikMovieDatabase.Models;
	using TelerikMovieDatabase.Utils;

	internal class EntryPoint
	{
        // Tasks:
        // C#
        // Prelimirary data
        // ZIP File Excel 2003 => SQL Server
        // MongoDB => SQL Server
        //
        // XML file => SQL Server and MongoDB
        // SQLite + MySQL => Excel 2007 (.xlsx)
        // SQL Server => JSON => MySQL
        //
        // Reports
        // SQL Server => XML
        // SQL Server => PDF
        private static void Main()
        {
            // Prepare Initial Data
            // Step 1
            InitializeMongoDb();
            // Step 2
            //Zip xls file to archive, extracting and import to sqlDB
            using (var dbContext = new TelerikMovieDatabaseMsSqlContext())
            {
                ZipManager.AddFileToZipArchive("..\\..\\..\\..\\Databases\\XLS\\XLSData\\BoxOffice-week1-September.xls", File.GetCreationTime("..\\..\\..\\..\\Databases\\XLS\\XLSData\\BoxOffice-week1-September.xls").ToString("dd-MMM-yyyy"), "..\\..\\..\\..\\Databases\\XLS\\Reports.zip");
                ZipManager.ExtractFiles("..\\..\\..\\..\\Databases\\XLS\\Reports.zip", "..\\..\\..\\..\\Databases\\XLS\\Reports\\");
                ExcelManager.ImportInSqlDb(dbContext, "..\\..\\..\\..\\Databases\\XLS\\Reports\\"); //If you want to add in database first uncoment the code inside this method
            }
            // Step 3
            //Create SqLite Database and fill data ?
            // Step 4
            //Create MySql Database and fill data ?

            // Step 5
            // Migrate Data From MongoDb To MsSql
            MigrateDataFromMongoDbToMsSql();

            // Export all data to xml
            using (var data = new TelerikMovieDatabaseMsSqlData())
            {
                XmlReport.ExportToXml<IGenericRepository<Person>, Person>(data.Persons, "Persons.xml", p => p.Jobs);
                XmlReport.ExportToXml<IGenericRepository<Language>, Language>(data.Languages, "Languages.xml");
                XmlReport.ExportToXml<IGenericRepository<Country>, Country>(data.Countries, "Countries.xml");
                XmlReport.ExportToXml<IGenericRepository<JobPosition>, JobPosition>(data.JobPositions, "JobPositions.xml");
                XmlReport.ExportToXml<IGenericRepository<Genre>, Genre>(data.Genres, "Genres.xml");

                // Custom report ( All movies after 2000 year exported with direcotr and actors )
                XmlReport.ExportToXml<IGenericRepository<Movie>, Movie>(
                    data.Movies,
                    "MoviesAfter2000.xml",
                    movie => movie.ReleaseDate.HasValue && movie.ReleaseDate.Value.Year > 2000,
                    movie => movie.Director,
                    movie => movie.Cast);
            }

            // Import all data from xml
            var persons = XmlReport.ImportFromXml<Person[]>("Persons.xml");
            var languages = XmlReport.ImportFromXml<Language[]>("Languages.xml");
            var countries = XmlReport.ImportFromXml<Country[]>("Countries.xml");
            var jobPositions = XmlReport.ImportFromXml<JobPosition[]>("JobPositions.xml");
            var genres = XmlReport.ImportFromXml<Genre[]>("Genres.xml");
            var moviesAfter2000 = XmlReport.ImportFromXml<Movie[]>("MoviesAfter2000.xml");
        }

        private static void InitializeMongoDb()
        {
            IEnumerable<Movie> movies;
            using (var dbContext = new TelerikMovieDatabaseMsSqlContext())
            {
                movies = OpenMovieDatabase.GetTop250()
                    .Select(movieJson => movieJson.GetMovieModel(dbContext));
            }

            new MongoDbInitializer().Init(movies);
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