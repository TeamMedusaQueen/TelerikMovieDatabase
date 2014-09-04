namespace TelerikMovieDatabase.Data.MongoDb.Models
{
	using MongoDB.Bson.Serialization.Attributes;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TelerikMovieDatabase.Models;

	public abstract class MongoDbBaseModel : IKeyHolder
	{
		[BsonId]
		public int _id { get; set; }

		[BsonIgnore]
		int IKeyHolder.ID
		{
			get
			{
				return this._id;
			}
		}
	}
}