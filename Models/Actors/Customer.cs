using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.Models.Identity;

namespace RDMS.Models.Actors
{
    public class Customer : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        //  public int AddressId{get;set;}
        // public Address Address {get;set;}
        public ICollection<Order> Orders = new HashSet<Order>();
        public ICollection<Comment> Comments {get;set;} = new HashSet<Comment>();
    }
}