namespace TelerikMovieDatabase.ConsoleClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using TelerikMovieDatabase.Data.MsSql.Repositories;
	using TelerikMovieDatabase.Data.Xml;

	public static class XmlReport
	{
		public static TEntity ImportFromXml<TEntity>(string fileName)
		{
			return XmlManager.Deserialize<TEntity>(fileName);
		}

		public static void ExportToXml<TRepository, TEntity>(TRepository repository, string fileName, params Expression<Func<TEntity, object>>[] includeProperties)
			where TRepository : IGenericRepository<TEntity>
			where TEntity : class
		{
			ExportToXml(repository, fileName, null, null, includeProperties);
		}

		public static void ExportToXml<TRepository, TEntity>(TRepository repository, string fileName, Expression<Func<TEntity, TEntity>> projectFunc, params Expression<Func<TEntity, object>>[] includeProperties)
			where TRepository : IGenericRepository<TEntity>
			where TEntity : class
		{
			ExportToXml(repository, fileName, projectFunc, null, includeProperties);
		}

		public static void ExportToXml<TRepository, TEntity>(TRepository repository, string fileName, Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
			where TRepository : IGenericRepository<TEntity>
			where TEntity : class
		{
			ExportToXml(repository, fileName, null, wherePredicate, includeProperties);
		}

		public static void ExportToXml<TRepository, TEntity>(TRepository repository, string fileName, Expression<Func<TEntity, TEntity>> projectFunc, Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
			where TRepository : IGenericRepository<TEntity>
			where TEntity : class
		{
			repository.DisableProxyCreation();
			var data = repository.Project(projectFunc, wherePredicate, includeProperties).ToArray();
			repository.EnableProxyCreation();

			ExportToXml(data, fileName);
		}

		public static void ExportToXml<TEntity>(TEntity[] data, string fileName)
			where TEntity : class
		{
			var xmlData = XmlManager.Serialize(data);
			XmlManager.SaveToFile(fileName, xmlData);
		}
	}
}