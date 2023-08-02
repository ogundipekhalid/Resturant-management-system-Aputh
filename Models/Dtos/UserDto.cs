using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
        public double Wallet { get; set; }
        public int AddressId { get; set; }
        public bool IsDeleted { get; set; }
        public AddressDto Address { get; set; }
        public ICollection<RoleDto> UserRoles { get; set; } = new HashSet<RoleDto>();
    }

    public class CreateUserRequestModel
    {
        [Display(Name="FirstName")][Required(ErrorMessage = "Please enter your FirstName.")][MinLength(5) , MaxLength(50)]
        public string FirstName { get; set; }
        [Display(Name="LastName")][Required(ErrorMessage = "Please enter your LastName.")][MinLength(5) , MaxLength(50)]
        public string LastName { get; set; }
        [Display(Name="Email")][Required(ErrorMessage = "Please enter your Email.")][MinLength(5) , MaxLength(50)]
        public string Email { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }

    public class UpdateUserRequestModel
    {
        [Display(Name="FirstName")][Required(ErrorMessage = "Please enter your FirstName.")][MinLength(5) , MaxLength(50)]
        public string FirstName { get; set; }
        [Display(Name="LastName")][Required(ErrorMessage = "Please enter your LastName.")][MinLength(5) , MaxLength(50)]
        public string LastName { get; set; }
        [Display(Name="Email")][Required(ErrorMessage = "Please enter your Email.")][MinLength(5) , MaxLength(50)]
        public string Email { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }

    public class LogingRequesteModel
    {
        [Display(Name="Email")][Required(ErrorMessage = "Please enter your Email.")][MinLength(5) , MaxLength(50)]
        public string Email { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginUserModel
    {
        public int BranchId { get; set; }
        public int EateryId { get; set; }
        public string Message { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
        public UserDto Data { get; set; }
    }
}
