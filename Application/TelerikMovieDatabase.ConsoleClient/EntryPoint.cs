namespace TelerikMovieDatabase.ConsoleClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TelerikMovieDatabase.Data.MongoDb;
	using TelerikMovieDatabase.Data.MsSql;
    using TelerikMovieDatabase.Data.Xml;

	internal class EntryPoint
	{
		private static void Main()
		{           
            // To test xml report
            // var test = new XmlManager();
            // test.ExportFromMovieToXml();

            // Prepare Initial Data
			// Step 1
			new MongoDbInitializer().Init();
			// Step 2
			//new ExcelZipFileInitializer().Init();
			// Step 3
			//Create SqLite Database and fill data ?
			// Step 4
			//Create MySql Database and fill data ?

			// Step 5
			// Migrate Data From MongoDb To MsSql
			MigrateDataFromMongoDbToMsSql();
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