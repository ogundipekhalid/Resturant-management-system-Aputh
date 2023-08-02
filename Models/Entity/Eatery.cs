using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Actors;
using RDMS.Models.Identity;

namespace RDMS.Models.Entity
{
    public class Eatery : BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string CertificationNumber{get; set;}
        public string Certificate{get; set;}
        public string Logo {get; set;}
        public bool IsVerify { get; set; } 
        public string? Note { get; set; }
        public ICollection<Branch> Branches { get; set; }
        public ICollection<Staff> Staffs{get; set;}
    }
}