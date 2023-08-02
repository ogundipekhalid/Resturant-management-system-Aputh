using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.ViewModel
{
    public class CreateFoodModel
    {
       public CreateFoodRequestModel Food {get; set;}
       public CreateCategoryRequestseModel Catigories {get; set;}
    }
}