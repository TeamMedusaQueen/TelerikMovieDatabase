namespace TelerikMovieDatabase.Models
{
	using Newtonsoft.Json;
	using System;
	using System.ComponentModel.DataAnnotations;

	using System.Linq;
	using System.Runtime.Serialization;

	[DataContract]
	[JsonObject(MemberSerialization.OptOut)]
	public abstract class BaseEntity : IKeyHolder
	{
		[Key]
		[JsonIgnore]
		public int ID { get; set; }
	}
}