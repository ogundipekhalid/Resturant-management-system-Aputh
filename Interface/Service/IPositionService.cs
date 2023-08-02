using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;

namespace RDMS.Interface.Service
{
    public interface IPositionService 
    {
        Task<BaseResponce<PositionDto>> Create(CreatePositionRequestModel model);
        Task<BaseResponce<PositionDto>> Get(int id);
        Task<List<Position>> GetlistPostios();
        Task<BaseResponce<IEnumerable<PositionDto>>> GetAll();
        Task<BaseResponce<PositionDto>> Delete(int id);
    }
} 