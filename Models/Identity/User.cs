using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Actors;
using RDMS.Models.Entity;

namespace RDMS.Models.Identity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? Token { get; set; }
        public double Wallet{get; set;}
        public int AddressId{get;set;}
        // public BranchManager  BranchManager { get; set; }
        // public EateryAdmin  EateryAdmin { get; set; }
        // public  Staff Staff { get; set; }
        public Address Address { get; set; } 
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}