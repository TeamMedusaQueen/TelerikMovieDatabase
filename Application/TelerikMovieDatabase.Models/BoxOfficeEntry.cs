namespace TelerikMovieDatabase.Models
{
	using Newtonsoft.Json;
	using System;
	using System.Linq;
	using System.Runtime.Serialization;

	public class BoxOfficeEntry : BaseEntity
	{
		public int Weeks { get; set; }

		public decimal GeneratedWeekendIncome { get; set; }

		[IgnoreDataMember]
		[JsonIgnore]
		public Movie Movie { get; set; }

		public override string ToString()
		{
			return this.Weeks.ToString();
		}
	}
}