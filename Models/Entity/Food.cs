using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Enums;

namespace RDMS.Models.Entity
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public  CartItem cartItem  { get; set; }
        public ICollection<BranchFood> BranchFoods { get; set; } = new HashSet<BranchFood>();
        public ICollection<OrderFood> OrderFoods { get; set; } = new HashSet<OrderFood>();
    }
}
