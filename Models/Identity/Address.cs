using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Entity;

namespace RDMS.Models.Identity
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; } //in muse
        public User User { get; set; }
        public Branch Branch{get; set;} 
   
    }
}