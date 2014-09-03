namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IKeyHolder
	{
		int ID { get; }
	}
}