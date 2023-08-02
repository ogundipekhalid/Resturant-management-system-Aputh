using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Models.Entity;

namespace RDMS.Interface.Repository
{
    public interface IBaseRepository<T> where T: BaseEntity, new()
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        void Save();  
         Task Saves();
        // //
        // Task<T> GetAsync(Expression<Func<T, bool>> expression);
        // Task<ICollection<T>> GetAllAsync();
        // Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> expression);


    }
}