using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Identity;

namespace RDMS.Implimentation.Repositry
{
    public class AddressRepositry : BaseRepositry<Address>, IAddressRepositry
    {
        public AddressRepositry(ResturantDbContext context)
        {
            _context = context;
        }
        public async Task<Address> Get(Expression<Func<Address, bool>> expression)
        {
            var address = await _context.Adresses
            .Include(a => a.User)
            .Include(b => b.Branch)
            .Where(a => a.IsDeleted == false)
            .FirstOrDefaultAsync(expression);
            return address;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _context.Adresses
            .Include(a => a.User)
            .Include(b => b.Branch)
            .Where(a => a.IsDeleted == false)
            .ToListAsync();
        }


        public async Task<Address> Get(int id)
        {
            var address = await _context.Adresses
            .Include(a => a.User)
            .Include(b => b.Branch)
            .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return address;
        }

        public  async Task<IEnumerable<Address>> GetSelected(List<int> ids)
        {
            return await _context.Adresses
            .Include(a => a.User)
            .Include(b => b.Branch)
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetSelected(Expression<Func<Address, bool>> expression)
        {
            return  await _context.Adresses
            .Include(a => a.User)
            .Include(b => b.Branch)
            .ThenInclude(b => b.BranchFoods)
            .Where(a => a.IsDeleted == false).Where(expression)
            .ToListAsync();
        }

            public  async Task<IList<Address>> SearchProductsProductName(string searchInput, User user)
        {
            var input = searchInput.ToLower().Trim();
          //  var searchedOutput = await _context.Orders.Where(p => p.FarmerEmail.ToLower() == input && p.ProductLocalGovernment == user.Address.LocalGovernment || p.FarmerUserName.ToLower() == input && p.ProductLocalGovernment == user.Address.LocalGovernment || p.ProductName.ToLower() == input && p.ProductLocalGovernment == user.Address.LocalGovernment).ToListAsync();
            return  await _context.Adresses.Where(p => p.Branch.Address.City.ToLower() == input && p.State == user.Address.City || p.User.FirstName.ToLower() == input && p.City == user.Address.State || p.Branch.Orders.ToString().ToLower() == input && p.State == user.Address.City).ToListAsync();
            
        }
    }
}