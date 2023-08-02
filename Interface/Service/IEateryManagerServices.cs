using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Actors;
using RDMS.Models.Dtos;

namespace RDMS.Interface.Service
{
    public interface IEateryManagerServices
    {
        
        Task<BaseResponce<EateryAdminDto>> Delete(int userId, int id);
        Task<BaseResponce<IEnumerable<EateryAdminDto>>> GetAllManager();
        Task<BaseResponce<EateryAdminDto>> GetManager(string email);
        Task<BaseResponce<EateryAdminDto>> Get( int id );
        Task<BaseResponce<IEnumerable<EateryAdminDto>>> GetManagerByCompanyId(int companyId);
        Task<BaseResponce<IEnumerable<EateryAdminDto>>> GetSelectedManager(List<int> ids);
        Task<BaseResponce<EateryAdminDto>> UpdateManager(int id, UpdateEateryManagerRequestModel model);
  
    }
}