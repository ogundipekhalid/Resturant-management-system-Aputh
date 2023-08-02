using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
         public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserDto UserDto { get; set; }
        public double Wallet { get; set; }
        public ICollection<OrderDto> Orders = new HashSet<OrderDto>();
    }

    public class CreateCustomerRequestModel
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
        public string ZipCode { get; set; }
    }

     public class UpdateCustomerRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Display(Name="FirstName")][Required][MinLength(5) , MaxLength(50)]
        public string FirstName { get; set; }
        [Display(Name="LastName")][Required][MinLength(5) , MaxLength(50)]
        public string LastName { get; set; }
        [Display(Name="Email")][Required][MinLength(5) , MaxLength(50)]
        public string Email { get; set; }
        [Display(Name="PhoneNumber")][Required][MinLength(5) , MaxLength(50)]
        public string PhoneNumber { get; set; }
    }




    public class UpdateWalletRequestModel
    {
        public int Id { get; set; }
        [Display(Name="Wallet")][Required][MinLength(5) , MaxLength(50)]
        public double Wallet { get; set; }
    }
}
