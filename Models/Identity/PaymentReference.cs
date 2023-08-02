using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Actors;
using RDMS.Models.Entity;

namespace RDMS.Models.Identity
{
    public class PaymentReference : BaseEntity 
    {
        public string ReferenceNumber { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}