namespace TMDB.DatabaseDataGet
{
    using System;
    using System.Linq;
    
    internal class SqLiteReturnObject
    {
        public SqLiteReturnObject(int reportId, string title, int budget)
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