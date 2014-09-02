namespace TelerikMovieDatabase.Data.MongoDb.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;


	public class MongoDbGenre : MongoDbBaseModel
	{
		public string Title { get; set; }
	}
}
