using System.Collections.Generic;

namespace Singular.Core.Transaction
{
    public class TransactionResult
    {
        public TransactionResult ()
        {
            Errors = new List<string>();
        }
        public IList<string> Errors { get; private set; }
        public bool Success { get { return Errors != null && Errors.Count > 0; } }
    }
}
