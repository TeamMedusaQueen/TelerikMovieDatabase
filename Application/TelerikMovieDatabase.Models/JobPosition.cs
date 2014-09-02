namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Collections.Generic;

	using System.Linq;

	public class JobPosition : BaseEntity
	{
		private ICollection<Person> workers;

		public JobPosition()
		{
			this.workers = new HashSet<Person>();
		}

		public virtual ICollection<Person> Workers
		{
			get
			{
				return this.workers;
			}
			set
			{
				this.workers = value;
			}
		}

		public JobPositionType Type { get; set; }
	}
}