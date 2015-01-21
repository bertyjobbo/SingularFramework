using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Repository;
using Singular.Core.Data.Transaction;

namespace Singular.Core.Data.EntityFramework
{
    /// <summary>
    /// Entity framework IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityFramework<T> : IRepository<T> where T : EntityBase
    {
        // fields
        private readonly DbContext _ctx;
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="uow"></param>
        public EntityFramework(DbContext ctx, IUnitOfWork uow)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = true;
            _uow = uow;
        }

        /// <summary>
        /// Entities
        /// </summary>
        public virtual IQueryable<T> Entities
        {
            get { return _ctx.Set<T>(); }
        }

        /// <summary>
        /// Readonly ents
        /// </summary>
        public virtual IQueryable<T> EntitiesReadOnly
        {
            get
            {
                return _ctx.Set<T>().AsNoTracking();
            }
        }

        /// <summary>
        /// Entities including sub properties
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual IQueryable<T> EntitiesIncluding<TProperty>(params Expression<Func<T, TProperty>>[] propertyExpressions)
        {
            var output = Entities;
            foreach (var propertyExpression in propertyExpressions)
            {
                output = output.Include(propertyExpression);
            }
            return output;
        }

        /// <summary>
        /// Entities, read only, including sub properties
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual IQueryable<T> EntitiesReadOnlyIncluding<TProperty>(params Expression<Func<T, TProperty>>[] propertyExpressions)
        {
            var output = EntitiesReadOnly;
            foreach (var propertyExpression in propertyExpressions)
            {
                output = output.Include(propertyExpression);
            }
            return output;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T Create(T model)
        {
            var output = _ctx.Set<T>().Add(model);
            _ctx.Entry(output).State = EntityState.Added;
            return output;
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Read(int id)
        {
            return _ctx.Set<T>().Find(id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T Update(T model)
        {
            var output = _ctx.Set<T>().Attach(model);
            _ctx.Entry(output).State = EntityState.Modified;
            return output;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T Delete(T model)
        {
            var output = _ctx.Set<T>().Attach(model);
            _ctx.Entry(output).State = EntityState.Deleted;
            return output;
        }
    }
}