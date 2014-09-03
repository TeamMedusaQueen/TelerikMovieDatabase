using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikMovieDatabase.Data.Json
{
	public static class JsonManager
	{
		public static string Serialize<TEntity>(TEntity data)
		{
			return JsonConvert.SerializeObject(data);
		}

		public static TEntity Deserialize<TEntity>(string jsonObject)
		{
			return JsonConvert.DeserializeObject<TEntity>(jsonObject);
		}
	}
}