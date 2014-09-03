namespace TelerikMovieDatabase.Data.MsSql
{
	using TelerikMovieDatabase.Data.MsSql.Repositories;
	using TelerikMovieDatabase.Models;

	public interface ITelerikMovieDatabaseMsSqlData
	{
		IGenericRepository<Movie> Movies { get; }

		IGenericRepository<Person> Persons { get; }

		IGenericRepository<JobPosition> JobPositions { get; }

		IGenericRepository<Award> Awards { get; }

		IGenericRepository<AwardAcademy> AwardAcademies { get; }

		IGenericRepository<BoxOfficeEntry> BoxOfficeEntries { get; }

		IGenericRepository<Nomination> Nominations { get; }

		IGenericRepository<Genre> Genres { get; }

		IGenericRepository<Country> Countries { get; }

		IGenericRepository<Language> Languages { get; }
	}
}