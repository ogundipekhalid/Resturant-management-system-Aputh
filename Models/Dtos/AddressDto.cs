using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class AddressDto
    {
        
        public int Id { get; set; }

        [Display(Name="Street")][Required][MinLength(5) , MaxLength(50)]
        public string Street { get; set; }
        [Display(Name="City")][Required][MinLength(5) , MaxLength(50)]
        public string City { get; set; }
        [Display(Name="State")][Required][MinLength(5) , MaxLength(50)]
        public string State { get; set; }    
        [Display(Name="ZipCode")][Required][MinLength(5) , MaxLength(50)]
        public string ZipCode { get; set; }
        //in muse
        [Display(Name="FullName")][Required][MinLength(5) , MaxLength(50)]
        public string FullName { get; set; }

        [Display(Name="BranchName")][Required][MinLength(5) , MaxLength(50)]
        public string BranchName{get; set;} 

    }

    public class CreateAddressRequestModel
    {
         [Display(Name="Street")][Required][MinLength(5) , MaxLength(50)]
        public string Street { get; set;}
         [Display(Name="City")][Required][MinLength(5) , MaxLength(50)]
        public string City { get; set; }
         [Display(Name="State")][Required][MinLength(5) , MaxLength(50)]
        public string State { get; set; }
         [Display(Name="ZipCode")][Required][MinLength(5) , MaxLength(50)]
          public string ZipCode { get; set; }//in muse
    }

    
}
