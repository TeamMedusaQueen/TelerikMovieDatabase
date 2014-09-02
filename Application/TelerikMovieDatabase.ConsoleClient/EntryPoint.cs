namespace TelerikMovieDatabase.ConsoleClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using TelerikMovieDatabase.Data.Imdb;
	using TelerikMovieDatabase.Data.MsSql;

	internal class EntryPoint
	{
		private static void Main()
		{
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
			//Start the tasks 1 by 1
		}

		// Temporary Method
		private void FillMsSqlDatabaseFromOMDB()
		{
			using (var dbContext = new TelerikMovieDatabaseMsSqlContext())
			{
				var movieJsonModels = OpenMovieDatabase.GetTop250();
				foreach (var movieJsonModel in movieJsonModels)
				{
					var movie = movieJsonModel.GetMovieModel(dbContext);
					dbContext.Movies.Add(movie);
				}

				dbContext.SaveChanges();
			}
		}
	}
}