using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Actors;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.Models.Identity;
using RDMS.ViewModel;


namespace RDMS.Implimentation.Service
{
    public class EateryServices : IEateryServices
    {
        private readonly IEateryRepositry _eateryRepositry;
        private readonly IFileUploders _fileUploders;
        private readonly IRoleRepositry _roleRepositry;
        private readonly IEateryAdminRepositry _eateryAdminRepositry;
        private readonly IUserRepositry _userRepositry;
        private readonly IMailServices _mailService;

        public EateryServices(IEateryRepositry eateryRepositry, IFileUploders fileUploders, IRoleRepositry roleRepositry, IEateryAdminRepositry eateryAdminRepositry, IUserRepositry userRepositry, IMailServices mailService)
        {
            _eateryRepositry = eateryRepositry;
            _fileUploders = fileUploders;
            _roleRepositry = roleRepositry;
            _eateryAdminRepositry = eateryAdminRepositry;
            _userRepositry = userRepositry;
            _mailService = mailService;
        }

        public async Task<BaseResponce<EateryDto>> CreateEatery( CreataEateryRequestModel model)
        {
             var checkExsit = await _eateryAdminRepositry.Get(a => a.User.Email == model.Email);
             if (checkExsit != null)
            {
                return new BaseResponce<EateryDto>
                {
                    Message = "User already exist",
                    Status = false,
                };
            }
            var eateryExist = await _eateryRepositry.Get(e => e.Name == model.Name);
            if (eateryExist != null)
            {
                return new BaseResponce<EateryDto>
                {
                    Message = "Eatery already exist",
                    Status = false
                };
            }
            var Certificate = _fileUploders.UploadFile(model.Certificate);
            var logo = _fileUploders.UploadFile(model.Logo);
             var allUser = await _userRepositry.GetAll();
            var userId = allUser.Count() + 1;
            var address = new Address
            {

                Street = model.Street,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode
            };

            var user = new User
            {
               // Id = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                AddressId = address.Id,
                Address = address,
                Wallet = 0,
            };
                await  _userRepositry.Add(user);
                 _userRepositry.Save();
            var role = await _roleRepositry.Get(b => b.Name == "Eatery");
            var userRole = new UserRole
            {
                UserId = user.Id,
                User = user,
                RoleId = role.Id,
                Role = role,
            };

            user.UserRoles.Add(userRole);

            var eatery = new Eatery
            {
                UserId = user.Id,
                Name = model.Name,
                CertificationNumber = model.CertificationNumber,
                Certificate = Certificate,
                Logo = logo,
            };

            var eatery1 = new EateryAdmin
            {
                UserId = user.Id,
                User = user,
                Eatery = eatery,
                EateryId = eatery.Id,
                RegNumber = $"ETRA" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper(),
            };
            
                await  _eateryRepositry.Add(eatery);
                await _eateryAdminRepositry.Add(eatery1);
            var mailRequest = new MailRequestDto
            {  
                Subject = "Complete Your Registration",
                ToEmail = user.Email,
                ToName = model.FirstName,
                HtmlContent =  $"Dear {user.LastName},this is to inform you that you have {user.FirstName}  register on Defeane Restaurant {DateTime.Now} ",
            };
            _mailService.SendEMailAsync(mailRequest);
            
            _eateryAdminRepositry.Save();
            _eateryRepositry.Save();
            return new BaseResponce<EateryDto>
            {
                Message = "Successful",
                Status = true,
                Data = new EateryDto
                {
                    Id = eatery.Id,
                    Name = eatery.Name,
                    CertificationNumber = eatery.CertificationNumber,
                    Certificate = eatery.Certificate,
                    Logo = eatery.Logo
                }
            };
        }

        public async Task<BaseResponce<EateryDto>> GetOneUser(int id)
        {
            var eatery = await _eateryRepositry.Getdetails(id);
            if (eatery != null)
            {
                return new BaseResponce<EateryDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new EateryDto
                    {
                        Id = eatery.Id,
                        Name = eatery.Name,
                        Certificate = eatery.Certificate,
                        Logo = eatery.Logo,
                        IsVerify = eatery.IsVerify,
                        Note = eatery.Note,
                        //Branches = eatery.Branches
                    },
                };
            }
            return new BaseResponce<EateryDto> { Message = "Eatery is not fund", Status = false };
        }

        public async Task<BaseResponce<EateryDto>> GetByAdminId(int admin)
        {
             var eatery = await _eateryRepositry.Get(o => o.UserId == admin);
            //var eatery = await _eateryRepositry.Get(o => o.Id == admin);
            if (eatery != null)
            {
                return new BaseResponce<EateryDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new EateryDto
                    {
                        Id = eatery.Id,
                        Name = eatery.Name,
                        CertificationNumber = eatery.CertificationNumber,
                        Certificate = eatery.Certificate,
                        Logo = eatery.Logo,
                        IsVerify = eatery.IsVerify,
                        Branches = eatery.Branches
                            .Select(
                                v =>
                                    new BranchDto
                                    {
                                        Id = v.Id,
                                        Name = v.Name,
                                        AddressName = v.Address,
                                        // Eatery =v.Eatery
                                    }
                            )
                            .ToList()
                    },
                };
            }
            return new BaseResponce<EateryDto> { Message = "Eatery is not fund", Status = false };
        }

         public async Task<BaseResponce<EateryDto>> DeleteEatery(int id)
        {
           var food = await _eateryRepositry.DeleteAsync(id);
            return new BaseResponce<EateryDto>
            {
                Message = "Food Deleted Successfully",
                Status = true
            };
        }
        

        public async Task<BaseResponce<IEnumerable<EateryDto>>> GetAllEatery()
        {
            var Eatery = await _eateryRepositry.GetAll();
            if (Eatery == null)
            {
                return new BaseResponce<IEnumerable<EateryDto>>
                {
                    Message = "Not found",
                    Status = false
                };
            }

            return new BaseResponce<IEnumerable<EateryDto>>
            {
                Message = "Successful",
                Status = true,
                Data = Eatery.Select(
                    e =>
                    new EateryDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        CertificationNumber = e.CertificationNumber,
                        Certificate = e.Certificate,
                        Note = e.Note,
                        Logo = e.Logo,
                        IsVerify = e.IsVerify,
                    }
                )
            };
        }

        public async Task<BaseResponce<EateryDto>> RemoveEatery(int id)
        {
            var objExists = await _eateryRepositry.Get(d => d.Id == id);
            if (objExists != null)
            {
                objExists.IsDeleted = true;
                await _eateryRepositry.Delete(objExists);
                _eateryRepositry.Save();
                return new BaseResponce<EateryDto> { Message = "Successful", Status = true };
            }
            return new BaseResponce<EateryDto>
            {
                Message = "Eatry Company already exists",
                Status = false
            };
        }

        
        public async Task<BaseResponce<EateryDto>> VerifyEatery( int id,
            VerifyEateryRequestModel model
        )
        {
            var eatery = await _eateryRepositry.Get(id);
            if (eatery == null)
            {
                return new BaseResponce<EateryDto>
                {
                    Message = " Eatery Not found",
                    Status = false,
                };
            }
            eatery.IsVerify = model.IsVerify;
            eatery.IsVerify = true;
            eatery.Note = "Verified";
            await _eateryRepositry.Update(eatery);
            _eateryRepositry.Save();
            return new BaseResponce<EateryDto> 
            { 
                Message = " Succesfuly Verify",
                Status = true,
            };
        }
        public async Task<BaseResponce<EateryDto>> NotVerify(
            int id,
            VerifyEateryRequestModel model
        )
        {
            var eatery = await _eateryRepositry.Get(id);
            if (eatery == null)
            {
                return new BaseResponce<EateryDto>
                {
                    Message = "  Not found",
                    Status = false,
                };
            }
            eatery.IsVerify = model.IsVerify;
            eatery.Note = "Not Yet Verify";
            await _eateryRepositry.Update(eatery);
            _eateryRepositry.Save();
            return new BaseResponce<EateryDto> 
            { 
                Message = " Not Verify",
                Status = true,
            };
        }

        public async Task<BaseResponce<EateryDto>> Update(int id, UpdateEateryRequestModel model)
        {
            var Eatery = await _eateryRepositry.Getdetails(id);
            if (Eatery is null && model.Name is null)
            {
                return new BaseResponce<EateryDto>
                 { 
                    Message = "Not found",
                     Status = false, 
                };
            }
            var Certificate  = "";
            var Logo  = "";
            if (model.Certificate != null || model.Logo != null)
            { 
                  Certificate = _fileUploders.UploadFile(model.Certificate);
                  Logo = _fileUploders.UploadFile(model.Logo);
            }
            
                Eatery.Name = model.Name;
                Eatery.CertificationNumber = model.CertificationNumber;
                Eatery.Certificate = Certificate;
                Eatery.Logo = Logo;
                await _eateryRepositry.Update(Eatery);
                _eateryRepositry.Save();    
                return new BaseResponce<EateryDto>
                {
                    Message = " Updated Successful",
                    Status = true,
                };
            
        }

        public Task<BaseResponce<EateryDto>> UpdateManager(int id, UpdateEateryManagerRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponce<IEnumerable<EateryDto>>> GetAllEateryOne(int id)
        {
            var user = await _userRepositry.GetDetails(l => l.Id == id);
             var eatery = await _eateryAdminRepositry.GetSelected(x => x.EateryId == user.Id);

            // var Eatery = await _eateryRepositry.GetSelected(a => a.User.EateryId == id);;
            var Eatery = await _eateryRepositry.GetSelected(a => a.User.Id == id);;
            if (Eatery == null)
            {
                return new BaseResponce<IEnumerable<EateryDto>>
                {
                    Message = "Not found",
                    Status = true
                };
            }

            return new BaseResponce<IEnumerable<EateryDto>>
            {
                Message = "Successful",
                Status = false,
                Data = Eatery.Select(
                    e =>
                    new EateryDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        CertificationNumber = e.CertificationNumber,
                        Certificate = e.Certificate,
                        Logo = e.Logo,
                        IsVerify = e.IsVerify,
                        Note = e.Note,
                    }
                )
            };
        }
    }
}
