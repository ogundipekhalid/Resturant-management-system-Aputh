using RDMS.Models.Dtos;
using RDMS.Models.Entity;

namespace RDMS.Interface.Service
{
    public interface IFoodServices
    {
        Task<BaseResponce<IEnumerable<FoodDto>>> GetList(int id, int userId);
        Task<BaseResponce<FoodDto>> AddFood(CreateFoodRequestModel model, int managerId);
        Task<BaseResponce<IEnumerable<FoodDto>>> AllAvailaleFood();
        Task<BaseResponce<IEnumerable<FoodDto>>> AllAvailableFoodInBranch();
        Task<BaseResponce<FoodDto>> GetFood(int id);
        Task<BaseResponce<FoodDto>> DeleteFoodAsync(int id);
        Task<BaseResponce<FoodDto>> Get(int id, int customerId);
        Task<BaseResponce<FoodDto>> DeleteFood(int id);
        Task<BaseResponce<FoodDto>> UpdateFood(UpdateFoodRequestModel model, int id);
        Task UpdateFoodStatus();
        Task<BaseResponce<IEnumerable<FoodDto>>> BranchFoodsByAddressId(string name);
        Task<BaseResponce<IList<FoodDto>>> BranchFoodsByAddressIds(int branchId);
        Task<IEnumerable<Food>> SearchFoods(string searchTerm);
        Task<BaseResponce<IEnumerable<FoodDto>>> AllFoodByaBranch(int branchId);



    }
}
