namespace TelerikMovieDatabase.Data.SqLite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class MovieBudgetReport
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Budget { get; set; }
    }
}