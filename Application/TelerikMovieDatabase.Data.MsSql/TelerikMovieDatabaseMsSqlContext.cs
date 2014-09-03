namespace TelerikMovieDatabase.Data.MsSql
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using TelerikMovieDatabase.Data.MsSql.Migrations;
	using TelerikMovieDatabase.Models;

	public class TelerikMovieDatabaseMsSqlContext : DbContext, IDisposable, ITelerikMovieDatabaseMsSqlContext
	{
		public const string ConnectionStringName = "TelerikMovieDatabase";

		public TelerikMovieDatabaseMsSqlContext()
			: base(ConnectionStringName)
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<TelerikMovieDatabaseMsSqlContext, Configuration>());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Tricky part with mapping two tables with two different many-to-many relationships
			modelBuilder.Entity<Person>()
				.HasMany(i => i.FilmedMovies)
				.WithMany(i => i.Cast)
				.Map(i =>
				{
					i.MapLeftKey("Person_ID");
					i.MapRightKey("Movie_ID");
					i.ToTable("ActorsMovies");
				});

			modelBuilder.Entity<Person>()
				.HasMany(i => i.WritedMovies)
				.WithMany(i => i.Writers)
				.Map(i =>
				{
					i.MapLeftKey("Person_ID");
					i.MapRightKey("Movie_ID");
					i.ToTable("WritersMovies");
				});
		}

		public IDbSet<Movie> Movies { get; set; }

		public IDbSet<Person> Persons { get; set; }

		public IDbSet<JobPosition> JobPositions { get; set; }

		public IDbSet<Award> Awards { get; set; }

		public IDbSet<AwardAcademy> AwardAcademies { get; set; }

		public IDbSet<BoxOfficeEntry> BoxOfficeEntries { get; set; }

		public IDbSet<Nomination> Nominations { get; set; }

		public IDbSet<Genre> Genres { get; set; }

		public IDbSet<Country> Countries { get; set; }

		public IDbSet<Language> Languages { get; set; }

		public new IDbSet<T> Set<T>() where T : class
		{
			return base.Set<T>();
		}

		public new void SaveChanges()
		{
			base.SaveChanges();
		}
	}
}