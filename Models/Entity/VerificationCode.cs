using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PayStack.Net.Charge;
// using RDMS.Models.Actors;


namespace RDMS.Models.Entity
{
    public class VerificationCode
    {
        public int Code { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    
    }
}