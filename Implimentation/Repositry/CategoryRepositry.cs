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
    public class CategoryRepositry : BaseRepositry<Category>, ICategoryRepositry
    { 
        public CategoryRepositry(ResturantDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Get(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Foods)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return category;
        }

         public async Task<Category> DeleteAsync(int CartiogyId)
        {
            var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == CartiogyId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Get(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories
                .Include(c => c.Foods)
                .ThenInclude(c => c.OrderFoods)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSelected(List<int> ids)
        {
            return await _context.Categories
             .Include(c => c.Foods)
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSelected(
            Expression<Func<Category, bool>> expression
        )
        {
            return await _context.Categories
             .Include(c => c.Foods)
             .Where(a => a.IsDeleted == false).Where(expression)
            .ToListAsync();
        }
    }
}
