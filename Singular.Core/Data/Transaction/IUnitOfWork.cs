using System;

namespace Singular.Core.Data.Transaction
{
    /// <summary>
    /// IUnit of work
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        TransactionResult Commit();
    }
}