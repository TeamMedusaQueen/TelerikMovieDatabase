namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Text;
	using System.Threading.Tasks;

	public class Country : BaseEntity
	{
		private ICollection<Movie> movies;

		public Country()
		{
			this.movies = new HashSet<Movie>();
		}

		public string Name { get; set; }

		[IgnoreDataMember]
		public virtual ICollection<Movie> Movies
		{
			get
			{
				return this.movies;
			}
			set
			{
				this.movies = value;
			}
		}
	}
}