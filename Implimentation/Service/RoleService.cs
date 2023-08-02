using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.Models.Identity;

namespace RDMS.Implimentation.Service
{
    public class RoleService : IRoleServices
    {
        private readonly IRoleRepositry _roleRepo;

        public RoleService(IRoleRepositry roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<BaseResponce<RoleDto>> Create(CreateRoleRequestModel model)
        {
            var determineRoleExist = await _roleRepo.Get(r => r.Name == model.Name);
            if (determineRoleExist != null)
                return new BaseResponce<RoleDto>
                {
                    Message = "Role Already Exist",
                    Status = false,
                };

            var role = new Role { Name = model.Name, };

            await _roleRepo.Add(role);
            _roleRepo.Save();

            return new BaseResponce<RoleDto>
            {
                Message = "Created Successfully",
                Status = true,
                Data = new RoleDto
                 {
                     Name = role.Name }
            };
        }

        public async Task<BaseResponce<RoleDto>> Delete(int id)
        {
            var role = await _roleRepo.Get(id);
            if (role is null)
            {
                return new BaseResponce<RoleDto> { Message = "Not Found", Status = false };
            }
            role.IsDeleted = true;
            await _roleRepo.Delete(role);
            _roleRepo.Save();
            return new BaseResponce<RoleDto> { Message = "Successful", Status = true };
        }

        public async Task<BaseResponce<RoleDto>> Get(int id)
        {
            var role = await _roleRepo.DeleteAsync(id);
            if (role == null)
                return new BaseResponce<RoleDto> { Message = "Role not found", Status = false, };
            return new BaseResponce<RoleDto>
            {
                Message = "Successful",
                Status = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    UserRoles = role.UserRoles
                        .Select(
                            v =>
                                new UserDto
                                {
                                    FirstName = v.User.FirstName,
                                    LastName = v.User.LastName,
                                    Email = v.User.Email,
                                }
                        )
                        .ToList(),
                },
            };
        }

        public async Task<BaseResponce<IEnumerable<RoleDto>>> GetAll()
        {
            var roles = await _roleRepo.GetAll();
            var listOfRoles = roles
                .ToList()
                .Select(
                    a =>
                        new RoleDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            UserRoles = a.UserRoles
                                .Select(
                                    b =>
                                        new UserDto
                                        {
                                            FirstName = b.User.FirstName,
                                            LastName = b.User.LastName,
                                            Email = b.User.Email,
                                        }
                                )
                                .ToList(),
                        }
                );
            return new BaseResponce<IEnumerable<RoleDto>>
            {
                Message = "Secessfully Get All",
                Status = true,
                Data = listOfRoles,
            };
        }
    }
}
