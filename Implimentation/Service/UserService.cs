using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;

namespace RDMS.Implimentation.Service
{
    public class UserService : IUserServices
    {
        private readonly IUserRepositry _userrepo;
        private readonly IRoleRepositry _RoleRepo;
        private readonly IEateryAdminRepositry _eateryAdminRepositry;


        public UserService(IUserRepositry userrepo, IRoleRepositry RoleRepo , IEateryAdminRepositry eateryAdminRepositry)
        {
            _userrepo = userrepo;
            _RoleRepo = RoleRepo;
            _eateryAdminRepositry = eateryAdminRepositry;
        }

        public async Task<BaseResponce<UserDto>> Get(int id)
        {
            var user = await _userrepo.Get(id);
            if (user == null)
                return new BaseResponce<UserDto>
             { 
                Message = "User not found",
                 Status = false, 
            };

            return new BaseResponce<UserDto>
            {
                Message = "Success",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    UserRoles = user.UserRoles
                        .Select(
                            a =>
                                new RoleDto
                                {
                                    Id = a.Role.Id,
                                    Name = a.Role.Name,
                                }
                        )
                        .ToList(),
                }
            };
        }

        public async Task<BaseResponce<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userrepo.GetAll();
            var listOfUsers = users .ToList().Select(user => new UserDto
           {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserRoles = user.UserRoles
                .Select(
                    a =>new RoleDto{
                        Id = a.Role.Id,
                        Name = a.Role.Name,
                    }).ToList(),
                });
            return new BaseResponce<IEnumerable<UserDto>>
            {
                Message = "success",
                Status = true,
                Data = listOfUsers,
            };
        }

        public async Task<BaseResponce<UserDto>> Login(LogingRequesteModel model)
        {
            var userLogin = await _userrepo.Get(
                a => a.Email == model.Email && a.Password == model.Password
            );
            if (userLogin == null)
                return new BaseResponce<UserDto>
                {
                    Message = "invalid Login Details",
                    Status = false,
                };
            else
            {
                return new BaseResponce<UserDto>
                {
                    Message = "login successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Id = userLogin.Id,
                        FirstName = userLogin.FirstName,
                        LastName = userLogin.LastName,
                        Email = userLogin.Email,
                        Password = userLogin.Password,
                        PhoneNumber = userLogin.PhoneNumber,
                        UserRoles = userLogin.UserRoles
                            .Select(
                                a =>
                                    new RoleDto
                                    {
                                        Id = a.Role.Id,
                                        Name = a.Role.Name,
                                    }
                            )
                            .ToList(),
                    }
                };
            }

            // if (userLogin.ISAproved = false )
            //     return new BaseResponce<UserDto>
            //     {
            //         Message = "invalid Login Details",
            //         Status = false,
            //     };
            // else
            // {
            //     return new BaseResponce<UserDto>
            //     {
            //         Message = "login successful",
            //         Status = true,
            //         Data = new UserDto
            //         {
            //             Id = userLogin.Id,
            //             FirstName = userLogin.FirstName,
            //             LastName = userLogin.LastName,
            //             Email = userLogin.Email,
            //             Password = userLogin.Password,
            //             PhoneNumber = userLogin.PhoneNumber,
            //             UserRoles = userLogin.UserRoles
            //                 .Select(
            //                     a =>
            //                         new RoleDto
            //                         {
            //                             Id = a.Role.Id,
            //                             Name = a.Role.Name,
            //                         }
            //                 )
            //                 .ToList(),
            //         }
            //     };
            // }

        }

       
    }
}
