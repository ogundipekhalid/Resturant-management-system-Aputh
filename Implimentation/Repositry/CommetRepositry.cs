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
    public class CommetRepositry : BaseRepositry<Comment>, ICommentRepositry
    {
        public CommetRepositry(ResturantDbContext context)
        {
           _context = context;  
        }
        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<List<Comment>> GetAllReviewsByCustomerAsync(int id)
        {
             var comment = await _context.Comments.Include(L=> L.Customer)
            .ThenInclude(L=> L.User).Include(L=> L.Customer)
            .Where(L=> L.IsDeleted == false).ToListAsync();
            return comment;
        }

        public async Task<Comment> GetComment(int id)
        {

             var comment = await _context.Comments
            .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return comment;
        }

        public async Task<Comment> GetComment(Expression<Func<Comment, bool>> expression)
        {
            return await _context.Comments
                .Include(a => a.Customer)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<List<Comment>> GetCommentsOfAPosts(int id)
        {
             return await _context.Comments.Include(L=> L.Customer)
            .ThenInclude(L=> L.User).Include(L=> L.Customer)
            .Where(L=> L.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Comment>> GetSelected(List<int> ids)
        {
             return await _context.Comments
             .Include(c => c.Customer)
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetSelected(Expression<Func<Comment, bool>> expression)
        {
            return await _context.Comments
             .Include(c => c.Customer)
             .Where(a => a.IsDeleted == false)
             .Where(expression)
             .ToListAsync();
        }

         public async Task<List<Comment>> GetAllReviewsAsync()
        {
            return await _context.Comments
            .Include(c => c.Customer)
            .ThenInclude(x => x.User)
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CommentDate)
            .ToListAsync();
        }

       
        
    }
}