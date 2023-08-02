using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;

namespace RDMS.Implimentation.Service
{
    public class CommentService : ICommetServies
    {
        private readonly ICommentRepositry _CommentRepository;
        private readonly IOrderRepositry _OrderRepositry;
        private readonly IUserRepositry _UserRepository;
        private readonly ICustomerRepositry _CustomerRepositry;

        public CommentService(
            ICustomerRepositry customerRepositry,
            IUserRepositry UserRepository,
            IOrderRepositry orderRepositry,
            ICommentRepositry commentRepository
        )
        {
            _CommentRepository = commentRepository;
            _CustomerRepositry = customerRepositry;
            _OrderRepositry = orderRepositry;
            _UserRepository = UserRepository;
        }

        public async Task<BaseResponce<CommentDto>> CreateComment(
            int CustomerId,
            CreateCommentRequestModel model
        )
        {
             var customer = await _CustomerRepositry.GetDetails(CustomerId);
            if (customer == null)
            {
                return new BaseResponce<CommentDto> 
                {
                    Message = "Customer not found",
                    Status = false,
                };
            }
            var comment = new Comment
            {
                // CommentId = model.CommentText
                CommentText = model.CommentText,
                CustomerId = customer.Id,
                CommentDate = model.CommentDate,
                // Seen = false
            };
            await _CommentRepository.Add(comment);
            _CommentRepository.Save();
            return new BaseResponce<CommentDto>
            {
                Message = "Successfully Commented",
                Status = true
            };
        }

        // public async Task<BaseResponce<CommentDto>> CreateComment(
        //     int CustomerId,
        //     CreateCommentRequestModel model
       // )
        // {
        //     var userInPost = await _CustomerRepositry.Get(x => x.User.Id == model.CustomerId);
        //                 //GetDetails(f => f.Name == model.Name);
        //     if (userInPost == null)
        //     {
        //         return new BaseResponce<CommentDto>
        //         {
        //             Message = "Member Id not found",
        //             Status = false
        //         };
        //     }
        //     var appUser = userInPost;
        //     var Order = await _CommentRepository.GetComment(model.id);
        //     System.Console.WriteLine(Order);
        //     if (Order == null)
        //     {
        //         return new BaseResponce<CommentDto>
        //         {
        //             Message = " not found",
        //             Status = false
        //         };
        //     }
        //     var comment = new Comment
        //     {
        //         Customer = appUser,
        //         CommentId = appUser.Id,
        //         CommentText = model.CommentText,
        //         CommentDate = DateTime.UtcNow,
        //         DateCreated = DateTime.UtcNow,
        //         // OrderId = model.OrderId,
        //     };
        //     await _CommentRepository.Add(comment);
        //     _CommentRepository.Save();
        //     return new BaseResponce<CommentDto>
        //     {
        //         Message = " Successful Initialization",
        //         Status = true,
        //     

        
        public async Task<bool> Delete(int Id)
        {
            var comment = await _CommentRepository.GetComment(Id);
            if (comment == null)
                return false;
            comment.IsDeleted = true;
            await _CommentRepository.Delete(comment);
            return true;
        }

        public async Task<BaseResponce<CommentDto>> Get(int id)
        {
            var comment = await _CommentRepository.GetComment(id);
            if (comment == null)
                return new BaseResponce<CommentDto> { Message = "Could Not Fetch", Status = true, };
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                CustomerId = comment.CustomerId,
                CustomerName = comment.Customer.User.FirstName,
                CommentText = comment.CommentText,
                CommentDate = comment.CommentDate,
                // PostDate = comment.Order.DateCreated,
            };
            return new BaseResponce<CommentDto> { Message = "Successfully gotten", Status = true, };
        }

        public async Task<BaseResponce<IEnumerable<CommentDto>>> GetAllCommentUser()
        {

              var coment = await _CommentRepository.GetAllComments();
               if (coment == null)
            {
                return new BaseResponce<IEnumerable<CommentDto>>
                {
                    Message = "Not found",
                    Status = true
                };
            }
          
            return new BaseResponce<IEnumerable<CommentDto>>
            {
                Message = "Successful Retrieval",
                Status = true,
                 Data = coment.Select(h => new CommentDto
                    {
                        Id = h.Id,
                        CustomerId = h.CustomerId,
                        CustomerName = h.Customer.User.FirstName,
                        CommentText = h.CommentText,
                        CommentDate = h.CommentDate,
                    }
                 )
                
            };
        }
        
       

        public async Task<BaseResponce<CommentDto>> GetALL()
        {
            var coment = await _CommentRepository.GetAllComments();
            var allcoment = coment.Select(h => new CommentDto
            {
                Id = h.Id,
                CustomerId = h.CustomerId,
                CommentText = h.CommentText,
                CommentDate = h.CommentDate,
                CustomerName = h.Customer.User.FirstName,
                // Customer = h.Customer
            });
          
            return new BaseResponce<CommentDto>
            {
                Message = "Successful Retrieval",
                Status = true,
                // Data = new CommentDto
                // {
                //     Id = 
                // }
            };
        }

       

        public async Task<BaseResponce<CommentDto>> GetCommentsOfAPost(int id)
        {
            var postComments = await _CommentRepository.GetCommentsOfAPosts(id);
            if (postComments == null)
                return new BaseResponce<CommentDto>
                {
                    Message = "Post Has No Comments yet",
                    Status = false,
                };
           
            var applicationUserCommentsReturned = postComments
                .Select(
                    comment =>
                        new CommentDto
                        {
                            Id = comment.Id,
                            CommentText = comment.CommentText,
                            CommentDate = comment.CommentDate,
                            CustomerName = comment.Customer.User.FirstName,
                        }
                )
                .ToList();

            return new BaseResponce<CommentDto>
            {
                Message = "Successful Retrieval",
                Status = true,
            };
        }

        public async Task<BaseResponce<CommentDto>> UpdateComment(
            UpdateCommentRequestModel model,
            int id
        )
        {
            var comment = await _CommentRepository.GetComment(id);
            if (comment == null)
                return new BaseResponce<CommentDto>
                {
                    Message = " Comment Not Found",
                    Status = false,
                };
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                CustomerId = comment.CustomerId,
                CustomerName = comment.Customer.User.FirstName,
                CommentText = comment.CommentText,
                CommentDate = comment.CommentDate,
               
            };
            comment.CommentText = comment.CommentText ?? model.CommentText;
            await _CommentRepository.Update(comment);
            return new BaseResponce<CommentDto> { Message = "Success In Updating", Status = true, };
        }

         public async Task<BaseResponce<IList<CommentDto>>> GetAllReviewsByCustomerAsync(int id)
        {
            var customer = await _CustomerRepositry.Get(id);
            if (customer == null)
            {
                return new BaseResponce<IList<CommentDto>>
                {
                    Message = "Customer not found",
                    Status = false,
                };
            }
            var reviews = await _CommentRepository.GetAllReviewsByCustomerAsync(customer.Id);
            if (reviews.Count == 0)
            {
                return new BaseResponce<IList<CommentDto>>
                {
                    Message = "Review not found",
                    Status = false
                };
            }
            return new BaseResponce<IList<CommentDto>>
            {
                Message = "Reviews found",
                Status = true,
                Data = reviews.Select(x => new CommentDto
                {
                    Id = x.Id,
                    CommentDate = x.CommentDate,
                    CommentText = x.CommentText,
                    CustomerName = x.Customer.User.FirstName,
                    PostDate =  x.CommentDate,
                }).ToList()
            };
        }


        public Task<BaseResponce<CommentDto>> UpdateAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponce<CommentDto>> UpdateReviewStatusAsync(int id)
        {
             var review = await _CommentRepository.GetComment(x => x.Id == id);
            if (review == null)
            {
                return new BaseResponce<CommentDto>
                {
                    Message = "review not found",
                    Status = false
                };
            }
            // review.Seen = true;
            review.IsDeleted = true;
            await _CommentRepository.Update(review);
             _CommentRepository.Save();

            return new BaseResponce<CommentDto>
            {
                Message = "Review updated successfully",
                Status = true
            };
        }

        public async Task<BaseResponce<IList<CommentDto>>> GetAllReviewsAsync()
        {
            var review = await _CommentRepository.GetAllReviewsAsync();
            if (review.Count == 0)
            {
                return new BaseResponce<IList<CommentDto>>
                {
                    Message = "no Reviews yet",
                    Status = false
                };
            }
            
            foreach (var item in review)
            {
                if ((DateTime.Now - item.CommentDate).TotalSeconds < 60)
                {
                    item.CreatedBy = (int)(DateTime.Now - item.DateCreated).TotalSeconds + " " + "Sec ago";
                }
                if ((DateTime.Now - item.CommentDate).TotalSeconds > 60 && (DateTime.Now - item.DateCreated).TotalHours < 1)
                {
                    item.CreatedBy = (int)(DateTime.Now - item.CommentDate).TotalMinutes + " " + "Mins ago";
                }
                if ((DateTime.Now - item.CommentDate).TotalMinutes > 60 && (DateTime.Now - item.DateCreated).TotalDays < 1)
                {
                    item.CreatedBy = (int)(DateTime.Now - item.CommentDate).TotalHours + " " + "Hours ago";
                }
                if ((DateTime.Now - item.CommentDate).TotalHours > 24 && (DateTime.Now - item.DateCreated).TotalDays < 30)
                {
                    item.CreatedBy = (int)(DateTime.Now - item.CommentDate).TotalDays + " " + "Days ago";
                }
                if ((DateTime.Now - item.CommentDate).TotalDays > 30 && (DateTime.Now - item.DateCreated).TotalDays <= 365)
                {
                    item.CreatedBy = ((int)(DateTime.Now - item.CommentDate).TotalDays / 30) + " " + "Months ago";
                }
                
            }
            return new BaseResponce<IList<CommentDto>>
            {
                Message = "Reviews found",
                Status  = true,
                Data = review.Select(x => new CommentDto
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    CommentText = x.CommentText,
                    CustomerName = x.Customer.User.FirstName,
                    CommentDate = x.CommentDate,
                    Customer = new CustomerDto
                    {
                        FirstName = x.Customer.User.FirstName
                    }

                }).ToList()
            };
            throw new NotImplementedException();
        }
    
    }
}
