using TMDB.Models;
namespace TMDB.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using TMDB.Models;

    public class Movie : BaseEntity
    {
        private ICollection<Person> writers;
        private ICollection<Person> cast;
        private ICollection<ProductionCompany> productionCompanies;
        private ICollection<Nomination> nominations;
        private ICollection<Award> awards;
        private ICollection<Genre> genres;

        public Movie()
        {
            this.writers = new HashSet<Person>();
            this.cast = new HashSet<Person>();
            this.productionCompanies = new HashSet<ProductionCompany>();
            this.nominations = new HashSet<Nomination>();
            this.awards = new HashSet<Award>();
            this.genres = new HashSet<Genre>();
        }

        public string Title { get; set; }

        public string Storyline { get; set; }

        public int RunningTime { get; set; }

        public DateTime ReleaseDate { get; set; }

		[ForeignKey("Person")]
        public int DirectorID { get; set; }

        public decimal Gross { get; set; }

        public int BoxOfficeEntryID { get; set; }

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
