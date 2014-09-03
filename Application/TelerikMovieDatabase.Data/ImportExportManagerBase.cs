namespace TelerikMovieDatabase.Data
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Linq.Expressions;
	using TelerikMovieDatabase.Data.MsSql.Repositories;
	using TelerikMovieDatabase.Models;

	public abstract class ImportExportManagerBase<TInput, TOutput>
		where TInput : class, IKeyHolder
	{
		public bool IsMultiple { get; set; }

		public abstract string FolderPath { get; }

		public abstract string FileExtension { get; }

		public abstract TOutput Serialize(IEnumerable<TInput> data);

		public virtual TOutput[] SerializeMultiple(IEnumerable<TInput> data)
		{
			throw new NotImplementedException();
		}

		public abstract IEnumerable<TInput> Deserialize(TOutput output);

		public virtual IEnumerable<TInput> DeserializeMultiple(TOutput[] output)
		{
			throw new NotImplementedException();
		}

		public abstract void SaveToFile(string filePath, TOutput contents);

		public virtual void SaveToFileMultiple(string fileName, Tuple<int, TOutput>[] contents)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TInput> Import(TOutput output)
		{
			return this.Deserialize(output);
		}

		public void Export(IGenericRepository<TInput> repository, string fileName, params Expression<Func<TInput, object>>[] includeProperties)
		{
			this.Export(repository, fileName, null, null, includeProperties);
		}

		public void Export(IGenericRepository<TInput> repository, string fileName, Expression<Func<TInput, TInput>> projectFunc, params Expression<Func<TInput, object>>[] includeProperties)
		{
			this.Export(repository, fileName, projectFunc, null, includeProperties);
		}

		public void Export(IGenericRepository<TInput> repository, string fileName, Expression<Func<TInput, bool>> wherePredicate, params Expression<Func<TInput, object>>[] includeProperties)
		{
			this.Export(repository, fileName, null, wherePredicate, includeProperties);
		}

		public void Export(IGenericRepository<TInput> repository, string fileName, Expression<Func<TInput, TInput>> projectFunc, Expression<Func<TInput, bool>> wherePredicate, params Expression<Func<TInput, object>>[] includeProperties)
		{
			repository.DisableProxyCreation();
			var data = repository.Project(projectFunc, wherePredicate, includeProperties).ToArray();
			repository.EnableProxyCreation();

			this.Export(data, fileName);
		}

		public void Export(TInput[] data, string fileName)
		{
			if (IsMultiple)
			{
				var output = this.SerializeMultiple(data);
				var outputWithID = new List<Tuple<int, TOutput>>();

				for (int itemIndex = 0; itemIndex < output.Length; itemIndex++)
				{
					outputWithID.Add(Tuple.Create(data[itemIndex].ID, output[itemIndex]));
				}

				this.SaveToFileMultiple(fileName, outputWithID.ToArray());
			}
			else
			{
				var output = this.Serialize(data);
				this.SaveToFile(this.GetFilePath(fileName), output);
			}
		}

		public string GetFilePath(string fileName)
		{
			var filePath = Path.Combine(this.FolderPath, fileName + this.FileExtension);
			return filePath;
		}
	}
}