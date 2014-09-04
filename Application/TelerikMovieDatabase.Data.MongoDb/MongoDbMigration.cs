namespace TelerikMovieDatabase.Data.MongoDb
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TelerikMovieDatabase.Data.MongoDb.Models;
	using TelerikMovieDatabase.Models;

	public class MongoDbMigration
	{
		public void AddMovies(IEnumerable<Movie> movies)
		{
			var movieMapper = new MovieMapper();
			var mongoDbMovies = movieMapper.Map(movies);

			var mongoDbManager = new MongoDbManager();
			mongoDbManager.InsertMovies(mongoDbMovies);
		}

		public IEnumerable<Movie> GetMovies()
		{
			var mongoDbManager = new MongoDbManager();

			var movies = mongoDbManager.GetDatabase().GetCollection<MongoDbMovie>(MongoDbManager.DbTableMoviesName);
			var persons = mongoDbManager.GetDatabase().GetCollection<MongoDbPerson>(MongoDbManager.DbTablePersonsName);
			var genres = mongoDbManager.GetDatabase().GetCollection<MongoDbGenre>(MongoDbManager.DbTableGenresName);
			var countries = mongoDbManager.GetDatabase().GetCollection<MongoDbCountry>(MongoDbManager.DbTableCountriesName);
			var languages = mongoDbManager.GetDatabase().GetCollection<MongoDbLanguage>(MongoDbManager.DbTableLanguagesName);
			var jobPositions = mongoDbManager.GetDatabase().GetCollection<MongoDbJob>(MongoDbManager.DbTableJobPositionsName);

			var moviesDictionary = new Dictionary<int, Movie>();
			var personsDictionary = new Dictionary<int, Person>();
			var genresDictionary = new Dictionary<int, Genre>();
			var countriesDictionary = new Dictionary<int, Country>();
			var languagesDictionary = new Dictionary<int, Language>();
			var jobPositionsDictionary = new Dictionary<int, JobPosition>();

			foreach (var jobPosition in jobPositions.FindAll())
			{
				jobPositionsDictionary.Add(jobPosition._id, new JobPosition()
				{
					Type = jobPosition.Type
				});
			}

			foreach (var language in languages.FindAll())
			{
				languagesDictionary.Add(language._id, new Language()
				{
					Name = language.Name
				});
			}

			foreach (var country in countries.FindAll())
			{
				countriesDictionary.Add(country._id, new Country()
				{
					Name = country.Name
				});
			}

			foreach (var genre in genres.FindAll())
			{
				genresDictionary.Add(genre._id, new Genre()
				{
					Title = genre.Title
				});
			}

			foreach (var person in persons.FindAll())
			{
				personsDictionary.Add(person._id, new Person()
				{
					Name = person.Name,
					Jobs = person.Jobs.Select(i => jobPositionsDictionary[i]).ToArray()
				});
			}

			foreach (var movie in movies.FindAll())
			{
				moviesDictionary.Add(movie._id, new Movie()
				{
					Title = movie.Title,
					Storyline = movie.Storyline,
					RunningTime = movie.RunningTime,
					Metascore = movie.Metascore,
					Rated = movie.Rated,
					Rating = movie.Rating,
					//Poster = movie.Poster,
					ReleaseDate = movie.ReleaseDate,
					Director = personsDictionary[movie.Director_id],
					Writers = movie.Writers.Select(i => personsDictionary[i]).ToArray(),
					Cast = movie.Cast.Select(i => personsDictionary[i]).ToArray(),
					Genres = movie.Genres.Select(i => genresDictionary[i]).ToArray(),
					Countries = movie.Countries.Select(i => countriesDictionary[i]).ToArray(),
					Languages = movie.Languages.Select(i => languagesDictionary[i]).ToArray()
				});
			}

			return moviesDictionary.Values;
		}
	}
}