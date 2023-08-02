using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Actors;
using RDMS.Models.Identity;

namespace RDMS.Models.Entity
{
    public class Branch : BaseEntity
    {
        public string Name{get;set;}
        // public int CustomerId { get; set; }
        public int EateryId { get; set; }
        public  Eatery Eatery  { get; set;}
        public int AddressId{get;set;}
        public Address Address {get;set;}
        public ICollection<Staff> Staffs {get; set;} = new HashSet<Staff>();
        public ICollection<Order> Orders{get;set;} = new HashSet<Order>();
        public ICollection<BranchFood> BranchFoods{get; set;} = new HashSet<BranchFood>();
    }
}