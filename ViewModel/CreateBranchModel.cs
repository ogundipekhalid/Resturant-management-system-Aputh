using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.ViewModel
{
    public class CreateBranchModel
    {
        public CreateUserRequestModel User {get; set;}
        public CreateBranchhManagerRequestModel Manager {get;set;}
        public CreateAddressRequestModel BranchAddress {get; set;}
    }
}