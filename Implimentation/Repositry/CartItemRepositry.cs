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
    public class CartItemRepositry : BaseRepositry<CartItem>, ICartItemRepositry
    {
        public CartItemRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public bool Find(int id)
        {
            var user = _context.CartItems.Any(u => u.Id == id);
            return user;
        }

        public bool Find(Expression<Func<CartItem, bool>> expression)
        {
            return _context.CartItems.Any(expression);
        }

        public CartItem Get(int id)
        {
            var item = _context.CartItems
                .Include(c => c.Food)
                // .ThenInclude(c => c.)
                .SingleOrDefault(u => u.Id == id && u.IsDeleted == false);
            return item;
        }

        public CartItem Get(Expression<Func<CartItem, bool>> expression)
        {
            var item = _context.CartItems
            .Include(c => c.Food)
            .SingleOrDefault(expression);
            return item;
        }

        public IEnumerable<CartItem> GetAll()
        {
             var items = _context.CartItems
            .Where(u => u.IsDeleted == false)
            .Include(c => c.Food)
            .ToList();
            return items;
        }

        public IEnumerable<CartItem> GetSelected(List<int> ids)
        {
            var items = _context.CartItems
                .Where(u => u.IsDeleted == false)
                .Include(c => c.Food)
                .ToList();
            return items;
        }

        public IEnumerable<CartItem> GetSelected(Expression<Func<CartItem, bool>> expression)
        {
            var items = _context.CartItems.Where(expression).Include(c => c.Food).ToList();
            return items;
        }
    }
}
