namespace TelerikMovieDatabase.Models
{
	using Newtonsoft.Json;
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
		[JsonIgnore]
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

		public override string ToString()
		{
			return this.Type.ToString();
		}
	}
}