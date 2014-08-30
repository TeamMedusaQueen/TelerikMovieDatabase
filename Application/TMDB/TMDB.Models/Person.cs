namespace TMDB.Models
{
    using System;
    using System.Linq;
    using TMDB.Models;
    
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}