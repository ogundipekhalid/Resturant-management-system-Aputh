using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Actors;

namespace RDMS.Implimentation.Repositry
{
    public class BranchManagerRepositry : BaseRepositry<BranchManager>, IBranchManagerRepositry
    {
        public BranchManagerRepositry(ResturantDbContext context)
        {
            _context = context;
        }
        public async Task<BranchManager> Get(int id)
        {
            var branchmanager = await _context.BranchManagers
                .Include(a => a.User)
                .ThenInclude(a => a.UserRoles)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.User.Id == id);
            return branchmanager;
        }
        public async Task<BranchManager> GetDetails(int id)
        {
            var branchmanager = await _context.BranchManagers
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.User.Id == id);
            return branchmanager;
        }

        public async Task<BranchManager> Get(Expression<Func<BranchManager, bool>> expression)
        {
             var branchmanager = await _context.BranchManagers
             .Include(b => b.User)
             .ThenInclude(b => b.UserRoles)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
                return branchmanager;
        }

        public async Task<IEnumerable<BranchManager>> GetAll()
        {
            return await _context.BranchManagers.ToListAsync();
        }
        

        public async Task<IEnumerable<BranchManager>> GetSelected(List<int> ids)
        {
             var branchmanager =  await _context.BranchManagers
                .Include(a => a.User)
                .ThenInclude(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .Include(a => a.Branch)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
                .ToListAsync();
            return  branchmanager;
        }

        public async Task<IEnumerable<BranchManager>> GetSelected(Expression<Func<BranchManager, bool>> expression)
        {
            var branchmanager = await _context.BranchManagers
                .Include(a => a.User)
                .ThenInclude(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .Include(a => a.Branch)
                .Where(expression)
                .ToListAsync();
                return branchmanager;
            }
    }
}