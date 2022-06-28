using System;
using System.Linq.Expressions;

namespace OMS.DataAccess.Shared.Specification
{
    internal class IdentitySpefication<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;   
        }
    }
}
