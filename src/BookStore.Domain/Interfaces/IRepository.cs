using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity 
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task Update(T entity);
        Task Remove(T entity);
        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges(); 
    }
}
