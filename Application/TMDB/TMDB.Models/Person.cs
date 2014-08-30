﻿namespace TMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TMDB.Models;
    
    public class Person : BaseEntity
    {
        private ICollection<JobPosition> jobs;
		private ICollection<Movie> filmedMovies;
		private ICollection<Movie> writedMovies;

        public Person()
        {
            this.jobs = new HashSet<JobPosition>();
			this.filmedMovies = new HashSet<Movie>();
			this.writedMovies = new HashSet<Movie>();
        }

		public virtual ICollection<JobPosition> Jobs
        {
            get
            {
                return this.jobs;
            }
            set
            {
                this.jobs = value;
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

        public string Name { get; set; }
    }
}