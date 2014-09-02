namespace TelerikMovieDatabase.Data.Imdb
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading.Tasks;
	using TelerikMovieDatabase.Data.Imdb.Models;

	public static class OpenMovieDatabase
	{
		private static readonly string cacheFolder = Settings.Default.CachedFolderPath;

		public static MovieJsonModel GetMovieDataByID(string movieID)
		{
			var fileName = cacheFolder + @"\" + movieID + ".json";
			string jsonData = null;

			if (!File.Exists(fileName))
			{
				jsonData = GetString("http://www.omdbapi.com/?i=" + movieID);
				File.WriteAllText(fileName, jsonData);
			}
			else
			{
				jsonData = File.ReadAllText(fileName);
			}

			var movieJsonModel = JsonConvert.DeserializeObject<MovieJsonModel>(jsonData);
			StripAdditinalInformation(movieJsonModel);

			return movieJsonModel;
		}

		public static MovieJsonModel GetMovieDataByTitle(string movieTitle)
		{
			var jsonData = GetString("http://www.omdbapi.com/?t=" + movieTitle);
			var movieJsonModel = JsonConvert.DeserializeObject<MovieJsonModel>(jsonData);
			StripAdditinalInformation(movieJsonModel);

			return movieJsonModel;
		}

		public static ICollection<MovieJsonModel> GetTop250()
		{
			var movies = new HashSet<MovieJsonModel>();

			foreach (var movieID in Top250Movies.Top250)
			{
				var movie = GetMovieDataByID(movieID);
				if (movie != null)
				{
					movies.Add(movie);
				}
			}

			return movies;
		}

		private static string GetString(string url)
		{
			using (var webClient = new WebClient())
			{
				return webClient.DownloadString(url);
			}
		}

		private static void StripAdditinalInformation(MovieJsonModel movieJsonModel)
		{
			movieJsonModel.Actors = StripAdditinalInformationFromNames(movieJsonModel.Actors);
			movieJsonModel.Writer = StripAdditinalInformationFromNames(movieJsonModel.Writer);
			movieJsonModel.Director = StripAdditinalInformationFromNames(movieJsonModel.Director);
		}

		private static string StripAdditinalInformationFromNames(string names)
		{
			if (string.IsNullOrWhiteSpace(names))
			{
				return names;
			}

			const string Separator = ", ";
			var namesArray = names.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
			var fixedNames = new HashSet<string>();

			foreach (var name in namesArray)
			{
				var fixedName = StripAdditionalInformationFromName(name);
				fixedNames.Add(fixedName);
			}

			return string.Join(Separator, fixedNames);
		}

		private static string StripAdditionalInformationFromName(string name)
		{
			string fixedName;
			int indexOfBracket = name.IndexOf('(');

			if (indexOfBracket > -1)
			{
				fixedName = name.Substring(0, indexOfBracket);
			}
			else
			{
				fixedName = name;
			}

			return fixedName;
		}
	}
}