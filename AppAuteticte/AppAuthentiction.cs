using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Models.Dtos;

namespace RDMS.AppAuteticte
{
    public class AppAuthentiction : IAppAuthentication
    {
        private readonly IBranchManagerRepositry _branchManagerRepositry;
        private readonly IEateryRepositry _eateryRepositry;
        private readonly IEateryAdminRepositry _eateryAdminRepositry;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStaffRepositry _staffRepository;
        private readonly IUserRepositry _userRepository;

        public AppAuthentiction(IBranchManagerRepositry branchManagerRepositry, IEateryRepositry eateryRepositry, IEateryAdminRepositry eateryAdminRepositry, IHttpContextAccessor httpContextAccessor, IStaffRepositry staffRepository, IUserRepositry userRepository)
        {
            _branchManagerRepositry = branchManagerRepositry;
            _eateryRepositry = eateryRepositry;
            _eateryAdminRepositry = eateryAdminRepositry;
            _httpContextAccessor = httpContextAccessor;
            _staffRepository = staffRepository;
            _userRepository = userRepository;
        }

        public  string GenerateStaffNo(string Name, int id)
        {
             if (Name == "EateryManager")
            {
                return $"CMG/0000"
                    + _eateryAdminRepositry.GetSelected(c => c.EateryId == id).Result
                    + 1;
            }
              else if (Name == "BrachManager")
            {
                return $"CMG/0000"
                    +  _branchManagerRepositry.GetSelected(c => c.BranchId == id).Result
                    + 1;
            }
              else if (Name == "Cutomer")
            {
                return $"CMG/0000"
                    +  _branchManagerRepositry.GetSelected(c => c.BranchId == id).Result
                    + 1;
            }
            else
            {
                return $"STF/0000"
                    + _staffRepository.GetSelected(c => c.EateryId == id).Result
                    + 1;
            }
        }

        public async Task<LoginUserModel> getLoginUser()
        {
              try
            {
                var userId = int.Parse(_httpContextAccessor.HttpContext.User
                    .FindFirst(ClaimTypes.NameIdentifier)
                    .Value);

                var user = await _userRepository.Get(userId);
                var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value ?? "";

                var EateryId = "";
                var branchId = "";

                if (role == "EateryManager")
                {
                    // EateryId = ( user.EateryAdmin.EateryId).ToString();
                    // branchId = user.BranchManager.BranchId.ToString();
                }
                else if (role == "BrachManager")
                {
                    // EateryId = ( user.EateryAdmin.EateryId).ToString();
                    // branchId = user.BranchManager.BranchId.ToString();
                }
                else if (role == "Staff")
                {
                    // EateryId = user.Staff.EateryId.ToString();
                    // branchId = user.Staff.BranchId.ToString();
                }

                return new LoginUserModel
                {
                    Status = true,
                    UserId = userId,
                    BranchId = Convert.ToInt32(branchId),
                    EateryId = Convert.ToInt32(EateryId),
                    RoleName = role,
                    Data = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        UserRoles = user.UserRoles
                        .Select(
                            h => new RoleDto
                            {
                                Id = h.Role.Id,
                                Name = h.Role.Name,
                                // Id = h.User.Id,
                                // Name = user.
                            } 
                        ).ToList(),
                        
                        
                    }
                };
            }
            catch (System.Exception ex)
            {
                // throw new Exception(ex.Message);
                return new LoginUserModel { Status = false, Message = ex.Message };
            }
            throw new NotImplementedException();
        }

         public static string GetRouteData(string key)
        {
            try
            {
                IHttpContextAccessor _httpAccessor = new HttpContextAccessor();
                var routeId = _httpAccessor.HttpContext.GetRouteValue(key).ToString();
                return routeId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> SetClaimsAndCookies(UserDto Data)
        {
            throw new NotImplementedException();
        }
    }
}
