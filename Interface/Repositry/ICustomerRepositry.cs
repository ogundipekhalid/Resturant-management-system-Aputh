using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Actors;
using RDMS.Models.Entity;

namespace RDMS.Interface.Repositry
{
    public interface ICustomerRepositry  : IBaseRepository<Customer>
    {
        Task<Customer> Get(int id);
         Task<Customer> GetDetails(int id);
         Task<Customer> getadcu(Expression<Func<Customer, bool>> expression);
        Task<bool> CheckEmailAsnc(string email);
        Task<Customer> Get(Expression<Func<Customer, bool>> expression);
        Task<IEnumerable<Customer>> GetSelected(List<int> ids);
        Task<IEnumerable<Customer>> GetSelected(Expression<Func<Customer, bool>> expression);
        Task<IEnumerable<Customer>> GetAll();
        Task<IEnumerable<Customer>> GetAllWithOreder();
    }
}