namespace TMDB.Models
{
	using Newtonsoft.Json;
	using System;
	using System.ComponentModel.DataAnnotations;

	using System.Linq;

	public abstract class BaseEntity
	{
		[Key]
		[JsonIgnore]
		public int ID { get; set; }
	}
}