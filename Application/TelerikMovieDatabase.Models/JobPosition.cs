namespace TelerikMovieDatabase.Models
{
	using System;
	using System.Collections.Generic;

	using System.Linq;
	using System.Runtime.Serialization;

	public class JobPosition : BaseEntity
	{
		private ICollection<Person> workers;

		public JobPosition()
		{
			this.workers = new HashSet<Person>();
		}

		[IgnoreDataMember]
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

		[DataMember]
		public JobPositionType Type { get; set; }
	}
}