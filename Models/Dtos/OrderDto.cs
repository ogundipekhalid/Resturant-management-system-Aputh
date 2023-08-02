using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Entity;
using RDMS.Models.Enums;

namespace RDMS.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string ReferenceNumber{get; set;}
         public int CustomerId { get; set; }
        public decimal TotalAmount  { get; set; }
        public CustomerDto CustomerDto { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public DateTime AvailableTime { get; set; }
        public int BranchId { get; set; }
        public  FoodDto Food { get; set; }
        public BranchDto Branch{get; set;}
        public OrderStatus OrderStatus{get; set;}
        public ICollection<CommentDto> Comments{get;set;} = new HashSet<CommentDto>();
        public ICollection<OrderFood> OrderFoods {get;set;} = new HashSet<OrderFood>();
    }

    
    public class CreateOrderRequestModel
    {
        public int OrderId { get; set; }///N
        public List<int> CartId { get; set; }
        public OrderStatus OrderStatus{get; set;}
        public int BranchId { get; set; }
   }

     public class UpdateOrderRequestModel
    {
        public OrderStatus OrderStatus{get; set;}
    }
     public class UpdateRefrenceNumber
    {
        public string ReferenceNumber{get; set;}
    }

    

}
