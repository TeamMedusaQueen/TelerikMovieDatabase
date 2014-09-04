namespace TelerikMovieDatabase.Data.Imdb.Models
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Linq;
	using System.Net;
	using TelerikMovieDatabase.Data.MsSql;
	using TelerikMovieDatabase.Models;

	public class MovieJsonModel
	{
		public string Title { get; set; }

		public string Year { get; set; }

		public string Rated { get; set; }

		public string Released { get; set; }

		public string Runtime { get; set; }

		public string Genre { get; set; }

		public string Director { get; set; }

		public string Writer { get; set; }

		public string Actors { get; set; }

		public string Plot { get; set; }

		public string Language { get; set; }

		public string Country { get; set; }

		public string Awards { get; set; }

		public string Poster { get; set; }

		public string Metascore { get; set; }

		public string Budget { get; set; }

		public float imdbRating { get; set; }

		public string imdbVotes { get; set; }

		public string imdbID { get; set; }

		public string Type { get; set; }

		public string Response { get; set; }

		public Movie GetMovieModel(TelerikMovieDatabaseMsSqlContext dbContext)
		{
			if (this.Title == null)
			{
				return null;
			}

			int @int;
			byte @byte;
			DateTime dateTime;

			int? runningTime = null;

			if (!string.IsNullOrWhiteSpace(this.Runtime)
				&& int.TryParse(this.Runtime.Split(' ')[0], out @int))
			{
				runningTime = @int;
			}

			byte? metascore = null;

			if (!string.IsNullOrWhiteSpace(this.Metascore)
				&& byte.TryParse(this.Metascore, out @byte))
			{
				metascore = @byte;
			}

			DateTime? releaseDate = null;

			if (!string.IsNullOrWhiteSpace(this.Released)
				&& DateTime.TryParse(this.Released, out dateTime))
			{
				releaseDate = dateTime;
			}

			var movieModel = new Movie()
			{
				Title = this.Title,
				Storyline = this.Plot,
				RunningTime = runningTime,
				Metascore = metascore,
				Rated = this.Rated,
				Rating = this.imdbRating,
				//Poster = this.GetImageData(this.Poster),
				ReleaseDate = releaseDate,
				Director = this.GetPerson(this.Director, dbContext, JobPositionType.Director),
				Writers = this.GetPersonsCollection(this.Writer, dbContext, JobPositionType.Writer),
				Cast = this.GetPersonsCollection(this.Actors, dbContext, JobPositionType.Actor),
				Genres = this.GetCollection(
					this.Genre,
					title => dbContext.Genres.Local.FirstOrDefault(i => i.Title == title),
					title =>
					{
						var genre = new Genre() { Title = title };
						dbContext.Genres.Add(genre);
						return genre;
					}),
				Countries = this.GetCollection(
					this.Country,
					name => dbContext.Countries.Local.FirstOrDefault(i => i.Name == name),
					name =>
					{
						var country = new Country() { Name = name };
						dbContext.Countries.Add(country);
						return country;
					}),
				Languages = this.GetCollection(
					this.Language,
					name => dbContext.Languages.Local.FirstOrDefault(i => i.Name == name),
					name =>
					{
						var language = new Language() { Name = name };
						dbContext.Languages.Add(language);
						return language;
					})
				//Gross =
				//Awards =
				//Nominations =
				//ProductionCompanies =
			};

			return movieModel;
		}

		private ICollection<Person> GetPersonsCollection(string names, TelerikMovieDatabaseMsSqlContext dbContext, JobPositionType jobType)
		{
			var personNames = names.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

			var persons = new HashSet<Person>();
			foreach (var personName in personNames)
			{
				var person = this.GetPerson(personName, dbContext, jobType);
				persons.Add(person);
			}

			return persons;
		}

		private ICollection<TEntity> GetCollection<TEntity>(string names, Func<string, TEntity> getItem, Func<string, TEntity> createNew)
		{
			var itemNames = names.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

			var items = new HashSet<TEntity>();

			foreach (var name in itemNames)
			{
				var item = getItem(name);

				if (item == null)
				{
					item = createNew(name);
				}

				items.Add(item);
			}

			return items;
		}

		private Person GetPerson(string name, TelerikMovieDatabaseMsSqlContext dbContext, JobPositionType jobType)
		{
			var person = dbContext.Persons.Local.FirstOrDefault(
					p => p.Name == name
					&& p.Jobs.Any(job => job.Type == jobType));

			var jobPosition = dbContext.JobPositions.Local.FirstOrDefault(j => j.Type == jobType);

			if (jobPosition == null)
			{
				jobPosition = new JobPosition()
				{
					Type = jobType
				};

				dbContext.JobPositions.Add(jobPosition);
			}

			if (person == null)
			{
				person = new Person()
				{
					Name = name,
					Jobs = new HashSet<JobPosition>() { jobPosition }
				};

				dbContext.Persons.Add(person);
			}
			else if (!person.Jobs.Any(j => j.Type == jobType))
			{
				person.Jobs.Add(jobPosition);
			}

			return person;
		}

		private byte[] GetImageData(string imageUrl)
		{
			if (string.IsNullOrWhiteSpace(imageUrl))
			{
				return null;
			}

			using (var webClient = new WebClient())
			{
				var imageData = webClient.DownloadData(imageUrl);
				Image image;
				byte[] imageBuffer;

				using (var memoryStream = new MemoryStream(imageData))
				{
					image = Image.FromStream(memoryStream);

					using (var jpgStream = new MemoryStream())
					{
						image.Save(jpgStream, ImageFormat.Jpeg);
						imageBuffer = jpgStream.ToArray();
					}
				}

				return imageBuffer;
			}
		}
	}
}