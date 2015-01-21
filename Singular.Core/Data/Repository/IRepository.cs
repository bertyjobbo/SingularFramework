using System;
using System.Linq;
using System.Linq.Expressions;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Transaction;

namespace Singular.Core.Data.Repository
{
    public interface IRepository<T> where T : EntityBase
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
        /// <returns></returns>
        T Create(T model);

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
        /// <returns></returns>
        T Update(T model);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Delete(T model);
    }
}