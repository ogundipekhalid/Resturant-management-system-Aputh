using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.ViewModel
{
    public class CreateOrderModel
    {
        public CreateAddressRequestModel Address {get; set;}
        public CreateOrderRequestModel Order {get;set;}
    }
}