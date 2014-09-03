namespace TelerikMovieDatabase.Data.MsSql
{
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using TelerikMovieDatabase.Models;

	public interface ITelerikMovieDatabaseMsSqlContext
	{
		DbContextConfiguration Configuration { get; }

		IDbSet<Movie> Movies { get; set; }

		IDbSet<Person> Persons { get; set; }

		IDbSet<JobPosition> JobPositions { get; set; }

		IDbSet<Award> Awards { get; set; }

		IDbSet<AwardAcademy> AwardAcademies { get; set; }

		IDbSet<BoxOfficeEntry> BoxOfficeEntries { get; set; }

		IDbSet<Nomination> Nominations { get; set; }

		IDbSet<Genre> Genres { get; set; }

		IDbSet<Country> Countries { get; set; }

		IDbSet<Language> Languages { get; set; }

		IDbSet<T> Set<T>() where T : class;

		DbEntityEntry<T> Entry<T>(T entity) where T : class;

		void SaveChanges();
	}
}