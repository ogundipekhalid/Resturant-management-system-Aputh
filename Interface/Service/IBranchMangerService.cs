using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.Interface.Service
{
    public interface IBranchMangerService
    {
        Task<BaseResponce<BrancheManagerDtoFordertails>> AddBranchManager(LoginUserModel user, string branchId);
        Task<BaseResponce<CreateBranchsRequestsModels>> Delete(int userId, int id);
        Task<BaseResponce<IEnumerable<BrancheManagerDtoFordertails>>> GetAllBranchManager();
        Task<BaseResponce<BrancheManagerDtoFordertails>> GetBranchManager(int id);
        Task<BaseResponce<IEnumerable<BrancheManagerDtoFordertails>>> GetBranchManagerByCompanyId(int branchId);
        Task<BaseResponce<BrancheManagerDtoFordertails>> UpdateManager(int userId, UpdateBranchManagerRequestModel model);
   
    }
}