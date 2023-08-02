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
    public class CustomerRepositry : BaseRepositry<Customer>, ICustomerRepositry
    {
        public CustomerRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Get(int id)
        {
            var customer = await _context.Customer
                .Include(a => a.User)
                .ThenInclude(c => c.UserRoles)
                .FirstOrDefaultAsync(a => a.UserId == id && a.IsDeleted == false);
            return customer;
        }

        public async Task<Customer> GetDetails(int id)
        {
            var customer = await _context.Customer
                .Include(c => c.User)
                .ThenInclude(c => c.UserRoles)
                //.ThenInclude(c => c.Address)
                .FirstOrDefaultAsync(a => a.User.Id == id && a.IsDeleted == false);
            return customer;
        }
        public async Task<Customer> getadcu(Expression<Func<Customer, bool>> expression)
        {
            var customer = await _context.Customer
                .Include(c => c.User)
                .ThenInclude(c => c.UserRoles)
                .ThenInclude(c => c.User.Address)
                .FirstOrDefaultAsync(expression);
            return customer;
        }

        public async Task<Customer> Get(Expression<Func<Customer, bool>> expression)
        {
            var customer = await _context.Customer
                .Include(c => c.User)
                .ThenInclude(c => c.Address)
                .Where(a => a.IsDeleted == false)
                //.FirstOrDefaultAsync(a => a.UserId ==  );
               .FirstOrDefaultAsync(expression);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customer
                .Include(a => a.User)
                .ThenInclude(s => s.UserRoles)
                .ToListAsync();
        }

        public async Task<bool> CheckEmailAsnc(string email)
        {
            return await _context.Customer
                .Include(c => c.User)
                .Where(x => x.User.Email == email)
                .AnyAsync();
        }

        public async Task<IEnumerable<Customer>> GetSelected(List<int> ids)
        {
            return await _context.Customer
                .Include(c => c.User)
                .ThenInclude(c => c.UserRoles)
                .Where(a => a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetSelected(
            Expression<Func<Customer, bool>> expression
        )
        {
            return await _context.Customer
                .Include(c => c.User)
                .ThenInclude(c => c.UserRoles)
                .Where(a => a.IsDeleted == false)
                .Where(expression)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllWithOreder()
        {
             return await _context.Customer
                .Include(a => a.User)
                .ThenInclude(s => s.UserRoles)
                .ToListAsync();
            throw new NotImplementedException();
        }
    }
}
