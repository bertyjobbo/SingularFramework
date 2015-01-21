using System;
using System.Data.Entity;
using Singular.Core.Context;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Transaction;

namespace Singular.Core.Data.EntityFramework
{
    /// <summary>
    /// Unit of work
    /// </summary>
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        // fields
        private readonly DbContext _ctx;
        private readonly Type _typeOfEntityBase = typeof(EntityBase);
        private readonly ISingularContext _sCtx;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="sCtx"></param>
        public EntityFrameworkUnitOfWork(DbContext ctx, ISingularContext sCtx)
        {
            _ctx = ctx;
            _sCtx = sCtx;
        }

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        public TransactionResult Commit()
        {
            var res = new TransactionResult();
            try
            {
                foreach (var mod in _ctx.ChangeTracker.Entries())
                {
                    if (mod.Entity.GetType().IsSubclassOf(_typeOfEntityBase) && (mod.State == EntityState.Added || mod.State == EntityState.Modified))
                    {
                        var modCast = (EntityBase)mod.Entity;
                        if (mod.State == EntityState.Added)
                        {
                            modCast.Active = true;
                            modCast.Created = DateTime.UtcNow;
                            modCast.CreatedBy = _sCtx.CurrentUser.Id;
                        }
                        if (mod.State == EntityState.Modified)
                        {
                            modCast.UpdatedBy = _sCtx.CurrentUser.Id;
                            modCast.Updated = DateTime.UtcNow;
                        }
                    }
                }

                var resInt = _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.AddException(ex);
            }
            return res;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}