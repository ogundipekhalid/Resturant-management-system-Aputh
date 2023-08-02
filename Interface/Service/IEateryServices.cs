using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.ViewModel;

namespace RDMS.Interface.Service
{
    public interface IEateryServices
    {
        Task<BaseResponce<EateryDto>> CreateEatery( CreataEateryRequestModel model);
        Task<BaseResponce<IEnumerable<EateryDto>>> GetAllEatery();
        Task<BaseResponce<IEnumerable<EateryDto>>> GetAllEateryOne(int id);
        Task<BaseResponce<EateryDto>> GetByAdminId(int admin);
        // Task<BaseResponce<EateryDto>> VerifyEatery(int id);
        Task<BaseResponce<EateryDto>> VerifyEatery( int id, VerifyEateryRequestModel model);
        Task<BaseResponce<EateryDto>> NotVerify(int id, VerifyEateryRequestModel model);
        Task<BaseResponce<EateryDto>> RemoveEatery(int id);
        Task<BaseResponce<EateryDto>> GetOneUser(int id);
        Task<BaseResponce<EateryDto>> Update(int id, UpdateEateryRequestModel model);
        Task<BaseResponce<EateryDto>> UpdateManager(int id, UpdateEateryManagerRequestModel model);
    }
}
