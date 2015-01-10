using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Useful
{
    public static class SingularExpressionHelper
    {
        public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            if (propertyLambda.IsNotNull() && propertyLambda.Body.IsNotNull())
            {
                var expression = (MemberExpression)propertyLambda.Body;
                return expression.Member.Name;
            }
            
            throw new Exception("PropertyLambda parameter on SingularExpressionHelper.GetPropertyName is malformed");
        }
    }
}
