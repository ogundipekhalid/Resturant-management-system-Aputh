using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.ViewModel;

namespace RDMS.Interface.Service
{
    public interface ICategoryservices
    {
        Task<BaseResponce<CategoryDto>> DeleteAysc(int id);
        Task<BaseResponce<CategoryDto>> CreateCategory(CreateCategoryRequestseModel model);
        Task<BaseResponce<CategoryDto>> GetSingleCategory(int id);
        Task<BaseResponce<IEnumerable<CategoryDto>>> GetAllCategory();
        Task<BaseResponce<CategoryDto>> CansuleOrder(int id);
    }
}
