namespace TMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TMDB.Models;
    
    public class Person : BaseEntity
    {
        private ICollection<JobPosition> job;
		private ICollection<Movie> filmedMovies;
		private ICollection<Movie> writedMovies;

        public Person()
        {
            this.job = new HashSet<JobPosition>();
			this.filmedMovies = new HashSet<Movie>();
			this.writedMovies = new HashSet<Movie>();
        }

		public virtual ICollection<JobPosition> Job
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

        public virtual ICollection<Movie> FilmedMovies
        {
            get
            {
                return this.filmedMovies;
            }
            set
            {
                this.filmedMovies = value;
            }
        }
		public virtual ICollection<Movie> WritedMovies
		{
			get
			{
				return this.writedMovies;
			}
			set
			{
				this.writedMovies = value;
			}
		}

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        
    }
}