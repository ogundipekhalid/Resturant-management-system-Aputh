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
    public class PositionRepositry : BaseRepositry<Position>, IPositionReposity
    {
        public PositionRepositry(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<Position> Get(int id)
        {
            var category = await _context.Positions
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return category;
        }

        public async Task<Position> Get(Expression<Func<Position, bool>> expression)
        {
            return await _context.Positions
                //.Include(c => c.staffPositions)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Position>> GetAll()
        {
            return  await _context.Positions
            .Where(a => a.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetSelected(List<int> ids)
        {
            return await _context.Positions
               /// .Include(c => c.staffPositions)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetSelected(Expression<Func<Position, bool>> expression)
        {
             return await _context.Positions
             //.Include(c => c.staffPositions)
             .Where(a => a.IsDeleted == false).Where(expression)
            .ToListAsync();
        }
    }
}
