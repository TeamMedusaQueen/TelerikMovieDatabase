namespace TelerikMovieDatabase.Common
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TelerikMovieDatabase.Data;
	using TelerikMovieDatabase.Data.Json;
	using TelerikMovieDatabase.Data.Xml;
	using TelerikMovieDatabase.Models;

	public static class ManagerProvider<TInput>
		where TInput : class, IKeyHolder
	{
		public static ImportExportManagerBase<TInput, string> Xml
		{
			get
			{
				return new XmlManager<TInput>();
			}
		}

		public static ImportExportManagerBase<TInput, string> Json
		{
			get
			{
				return new JsonManager<TInput>();
			}
		}
	}
}