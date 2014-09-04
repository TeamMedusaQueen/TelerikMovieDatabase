namespace TelerikMovieDatabase.Data.MsSql.Repositories
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Linq;
	using System.Linq.Expressions;
	using TelerikMovieDatabase.Data.MsSql;

	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		private readonly ITelerikMovieDatabaseMsSqlContext context;
		private readonly IDbSet<TEntity> set;

		public GenericRepository(ITelerikMovieDatabaseMsSqlContext context)
		{
			this.context = context;
			this.set = context.Set<TEntity>();
		}

		public IQueryable<TEntity> All(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			IQueryable<TEntity> set = this.set;
			foreach (var property in includeProperties)
			{
				set = set.Include(property);
			}

			return set.AsQueryable();
		}

		public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var data = this.All(includeProperties);

			if (wherePredicate != null)
			{
				data = data.Where(wherePredicate);
			}

			return data;
		}

		public IQueryable<TEntity> Project(Func<TEntity, TEntity> projectFunc, Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var data = this.SearchFor(wherePredicate, includeProperties);

			if (projectFunc != null)
			{
				data = data.ToArray()
					.Select(projectFunc)
					.AsQueryable();
			}

			return data;
		}

		public void Add(TEntity entity)
		{
			var entry = AttachIfDetached(entity);
			entry.State = EntityState.Added;
		}

		public void Update(TEntity entity)
		{
			var entry = AttachIfDetached(entity);
			entry.State = EntityState.Modified;
		}

		public void Delete(TEntity entity)
		{
			var entry = AttachIfDetached(entity);
			entry.State = EntityState.Deleted;
		}

		public void Detach(TEntity entity)
		{
			var entry = this.context.Entry(entity);
			entry.State = EntityState.Detached;
		}

		public void DisableProxyCreation()
		{
			this.context.Configuration.ProxyCreationEnabled = false;
		}

		public void EnableProxyCreation()
		{
			this.context.Configuration.ProxyCreationEnabled = true;
		}

		private DbEntityEntry AttachIfDetached(TEntity entity)
		{
			var entry = this.context.Entry(entity);
			if (entry.State == EntityState.Detached)
			{
				this.set.Attach(entity);
			}

			return entry;
		}
	}
}