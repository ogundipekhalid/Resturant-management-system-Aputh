using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.Interface.Service
{
    public interface ICommetServies
    {
        Task<BaseResponce<CommentDto>> CreateComment(
            int CustomerId,
            CreateCommentRequestModel model
        );
        Task<BaseResponce<CommentDto>> UpdateComment(UpdateCommentRequestModel model, int id);
        Task<BaseResponce<CommentDto>> GetCommentsOfAPost(int id);
        Task<BaseResponce<IList<CommentDto>>> GetAllReviewsByCustomerAsync(int id);
        Task<bool> Delete(int Id);
        Task<BaseResponce<CommentDto>> Get(int id);
        Task<BaseResponce<CommentDto>> GetALL();
        Task<BaseResponce<CommentDto>> UpdateAll();
        Task<BaseResponce<CommentDto>> UpdateReviewStatusAsync(int id);
        Task<BaseResponce<IList<CommentDto>>> GetAllReviewsAsync();
        Task<BaseResponce<IEnumerable<CommentDto>>> GetAllCommentUser();
    }
}
