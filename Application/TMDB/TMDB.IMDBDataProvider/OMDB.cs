namespace TMDB.IMDBDataProvider
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading.Tasks;
	using TMDB.Data;
	using TMDB.IMDBDataProvider.Models;
	using TMDB.Models;

	public static class OMDB
	{
		private const string CacheFolder = "OMDBCache\\";

		public static Movie GetMovieDataByID(string movieID, TmdbContext dbContext)
		{
			var fileName = CacheFolder + movieID + ".json";
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

			var movieJSONModel = JsonConvert.DeserializeObject<MovieJSONModel>(jsonData);

			return movieJSONModel.GetMovieModel(dbContext);
		}

		public static Movie GetMovieDataByTitle(string movieTitle, TmdbContext dbContext)
		{
			var jsonData = GetString("http://www.omdbapi.com/?t=" + movieTitle);
			var movieJSONModel = JsonConvert.DeserializeObject<MovieJSONModel>(jsonData);

			return movieJSONModel.GetMovieModel(dbContext);
		}

		public static ICollection<Movie> GetTop250()
		{
			var movies = new HashSet<Movie>();

			using (var dbContext = new TmdbContext())
			{
				foreach (var movieID in MovieNames.Top250)
				{
					var movie = GetMovieDataByID(movieID, dbContext);
					if (movie != null)
					{
						movies.Add(movie);
					}
				}
			}

			return movies;
		}

		public static string GetString(string url)
		{
			using (var webClient = new WebClient())
			{
				return webClient.DownloadString(url);
			}
		}
	}
}