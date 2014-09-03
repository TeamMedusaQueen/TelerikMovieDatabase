namespace TelerikMovieDatabase.Models
{
	using Newtonsoft.Json;
	using System;
	using System.ComponentModel.DataAnnotations;

	using System.Linq;
	using System.Runtime.Serialization;

	[DataContract]
	public abstract class BaseEntity
	{
		[Key]
		[JsonIgnore]
		public int ID { get; set; }
	}
}