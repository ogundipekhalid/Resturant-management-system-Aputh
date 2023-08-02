using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Entity;

namespace RDMS.Interface.Repositry
{
    public interface ICategoryRepositry :IBaseRepository<Category>
    {
        Task<Category> Get(int id);
        Task<Category> DeleteAsync(int CartiogyId);
        Task<Category> Get(Expression<Func<Category, bool>> expression);
        Task<IEnumerable<Category>> GetSelected(List<int> ids);
        Task<IEnumerable<Category>> GetSelected(Expression<Func<Category, bool>> expression);
        Task<IEnumerable<Category>> GetAll();
    }
}
