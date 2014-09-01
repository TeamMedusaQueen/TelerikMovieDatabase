namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	using System.Linq;

	public class Movie : BaseEntity
	{
		private ICollection<Person> writers;
		private ICollection<Person> cast;
		private ICollection<ProductionCompany> productionCompanies;
		private ICollection<Nomination> nominations;
		private ICollection<Award> awards;
		private ICollection<Genre> genres;
		private ICollection<Country> countries;
		private ICollection<Language> languages;

		public Movie()
		{
			this.writers = new HashSet<Person>();
			this.cast = new HashSet<Person>();
			this.productionCompanies = new HashSet<ProductionCompany>();
			this.nominations = new HashSet<Nomination>();
			this.awards = new HashSet<Award>();
			this.genres = new HashSet<Genre>();
			this.countries = new HashSet<Country>();
			this.languages = new HashSet<Language>();
		}

		[StringLength(100)]
		public string Title { get; set; }

		[StringLength(500)]
		public string Storyline { get; set; }

		public int? RunningTime { get; set; }

		public byte? Metascore { get; set; }

		[StringLength(10)]
		public string Rated { get; set; }

		public float Rating { get; set; }

		[MaxLength(int.MaxValue)]
		public byte[] Poster { get; set; }

		public DateTime? ReleaseDate { get; set; }

		[Column("Director_ID")]
		public Person Director { get; set; }

		public decimal Gross { get; set; }

		public int? BoxOfficeEntryID { get; set; }

		public virtual BoxOfficeEntry BoxOfficeEntry { get; set; }

		public virtual ICollection<Person> Writers
		{
			get
			{
				return this.writers;
			}
			set
			{
				this.writers = value;
			}
		}

		public virtual ICollection<Person> Cast
		{
			get
			{
				return this.cast;
			}
			set
			{
				this.cast = value;
			}
		}

		public virtual ICollection<ProductionCompany> ProductionCompanies
		{
			get
			{
				return this.productionCompanies;
			}
			set
			{
				this.productionCompanies = value;
			}
		}

		public virtual ICollection<Genre> Genres
		{
			get
			{
				return this.genres;
			}
			set
			{
				this.genres = value;
			}
		}

		public virtual ICollection<Country> Countries
		{
			get
			{
				return this.countries;
			}
			set
			{
				this.countries = value;
			}
		}

		public virtual ICollection<Language> Languages
		{
			get
			{
				return this.languages;
			}
			set
			{
				this.languages = value;
			}
		}

		public virtual ICollection<Nomination> Nominations
		{
			get
			{
				return this.nominations;
			}
			set
			{
				this.nominations = value;
			}
		}

		public virtual ICollection<Award> Awards
		{
			get
			{
				return this.awards;
			}
			set
			{
				this.awards = value;
			}
		}
	}
}