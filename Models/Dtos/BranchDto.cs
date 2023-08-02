using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Identity;

namespace RDMS.Models.Dtos
{
    public class BranchDto
    {
        public int Id { get; set; }

        ///[Display(Name = "Name")][Required] [MinLength(5)][MaxLength(50)]
        public string Name { get; set; }
        public int EateryId { get; set; }
        public EateryDto Eatery { get; set; }
        public int AddressId { get; set; }
        public Address AddressName { get; set; }
        public string State { get; set; }
        public bool IsVerify { get; set; }
        public ICollection<StaffDto> Staffs { get; set; } = new HashSet<StaffDto>();
        public ICollection<OrderDto> Orders { get; set; } = new HashSet<OrderDto>();
        public ICollection<FoodDto> Foods { get; set; } = new HashSet<FoodDto>();
    }

    public class CreateBranchsRequestsModels
    {
        [Display(Name = "Name")][Required] [MinLength(5)][MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "FirstName")][Required(ErrorMessage="Please enter your FirstName.")] [MinLength(5)][MaxLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "LastName")][Required(ErrorMessage="Please enter your LastName.")]  [MinLength(5)][MaxLength(50)]

        public string LastName { get; set; }
        [Display(Name = "Email")][Required(ErrorMessage="Please enter your Email.")]  [MinLength(5)][MaxLength(50)]

        public string Email { get; set; }
        [Display(Name = "Password")][Required(ErrorMessage="Please enter your Password.")]  [MinLength(5)][MaxLength(50)]

        public string Password { get; set; }
        [Display(Name = "PhoneNumber")][Required(ErrorMessage="Please enter your PhoneNumber.")]  [MinLength(5)][MaxLength(50)]
        public string PhoneNumber { get; set; }

    }

    public class CreateBranchRequestModel
    {
        public string Name { get; set; }
    }

    public class UpdateBranchViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class UpDateBranchRequestModel
    {
        //  [Required, StringLength(50, MinimumLength = 9)]
        public int Id { get; set; }

        //  [Required, StringLength(50, MinimumLength = 9)]
        public int EateryId { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        //[Required, StringLength(50, MinimumLength = 6), DataType(DataType.EmailAddress)]


        //public string Address { get; set; }
        //public Address AddressName { get; set; }
       // [Display(Name = "Street")]
        [Required, StringLength(50, MinimumLength = 9)]

       public string Street { get; set; }

         [Required, StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

         [Required, StringLength(50, MinimumLength = 3)]
        public string State { get; set; }
    }

      public class BranchDeshResponceModel
    {
        public int ViewAllFood { get; set; }
        public int AllStaff { get; set; }
        public int ListofCatigories { get; set; }
    }

}
