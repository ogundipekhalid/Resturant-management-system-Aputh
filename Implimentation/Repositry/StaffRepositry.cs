using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Actors;
using RDMS.Models.Identity;

// using RDMS.Models.Entity;

namespace RDMS.Implimentation.Repositry
{
    public class StaffRepositry : BaseRepositry<Staff>, IStaffRepositry
    {
         public StaffRepositry(ResturantDbContext context)
        {
            _context = context;
        }
        public async Task<Staff> Get(int id)
        {
            var staff =  await _context.Staffs
            .Include(c => c.User)
            .Include(a => a.Branch)
            .Where(a => a.IsDeleted == false)
            .SingleOrDefaultAsync(x => x.Id == id);
            return staff;
        }

        public async Task<Staff> GetDetails(Expression<Func<Staff, bool>> expression)
        {
            var staff =  await _context.Staffs
            .Where(a => a.IsDeleted == false)
            .FirstOrDefaultAsync(expression);
            return staff;
        }
        public async Task<Staff> Get(Expression<Func<Staff, bool>> expression)
        {
            var staff =  await _context.Staffs
            .Include(a => a.User)
            .Include(a => a.Branch)
            .ThenInclude(a => a.Name)//n
            .Where(a => a.IsDeleted == false)
            .FirstOrDefaultAsync(expression);
            return staff;
        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            return await _context.Staffs.Include(c => c.User).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Staff staff)
        {
            _context.Staffs.Remove(staff);
            await  _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Staff>> GetSelected(List<int> ids)
        {
            var staff  =  await _context.Staffs
            .Include(a => a.User)
            .Include(a => a.Branch)
            .Where(a => a.IsDeleted == false)
            .Where(a => ids.Contains(a.Id)).ToListAsync();
            return staff;
        }

        public async Task<IEnumerable<Staff>> GetSelected(Expression<Func<Staff, bool>> expression)
        {
            var staff = await _context.Staffs
            .Include(a => a.User)
            .Include(a => a.Branch) 
            .Include(a => a.Eatery) 
            .Where(a => a.IsDeleted == false)
            .Where(expression).ToListAsync();
            return staff;
        }

         public async Task<IEnumerable<Staff>> GetSelecteds(Expression<Func<Staff, bool>> expression)
        {
            var staff = await _context.Staffs
            .Include(a => a.User)
            .Include(a => a.Branch) 
            .Include(a => a.Eatery) 
            .Where(a => a.IsDeleted == false)
            .Where(expression).ToListAsync();
            return staff;
        }
    }
}
