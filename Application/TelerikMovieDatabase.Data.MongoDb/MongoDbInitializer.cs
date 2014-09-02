namespace TelerikMovieDatabase.Data.MongoDb
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using TelerikMovieDatabase.Data.Imdb;

	public class MongoDbInitializer
	{
		public void Init()
		{
			var mongoDbHandler = new MongoDbManager();
			mongoDbHandler.Connect();

			if (!mongoDbHandler.DatabaseExists())
			{
				var modelMapper = new MovieJsonModelMapper();
				var movieJsonModels = OpenMovieDatabase.GetTop250();

				var movieProjections = modelMapper.Map(movieJsonModels);
				var personProjections = modelMapper.GetPersonProjections();
				var genreProjections = modelMapper.GetGenreProjections();
				var countryProjections = modelMapper.GetCountryProjections();
				var languageProjections = modelMapper.GetLanguageProjections();
				var jobProjections = modelMapper.GetJobProjections();

				mongoDbHandler.InsertMovies(movieProjections);
				mongoDbHandler.InsertPersons(personProjections);
				mongoDbHandler.InsertGenres(genreProjections);
				mongoDbHandler.InsertCountries(countryProjections);
				mongoDbHandler.InsertLanguages(languageProjections);
				mongoDbHandler.InsertJobPositions(jobProjections);
			}
		}
	}
}