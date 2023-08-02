using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Actors;
using RDMS.Models.Entity;

namespace RDMS.Implimentation.Repositry
{
    public class EateryAdminRepositry : BaseRepositry<EateryAdmin>, IEateryAdminRepositry
    {
        public EateryAdminRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<EateryAdmin> Get(Expression<Func<EateryAdmin, bool>> expression)
        {
            var EateryManager = await _context.EateryAdmins
                .Include(a => a.User)
                .ThenInclude(a => a.UserRoles)
                .Include(a => a.Eatery)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
            return EateryManager;
        }

        public async Task<IEnumerable<EateryAdmin>> GetAll()
        {
            return await _context.EateryAdmins.ToListAsync();
        }

        public async Task<EateryAdmin> Get(int id)
        {
            var eateryManager = await _context.EateryAdmins
                .Include(a => a.User)
                .ThenInclude(a => a.UserRoles)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.User.Id == id);
            return eateryManager;
        }

        public async Task<IEnumerable<EateryAdmin>> GetSelected(List<int> ids)
        {
            var eatermanager =  await _context.EateryAdmins
                .Include(a => a.User)
                .ThenInclude(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .Include(a => a.Eatery)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
                .ToListAsync();
                return eatermanager;
        } 

        public async Task<IEnumerable<EateryAdmin>> GetSelected(
            Expression<Func<EateryAdmin, bool>> expression
        )
        {
            return await _context.EateryAdmins
                .Include(a => a.User)
                .ThenInclude(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .Include(a => a.Eatery)
                .Where(expression)
                .ToListAsync();
        }
    }
}
