using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Logging.Logic.Helpers
{
    internal class SubstituteParameterVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> Sub = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;

            if (Sub.TryGetValue(node, out newValue))
            {
                return newValue;
            }

            return node;
        }
    }
}
