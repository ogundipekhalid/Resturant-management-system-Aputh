using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Identity;

// using RDMS.Models.Entity;

namespace RDMS.Implimentation.Repositry
{
    public class RoleRepositry : BaseRepositry<Role>, IRoleRepositry
    {
        public RoleRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<Role> Get(int id)
        {
            var role = await _context.Roles
                .Include(a => a.UserRoles)
                //.ThenInclude(a => a.Role)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.Id == id);
            return role;
        }

         public async Task<Role> Getdetails(int id)
        {
            var role = await _context.Roles
             
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.Id == id);
            return role;
        }

         public async Task<Role> DeleteAsync(int id)
        {
            var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return role;
        }


        public async Task<Role> Get(Expression<Func<Role, bool>> expression)
        {
            var role = await _context.Roles
            .Include(a => a.UserRoles)
            .ThenInclude(a => a.User)
            .Where(a => a.IsDeleted == false).FirstOrDefaultAsync(expression);
            return role;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetSelected(List<int> ids)
        {
            return await _context.Roles
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.User)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetSelected(Expression<Func<Role, bool>> expression)
        {
            var role = await _context.Roles
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.User)
                .Where(a => a.IsDeleted == false)
                .Where(expression)
                .ToListAsync();
            return role;
        }
    }
}
