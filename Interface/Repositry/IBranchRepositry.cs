using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Entity;

namespace RDMS.Interface.Repositry
{
    public interface IBranchRepositry : IBaseRepository<Branch> 
    {
        Task<Branch> Get(int id);
        Task<Branch> GetName(string name);
        Task<Branch> GetDetails(int id);
        Task<IEnumerable<BranchFood>> GetFoodByBranchId(int branchId);
        Task<Branch> Get(Expression<Func<Branch, bool>> expression);
        Task<IEnumerable<Branch>> GetSelected(List<int> ids);
        Task<IEnumerable<Branch>> GetSelected(Expression<Func<Branch, bool>> expression);
        Task<IEnumerable<Branch>> GetBranchFoods (Expression<Func<Branch, bool>> expression);
        Task<IEnumerable<Branch>> GetAll();
        Task<IEnumerable<BranchFood>> BranchFoods();
         Task<IEnumerable<Food>> SearchFoods(Expression<Func<Food, bool>> expression);

    }
}
