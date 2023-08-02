using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class EateryAdminDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RegNumber{get; set;}
        public int EateryId { get; set; }
        public string EateryName { get; set; }
        public int BranchId { get; set; }
        public BranchDto Branch { get; set; }
        public EateryDto Eatery { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Display(Name = "Phone Number"),]
        public string PhoneNumber { get; set; }

        [Display(Name = "Delete Status"),]
        public bool IsDeleted { get; set; }
        public UserDto User { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateEateryAdminRequestModel
    {
        [Display(Name="UserName")][Required][MinLength(5) , MaxLength(50)]
        public string UserName { get; set; }
        [Display(Name="RegNumber")][Required][MinLength(5) , MaxLength(50)]
        public string RegNumber { get; set; }
        [Display(Name="EateryName")][Required][MinLength(5) , MaxLength(50)]
        public string EateryName { get; set; }
    }

    public class UpdateAminEateryRequestModel
    {
        [Display(Name="EateryName")][Required][MinLength(5) , MaxLength(50)]
        public string RegNumber { get; set; }
        [Display(Name="EateryName")][Required][MinLength(5) , MaxLength(50)]
        public string EateryName { get; set; }
    }

    public class UpdateEateryManagerRequestModel
    {
        public int Id { get; set; }
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
    }

    
}
