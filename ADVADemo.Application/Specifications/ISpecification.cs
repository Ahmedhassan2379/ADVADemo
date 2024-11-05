using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Specifications
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Where { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public Expression<Func<T, object>> OrderBy { get; }
        int Take { get; }
        int Skip { get; }
        public bool IsPagingEnabled { get; }
    }
}
