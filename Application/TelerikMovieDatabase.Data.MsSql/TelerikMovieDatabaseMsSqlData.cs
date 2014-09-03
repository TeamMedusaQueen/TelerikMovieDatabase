namespace TelerikMovieDatabase.Data.MsSql
{
	using System;
	using System.Collections.Generic;
	using TelerikMovieDatabase.Data.MsSql.Repositories;
	using TelerikMovieDatabase.Models;

	public class TelerikMovieDatabaseMsSqlData : ITelerikMovieDatabaseMsSqlData, IDisposable
	{
		private readonly IDictionary<Type, object> repositories;
		private readonly TelerikMovieDatabaseMsSqlContext context;

		public TelerikMovieDatabaseMsSqlData()
			: this(new TelerikMovieDatabaseMsSqlContext())
		{
		}

		public TelerikMovieDatabaseMsSqlData(TelerikMovieDatabaseMsSqlContext context)
		{
			this.context = context;
			this.repositories = new Dictionary<Type, object>();
		}

		public IGenericRepository<Movie> Movies
		{
			get
			{
				return this.GetRepository<Movie>();
			}
		}

		public IGenericRepository<Person> Persons
		{
			get
			{
				return this.GetRepository<Person>();
			}
		}

		public IGenericRepository<JobPosition> JobPositions
		{
			get
			{
				return this.GetRepository<JobPosition>();
			}
		}

		public IGenericRepository<Award> Awards
		{
			get
			{
				return this.GetRepository<Award>();
			}
		}

		public IGenericRepository<AwardAcademy> AwardAcademies
		{
			get
			{
				return this.GetRepository<AwardAcademy>();
			}
		}

		public IGenericRepository<BoxOfficeEntry> BoxOfficeEntries
		{
			get
			{
				return this.GetRepository<BoxOfficeEntry>();
			}
		}

		public IGenericRepository<Nomination> Nominations
		{
			get
			{
				return this.GetRepository<Nomination>();
			}
		}

		public IGenericRepository<Genre> Genres
		{
			get
			{
				return this.GetRepository<Genre>();
			}
		}

		public IGenericRepository<Country> Countries
		{
			get
			{
				return this.GetRepository<Country>();
			}
		}

		public IGenericRepository<Language> Languages
		{
			get
			{
				return this.GetRepository<Language>();
			}
		}

		public void SaveChanges()
		{
			this.context.SaveChanges();
		}

		public void Dispose()
		{
			this.context.Dispose();
		}

		private IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			var typeOfModel = typeof(TEntity);
			if (!this.repositories.ContainsKey(typeOfModel))
			{
				var type = typeof(GenericRepository<TEntity>);
				this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
			}

			return (IGenericRepository<TEntity>)this.repositories[typeOfModel];
		}
	}
}