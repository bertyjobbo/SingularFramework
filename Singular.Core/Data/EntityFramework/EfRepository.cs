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
    public class EfRepository<T> : IRepository<T>, IDisposable where T : EntityBase
    {
        // fields
        private readonly DbContext _ctx;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx"></param>
        public EfRepository(DbContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = true;
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
        /// <param name="commit"></param>
        /// <returns></returns>
        public virtual TransactionResult<T> Create(T model, bool commit = true)
        {
            var res = new TransactionResult<T>();
            try
            {
                _ctx.Set<T>().Add(model);
                if (commit)
                {
                    Commit();
                }
            }
            catch (Exception ex)
            {
                res.AddError(ex.Message);
            }
            return res;
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
        /// <param name="commit"></param>
        /// <returns></returns>
        public virtual TransactionResult<T> Update(T model, bool commit = true)
        {
            var res = new TransactionResult<T>();
            try
            {
                _ctx.Set<T>().Attach(model);
                _ctx.Entry(model).State = EntityState.Modified;
                if (commit)
                {
                    Commit();
                }
            }
            catch (Exception ex)
            {
                res.AddError(ex.Message);
            }
            return res;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public virtual TransactionResult<T> Delete(T model, bool commit = true)
        {
            var res = new TransactionResult<T>();
            try
            {
                _ctx.Set<T>().Attach(model);
                _ctx.Entry(model).State = EntityState.Deleted;
                if (commit)
                {
                    Commit();
                }
            }
            catch (Exception ex)
            {
                res.AddError(ex.Message);
            }
            return res;
        }

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        public virtual int Commit()
        {
            return _ctx.SaveChanges();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            _ctx.Dispose();
        }
    }
}