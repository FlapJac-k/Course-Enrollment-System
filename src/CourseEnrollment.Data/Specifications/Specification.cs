using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CourseEnrollment.Data.Specifications
{
    public abstract class Specification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
        protected Specification() : this(null) { }
        public Expression<Func<T, bool>>? Criteria => criteria;
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }
            return query;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

    }
}

