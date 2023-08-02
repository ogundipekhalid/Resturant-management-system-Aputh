using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDMS.Models.Entity;
using RDMS.Models.Enums;
using RDMS.Models.Identity;

namespace RDMS.Models.Dtos
{
    public class FoodDto
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public string FoodName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public DateTime AvailableTime { get; set; }
        public double Quantity { get; set; } //
        public Category Category { get; set; }
        public  CartItem cartItem  { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int CategoryId { get; set; }
        public SelectList  Name{ get; set; }
        public int BranchId { get; set; }
        public int EateryId { get; set; }
        public ICollection<BranchFood> BranchFoods { get; set; } = new HashSet<BranchFood>();
        // public ICollection<Branch> Branchs { get; set; } = new HashSet<Branch>();
    }

    public class CreateFoodRequestModel
    {
        public int Id { get; set; }
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
        public string FoodName { get; set; }
         public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        [Display(Name="CatigoriesName")][Required][MinLength(5) , MaxLength(50)]
         public Category Category { get; set; }
        public SelectList  Name{ get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
    }

    public class UpdateFoodRequestModel
    {
        public int Id { get; set; }
      //  [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
         public string FoodName { get; set; }

       // [Display(Name="Price")][Required][MinLength(5) , MaxLength(50)]
        public decimal Price { get; set; }
       // [Required, StringLength(50, MinimumLength = 9), DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
        [Required]
       public OrderStatus OrderStatus { get; set; }
    }
}
