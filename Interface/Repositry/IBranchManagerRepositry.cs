using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Actors;

namespace RDMS.Interface.Repositry
{
    public interface IBranchManagerRepositry : IBaseRepository<BranchManager> 
    {
       Task<BranchManager> Get(int id);
        Task<BranchManager> Get(Expression<Func<BranchManager, bool>> expression);
        Task<IEnumerable<BranchManager>> GetSelected(List<int> ids);
        Task<IEnumerable<BranchManager>> GetSelected(Expression<Func<BranchManager, bool>> expression);
        Task<IEnumerable<BranchManager>> GetAll();
    }
}