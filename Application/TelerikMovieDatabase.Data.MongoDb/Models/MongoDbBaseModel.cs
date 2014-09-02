namespace TelerikMovieDatabase.Data.MongoDb.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;


	public abstract class MongoDbBaseModel
	{
		public int _id { get; set; }
	}
}
