using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Singular.Useful;

namespace Singular.Core.Transaction
{
    public class TransactionResult
    {
        public TransactionResult()
        {
            Errors = new List<string>();
        }
        public IList<string> Errors { get; private set; }
        public virtual bool Success { get { return Errors == null || Errors.Count < 1; } }

        public void AddError(string err)
        {
            Errors.Add(err);
        }
    }

    public class TransactionResult<T> : TransactionResult where T:class
    {
        public TransactionResult()
            : base()
        {
            PropertyErrors = new List<PropertyError>();
        }

        public IList<PropertyError> PropertyErrors { get; set; }

        public override bool Success
        {
            get { return base.Success && (PropertyErrors == null || PropertyErrors.Count <1); }
        }

        public void AddPropertyError<TProperty>(Expression<Func<T,TProperty>> expression, string err)
        {
            var prop = SingularExpressionHelper.GetPropertyName(expression);
            PropertyErrors.Add(new PropertyError
            {
                PropertyName = prop, ErrorMessage = err
            });
        }
    }
}
