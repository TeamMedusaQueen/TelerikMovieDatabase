namespace TelerikMovieDatabase.Data.MongoDb
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TelerikMovieDatabase.Models;

	public class MongoDbInitializer
	{
		public bool Init(IEnumerable<Movie> movies)
		{
			var mongoDbHandler = new MongoDbManager();

			if (!mongoDbHandler.DatabaseExists())
			{
				var movieMapper = new MovieMapper();
				var movieProjections = movieMapper.Map(movies);
				var personProjections = movieMapper.GetPersonProjections();
				var genreProjections = movieMapper.GetGenreProjections();
				var countryProjections = movieMapper.GetCountryProjections();
				var languageProjections = movieMapper.GetLanguageProjections();
				var jobProjections = movieMapper.GetJobProjections();

				mongoDbHandler.InsertMovies(movieProjections);
				mongoDbHandler.InsertPersons(personProjections);
				mongoDbHandler.InsertGenres(genreProjections);
				mongoDbHandler.InsertCountries(countryProjections);
				mongoDbHandler.InsertLanguages(languageProjections);
				mongoDbHandler.InsertJobPositions(jobProjections);

				return true;
			}

			return false;
		}
	}
}