namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class AwardAcademy : BaseEntity
	{
		public string Title { get; set; }
		public override string ToString()
		{
			return this.Title;
		}
	}
}