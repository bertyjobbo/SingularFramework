using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Repository;
using Singular.Core.Data.Transaction;

namespace Singular.Core.Data.Service
{
    /// <summary>
    /// Standard data service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StandardDataService<T>:IDataService<T> where T: EntityBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repo"></param>
        public StandardDataService(IRepository<T> repo)
        {
            Repository = repo;
        }

        /// <summary>
        /// Repository
        /// </summary>
        protected IRepository<T> Repository { get; private set; }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public virtual TransactionResult<T> Create(T model, bool commit = true)
        {
            return Repository.Create(model, commit);
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Read(int id)
        {
            return Repository.Read(id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public virtual TransactionResult<T> Update(T model, bool commit = true)
        {
            return Repository.Update(model, commit);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public virtual TransactionResult<T> Delete(T model, bool commit = true)
        {
            return Repository.Delete(model, commit);
        }

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        public virtual int Commit()
        {
            return Repository.Commit();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            Repository.Dispose();
        }
    }
}
