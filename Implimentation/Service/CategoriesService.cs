using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.ViewModel;

namespace RDMS.Implimentation.Service
{
    public class CategoriesService : ICategoryservices
    {
        private readonly ICategoryRepositry _catigoriesRepositry;
        public CategoriesService(ICategoryRepositry catigoriesRepositry)
        {
            _catigoriesRepositry = catigoriesRepositry;
        }
        public async Task<BaseResponce<CategoryDto>> CreateCategory(CreateCategoryRequestseModel model)
        {
          var catigoriesExsit = await _catigoriesRepositry.Get(r => r.Name == model.Name);
              if (catigoriesExsit != null) return new BaseResponce<CategoryDto>
            {
                Message = "Unsuccessful Because catigories Already exists",
                Status = false,
            };
            var catigories = new Category
            {
                Name = model.Name,
            };
            await _catigoriesRepositry.Add(catigories);
            _catigoriesRepositry.Save();

             
            return new BaseResponce<CategoryDto>
            {
                Message = "Created Successfully",
                Status = true,
                Data = new CategoryDto
                 { Name = catigories.Name }
            };
        }

        public async Task<BaseResponce<IEnumerable<CategoryDto>>> GetAllCategory()
        {
             var roles = await _catigoriesRepositry.GetAll();
            var listOfCatigories = roles.ToList().Select( a =>new CategoryDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Foods = a.Foods.Select(b => new FoodDto
                            {
                                FoodName = b.Name
                            }).ToList(),
                        }
                ); 
                return new BaseResponce<IEnumerable<CategoryDto>>
            {
                Message = "Secessfully Get All",
                Status = true,
                Data = listOfCatigories,
            };
        }

        public async Task<BaseResponce<CategoryDto>> CansuleOrder(int id)
        {
            var order = await _catigoriesRepositry.Get(id);
            if (order is null)
            {
                return new BaseResponce<CategoryDto> { Message = "Not Found", Status = false };
            }
            order.IsDeleted = true;
            _catigoriesRepositry.Delete(order);
            _catigoriesRepositry.Save();
            return new BaseResponce<CategoryDto> { Message = "Successful", Status = true };
        }

        public async Task<BaseResponce<CategoryDto>> DeleteAysc(int id)
        {
           var food = await _catigoriesRepositry.DeleteAsync(id);
            return new BaseResponce<CategoryDto>
            {
                Message = "Food Deleted Successfully",
                Status = true
            };
        }

        

        public async Task<BaseResponce<CategoryDto>> GetSingleCategory(int id)
        {
            var catigories = await _catigoriesRepositry.Get(id);
            if (catigories == null)
            {
                return new BaseResponce<CategoryDto>
                {
                    Message = "Not Found",
                    Status = false,
                };
            }
            return new BaseResponce<CategoryDto>
            {
                Message = "Sucessfully ",
                Status = true,
                Data = new CategoryDto
                {
                    Id  = catigories.Id,
                    Name = catigories.Name,
                    Foods = catigories.Foods.Select(b => new FoodDto
                    {
                        FoodName = b.Name
                    }).ToList(),
                }

            };
        }
    }
}