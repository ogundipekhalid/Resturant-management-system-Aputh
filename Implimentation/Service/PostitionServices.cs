using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;

namespace RDMS.Implimentation.Service
{
    public class PostitionServices : IPositionService
    {
        private readonly IPositionReposity _positionRepositry;
        public PostitionServices(IPositionReposity positionRepositry)
        {
           _positionRepositry = positionRepositry;
        }
        public async Task<BaseResponce<PositionDto>> Create(CreatePositionRequestModel model)
        {
            var position = await  _positionRepositry.Get(p => p.Name == model.Name);
            if (position != null)
                return new BaseResponce<PositionDto>
                {
                    Message = "position Creation Unsuccessful Because position Already exists",
                    Status = false,
                };

                 var postion = new Position
                 {
                    Name = model.Name,
                 };

                await _positionRepositry.Add(position);
                _positionRepositry.Save();
                return new BaseResponce<PositionDto>
            {
                Message = "Created Successfully",
                Status = true,
                Data = new PositionDto
                {
                    Name = position.Name
                }
            };
          
        }

        public async Task<BaseResponce<PositionDto>> Delete(int id)
        {
              var postion = await _positionRepositry.Get(id);
             if (postion is null)
            {
                return new BaseResponce<PositionDto>
                {
                    
                    Message = "Not Found",
                    Status = false
                };
            }
             postion.IsDeleted = true;
          await _positionRepositry.Update(postion);
            _positionRepositry.Save();
            return new BaseResponce<PositionDto>
            {
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponce<PositionDto>> Get(int id)
        {
            
            var role = await _positionRepositry.Get(id);
            if (role == null) return new BaseResponce<PositionDto>
            {
                Message = "Role not found",
                Status = false,
            };
            return new BaseResponce<PositionDto>
            {
                Message = "Successful",
                Status = true,
                Data = new PositionDto
                {
                    Id = role.Id,
                    Name = role.Name
                },
            };
         
        }

        public async Task<BaseResponce<IEnumerable<PositionDto>>> GetAll()
        {
            var roles = await _positionRepositry.GetAll();
            var listOfRoles = roles.ToList().Select(a => new PositionDto
            {
                Id = a.Id,
                Name = a.Name,
            });
             return new BaseResponce<IEnumerable<PositionDto>>
            {
                Message = "Secessfully Get All",
                Status = true,
                Data = listOfRoles,
            };
        }

         public async Task<List<Position>> GetlistPostios()
        {
            var add = await _positionRepositry.GetAll();
            if (add == null)
                return null;
            return add.ToList();
        }
    }
}