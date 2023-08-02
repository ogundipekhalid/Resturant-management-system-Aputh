using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Actors;
using RDMS.Models.Entity;
using RDMS.Models.Identity;

namespace RDMS.Interface.Repositry
{
    public interface IStaffRepositry  : IBaseRepository<Staff>
    {
        Task<Staff> Get(int id);
        Task<bool> DeleteAsync(Staff staff);
        Task<Staff> GetDetails(Expression<Func<Staff, bool>> expression);
        Task<Staff> Get(Expression<Func<Staff, bool>> expression);
        Task<IEnumerable<Staff>> GetSelected(List<int> ids);
        Task<IEnumerable<Staff>> GetSelected(Expression<Func<Staff, bool>> expression);
        Task<IEnumerable<Staff>> GetAll();        
    }
}