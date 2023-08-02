using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.ViewModel;

namespace RDMS.Interface.Service
{
    public interface IBranchService 
    {
        Task<BaseResponce<BranchDto>> CreateBranch(CreateBranchModel model);
        Task<BaseResponce<BranchDto>> Get(int id);
        // Task<BaseResponce<BranchDto>> GetBranchById(int useId ,int id);
        Task<BaseResponce<IEnumerable<BranchDto>>> GetBranchesByCompanyId();
        Task<BaseResponce<IEnumerable<BranchDto>>> GetAllBranch();
        Task<BaseResponce<IEnumerable<BranchDto>>> GetBranchByEateryId(int EateryId);
        Task<BaseResponce<BranchDto>> GetBranchesByEateryId(int EateryId);
        Task<IEnumerable<Food>> SearchFoods(string searchTerm);
        Task<BaseResponce<BranchDto>> DeleteBranch( int id);
        Task<BaseResponce<BranchDeshResponceModel>> BranchDeshResponceModels();
        // Task<IEnumerable<BranchFood>> BranchFoods();
        Task<BaseResponce<BranchDto>> UpdateBranch(int id ,UpDateBranchRequestModel model);
        // //Task<BaseResponce<BranchDto>> UpdateBranchManager(int id ,UpdateBranchManagerRequestModel model);
        //  Task<BaseResponce<IEnumerable<FoodDto>>> BranchFoodsByAddressId(string name);
         Task<BaseResponce<IEnumerable<BranchDto>>> GetListBranchById( int EateryId , int userId);
    }
}