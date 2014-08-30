namespace TMDB.ConsoleClient
{
    using System;
    using System.Linq;
    using TMDB.Data;
    using TMDB.Models;

    class EntryPoint
    {
        static void Main(string[] args)
        {
            TmdbContext db = new TmdbContext();

            db.ProductionCompanies.Add(new ProductionCompany { Title = "Twenty Century Fox" });
            db.SaveChanges();
        }
    }
}