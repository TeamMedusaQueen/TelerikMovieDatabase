namespace TelerikMovieDatabase.ConsoleClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TelerikMovieDatabase.Common;
	using TelerikMovieDatabase.Data.Imdb;
	using TelerikMovieDatabase.Data.MongoDb;
	using TelerikMovieDatabase.Data.MsSql;
	using TelerikMovieDatabase.Models;

	internal class EntryPoint
	{
		private const string MoviesInitialXmlFileName = "MoviesInitial";
		private const string InitialExcelZipFileName = "BoxOffice-week1-September";

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
			// Step 1 - MongoDB
			//InitializeMongoDbAndXml();
			// Step 2 - ZIP File Excel 2003
			//ExcelZipInitializer.Init(InitialExcelZipFileName);

			// Import Initial Data
			// (Problem #1) - Load Excel Reports from ZIP File (ZIP File Excel 2003 => SQL Server)
			//using (var data = new TelerikMovieDatabaseMsSqlData())
			//{
			//	ExcelManager.ImportBoxOfficeEntriesFromZip(data, InitialExcelZipFileName);
			//}

			// (Problem #1) - MongoDB => SQL Server
			//MigrateDataFromMongoDbToMsSql();

			// (Problem #2) - Generate PDF Report (SQL Server => PDF)
			//PdfManager.ExportPdfReport("MoviesReport");

			// (Problem #3) - Generate XML Report (SQL Server => XML)
			//using (var data = new TelerikMovieDatabaseMsSqlData())
			//{
			//	ManagerProvider<Movie>.Xml.Export(
			//		data.Movies,
			//		"GoodOldMovies",
			//		movie => movie.Metascore.HasValue && movie.Metascore.Value > 70
			//			&& movie.ReleaseDate.HasValue && movie.ReleaseDate.Value.Year < 1970,
			//		movie => movie.Director,
			//		movie => movie.Writers,
			//		movie => movie.Cast);
			//}

			// (Problem #4) - Generate JSON Report (SQL Server => JSON => MySQL)
			using (var data = new TelerikMovieDatabaseMsSqlData())
			{
				var jsonManager = ManagerProvider<Movie>.Json;
				jsonManager.IsMultiple = true;
				jsonManager.Export(
					data.Movies,
					"MovieBoxOfficeReport",
					movie => new Movie()
					{
						ID = movie.ID,
						Title = movie.Title,
						Gross = movie.BoxOfficeEntry.GeneratedWeekendIncome,
					},
					movie => movie.BoxOfficeEntry != null,
					movie => movie.BoxOfficeEntry);
			}
			// TODO: Read the exported json files to MySql

			// (Problem #5) - XML file => SQL Server and MongoDB
			// MigrateDataFromXmlToMongoDbAndMsSql();

			// (Problem #6) - Excel data (SQLite + MySQL => Excel 2007 (.xlsx))
			// TODO:
		}

		private static void InitializeMongoDbAndXml()
		{
			IEnumerable<Movie> movies;
			using (var data = new TelerikMovieDatabaseMsSqlData())
			{
				data.Movies.DisableProxyCreation();
				movies = OpenMovieDatabase.GetTop250()
					.Select(movieJson => movieJson.GetMovieModel(data.Context)).ToArray();
				data.Movies.EnableProxyCreation();
			}

			// Import First 150 to MongoDB
			new MongoDbInitializer().Init(movies.Take(150), forceReCreate: false);
			// Import the rest to the initial xml file
			ManagerProvider<Movie>.Xml.Export(movies.Skip(150).ToArray(), MoviesInitialXmlFileName);
		}

		private static void MigrateDataFromMongoDbToMsSql()
		{
			using (var data = new TelerikMovieDatabaseMsSqlData())
			{
				var movies = new MongoDbMigration().GetMovies();
				foreach (var movie in movies)
				{
					data.Context.Movies.Add(movie);
				}

				data.SaveChanges();
			}
		}

		private static void MigrateDataFromXmlToMongoDbAndMsSql()
		{
			var movies = ManagerProvider<Movie>.Xml.Import(MoviesInitialXmlFileName);

			// Import to MsSql
			using (var data = new TelerikMovieDatabaseMsSqlData())
			{
				foreach (var movie in movies)
				{
					data.Movies.Add(movie);
				}

				data.SaveChanges();
			}

			// Import to MongoDb
			new MongoDbMigration().AddMovies(movies);
		}
	}
}