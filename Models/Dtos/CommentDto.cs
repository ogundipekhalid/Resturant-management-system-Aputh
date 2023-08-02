using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CommentText { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime CommentDate { get; set; }
    }

    public class CreateCommentRequestModel
    {
        [Display(Name="CommentText")][Required][MinLength(5) , MaxLength(50)]
        public string CommentText { get; set; }
        // [Required, StringLength(50, MinimumLength = 9), DataType(DataType.DateTime)]
        public DateTime CommentDate { get; set; } 
    }

    public class UpdateCommentRequestModel
    {
        [Display(Name="CommentText")][Required][MinLength(5) , MaxLength(50)]
        public string CommentText { get; set; }
    }
}
