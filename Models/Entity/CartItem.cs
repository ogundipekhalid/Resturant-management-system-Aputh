using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Entity
{
    public class CartItem : BaseEntity
    {
        public int BranchId { get; set; }
        public bool IsPaid { get; set; }
        public int EateryId { get; set; }
        public string FoodName { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public int FoodId { get; set; }
        public  Food Food{ get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }
    }
}