namespace TMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    class JobPosition : BaseEntity
    {
        private ICollection<Person> workers;

        public JobPosition()
        {
            this.workers = new HashSet<Person>();
        }

        public ICollection<Person> Workers
        {
            get
            {
                return this.workers;
            }
            set
            {
                this.workers = value;
            }
        }

        public JobPositionType JobPositionType { get; set; }


    }
}