using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Identity;

namespace RDMS.Interface.Repositry
{
    public interface IAddressRepositry :IBaseRepository<Address> 
    {
        Task<Address> Get(int id);
        Task<Address> Get(Expression<Func<Address, bool>> expression);
        Task<IEnumerable<Address>> GetSelected(List<int> ids);
        Task<IEnumerable<Address>> GetSelected(Expression<Func<Address, bool>> expression);
        Task<IEnumerable<Address>> GetAll();
    }
}