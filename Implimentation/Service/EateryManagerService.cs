using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Actors;
using RDMS.Models.Dtos;

namespace RDMS.Implimentation.Service
{
    public class EateryManagerService : IEateryManagerServices
    {
            private readonly IBranchManagerRepositry _branchManagerRepository;
            private readonly IBranchRepositry _branchRepository;
            private readonly IEateryAdminRepositry _eateryAdminRepositry;
            private readonly IRoleRepositry _roleRepository;
            private readonly IUserRepositry _userRepository;


        // public async Task<BaseResponce<EateryDto>> CreateEatery(CreataEateryRequestModel model)
        // {
        //     var eatery = await _eateryRepositry.Get(e => e.Name == model.Name);
        //     if (eatery != null)
        //     {
        //         return new BaseResponce<EateryDto>
        //         {
        //             Message = "Eatery already exist",
        //             Status = false
        //         };
        //     }
        //         var Certificate = _fileUploders.UploadFile(model.Certificate);
        //     var logo = _fileUploders.UploadFile(model.Logo);
        //     var newEatery = new Eatery
        //     {
        //         Name = model.Name,
        //         CertificationNumber = model.CertificationNumber,
        //         Certificate = Certificate,
        //         Logo = logo,
        //     };
        //      await _eateryRepositry.Add(newEatery);
        //     _eateryRepositry.Save();
        //     return new BaseResponce<EateryDto>
        //     {
        //         Message = "Successful",
        //         Status = true,
        //         Data = new EateryDto
        //         {
        //             Id = newEatery.Id,
        //             Name = newEatery.Name,
        //             CertificationNumber = newEatery.CertificationNumber,
        //             Certificate = newEatery.Certificate,
        //             Logo = newEatery.Logo
        //         }
        //     };
        // }
      

        public async Task<BaseResponce<IEnumerable<EateryAdminDto>>> GetManagerByCompanyId(int companyId)
        {
              var managers = await _eateryAdminRepositry.GetSelected(c => c.EateryId == companyId);
            if (managers == null)
            {
                return new BaseResponce<IEnumerable<EateryAdminDto>>
                {
                    Status = false,
                    Message = "Manager does not found"
                };
            }


            return new BaseResponce<IEnumerable<EateryAdminDto>>
            {
                Status = true,
                Message = "Successful",
                Data = managers.Select(
                     n => new EateryAdminDto
                    {
                        Id = n.Id,
                        EateryId = companyId,
                        FirstName = n.User.FirstName,
                        LastName = n.User.LastName,
                        Email = n.User.Email,
                        PhoneNumber = n.User.PhoneNumber,
                    }
                )
            };
        }

        

       

       

      public async Task<BaseResponce<EateryAdminDto>> Delete(int userId, int id)
        {

             var objExists = await _branchManagerRepository.Get(a => a.Id == id);
            if (objExists != null)
            {
                objExists.IsDeleted = true;
                _branchManagerRepository.Delete(objExists);
                _branchManagerRepository.Save();

                return new BaseResponce<EateryAdminDto>
                {
                    Message = "Successful",
                    Status = true
                };
            }
             return new BaseResponce<EateryAdminDto>
            {
                Message = "Manager not found",
                Status = false
            };
        }

     public async Task<BaseResponce<IEnumerable<EateryAdminDto>>> GetAllManager()
        {
             var companyManagers =  await _eateryAdminRepositry.GetAll();
            if (companyManagers is not null)
            {
                return  new BaseResponce<IEnumerable<EateryAdminDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = companyManagers.Select(m =>  new EateryAdminDto
                    {
                        Id = m.Id,
                        EateryId = m.EateryId,
                        FirstName = m.User.FirstName,
                        LastName = m.User.LastName,
                        Email = m.User.Email,
                        PhoneNumber = m.User.PhoneNumber,
                    })
                };
            }
            return  new BaseResponce<IEnumerable<EateryAdminDto>> 
            {
                Message = "Not found",
                Status = false,
            };
            throw new NotImplementedException();
        }

       public async Task<BaseResponce<EateryAdminDto>> GetManager(string email)
        {
            // var companyManager = await _userRepository.Get(id);
            var companyManager = await _userRepository.GetEmail(email);
                if (companyManager is not null)
                {
                    return new BaseResponce<EateryAdminDto>
                    {
                        Message = "Successful",
                        Status = true,
                        Data = new EateryAdminDto
                        {
                            Id = companyManager.Id,
                            // EateryId = companyManager.EateryId,
                            // FirstName = companyManager.User.FirstName,
                            // LastName = companyManager.User.LastName,
                            // Email = companyManager.User.Email,
                            // PhoneNumber = companyManager.User.PhoneNumber,
                            // RegNumber = companyManager.RegNumber,
                            // EateryId = companyManager.EateryAdmin.EateryId,
                            FirstName = companyManager.FirstName,
                            LastName = companyManager.LastName,
                            Email = companyManager.Email,
                            PhoneNumber = companyManager.PhoneNumber,
                            // RegNumber = companyManager.
                        }
                    };
                }
                return new BaseResponce<EateryAdminDto>
                {
                    Message = "Not found",
                    Status = false,
                };
        }
       public async Task<BaseResponce<EateryAdminDto>> Get(int id)
        {
            // var companyManager = await _userRepository.Get(id);
            var companyManager = await _userRepository.GetDetails(i => i.Id == i.Id);
                if (companyManager is not null)
                {
                    return new BaseResponce<EateryAdminDto>
                    {
                        Message = "Successful",
                        Status = true,
                        Data = new EateryAdminDto
                        {
                            Id = companyManager.Id,
                            // EateryId = companyManager.EateryId,
                            // FirstName = companyManager.User.FirstName,
                            // LastName = companyManager.User.LastName,
                            // Email = companyManager.User.Email,
                            // PhoneNumber = companyManager.User.PhoneNumber,
                            // RegNumber = companyManager.RegNumber,
                            // EateryId = companyManager.EateryAdmin.EateryId,
                            FirstName = companyManager.FirstName,
                            LastName = companyManager.LastName,
                            Email = companyManager.Email,
                            PhoneNumber = companyManager.PhoneNumber,
                            // RegNumber = companyManager.
                        }
                    };
                }
                return new BaseResponce<EateryAdminDto>
                {
                    Message = "Not found",
                    Status = false,
                };
        }

       

        public    async Task<BaseResponce<IEnumerable<EateryAdminDto>>> GetSelectedManager(List<int> ids)
        {
             var managers = await  _eateryAdminRepositry.GetSelected(c => ids.Contains(c.EateryId) && !c.IsDeleted);
            if (managers == null)
            {
                return new BaseResponce<IEnumerable<EateryAdminDto>>
                {
                    Status = false,
                    Message = "Manager does not found"
                };
            }
            return new BaseResponce<IEnumerable<EateryAdminDto>>
            {
                Status = true,
                Message = "Successful",
                Data = managers.Select(
                     n => new EateryAdminDto
                    {
                        Id = n.Id,
                        EateryId = n.EateryId,
                        FirstName = n.User.FirstName,
                        LastName = n.User.LastName,
                        Email = n.User.Email,
                        PhoneNumber = n.User.PhoneNumber,
                        RegNumber = n.RegNumber,
                    }
                )
            };
        }

        public   async Task<BaseResponce<EateryAdminDto>> UpdateManager(int id, UpdateEateryManagerRequestModel model)
        {
             var companyManager = await _branchManagerRepository.Get(id);
                if (companyManager is not null)
                {
                    companyManager.UserId = model.Id;
                    companyManager.User.Email = model.Email;
                    companyManager.User.FirstName = model.FirstName;
                    companyManager.User.LastName = model.LastName;
                    companyManager.User.PhoneNumber = model.PhoneNumber;

                    _branchManagerRepository.Update(companyManager);
                    _branchManagerRepository.Save();
                    return new BaseResponce<EateryAdminDto>
                    {
                        Message = "Successfully",
                        Status = true,
                        Data = new EateryAdminDto
                        {
                            Email = companyManager.User.Email,
                            FirstName = companyManager.User.FirstName,
                            LastName = companyManager.User.LastName,
                            PhoneNumber = companyManager.User.PhoneNumber,
                        }
                    };
                }
                return new BaseResponce<EateryAdminDto>
                {
                    Message = "Not found",
                    Status = false,
                };

        }
    }
}