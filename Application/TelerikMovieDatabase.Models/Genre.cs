namespace TelerikMovieDatabase.Models
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;

	public class Genre : BaseEntity
	{
		private ICollection<Movie> movies;

		public Genre()
		{
			this.movies = new HashSet<Movie>();
		}

		public string Title { get; set; }

		[IgnoreDataMember]
		[JsonIgnore]
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