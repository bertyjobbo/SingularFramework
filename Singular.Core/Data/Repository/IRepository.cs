using System;
using System.Linq;
using System.Linq.Expressions;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Transaction;

namespace Singular.Core.Data.Repository
{
    public interface IRepository<T>: IDisposable where T : EntityBase
    {
        /// <summary>
        /// Entities
        /// </summary>
        IQueryable<T> Entities { get; }

        /// <summary>
        /// Readonly ents
        /// </summary>
        IQueryable<T> EntitiesReadOnly { get; }

        /// <summary>
        /// Entities including sub properties
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        IQueryable<T> EntitiesIncluding<TProperty>(params Expression<Func<T, TProperty>>[] propertyExpressions);

        /// <summary>
        /// Entities, read only, including sub properties
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        IQueryable<T> EntitiesReadOnlyIncluding<TProperty>(params Expression<Func<T, TProperty>>[] propertyExpressions);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        TransactionResult<T> Create(T model, bool commit = true);

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Read(int id);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        TransactionResult<T> Update(T model, bool commit = true);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        TransactionResult<T> Delete(T model, bool commit = true);

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        int Commit();
    }
}