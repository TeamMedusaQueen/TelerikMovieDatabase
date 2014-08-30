﻿namespace TMDB.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TMDB.Models;
    using TMDB.Data.Migrations;
    
   public class TmdbContext : DbContext
    {
        public TmdbContext()
            : base("TMDB")
        {
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<TmdbContext, Configuration>());
        }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

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

        public IDbSet<Award> Awards { get; set; }

        public IDbSet<AwardAcademy> AwardAcademies { get; set; }

        public IDbSet<BoxOfficeEntry> BoxOfficeEntries { get; set; }

        public IDbSet<Nomination> Nominations { get; set; }

        public IDbSet<ProductionCompany> ProductionCompanies { get; set; }

        public IDbSet<Genre> Genres { get; set; }
    }
}