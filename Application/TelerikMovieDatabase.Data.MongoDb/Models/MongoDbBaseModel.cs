namespace TelerikMovieDatabase.Data.MongoDb.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TelerikMovieDatabase.Models;

	public abstract class MongoDbBaseModel : IKeyHolder
	{
		public int _id { get; set; }

		public int ID
		{
			get
			{
				return this._id;
			}
		}
	}
}