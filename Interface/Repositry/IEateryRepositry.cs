using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;

namespace RDMS.Interface.Repositry
{
    public interface IEateryRepositry : IBaseRepository<Eatery>
    {
        Task<Eatery> Get(int id);
       Task<Eatery> DeleteAsync(int EateryId);
        Task<Eatery> Get(Expression<Func<Eatery, bool>> expression);
        Task<IEnumerable<Eatery>> GetSelected(List<int> ids);
        Task<Eatery> Getdetails(int id);
        Task<IEnumerable<Eatery>> GetSelected(Expression<Func<Eatery, bool>> expression);
        Task<IEnumerable<Eatery>> GetAll();
    }
}
