namespace TelerikMovieDatabase.Data.MsSql.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<TelerikMovieDatabase.Data.MsSql.TelerikMovieDatabaseMsSqlContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(TelerikMovieDatabase.Data.MsSql.TelerikMovieDatabaseMsSqlContext context)
		{
			//  This method will be called after migrating to the latest version.
			//  You can use the DbSet<T>.AddOrUpdate() helper extension method
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
		}
	}
}