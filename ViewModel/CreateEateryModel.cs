using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.ViewModel
{
    public class CreateEateryModel
    {
        public CreateAddressRequestModel Address{get; set;}
        public CreateUserRequestModel User{get; set;}
        public CreataEateryRequestModel Eatery{get; set;}
    }
}