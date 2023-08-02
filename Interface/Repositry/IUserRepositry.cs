using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Identity;
// using RDMS.Models.Entity;

namespace RDMS.Interface.Repositry
{
    public interface IUserRepositry :IBaseRepository<User>
    {
        Task<User> Get(int id);
        Task<User> GetDetails(Expression<Func<User, bool>> expression);
        Task<User> Get(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetSelected(List<int> ids);
        Task<IEnumerable<User>> GetSelected(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetEmail(string email);
        

    }
}