namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Linq;

	public class Award : BaseEntity
	{
		public int AwardAcademyID { get; set; }

		public virtual AwardAcademy AwardAcademy { get; set; }
	}
}