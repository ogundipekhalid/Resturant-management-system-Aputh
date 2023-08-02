using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Actors;
using RDMS.Models.Entity;

namespace RDMS.Models.Identity
{
    public class  Position : BaseEntity
    {
        public string Name {get;set;}
       // public ICollection<Staff> Staff { get; set; } = new HashSet<Staff>();
    }
}