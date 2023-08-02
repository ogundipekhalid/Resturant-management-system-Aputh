using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.AppAuteticte
{
    public interface IAppAuthentication
    {
        Task<bool> SetClaimsAndCookies(UserDto Data);
        Task<LoginUserModel> getLoginUser();
        string GenerateStaffNo(string Name, int id);
    }
}
