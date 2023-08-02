using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Entity;
using RDMS.Models.Identity;

namespace RDMS.Models.Actors
{
    public class EateryAdmin : BaseEntity
    {
        public int UserId{get; set;}
        public User User{get; set;}
        public string RegNumber{get; set;}
        public int EateryId{get; set;}
        public Eatery Eatery{get; set;}
        // public int BranchId { get; set; }//
        // public Branch Branch { get; set; }//
    }
}