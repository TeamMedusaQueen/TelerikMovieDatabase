namespace IMDB.Models
{
    using System;
    using System.Linq;

    public class Movie
    {
        public int MovieID { get; set; }

        public string Title { get; set; }

        public string Storyline { get; set; }

        public int RunningTime { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int DirectorID { get; set; }

        public int GenreID { get; set; }

        public int BoxOfficeEntry { get; set; }

        public decimal Gross { get; set; }
    }
}