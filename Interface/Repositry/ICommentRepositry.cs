using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Implimentation.Repositry;
using RDMS.Interface.Repository;
using RDMS.Models.Identity;

namespace RDMS.Interface.Repositry
{
    public interface ICommentRepositry : IBaseRepository<Comment>
    {
        Task<Comment> GetComment(int id);
        Task<Comment> GetComment(Expression<Func<Comment, bool>> expression);
        Task<IEnumerable<Comment>> GetAllComments();
       Task<List<Comment>> GetCommentsOfAPosts(int id);
        Task<List<Comment>> GetAllReviewsByCustomerAsync(int id);
       Task<IList<Comment>> GetSelected(List<int> ids);
        Task<IEnumerable<Comment>> GetSelected(Expression<Func<Comment, bool>> expression);
        Task<List<Comment>> GetAllReviewsAsync();
    }
}