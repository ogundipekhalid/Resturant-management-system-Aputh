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
    public class EateryRepositry : BaseRepositry<Eatery>, IEateryRepositry
    {
        public EateryRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<Eatery> Get(int id)
        {
            var Eatery = await _context.Eateries
                .Where(a => a.IsDeleted == false)
                .Include(a => a.Branches)
                .Include(s => s.Staffs)
                .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
            return Eatery;
        }

        public async Task<Eatery> DeleteAsync(int EateryId)
        {
            var eatery = await _context.Eateries
            .FirstOrDefaultAsync(x => x.Id == EateryId);
            _context.Eateries.Remove(eatery);
            await _context.SaveChangesAsync();
            return eatery;
        }


        public async Task<Eatery> Getdetails(int id)
        {
            var Eatery = await _context.Eateries
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.Id == id);
            return Eatery;
        }

        public async Task<Eatery> Get(Expression<Func<Eatery, bool>> expression)
        {
            var eatert = await _context.Eateries
                .Include(a => a.Branches)
                .Include(a => a.Staffs)
                .ThenInclude(a => a.User)
                .Where(a => !a.IsDeleted)
                .FirstOrDefaultAsync(expression);
                return eatert;
        }


        public async Task<IEnumerable<Eatery>> GetAll()
        {
            return await _context.Eateries.ToListAsync();
        }

        public async Task<IEnumerable<Eatery>> GetSelected(List<int> ids)
        {
            var Eatery = await _context.Eateries
                .Include(a => a.Branches)
                .Include(s => s.Staffs)
                .ThenInclude(a => a.User)
                .Where(a => a.IsDeleted == false)
                .Where(a => ids.Contains(a.Id)).ToListAsync();
            return Eatery;
        }

        public async Task<IEnumerable<Eatery>> GetSelected(Expression<Func<Eatery, bool>> expression)
        {
            return  await _context.Eateries
                .Include(a => a.Branches)
                .Include(s => s.Staffs)
                .ThenInclude(a => a.User)
                .Where(a => a.IsDeleted == false)
                .Where(expression)
                .ToListAsync();
             
        }
    }
}
