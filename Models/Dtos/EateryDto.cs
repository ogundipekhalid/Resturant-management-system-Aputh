using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class EateryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CertificationNumber { get; set; }
        public string Certificate { get; set; }
        public string Logo { get; set; }
        public bool IsVerify { get; set; }
        public string Note { get; set; }
        public ICollection<BranchDto> Branches { get; set; } = new HashSet<BranchDto>();
        public ICollection<StaffDto> Staffs { get; set; } = new HashSet<StaffDto>();
    }

    public class CreataEateryRequestModel
    {
        [Display(Name="FirstName")][Required][MinLength(5) , MaxLength(50)]
        public string FirstName { get; set; }
        [Display(Name="LastName")][Required][MinLength(5) , MaxLength(50)]
        public string LastName { get; set; }
        [Display(Name="Email")][Required][MinLength(5) , MaxLength(50)]
        public string Email { get; set; }
        [Display(Name="Password")][Required][MinLength(5) , MaxLength(50)]
        public string Password { get; set; }
        [Display(Name="PhoneNumber")][Required][MinLength(5) , MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Display(Name="Street")][Required][MinLength(5) , MaxLength(50)]
        public string Street { get; set; }
        [Display(Name="City")][Required][MinLength(5) , MaxLength(50)]
        public string City { get; set; }
        [Display(Name="State")][Required][MinLength(5) , MaxLength(50)]
        public string State { get; set; }
        [Display(Name="ZipCode")][Required][MinLength(5) , MaxLength(50)]
        public string ZipCode  { get; set; }
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
        public string Name { get; set; }
        [Display(Name="CertificationNumber")][Required][MinLength(5) , MaxLength(50)]
        public string CertificationNumber { get; set; }
        // [Required, StringLength(50, MinimumLength = 9), DataType(DataType.)]
        public IFormFile Certificate { get; set; }
        public IFormFile Logo { get; set; }
    }
    public class UpdateEateryRequestModel
    {
        public int Id { get; set; }
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
        public string Name { get; set; }
        [Display(Name="CertificationNumber")][Required][MinLength(5) , MaxLength(50)]
        public string CertificationNumber { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.Upload)]
         public IFormFile Certificate { get; set; }
        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.Upload)]
        public IFormFile Logo { get; set; }
    }
   
    public class VerifyEateryRequestModel
    {
        // public int EateryId { get; set; }
        public bool IsVerify { get; set; } = false;
        public string Note { get; set; }
    }
    
}
