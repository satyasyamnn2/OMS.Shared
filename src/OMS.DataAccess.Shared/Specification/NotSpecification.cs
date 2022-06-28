using System;
using System.Linq;
using System.Linq.Expressions;

namespace OMS.DataAccess.Shared.Specification
{
    public class NotSpecification<T> : Specification<T>
    {
        private Specification<T> _specification;
        public NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }
        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = _specification.ToExpression();
            UnaryExpression notExpression = Expression.Not(expression);
            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}
