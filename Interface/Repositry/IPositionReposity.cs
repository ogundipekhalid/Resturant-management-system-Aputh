using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Identity;

namespace RDMS.Interface.Repositry
{
    public interface IPositionReposity : IBaseRepository<Position>
    {
         Task<Position> Get(int id);
        Task<Position> Get(Expression<Func<Position, bool>> expression);
        Task<IEnumerable<Position>> GetSelected(List<int> ids);
        Task<IEnumerable<Position>> GetSelected(Expression<Func<Position, bool>> expression);
        Task<IEnumerable<Position>> GetAll();
    }
}