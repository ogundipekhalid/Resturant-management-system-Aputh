using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Food> Foods {get; set;} = new HashSet<Food>();
    }
}