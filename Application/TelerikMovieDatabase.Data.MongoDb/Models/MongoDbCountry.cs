namespace TelerikMovieDatabase.Data.MongoDb.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;


	public class MongoDbCountry : MongoDbBaseModel
	{
		public string Name { get; set; }
	}
}
