using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Actors;
using RDMS.Models.Dtos;

namespace RDMS.Implimentation.Service
{
    public class BranchManagerServices : IBranchMangerService
    {
        private readonly IBranchManagerRepositry _branchManagerRepository;
        private readonly IBranchRepositry _branchRepository;
        private readonly IEateryAdminRepositry _eateryAdminRepositry;
        private readonly IRoleRepositry _roleRepository;
        private readonly IUserRepositry _userRepositry;

        public BranchManagerServices(IBranchManagerRepositry branchManagerRepository, IBranchRepositry branchRepository, IEateryAdminRepositry eateryAdminRepositry, IRoleRepositry roleRepository, IUserRepositry userRepositry)
        {
            _branchManagerRepository = branchManagerRepository;
            _branchRepository = branchRepository;
            _eateryAdminRepositry = eateryAdminRepositry;
            _roleRepository = roleRepository;
            _userRepositry = userRepositry;
        }

        public Task<BaseResponce<BrancheManagerDtoFordertails>> AddBranchManager(LoginUserModel user, string branchId)
        {
            throw new NotImplementedException();
        }


        public async Task<BaseResponce<CreateBranchsRequestsModels>> Delete(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public  async Task<BaseResponce<IEnumerable<BrancheManagerDtoFordertails>>> GetAllBranchManager()
        {
            var managers = await _branchManagerRepository.GetAll();
            var getall = managers.Select(
                manager => new  BrancheManagerDtoFordertails
                {
                    Id =  manager.Id,
                FirstName =manager.User.FirstName,
                LastName =manager.User.LastName,
                Email =manager.User.Email,
                Password =manager.User.Password,
                PhoneNumber =manager.User.PhoneNumber,
                }
            );

            // if (managers == null)
            // {
            //     return new BaseResponce<IEnumerable<BrancheManagerDtoFordertails>>
            //     {
            //         Status = false,
            //         Message = "Manager does not found"
            //     };
            // }


            return new BaseResponce<IEnumerable<BrancheManagerDtoFordertails>>
            {
                Status = true,
                Message = "Successful",
                Data = getall,
                //  managers.Select(m => CreateBranchManagerDto(m))
            };
            // throw new NotImplementedException();
        }

        public async Task<BaseResponce<BrancheManagerDtoFordertails>> GetBranchManager(int id)
        {
             var branchmanager = await _userRepositry.GetDetails(i => i.Id == id);//Get(i => i.Id == id);
            if (branchmanager != null)
            {
                return new BaseResponce<BrancheManagerDtoFordertails>
                {
                    Message = "Successful",
                    Status = true,
                    Data 
                  //= CreatebranchmanagerDto(branchmanager)
                    = new BrancheManagerDtoFordertails
                    {
                        Id = branchmanager.Id,
                       BranchId = branchmanager.Id,
                       EateryId = branchmanager.Id,
                       FirstName = branchmanager.FirstName,
                       LastName = branchmanager.LastName,
                       PhoneNumber = branchmanager.PhoneNumber,
                       Email = branchmanager.Email,
                       Password = branchmanager.Password,
                       //Positions = staff. 
                       //FullName = $"{staff.User.FirstName} {staff.User.LastName}",
                    }
                };
            }
            return new BaseResponce<BrancheManagerDtoFordertails> { Message = "Not found", Status = false, };
       
        }

        public async Task<BaseResponce<IEnumerable<BrancheManagerDtoFordertails>>> GetBranchManagerByCompanyId(int branchId)
        {
            var staff = await _branchManagerRepository.GetSelected(a => a.BranchId == branchId);
            if (staff != null)
            {
                return new BaseResponce<IEnumerable<BrancheManagerDtoFordertails>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = staff.Select(
                        s =>
                            new BrancheManagerDtoFordertails
                            {
                                
                                Id = s.Id,
                               BranchId = s.BranchId,
                                FirstName = s.User.FirstName,
                                LastName = s.User.LastName,
                                PhoneNumber = s.User.PhoneNumber,
                                Email = s.User.Email,
                                Password = s.User.Password,
                                
                            }
                    )
                };
            }
            return new BaseResponce<IEnumerable<BrancheManagerDtoFordertails>>
            {
                Message = "Not found",
                Status = false,
            };
        }

        public async Task<BaseResponce<BrancheManagerDtoFordertails>> UpdateManager(int userId, UpdateBranchManagerRequestModel model)
        {
              var manager = await _branchManagerRepository.Get(s => s.Id == model.Id);
            if (manager == null)
            {
                return new BaseResponce<BrancheManagerDtoFordertails>
                {
                    Status = false,
                    Message = "Manager does not exist"
                };
            }


            // manager.UpdatedBy = userId;
            manager.User.FirstName = model.FirstName;
            manager.User.LastName = model.LastName;
            manager.User.PhoneNumber = model.PhoneNumber;
            manager.User.Email = model.Email;
            // manager.User.IsActive = model.IsActive;
            // manager.User.UpdatedBy = userId;


            _branchManagerRepository.Update(manager);
            _branchManagerRepository.Save();

            return new BaseResponce<BrancheManagerDtoFordertails>
            {
                Status = true,
                Message = "Manager updated",
                // Data = CreateBranchManagerDto(manager)
            };
        }



       
    }
}