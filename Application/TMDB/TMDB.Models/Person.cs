namespace TMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TMDB.Models;
    
    public class Person : BaseEntity
    {
        private ICollection<JobPosition> job;

        public Person()
        {
            this.job = new HashSet<JobPosition>();
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

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        
    }
}