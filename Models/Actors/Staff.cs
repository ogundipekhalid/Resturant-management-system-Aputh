using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Entity;
using RDMS.Models.Enums;
using RDMS.Models.Identity;

namespace RDMS.Models.Actors
{
    public class Staff : BaseEntity
    {
        public string StaffRegNumber{get; set;}
        public int UserId { get; set; }
        public User User { get; set; }
        public int BranchId { get; set; }
        public Branch Branch{get; set;}
        public int EateryId {get; set;}
        public Eatery Eatery{get; set;}
        public Roles Roles { get; set; }
        //public ICollection<Position> StaffPositions {get; set;} = new HashSet<Position>();
    }
}