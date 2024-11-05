using ADVADemo.Application.Specifications;
using ADVADemo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Infrastructure.Repository
{
    public static class QueryCreator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> CreateQuery(IQueryable<TEntity> Basequery,ISpecification<TEntity> spec)
        {
            var query = Basequery;
            if (spec.Where is not null) 
            { 
                query = query.Where(spec.Where);
            }
            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (currentQuery, includ) => currentQuery.Include(includ));
            return query;
        }
    }
}
