namespace TelerikMovieDatabase.Data.MongoDb.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using TelerikMovieDatabase.Models;


	public class MongoDbJob : MongoDbBaseModel
	{
		public JobPositionType Type { get; set; }
	}
}
