using ADVADemo.Application.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsyncWithSpec(ISpecification<T> spec);
        Task<int> GetCountAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpec(ISpecification<T> spec);
        Task<List<T>> GetEmployeesByIdWithSpec(ISpecification<T> spec);
        Task<List<T>> GetByDepartIdWithSpec(ISpecification<T> spec);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);


    }
}
