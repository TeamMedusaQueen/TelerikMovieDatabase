namespace TelerikMovieDatabase.Data.MySql
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class MySqlManager
	{
		public List<GrossReport> GetDataFromMySqlDatabase()
		{
			using (var dbContext = new TelerikMovieDatabaseMySqlContext())
			{
				var data = dbContext.GrossReports.ToList<GrossReport>();
				return data;
			}
		}

		public void InsertIntoMySqlDatabase(IDictionary<string, int> data)
		{
			using (var dbContext = new TelerikMovieDatabaseMySqlContext())
			{
				foreach (var pair in data)
				{
					GrossReport report = new GrossReport();
					report.Title = pair.Key;
					report.Gross = pair.Value;
					dbContext.Add(report);
				}

				dbContext.SaveChanges();
			}
		}
	}
}