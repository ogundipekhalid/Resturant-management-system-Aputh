using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class CreatePaymentRequestModel
    {
        [Display][Required][MinLength(5) , MaxLength(50)]
        public int Amount { get; set; }
        [Display(Name="Email")][Required][MinLength(5) , MaxLength(50)]
        public string Email { get; set; }
        [Display(Name="PhoneNumber")][Required][MinLength(5) , MaxLength(50)]
        public string PhoneNumber { get; set; }

    }
}