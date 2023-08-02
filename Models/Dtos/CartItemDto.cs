using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Entity;
using RDMS.Models.Enums;

namespace RDMS.Models.Dtos
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int EateryId { get; set; }
        public int FoodId { get; set; }
        public  Food Food { get; set; }
        public double Quantity { get; set; }
         public bool IsPaid { get; set; }
        public int BranchId { get; set; }
        public decimal Price { get; set; }
        public string FoodName { get; set;}
        public bool IsDeleted { get; set; }
        public decimal Total { get; set; }
        public int userId { get; set; }
    }

     public class AddCartItemViewModel
    {
        public int FoodId { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string FoodName { get; set; }
    }
    
    
    public class UpdateCartItemViewModel
    {
        public int Id { get; set; }
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
      
        public string Name { get; set; }
        
        [Required]
        public bool IsDeleted { get; set; }
    }

    
}