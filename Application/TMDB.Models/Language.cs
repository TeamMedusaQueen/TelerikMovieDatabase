namespace TMDB.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class Language : BaseEntity
	{
		private ICollection<Movie> movies;

		public Language()
		{
			this.movies = new HashSet<Movie>();
		}

		public string Name { get; set; }

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