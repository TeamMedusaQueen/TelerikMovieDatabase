namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Linq;

	public class BoxOfficeEntry : BaseEntity
	{
		public int Weeks { get; set; }

		public decimal GeneratedWeekendIncome { get; set; }
	}
}