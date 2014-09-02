namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Linq;

	public class Nomination : BaseEntity
	{
		public int AwardAcademyID { get; set; }

		public AwardAcademy AwardAcademy { get; set; }
	}
}