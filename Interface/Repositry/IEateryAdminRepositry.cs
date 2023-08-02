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
    public interface IEateryAdminRepositry :IBaseRepository<EateryAdmin>
    {
        Task<EateryAdmin> Get(int id);
        Task<EateryAdmin> Get(Expression<Func<EateryAdmin, bool>> expression);
        Task<IEnumerable<EateryAdmin>> GetSelected(List<int> ids);
        Task<IEnumerable<EateryAdmin>> GetSelected(Expression<Func<EateryAdmin, bool>> expression);
        Task<IEnumerable<EateryAdmin>> GetAll();
    }
}
