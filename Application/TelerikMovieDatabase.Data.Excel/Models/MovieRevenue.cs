namespace TelerikMovieDatabase.Data.Excel.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TelerikMovieDatabase.Models;

	public class MovieRevenueReport : IKeyHolder
	{
		public int ID { get; set; }

		public string Title { get; set; }

		public decimal Gross { get; set; }

		public decimal Revenue { get; set; }
	}
}