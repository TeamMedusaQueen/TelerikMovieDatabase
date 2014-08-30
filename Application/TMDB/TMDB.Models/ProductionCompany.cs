namespace TMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductionCompany : BaseEntity
    {
        private ICollection<Movie> movies;

        public ProductionCompany()
        {
            this.movies = new HashSet<Movie>();
        }

        public string Title { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get
            {
                return this.movies;
            }
            set
            {
                this.movies = value;
            }
        } 
    }
}