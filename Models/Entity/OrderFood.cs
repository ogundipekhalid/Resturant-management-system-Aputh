using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Entity
{
    public class OrderFood : BaseEntity
    {  
        public int BranchId { get; set; }
        public int OrderId { get; set; }
        public  Order Order { get; set; }
        public int FoodId { get; set; }
        public  Food Food { get; set; }
        public double Quantity {get; set;}
        public decimal Price {get; set;}
    }
}