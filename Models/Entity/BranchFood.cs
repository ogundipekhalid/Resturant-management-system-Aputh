using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Entity
{
    public class BranchFood : BaseEntity
    {
        public int BranchId{get; set;}
        public Branch Branch{get; set;}
        public int FoodId{get; set;}
        public Food Food{get; set;}
        // public bool IsFoodAvailable{get; set;}
    }
}