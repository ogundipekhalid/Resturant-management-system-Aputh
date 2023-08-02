using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.Interface.Service
{
    public interface IUserServices
    {
        Task<BaseResponce<UserDto>> Login(LogingRequesteModel model);
        Task<BaseResponce<UserDto>> Get(int id);
        Task<BaseResponce<IEnumerable<UserDto>>> GetAll(); 
    }
}