using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
        public string Name { get; set; }
        public ICollection<FoodDto> Foods { get; set; } = new HashSet<FoodDto>();
    }

    public class CreateCategoryRequestseModel
    {
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
        public string Name { get; set; }
    }
}
