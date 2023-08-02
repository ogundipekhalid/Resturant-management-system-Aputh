using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.Interface.Service
{
    public interface IRoleServices
    {
        Task<BaseResponce<RoleDto>> Create(CreateRoleRequestModel model);
        Task<BaseResponce<RoleDto>> Get(int id);
        Task<BaseResponce<IEnumerable<RoleDto>>> GetAll();
        Task<BaseResponce<RoleDto>> Delete(int id);
    }
}
