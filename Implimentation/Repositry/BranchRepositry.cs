using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Entity;

namespace RDMS.Implimentation.Repositry
{
    public class BranchRepositry : BaseRepositry<Branch>, IBranchRepositry
    {
        public BranchRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BranchFood>> BranchFoods()
        {
            return await _context.BranchFoods.ToListAsync();
        }

        public async Task<Branch> Get(Expression<Func<Branch, bool>> expression)
        {
            var branch = await _context.Branches
                .Include(a => a.Address)
                .Include(a => a.Eatery)
                .Include(a => a.Staffs)
                .Include(a => a.Orders)
                .Include(a => a.BranchFoods)
                .Where(a => !a.IsDeleted)
                .FirstOrDefaultAsync(expression);
            return branch;
        }

        public async Task<Branch> Get(int id)
        {
            var branck = await _context.Branches
                .Include(a => a.Eatery)
                .Include(a => a.Address)
                .Include(b => b.Staffs)//
                .ThenInclude(b => b.User)//
                .Where(a => !a.IsDeleted)
                .FirstOrDefaultAsync(a => a.EateryId == id);
            return branck;
        }
        public async Task<Branch> GetDetails(int id)
        {
            var branck = await _context.Branches
                .Where(a => !a.IsDeleted)
                .Include(a => a.Eatery)
                .Include(a => a.Address)
                .FirstOrDefaultAsync(a => a.Id == id);
            return branck;
        }

         public async Task<Branch> GetDe(int id)
        {
            var branch =  _context.Branches
                .Include(a => a.Eatery)
                .FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                //.SingleOrDefaultAsync(x => x.Eatery.UserId == id);
            return branch;
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

        public async Task<IEnumerable<BranchFood>> GetFoodByBranchId(int branchId)
        {
            return await _context.BranchFoods
                .Include(c => c.Food)
                .Where(x => x.BranchId == branchId)
                .ToListAsync();
        }

        // public async Task<IEnumerable<Branch>> GetAll1()
        // {
        //       var addressIds = allbranch.Select(c => c.AddressId).ToList();

        //     var branchaddres= await _context.Branches
        //         .Where(a => !a.IsDeleted)
        //         .Include(x => x.Address)
        //         .Select(c  => c.AddressId)
        //         .ToListAsync();
        // }

        public async Task<IEnumerable<Branch>> GetAll()
        {
            return await _context.Branches
                .Where(a => a.IsDeleted == false)
                .Include(a => a.Eatery)
                .ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetSelected(List<int> ids)
        {
            return await _context.Branches
                .Include(a => a.Eatery)
                //.Include(a => a.Address)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Branch>> GetSelected(
            Expression<Func<Branch, bool>> expression)
        {
            return await _context.Branches
                .Include(a => a.Eatery)
               .Include(a => a.Address)
                .Where(a => a.IsDeleted == false)
                .Where(expression)
                .ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetBranchFoods(
            Expression<Func<Branch, bool>> expression
        )
        {
            var branch = await _context.Branches
                .Include(a => a.BranchFoods)
                .ThenInclude(x => x.Food)
                .Where(a => a.IsDeleted == false)
                .Where(expression)
                .ToListAsync();
            return branch;
        }

        public async Task<IEnumerable<Food>> SearchFoods(Expression<Func<Food, bool>> expression)
        {
            var foods = await _context.Foods.Where(expression).ToListAsync();
            return foods;
        } 
    }
}
