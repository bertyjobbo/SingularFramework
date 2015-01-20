using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Transaction;

namespace Singular.Core.Data.Service
{
    public interface IDataService<T> :IDisposable where T: EntityBase
    {
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
