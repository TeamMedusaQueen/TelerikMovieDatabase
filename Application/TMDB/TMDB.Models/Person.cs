namespace TMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TMDB.Models;
    
    public class Person : BaseEntity
    {
        private ICollection<JobPosition> job;
        private ICollection<Movie> movies;

        public Person()
        {
            this.job = new HashSet<JobPosition>();
            this.movies = new HashSet<Movie>();
        }

        public ICollection<JobPosition> Job
        {
            get
            {
                return this.job;
            }
            set
            {
                this.job = value;
            }
        }

        public ICollection<Movie> Movies
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

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        
    }
}