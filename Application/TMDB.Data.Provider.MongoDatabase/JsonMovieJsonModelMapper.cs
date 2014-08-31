namespace TMDB.Data.Provider.MongoDatabase
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using TMDB.Data.Provider.IMDB.Models;
	using TMDB.Models;

	internal class JsonMovieJsonModelMapper
	{
		private HashSet<Person> persons = new HashSet<Person>();
		private HashSet<Genre> genres = new HashSet<Genre>();
		private HashSet<ProductionCompany> productionCompanies = new HashSet<ProductionCompany>();
		private HashSet<Country> countries = new HashSet<Country>();
		private HashSet<Language> languages = new HashSet<Language>();
		private HashSet<JobPosition> jobs = new HashSet<JobPosition>();

		private int movieID = 1;
		private int personID = 1;
		private int genreID = 1;
		private int productionCompanyID = 1;
		private int countryID = 1;
		private int languageID = 1;
		private int jobID = 1;

		public IEnumerable<object> Map(IEnumerable<MovieJsonModel> models)
		{
			var movieProjections = new HashSet<object>();

			using (var dbContext = new TmdbContext())
			{
				foreach (var model in models)
				{
					var movie = this.Map(model, dbContext);
					movieProjections.Add(movie);
				}
			}

			return movieProjections;
		}

		public object Map(MovieJsonModel model, TmdbContext dbContext)
		{
			var movie = model.GetMovieModel(dbContext);

			var director = persons.SingleOrDefault(i => i.Name == movie.Director.Name
				&& i.Jobs.All(job => movie.Director.Jobs.Contains(job)));

			if (director == null)
			{
				persons.Add(movie.Director);
				director = movie.Director;
				director.ID = personID++;
			}

			var movieWriters = new HashSet<Person>();
			foreach (var writer in movie.Writers)
			{
				var movieWriter = persons.SingleOrDefault(i => i.Name == writer.Name
				&& i.Jobs.All(job => writer.Jobs.Contains(job)));

				if (movieWriter == null)
				{
					persons.Add(writer);
					movieWriter = writer;
					movieWriter.ID = personID++;
				}

				movieWriters.Add(movieWriter);
			}

			var movieCast = new HashSet<Person>();
			foreach (var actor in movie.Cast)
			{
				var movieActor = persons.SingleOrDefault(i => i.Name == actor.Name
				&& i.Jobs.All(job => actor.Jobs.Contains(job)));

				if (movieActor == null)
				{
					persons.Add(actor);
					movieActor = actor;
					movieActor.ID = personID++;
				}

				movieCast.Add(movieActor);
			}

			var movieGenres = new HashSet<Genre>();
			foreach (var genre in movie.Genres)
			{
				var movieGenre = genres.SingleOrDefault(i => i.Title == genre.Title);

				if (movieGenre == null)
				{
					genres.Add(genre);
					movieGenre = genre;
					movieGenre.ID = genreID++;
				}

				movieGenres.Add(movieGenre);
			}

			var movieProductionCompanies = new HashSet<ProductionCompany>();
			foreach (var productionCompany in movie.ProductionCompanies)
			{
				var movieProductionCompany = productionCompanies.SingleOrDefault(i => i.Name == productionCompany.Name);

				if (movieProductionCompany == null)
				{
					productionCompanies.Add(productionCompany);
					movieProductionCompany = productionCompany;
					movieProductionCompany.ID = productionCompanyID++;
				}

				movieProductionCompanies.Add(movieProductionCompany);
			}

			var movieCountries = new HashSet<Country>();
			foreach (var country in movie.Countries)
			{
				var movieCountry = countries.SingleOrDefault(i => i.Name == country.Name);

				if (movieCountry == null)
				{
					countries.Add(country);
					movieCountry = country;
					movieCountry.ID = countryID++;
				}

				movieCountries.Add(movieCountry);
			}

			var movieLanguages = new HashSet<Language>();
			foreach (var language in movie.Languages)
			{
				var movieLanguage = languages.SingleOrDefault(i => i.Name == language.Name);

				if (movieLanguage == null)
				{
					languages.Add(language);
					movieLanguage = language;
					movieLanguage.ID = languageID++;
				}

				movieLanguages.Add(movieLanguage);
			}

			var movieProjection = new
			{
				_id = movieID++,
				movie.Title,
				movie.Storyline,
				movie.RunningTime,
				movie.Metascore,
				movie.Rated,
				movie.Poster,
				movie.ReleaseDate,
				Director_id = director.ID,
				Writers = movieWriters.Select(i => i.ID),
				Cast = movieCast.Select(i => i.ID),
				Genres = movieGenres.Select(i => i.ID),
				ProductionCompanies = movieProductionCompanies.Select(i => i.ID),
				Countries = movieCountries.Select(i => i.ID),
				Languages = movieLanguages.Select(i => i.ID),
			};

			return movieProjection;
		}

		public IEnumerable<object> GetPersonProjections()
		{
			var personProjections = new HashSet<object>();

			foreach (var person in persons)
			{
				var personJobs = new HashSet<JobPosition>();
				foreach (var job in person.Jobs)
				{
					var personJob = jobs.SingleOrDefault(i => i.Type == job.Type);

					if (personJob == null)
					{
						jobs.Add(job);
						personJob = job;
						personJob.ID = jobID++;
					}

					personJobs.Add(personJob);
				}

				var personProjection = new
				{
					_id = person.ID,
					Name = person.Name,
					Jobs = personJobs.Select(i => i.ID)
				};

				personProjections.Add(personProjection);
			}

			return personProjections;
		}

		public IEnumerable<object> GetGenreProjections()
		{
			return this.GetProjections(this.genres, item => new { _id = item.ID, Title = item.Title });
		}

		public IEnumerable<object> GetProductionCompanyProjections()
		{
			return this.GetProjections(this.productionCompanies, item => new { _id = item.ID, Name = item.Name });
		}

		public IEnumerable<object> GetCountryProjections()
		{
			return this.GetProjections(this.countries, item => new { _id = item.ID, Name = item.Name });
		}

		public IEnumerable<object> GetLanguageProjections()
		{
			return this.GetProjections(this.languages, item => new { _id = item.ID, Name = item.Name });
		}

		public IEnumerable<object> GetJobProjections()
		{
			return this.GetProjections(this.jobs, item => new { _id = item.ID, Type = item.Type });
		}

		public IEnumerable<object> GetProjections<TModel>(IEnumerable<TModel> collection, Func<TModel, object> getProjection)
		{
			var projections = new HashSet<object>();

			foreach (var item in collection)
			{
				var projection = getProjection(item);
				projections.Add(projection);
			}

			return projections;
		}
	}
}