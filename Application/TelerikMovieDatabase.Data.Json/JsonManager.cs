using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TelerikMovieDatabase.Models;

namespace TelerikMovieDatabase.Data.Json
{
	public class JsonManager<TEntity> : ImportExportManagerBase<TEntity, string>
		where TEntity : class, IKeyHolder
	{
		private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();

		public override string FolderPath
		{
			get
			{
				return Settings.Default.FolderPath;
			}
		}

		public override string FileExtension
		{
			get
			{
				return ".json";
			}
		}

		public override string Serialize(IEnumerable<TEntity> data)
		{
			return JsonConvert.SerializeObject(data, Formatting.Indented);
		}

		public override IEnumerable<TEntity> Deserialize(string fileName)
		{
			var filePath = GetFilePath(fileName);
			return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(File.ReadAllText(filePath));
		}

		public override void SaveToFile(string filePath, string contents)
		{
			File.WriteAllText(filePath, contents);
		}

		public override string[] SerializeMultiple(IEnumerable<TEntity> data)
		{
			var items = new List<string>();

			foreach (var item in data)
			{
				var json = JsonConvert.SerializeObject(item, Formatting.Indented, jsonSerializerSettings);
				items.Add(json);
			}

			return items.ToArray();
		}

		public override IEnumerable<TEntity> DeserializeMultiple(string[] fileNames)
		{
			var items = new List<TEntity>();

			foreach (var fileName in fileNames)
			{
				var filePath = GetFilePath(fileName);
				items.Add(JsonConvert.DeserializeObject<TEntity>(File.ReadAllText(filePath)));
			}

			return items.ToArray();
		}

		public override void SaveToFileMultiple(string fileName, Tuple<int, string>[] contents)
		{
			foreach (var item in contents)
			{
				var currentFilePath = Path.Combine(this.FolderPath, fileName + "_" + item.Item1 + FileExtension);
				File.WriteAllText(currentFilePath, item.Item2);
			}
		}
	}
}