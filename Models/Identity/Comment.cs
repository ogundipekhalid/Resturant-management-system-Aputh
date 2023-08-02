using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Actors;
using RDMS.Models.Entity;

namespace RDMS.Models.Identity
{
    public class Comment : BaseEntity
    {
        //  public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime CommentDate{get;set;} = DateTime.Now;

    }
}