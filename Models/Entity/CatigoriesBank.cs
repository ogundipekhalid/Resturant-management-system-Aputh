using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Entity
{
    public class CatigoriesBank :BaseEntity
    {
         public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryImage { get; set; }
        public bool IsAvailable { get; set; }
        //public IEnumerable<Order> OrderCatigries { get; set; }
    }
}