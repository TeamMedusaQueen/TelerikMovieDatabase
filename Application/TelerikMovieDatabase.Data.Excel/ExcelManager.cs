namespace TelerikMovieDatabase.Data.Excel
{
	using System;
	using System.Collections.Generic;
	using System.Data.OleDb;
	using System.IO;
	using System.Linq;

	using TelerikMovieDatabase.Data.MsSql;
	using TelerikMovieDatabase.Models;
	using TelerikMovieDatabase.Utils;

	public class ExcelManager
	{
		public static void ImportBoxOfficeEntriesFromZip(TelerikMovieDatabaseMsSqlData data, string fileName)
		{
			const string SubFolderName = "Reports";
			const string ZipExtension = ".zip";
			var zipFilePath = Path.Combine(Settings.Default.FolderPath, fileName + ZipExtension);
			var extractedFilesPath = Path.Combine(Settings.Default.FolderPath, SubFolderName);
			ZipManager.ExtractFiles(zipFilePath, extractedFilesPath);

			const string MovieTitleKey = "Movie";
			const string WeeksKey = "Weeks";
			const string GrossKey = "Gross";

			if (!Directory.Exists(fileName))
			{
				Directory.CreateDirectory(fileName);
			}

			string[] files = Directory.GetFiles(extractedFilesPath);

			foreach (var file in files)
			{
				OleDbConnection xlsConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");

				xlsConn.Open();

				using (xlsConn)
				{
					OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", xlsConn);
					OleDbDataReader reader = command.ExecuteReader();

					using (reader)
					{
						while (reader.Read())
						{
							string movieTitle = (string)reader[MovieTitleKey];
							int weeksValue = int.Parse((string)reader[WeeksKey]);
							decimal grossValue = decimal.Parse((string)reader[GrossKey]);

							var boxOfficeEntry = new BoxOfficeEntry
							{
								Movie = data.Movies.All()
									.FirstOrDefault(movie => movie.Title.Equals(movieTitle, StringComparison.OrdinalIgnoreCase)),
								Weeks = weeksValue,
								GeneratedWeekendIncome = grossValue
							};

							boxOfficeEntry.ID = boxOfficeEntry.Movie.ID;
							data.BoxOfficeEntries.Add(boxOfficeEntry);
						}

						data.SaveChanges();
					}
				}
			}

			Directory.Delete(extractedFilesPath, true);
		}
	}
}