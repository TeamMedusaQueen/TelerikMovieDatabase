namespace TMDB.DatabaseDataGet
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TMDB.ExcelOperations;
    using TMDB.GrossReports;

    public class MySqlManager
    {
        public List<Grossreport> GetDataFromMySqlDatabase()
        {
            var db = new GrossReports();
            var data = db.Grossreports.ToList<Grossreport>();
            return data;
        }

        public void InsertIntoMySqlDatabase(IDictionary<string, int> data)
        {
            var db = new GrossReports();
            using (db)
            {
                foreach (var pair in data)
                {
                    Grossreport report = new Grossreport();
                    report.Title = pair.Key;
                    report.Gross = pair.Value;
                    db.Add(report);
                }
                db.SaveChanges();
            }
        }
    }
}