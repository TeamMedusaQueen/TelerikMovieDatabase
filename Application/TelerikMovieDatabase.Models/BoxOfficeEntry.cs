namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Linq;

	public class BoxOfficeEntry : BaseEntity
	{
		public int Weeks { get; set; }

		public decimal GeneratedWeekendIncome { get; set; }

		public Movie Movie { get; set; }

		public override string ToString()
		{
			return this.Weeks.ToString();
		}
	}
}