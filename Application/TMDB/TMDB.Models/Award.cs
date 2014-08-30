namespace TMDB.Models
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public class Award : BaseEntity
	{
		public int AwardAcademyID { get; set; }

		public virtual AwardAcademy AwardAcademy { get; set; }
	}
}