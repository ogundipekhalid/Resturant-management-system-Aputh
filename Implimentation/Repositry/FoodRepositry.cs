using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.Models.Identity;

namespace RDMS.Implimentation.Repositry
{
    public class FoodRepositry : BaseRepositry<Food>, IFoodRepositry
    {
        public FoodRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<Food> Get(Expression<Func<Food, bool>> expression)
        {
            var food = await _context.Foods
                .Include(f => f.Category)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
            return food;
        }
        public async Task<Food> GetDetails(Expression<Func<Food, bool>> expression)
        {
            var food = await _context.Foods
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
            return food;
        }

        public async Task<Food> DeleteAsync(int foodId)
        {
            var food = await _context.Foods
            .FirstOrDefaultAsync(x => x.Id == foodId);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return food;
        }

        public async Task<IEnumerable<Food>> GetAll()
        {
            return await _context.Foods
            .Include(f => f.BranchFoods)
            .ThenInclude(f => f.Branch)
            .ToListAsync();
        }

        public async Task<Food> Get(int id)
        {
            var food = await _context.Foods
            .Include(f => f.BranchFoods)
            .ThenInclude(f => f.Branch)
            .Include(c => c.Category)
            //// .Include(c => c.)
            .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return food;
        }

        public async Task<IEnumerable<Food>> GetSelected(List<int> ids)
        {
             return await _context.Foods
            .Include(f => f.BranchFoods)
            .ThenInclude(f => f.Branch)
             .Include(c => c.Category)
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<IEnumerable<Food>> GetSelected(Expression<Func<Food, bool>> expression)
        {
            return await _context.Foods
            .Include(f => f.BranchFoods)
            .ThenInclude(f => f.Branch)
             .Include(c => c.Category)
             .Where(a => a.IsDeleted == false).Where(expression)
            .ToListAsync();
        }

           public async Task<IEnumerable<BranchFood>> GetSelectedlist(int branchId)
        {
            return await _context.BranchFoods
            .Include(f => f.Branch)
            .Include(f => f.Food)
            .Include(f => f.Branch.Address)
             .Where(a => a.IsDeleted == false).Where( x => x.Branch.Id ==  branchId)
            .ToListAsync();
        }
        public async Task<IEnumerable<BranchFood>> GetSelectedlast(Expression<Func<BranchFood, bool>> expression)
        {
            return await _context.BranchFoods
            .Include(f => f.Branch)
            .Include(f => f.Food)
            .Include(f => f.Branch.Address)
             .Where(a => a.IsDeleted == false).Where(expression)
            .ToListAsync();
        }


        public async Task<IEnumerable<BranchFood>> BranchFoods()
        {
            return await _context.BranchFoods.ToListAsync();
        }

        public async Task<Branch> GetName(string name)
        {
             var branck = await _context.Branches
                .Include(a => a.Eatery)
                .Include(a => a.Address)
                .Where(a => !a.IsDeleted)
                .FirstOrDefaultAsync(a => a.Name == name);
            return branck;
        }

        public async Task<IEnumerable<Food>> SearchFoods(Expression<Func<Food, bool>> expression)
        {
            var foods = await _context.Foods.Where(expression).ToListAsync();

            return foods;
        }
        //    public  async Task<IList<Food>> SearchProductsProductName(string searchInput, User user)
        // {
        //     var input = searchInput.ToLower().Trim();
        //   //  var searchedOutput = await _context.Orders.Where(p => p.FarmerEmail.ToLower() == input && p.ProductLocalGovernment == user.Address.LocalGovernment || p.FarmerUserName.ToLower() == input && p.ProductLocalGovernment == user.Address.LocalGovernment || p.ProductName.ToLower() == input && p.ProductLocalGovernment == user.Address.LocalGovernment).ToListAsync();
        //     var searchedOutput = await _context.Foods.Where(p => p.Customer.User.Email.ToLower() == input && p.Address == user.Address || p.Customer.User.FirstName.ToLower() == input && p.Address.City == user.Address.State || p.Customer.Orders.ToString().ToLower() == input && p.Address == user.Address).ToListAsync();
        //     return searchedOutput;
        // }
    }
}
