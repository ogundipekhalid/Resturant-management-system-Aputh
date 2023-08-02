using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class PositionDto
    {
        public int Id { get; set; }
        public string Name {get;set;}
        // public ICollection<PositionDto> StaffPosition {get; set;} = new HashSet<PositionDto>();
        public ICollection<StaffDto> Staff { get; set; } = new HashSet<StaffDto>();

    }

    public class CreatePositionRequestModel
    {
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
        public string Name {get;set;}   
    }

     public class UpdatePositionRequestModel
    {
        [Display(Name="Name")][Required][MinLength(5) , MaxLength(50)]
        public string Name {get;set;}   
    }
}