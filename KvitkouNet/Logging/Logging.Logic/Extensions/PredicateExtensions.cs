using System;
using System.Linq.Expressions;
using Logging.Logic.Helpers;

namespace Logging.Logic.Extensions
{
    public static class PredicateExtensions
    {
        public static Expression<Func<T, bool>> Begin<T>(bool value = true)
        {
            return parameter => value;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
           Expression<Func<T, bool>> right)
        {
            return CombineLambdas(left, right, ExpressionType.AndAlso);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return CombineLambdas(left, right, ExpressionType.OrElse);
        }

        #region private

        private static Expression<Func<T, bool>> CombineLambdas<T>(this Expression<Func<T, bool>> left,
           Expression<Func<T, bool>> right, ExpressionType expressionType)
        {
            if (IsExpressionBodyConstant(left))
                return (right);

            var p = left.Parameters[0];

            var visitor = new SubstituteParameterVisitor();

            visitor.Sub[right.Parameters[0]] = p;

            Expression body = Expression.MakeBinary(expressionType, left.Body, visitor.Visit(right.Body));

            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        private static bool IsExpressionBodyConstant<T>(Expression<Func<T, bool>> left)
        {
            return left.Body.NodeType == ExpressionType.Constant;
        }

        #endregion
    }
}
