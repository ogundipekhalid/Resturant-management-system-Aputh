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
    public interface IFoodRepositry :IBaseRepository<Food>
    {
         Task<Food> Get(int id);
        Task<Food> Get(Expression<Func<Food, bool>> expression);
        Task<Food> DeleteAsync(int foodId);
        Task<Food> GetDetails(Expression<Func<Food, bool>> expression);
        Task<IEnumerable<BranchFood>> GetSelectedlist(int branchId);
        Task<IEnumerable<BranchFood>> GetSelectedlast(Expression<Func<BranchFood, bool>> expression);
        Task<IEnumerable<Food>> GetSelected(List<int> ids);
        Task<IEnumerable<Food>> GetSelected(Expression<Func<Food, bool>> expression);
        Task<IEnumerable<Food>> GetAll();
        Task<IEnumerable<BranchFood>> BranchFoods();
        Task<Branch> GetName(string name);
        Task<IEnumerable<Food>> SearchFoods(Expression<Func<Food, bool>> expression);

        
    }
}