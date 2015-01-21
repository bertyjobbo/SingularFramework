using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;

namespace Singular.Core.Data.EntityFramework
{
    public static class EntityFrameworkRepositoryExtensions
    {
        
        public static T QueryableFind<T>(this IQueryable<T> set, string primaryKeyName, string primaryKeyValue)
        {
            return set.FilterByValueFirstOrDefault(primaryKeyName, primaryKeyValue);
        }

        public static T FilterByValueFirstOrDefault<T>(this IQueryable<T> obj, string propertyName,
            string propertyValue)
        {
            if (!string.IsNullOrEmpty(propertyValue))
            {
                var lambda = getFilterLambda<T>(propertyName, propertyValue);

                return obj.FirstOrDefault(lambda);
            }
            return default(T);
        }

        private static readonly Dictionary<string, Expression> _lambdaCache = new Dictionary<string, Expression>(); 

        private static Expression<Func<T,bool>>  getFilterLambda<T>(string propertyName, string propertyValue)
        {
            var key = typeof (T).FullName + ":" + propertyName;
            if (_lambdaCache.ContainsKey(key)) return (Expression<Func<T, bool>>) _lambdaCache[key];


            Expression<Func<T, string, bool>> expression =
                    (ex, value) => SqlFunctions.PatIndex(propertyValue.ToString(),//.Trim(),
                        value.ToString()/*.Trim()*/) > 0;

            var newSelector = Expression.Property(expression.Parameters[0], propertyName);
            //propertySelector.Body.Replace(
            //propertySelector.Parameters[0],
            //expression.Parameters[0]);

            var body = expression.Body.Replace(expression.Parameters[1],
                newSelector);
            var lambda = Expression.Lambda<Func<T, bool>>(
                body, expression.Parameters[0]);
            
            _lambdaCache.Add(key,lambda);

            return lambda;
        }

        public static IQueryable<T> FilterByValue<T>(this IQueryable<T> obj, string propertyName, string propertyValue)
        {
            if (!string.IsNullOrEmpty(propertyValue))
            {
                var lambda = getFilterLambda<T>(propertyName, propertyValue);

                return obj.Where(lambda);
            }
            else
                return obj;
        }

        public class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression _from, _to;
            public ReplaceVisitor(Expression from, Expression to)
            {
                this._from = from;
                this._to = to;
            }
            public override Expression Visit(Expression node)
            {
                return node == _from ? _to : base.Visit(node);
            }
        }

        public static Expression Replace(this Expression expression,
            Expression searchEx, Expression replaceEx)
        {
            return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
        }
    }
}
