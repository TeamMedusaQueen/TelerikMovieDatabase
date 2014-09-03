namespace TelerikMovieDatabase.Data.Excel
{
	using System;
	using System.Collections.Generic;

	using System.Linq;
	using TelerikMovieDatabase.Data;
	using TelerikMovieDatabase.Models;

	public class Excel2003Manager<TEntity> : ImportExportManagerBase<TEntity, string>
		where TEntity : class, IKeyHolder
	{
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
				return ".xls";
			}
		}

		public override string Serialize(IEnumerable<TEntity> data)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<TEntity> Deserialize(string output)
		{
			throw new NotImplementedException();
		}

		public override void SaveToFile(string filePath, string contents)
		{
			throw new NotImplementedException();
		}
	}
}