using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Implimentation.Repositry;
using RDMS.Interface.Repository;
using RDMS.Interface.Repositry;
using RDMS.Models.Identity;

namespace RDMS.Implementation.Repository
{
    public class UserRepository : BaseRepositry<User>, IUserRepositry
    {
        public UserRepository(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<User> Get(Expression<Func<User, bool>> expression)
        {
            var user = await _context.Users
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                //  .Include(u => u.)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
            return user;
        }

        public async Task<User> GetDetails(Expression<Func<User, bool>> expression)
        {
            var user = await _context.Users
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetEmail(string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            return user;
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<IEnumerable<User>> GetSelected(List<int> ids)
        {
            return await _context.Users.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSelected(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchUserByEmailOrUsername(string searchInput)
        {
            var input = searchInput.ToLower().Trim();
            var users = await _context.Users
                .Include(u => u.Address)
                .Where(u => u.LastName.ToLower() == input || u.Email.ToLower() == input)
                .ToListAsync();
            return users;
        }
    }
}
