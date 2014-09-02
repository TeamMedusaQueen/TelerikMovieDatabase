namespace TelerikMovieDatabase.Data.MongoDb.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class MongoDbMovie : MongoDbBaseModel
	{
		public string Title { get; set; }

		public string Storyline { get; set; }

		public int? RunningTime { get; set; }

		public byte? Metascore { get; set; }

		public string Rated { get; set; }

		public double Rating { get; set; }

		//public byte[] Poster { get; set; }

		public DateTime? ReleaseDate { get; set; }

		public int Director_id { get; set; }

		public IEnumerable<int> Writers { get; set; }

		public IEnumerable<int> Cast { get; set; }

		public IEnumerable<int> Genres { get; set; }

		public IEnumerable<int> Countries { get; set; }

		public IEnumerable<int> Languages { get; set; }
	}
}