using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }      
        public string Name { get; set; }
        public ICollection<UserDto> UserRoles { get; set; } = new HashSet<UserDto>();
    }

    
    public class CreateRoleRequestModel
    {
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
        public string Name { get; set; }
    }
}