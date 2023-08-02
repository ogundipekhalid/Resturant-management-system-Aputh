using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;
using RDMS.ViewModel;

namespace RDMS.Interface.Service
{
    public interface IStaffService
    {
       Task<BaseResponce<StaffDto>> Create(int branchId, CreateStaffRequestModel model);
        Task<BaseResponce<StaffDto>> AddStaff( CreateStaffRequestModel model);
        Task<BaseResponce<StaffDto>> Get(int id);
        Task<BaseResponce<IEnumerable<StaffDto>>> GetAllStaff();
        Task<BaseResponce<StaffDto>> RemoveStaff(int id);
        Task<BaseResponce<StaffDto>> DeleteAsync(int id);
        Task<BaseResponce<IEnumerable<StaffDto>>> GetStaffsByEateryId(int EateryId);
        Task<BaseResponce<IEnumerable<StaffDto>>> GetStaffsByBranchId(int id );
        Task<BaseResponce<StaffDto>> Update(int id, UpdateStaffRequestModel model);
    }
}
