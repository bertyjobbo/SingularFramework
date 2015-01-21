using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Singular.Useful;

namespace Singular.Core.Data.Transaction
{
    public class TransactionResult
    {
        public TransactionResult()
        {
            Errors = new List<string>();
            Exceptions = new List<Exception>();
        }
        public IList<string> Errors { get; private set; }

        public virtual bool Success
        {
            get { return (Errors.Count + Exceptions.Count) < 1; }
        }

        public void AddError(string err)
        {
            Errors.Add(err);
        }

        public void AddException(Exception ex)
        {
            Exceptions.Add(ex);
            Errors.Add(ex.MessageIncludingInnerException());
        }

        public IList<Exception> Exceptions { get; private set; }
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
