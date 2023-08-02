using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Actors;
using RDMS.Models.Enums;
using RDMS.Models.Identity;

namespace RDMS.Models.Entity
{
    public class Order : BaseEntity
    {
        public string ReferenceNumber{get; set;}
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount  { get; set; }
        public int BranchId { get; set; }
        public Branch Branch{get; set;}
        public OrderStatus OrderStatus{get; set;}
        public ICollection<OrderFood> OrderFoods { get;set; } = new HashSet<OrderFood>();
       
    }

}