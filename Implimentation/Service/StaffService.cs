using System.Security.Claims;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Actors;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;

namespace RDMS.Implimentation.Service
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepositry _staffRepositry;
        private readonly IUserRepositry _userRepository;
        private readonly IBranchRepositry _branchRepositry;
        private readonly IBranchManagerRepositry _branchManagerRepositry;
        private readonly IAddressRepositry _addressRepositry;

        private readonly IPositionReposity _positionRepositry;
        private readonly IRoleRepositry _roleRepositry;
        private readonly IEateryAdminRepositry _eateryAdminRepositry;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaffService(IStaffRepositry staffRepositry, IUserRepositry userRepository, IBranchRepositry branchRepositry, IBranchManagerRepositry branchManagerRepositry, IAddressRepositry addressRepositry, IPositionReposity positionRepositry, IRoleRepositry roleRepositry, IEateryAdminRepositry eateryAdminRepositry, IHttpContextAccessor httpContextAccessor)
        {
            _staffRepositry = staffRepositry;
            _userRepository = userRepository;
            _branchRepositry = branchRepositry;
            _branchManagerRepositry = branchManagerRepositry;
            _addressRepositry = addressRepositry;
            _positionRepositry = positionRepositry;
            _roleRepositry = roleRepositry;
            _eateryAdminRepositry = eateryAdminRepositry;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponce<StaffDto>> Create(
          int UserId,CreateStaffRequestModel model)
        {
            var staffExist = await _userRepository.Get(s => s.Email == model.Email);
            if (staffExist != null)
            {
                return new BaseResponce<StaffDto>
                {
                    Message = "Email already exist",
                    Status = false
                };
            }
            var address = new Address
            {
                Street = model.Street,
                State = model.State,
                City = model.City,
                ZipCode = model.ZipCode
            };
            // var allUser = await _userRepository.GetAll();
            // var userId = allUser.Count() + 1;
            User userName = new User
            {
               
                FirstName = model.FirstName,
                PhoneNumber = model.PhoneNumber,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Address = address,
                AddressId = address.Id,
                //address.Id,
            };
             await  _userRepository.Add(userName);
             _userRepository.Save();
            var role = await _roleRepositry.Get(g => g.Name == "Staff");
            var userRole = new UserRole
            {
                UserId = userName.Id,
                User = userName,
                RoleId = role.Id,
                Role = role,
            };
                userName.UserRoles.Add(userRole);
            var branch = await _branchManagerRepositry.Get(UserId);
            var branch1 = await _branchRepositry.GetDetails(branch.BranchId);
            if (branch == null)
            {
                return new BaseResponce<StaffDto>
                {
                    Message = "not successfully created",
                    Status = true,
                };
            }

            var staff = new Staff
            {
                // Id = userId,
                BranchId = branch.BranchId,
                EateryId = branch1.EateryId,
                Roles = model.Roles,
                StaffRegNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper(),
               UserId = userName.Id
            //  User = staffExist.
            };

          //  userName.UserRoles.Add(userRole);
          //  await  _addressRepositry.Add(address);
            
            await  _staffRepositry.Add(staff);
              _userRepository.Save();

            return new BaseResponce<StaffDto>
            {
                Message = "Staff successfully created",
                Status = true,
                Data = new StaffDto
                {
                    Id = staff.Id,
                    BranchId = staff.Eatery.Id,
                    EateryId = staff.EateryId,
                    FirstName = staff.User.FirstName,
                    LastName = staff.User.LastName,
                    Email = staff.User.Email,
                    PhoneNumber = staff.User.PhoneNumber,
                }
            };
        }



        public async Task<BaseResponce<StaffDto>> AddStaff(CreateStaffRequestModel model)
        {
            var userExist = await _userRepository.Get(u => u.Email == model.Email);
            if (userExist != null)
            {
                return new BaseResponce<StaffDto>
                {
                    Status = false,
                    Message = "Staff already exist"
                };
            }
            var address = new Address
            {
                Street = model.Street,
                State = model.State,
                City = model.City,
                ZipCode = model.ZipCode
            };
            var role = await _roleRepositry.Get(r => r.Name == "Staff");
            User newUser = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                AddressId = address.Id,
                Address = address,
            };
            var branch = await _branchRepositry.GetDetails(model.BranchId);
            var userRole = new UserRole
            {
                UserId = newUser.Id,
                User = newUser,
                RoleId = role.Id,
                Role = role,
            };
            var staff = new Staff
            {
                BranchId = model.BranchId,
                EateryId = branch.EateryId,
                StaffRegNumber = Guid.NewGuid()
                    .ToString()
                    .Replace("-", "")
                    .Substring(0, 5)
                    .ToUpper(),
                UserId = newUser.Id,
                Roles = model.Roles
            };

            await _userRepository.Add(newUser);
            await _staffRepositry.Add(staff);
            _staffRepositry.Save();

            return new BaseResponce<StaffDto>
            {
                Status = true,
                Message = "Staff created",
                Data = CreateStaffDto(staff)
            };
        }

        public async Task<BaseResponce<StaffDto>> Get(int id)
        {
            var staff = await _userRepository.Get(i => i.Id == id);
            // var staff = await _staffRepositry.Get(id);//Get(i => i.Id == id);
            if (staff != null)
            {
                return new BaseResponce<StaffDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data 
                  //= CreateStaffDto(staff)
                    = new StaffDto
                    {
                        Id = staff.Id,
                       BranchId = staff.Id,
                       EateryId = staff.Id,
                       FirstName = staff.FirstName,
                       LastName = staff.LastName,
                       PhoneNumber = staff.PhoneNumber,
                       Email = staff.Email,
                       Password = staff.Password,
                    //    Roles = staff.,
                    }
                };
            }
            return new BaseResponce<StaffDto> { Message = "Not found", Status = false, };
        }

        public async Task<BaseResponce<IEnumerable<StaffDto>>> GetAllStaff()
        {
            var staffs = await _staffRepositry.GetAll();
            //var position = await _positionRepositry.GetAll();
            if (staffs != null)
            {
                return new BaseResponce<IEnumerable<StaffDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = staffs.Select(
                        s =>
                            new StaffDto
                            {
                                Id = s.Id,
                                BranchId = s.BranchId,
                                EateryId = s.EateryId,
                                FirstName = s.User.FirstName,
                                LastName = s.User.LastName,
                                PhoneNumber = s.User.PhoneNumber,
                                Email = s.User.Email,
                                Password = s.User.Password,
                                Roles  = s.Roles,
                            }
                    ).ToList()
                };
            }
            return new BaseResponce<IEnumerable<StaffDto>>
            {
                Message = "NotFound",
                Status = false,
            };
        }

        public async Task<BaseResponce<IEnumerable<StaffDto>>> GetStaffsByBranchId(int id )
        {
        //   var staff = await _staffRepositry.GetSelected(a => a.EateryId ==  id   && !a.User.IsDeleted);
          var staff = await _staffRepositry.GetSelected(a => a.EateryId ==  id || a.BranchId == id && !a.User.IsDeleted);
            if (staff == null)
            {
                return new BaseResponce<IEnumerable<StaffDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = staff.Select(
                        s =>
                            new StaffDto
                            {
                                Id = s.Id,
                               BranchId = s.BranchId,
                                EateryId = s.EateryId,
                                FirstName = s.User.FirstName,
                                LastName = s.User.LastName,
                                PhoneNumber = s.User.PhoneNumber,
                                Email = s.User.Email,
                                Password = s.User.Password,
                                Roles = s.Roles,
                            }
                    )
                };
            }
            return new BaseResponce<IEnumerable<StaffDto>>
            {
                Message = "Not found",
                Status = false,
            };
        }

        public async Task<BaseResponce<IEnumerable<StaffDto>>> GetStaffsByEateryId(int EateryId)
        {

            // var user = await _staffRepositry.Get(a => a.User.Id == EateryId);
            var user = await _userRepository.Get(EateryId);
            //var staff = await _staffRepositry.GetSelected(a => a.Eatery.User.Id == EateryId);
            var staff = await _staffRepositry.GetSelected(a => a.Branch.Id == EateryId);
            if (staff is not null)
            {
                return new BaseResponce<IEnumerable<StaffDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = staff.Select(
                        s =>
                            new StaffDto
                            {
                                Id = s.Id,
                                BranchId = s.BranchId,
                                EateryId = s.EateryId,
                                FirstName = s.User.FirstName,
                                LastName = s.User.LastName,
                                PhoneNumber = s.User.PhoneNumber,
                                Email = s.User.Email,
                                Password = s.User.Password,
                                // FullName = $"{s.User.FirstName} {s.User.LastName}",
                            }
                    )
                };
            }
            return new BaseResponce<IEnumerable<StaffDto>>
            {
                Message = "Not found",
                Status = false,
            };
        }

        public async Task<BaseResponce<StaffDto>> RemoveStaff(int id)
        {
            var staff = await _userRepository.Get(id);
            if (staff == null)
            {
                return new BaseResponce<StaffDto> { Message = "Staff Not Found", Status = false };
            }
            await _userRepository.Delete(staff);
            _userRepository.Save();
            return new BaseResponce<StaffDto>
            {
                Message = "Staff Deleted Successfully",
                Status = true
            };
        }

           public async Task<BaseResponce<StaffDto>> DeleteAsync(int id)
        {
            var staff = await _staffRepositry.Get(id);
            if (staff == null)
            {
                return new BaseResponce<StaffDto>
                {
                    Message = "Staff Not Found",
                    Status = false
                };
            }

            await _staffRepositry.DeleteAsync(staff);
                return new BaseResponce<StaffDto>
                {
                    Message = "Staff Deleted Successfully",
                    Status = true
                };
        }

        private StaffDto CreateStaffDto(Staff staff)
        {
            return new StaffDto
            {
                Id = staff.Id,
                Branch = new BranchDto
                {
                    Id = staff.Branch.Id,
                    AddressName = staff.Branch.Address,
                    Name = staff.Branch.Name,
                },
                BranchId = staff.BranchId,
                Eatery = new EateryDto
                {
                    Id = staff.Eatery.Id,
                    Name = staff.Eatery.Name,
                    Logo = staff.Eatery.Logo,
                    IsVerify = staff.Eatery.IsVerify,
                },
                EateryId = staff.EateryId,
                CreatedBy = staff.CreatedBy,
                User = new UserDto
                {
                    Id = staff.User.Id,
                    Email = staff.User.Email,
                    FirstName = staff.User.FirstName,
                    LastName = staff.User.LastName,
                    PhoneNumber = staff.User.PhoneNumber,
                    IsDeleted = staff.User.IsDeleted,
                },
                UserId = staff.UserId,
                IsDeleted = staff.IsDeleted,
            };
        }

        public async Task<BaseResponce<StaffDto>> Update(int id, UpdateStaffRequestModel model)
        {
            var staff = await _staffRepositry.Get(id);
             if (staff is not null)
            {
                staff.User.FirstName = model.FirstName;
                staff.User.LastName = model.LastName;
                staff.User.Email = model.Email;
                staff.User.PhoneNumber = model.PhoneNumber;
                staff.Roles = model.Roles;
                staff.User.Password = model.Password;
                
                _staffRepositry.Update(staff);
                _staffRepositry.Save();
                return new BaseResponce<StaffDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new StaffDto
                    {
                        FirstName = staff.User.FirstName,
                        LastName = staff.User.LastName,
                        PhoneNumber = staff.User.PhoneNumber,
                        Email = staff.User.Email,
                        Roles = staff.Roles,
                         Password = staff.User.Password
                    }
                };
            }
            return new BaseResponce<StaffDto>
            {
                Message = "Not found",
                Status = false,
            };
        }

    }
}
