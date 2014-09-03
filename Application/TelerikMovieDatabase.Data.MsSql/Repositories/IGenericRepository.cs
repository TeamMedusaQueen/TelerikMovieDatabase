using System;
using System.Linq;
using System.Linq.Expressions;

namespace TelerikMovieDatabase.Data.MsSql.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> All(params Expression<Func<TEntity, object>>[] includeProperties);

		IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties);

		IQueryable<TEntity> Project(Expression<Func<TEntity, TEntity>> projectFunc, Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties);

		void Add(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);

		void Detach(TEntity entity);

		void DisableProxyCreation();

		void EnableProxyCreation();
	}
}