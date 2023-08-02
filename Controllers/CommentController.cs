using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;

namespace RDMS.Controllers
{
    public class CommentController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
         private readonly IOrderService _orderService;
         private readonly ICommetServies _commetServies;
   
        public CommentController(ICommetServies commentService,IOrderService orderService , IWebHostEnvironment webHostEnvironment)
        {
            _commetServies = commentService;
            _webHostEnvironment = webHostEnvironment;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        
        //  [ActionName  ("CreateComment")]
        // [HttpPost("/Comment/CreateComment")]
       [HttpPost]
        public async Task<IActionResult> Create(CreateCommentRequestModel model )
        {
            var Userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var comment = await _commetServies.CreateComment(Userid,model);
            if(!comment.Status) 
            {
               TempData["Exist"] = " created Successfully";
            }
            // return RedirectToAction(nameof(User),"Customer");
            // return RedirectToAction("User/Customer");
            // return RedirectToAction("Customer","User");
            return RedirectToAction("GetAll");
        }

         [HttpGet]
        public async Task<IActionResult> GetCommentsOfAPost(  int id)
        {
            var comment = await _commetServies.GetCommentsOfAPost(id);
            if(!comment.Status) return BadRequest(comment);
            return Ok(comment);
        }

        [HttpPost("UpdateComment/{Id}")]
        public async Task<IActionResult> UpdateComment(UpdateCommentRequestModel model,  int CommentId)
        {
            var comment = await _commetServies.UpdateComment(model, CommentId);
             if (comment.Status == false)
            {
                return View(comment.Data);
            }
              return RedirectToAction("GetAll", "Comments");
              // //return RedirectToAction("Customer","User");
        }


        // [HttpGet("GetAll")]
         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comment = await _commetServies.GetAllReviewsAsync();
            return View(comment.Data);
        }

         [HttpGet]
         public async Task<IActionResult> GetComments()
        {
            var reviews = await _commetServies.GetAllReviewsAsync();
            if (reviews.Status == false)
            {
                return View(reviews.Data);
            }
            return View(reviews);
        }


         [HttpGet]
        public async Task<IActionResult> GetAComment(  int CommentId)
        {
            var comment = await _commetServies.Get(CommentId);
            return View(comment.Data);
        }

        [HttpPost, ActionName("Delete")]
          public async Task<IActionResult> DeleteAsync(int id)
        {
            var review = await _commetServies.Delete(id);
           
            return RedirectToAction("GetAll", "Comments");
        }

   
    }
}