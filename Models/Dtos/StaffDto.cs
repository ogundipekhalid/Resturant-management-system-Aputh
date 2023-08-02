using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDMS.Models.Enums;
using RDMS.Models.Identity;

namespace RDMS.Models.Dtos
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string StaffRegNumber { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int EateryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public UserDto User { get; set; }
        public BranchDto Branch { get; set; }
        public EateryDto Eatery { get; set; }
        // public PositionDto PositionDto { get; set; }
        public Roles Roles { get; set; }
        public SelectList PossName { get; set; }
        //public ICollection<PositionDto> Positions { get; set; } = new HashSet<PositionDto>();
    }

    public class ChangerStaffPasswordRequestModel
    {
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class CreateStaffRequestModel
    {
        [Display(Name="Name")][Required(ErrorMessage = "Please enter your Email.")][MinLength(5) , MaxLength(50)]
        public string FirstName { get; set; }
        [Display(Name="Name")][Required(ErrorMessage = "Please enter your Email.")][MinLength(5) , MaxLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }
        public int EateryId { get; set; }
        public SelectList EateryList {get;set;}
        public Roles Roles { get; set; }  
        //[Required,StringLength(50, MinimumLength = 6),Display(Name ="Branch")]
        public int BranchId { get; set; }
        public SelectList BranchList {get;set;}
        [Display(Name="Street")][Required(ErrorMessage = "Please enter your Street.")][MinLength(5) , MaxLength(50)]
        public string Street { get; set; }
        [Display(Name="State")][Required(ErrorMessage = "Please enter your State.")][MinLength(5) , MaxLength(50)]
        public string State { get; set; }
        [Display(Name="City")][Required(ErrorMessage = "Please enter your City.")][MinLength(5) , MaxLength(50)]
        public string City { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
    }

    public class CreatesStaffRequestsModelsdddddd
    {
        [MaxLength(50), MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50), MinLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Address { get; set; }

        //[Required, StringLength(50, MinimumLength = 6),]
        public Roles Rolse { get; set; }
         
        [
            Required,
            StringLength(50, MinimumLength = 6),
            Display(Name ="Branch")
        ]
        public int BranchId { get; set; }
        SelectList BranchList {get;set;}
        public int EateryId { get; set; }
    }

    public class UpdateStaffRequestModel
    {
        [Required]
        [MaxLength(50), MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50), MinLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(50), MinLength(10)]
        public string Email { get; set; }
        public Roles Roles { get; set; }
        public string Password { get; set; }
    }
}
