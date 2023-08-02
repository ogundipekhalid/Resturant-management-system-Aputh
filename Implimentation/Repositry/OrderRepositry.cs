using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Entity;
using RDMS.Models.Identity;

namespace RDMS.Implimentation.Repositry
{
    public class OrderRepositry : BaseRepositry<Order>, IOrderRepositry
    {
        public OrderRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Get(int id)
        {
            var order = await _context.Orders
                .Include(c => c.Customer)
                .Include(c => c.OrderFoods)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return order;
        }

        public async Task<Order> Get(Expression<Func<Order, bool>> expression)
        {
            var order = await _context.Orders
                .Include(c => c.Customer)
                .ThenInclude(c => c.User)
                .ThenInclude(c => c.Address)
                .Include(c => c.OrderFoods)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> DeleteAsync(int OrderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == OrderId);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<OrderFood>> GetAllOrderFood()
        {
            return await _context.OrderFoods
                .Include(c => c.Food)
                .Include(x => x.Order)
                .Include(y => y.Order.Customer)
                .ThenInclude(z => z.User)
                .ToListAsync();
        }
        
        public async Task<List<OrderFood>> OrdersAsyncDrop()
        {
            return await _context.OrderFoods
            .Include(c => c.Food)
            .Include(x => x.Order)
            .Include(c => c.Order.Customer.User.Address)
            .Include(y => y.Order.Customer)
            .ThenInclude(z => z.User).ToListAsync();
        }

        public async Task<Order> GetDetail(int id)
        {
            return await _context.Orders
                .Include(c => c.Customer)
                .ThenInclude(c => c.User)
                .Where(x => x.Id == id)
                .Where(a => a.IsDeleted == false)
                .SingleOrDefaultAsync();
        }

        public async Task<IList<Order>> GetSelected(List<int> ids)
        {
            return await _context.Orders
                .Include(c => c.Customer)
                .ThenInclude(c => c.User)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IList<Order>> GetSelected(Expression<Func<Order, bool>> expression)
        {
            var order = await _context.Orders
                .Include(c => c.Customer)
                .ThenInclude(c => c.User)
                .ThenInclude(c => c.Address)
                .Include(c => c.OrderFoods)
                .Where(a => a.IsDeleted == false)
                .Where(expression)
                .ToListAsync();
            return order;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<Order>> SearchProductsProductName(string searchInput, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersWithCustomer(
            Expression<Func<Order, bool>> expression
        )
        {
            return await _context.Orders
                .Include(c => c.Customer)
                .ThenInclude(c => c.User)
                .ThenInclude(c => c.Address)
                .Include(c => c.OrderFoods)
                .Where(a => a.IsDeleted == false)
                .Where(expression)
                .ToListAsync();
        }
    }
}
