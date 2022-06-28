using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OMS.DataAccess.Shared.Specification
{
    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new IdentitySpefication<T>();
        public Specification()
        {
            Includes = new List<Expression<Func<T, object>>>();
        }
        public List<Expression<Func<T, object>>> Includes { get; }
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
        public Specification<T> And(Specification<T> specification)
        {
            if (this == All) return specification;
            if (specification == All) return this;

            return new AndSpecification<T>(this, specification);
        }
        public Specification<T> Or(Specification<T> specification)
        {
            if (this == All || specification == All) return All;
            return new OrSpecification<T>(this, specification);
        }
        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
        public abstract Expression<Func<T, bool>> ToExpression();
    }
}