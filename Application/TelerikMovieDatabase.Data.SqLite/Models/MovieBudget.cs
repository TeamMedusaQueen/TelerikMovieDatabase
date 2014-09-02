namespace TelerikMovieDatabase.Data.SqLite.Models
{
    using System;
    using System.Linq;
    
    internal class MovieBudget
    {
        public MovieBudget(int reportId, string title, int budget)
        {
            this.ReportId = reportId;
            this.Title = title;
            this.Budget = budget;
        }

        public int ReportId { get; private set; }

        public int Budget { get; private set; }

        public string Title { get; private set; }
    }
}