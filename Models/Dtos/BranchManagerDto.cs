using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class BranchManagerDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string RegNumber { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }
    
    public class CreateBranchhManagerRequestModel
    {
        public string RegNumber { get; set; }
        [Display(Name = "BranchName")]
        [Required(ErrorMessage = "Please enter your BranchName.")]
        [MinLength(5)]
        [MaxLength(50)]
        public string BranchName { get; set; }
    }

    public class BrancheManagerDtoFordertails
    {
        public int Id { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "Please enter your FirstName.")]
        [MinLength(5)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "Please enter your LastName.")]
        [MinLength(5)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your Email.")]
        [MinLength(5)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your Password.")]
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "Please enter your PhoneNumber.")]
        [MinLength(5)]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        // [Display(Name = "BranchName")][Required(ErrorMessage="Please enter your BranchName.")] [MinLength(5)][MaxLength(50)]
        public int BranchId { get; set; }
        public BranchDto Branch { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int EateryId { get; set; }
        public EateryDto EateryDto { get; set; }
    }

    public class UpdateBranchManagerRequestModel
    {
        public int Id { get; set; }

        public int EateryId { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 9), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required, StringLength(50, MinimumLength = 9)]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
    }
}
