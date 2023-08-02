using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Identity;

namespace RDMS.Interface.Repositry
{
    public interface IRoleRepositry : IBaseRepository<Role>
    {
        Task<Role> Get(int id);
        Task<Role> Getdetails(int id);
        Task<Role> DeleteAsync(int id);
        Task<Role> Get(Expression<Func<Role, bool>> expression);
        Task<IEnumerable<Role>> GetSelected(List<int> ids);
        Task<IEnumerable<Role>> GetSelected(Expression<Func<Role, bool>> expression);
        Task<IEnumerable<Role>> GetAll();
    }
}