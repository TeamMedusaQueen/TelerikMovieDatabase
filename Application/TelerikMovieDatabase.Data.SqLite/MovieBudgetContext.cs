namespace TelerikMovieDatabase.Data.SqLite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;
    
    public class MovieBudgetContext : DbContext
    {
        public DbSet<MovieBudgetReport> MovieBudgetReports { get; set; }
    }
}