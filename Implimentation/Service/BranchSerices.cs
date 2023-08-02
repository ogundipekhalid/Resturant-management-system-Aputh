using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using RDMS.Implimentation.Repositry;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Actors;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.Models.Identity;
using RDMS.ViewModel;

namespace RDMS.Implimentation.Service
{
    public class BranchSerices : IBranchService
    {
        private readonly IBranchRepositry _branchRepositry;
        private readonly IFoodRepositry _foodRepositry;
        private readonly IEateryAdminRepositry _eateryAdminRepositry;
        private readonly IEateryRepositry _eateryRepositry;
        private readonly IAddressRepositry _addressRepositry;
        private readonly IOrderRepositry _orderRepositry;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleRepositry _roleRepositry;
        private readonly IBranchManagerRepositry _branchManagerRepositry;
        private readonly IUserRepositry _userRepositry;
        private readonly IStaffRepositry _staffRepositry;
        private readonly IMailServices _mailService;
        private readonly ICategoryRepositry _catigoriesRepositry;

        public BranchSerices(IBranchRepositry branchRepositry, IFoodRepositry foodRepositry, IEateryAdminRepositry eateryAdminRepositry, IEateryRepositry eateryRepositry, IAddressRepositry addressRepositry, IOrderRepositry orderRepositry, IHttpContextAccessor httpContextAccessor, IRoleRepositry roleRepositry, IBranchManagerRepositry branchManagerRepositry, IUserRepositry userRepositry, IStaffRepositry staffRepositry, IMailServices mailService, ICategoryRepositry catigoriesRepositry)
        {
            _branchRepositry = branchRepositry;
            _foodRepositry = foodRepositry;
            _eateryAdminRepositry = eateryAdminRepositry;
            _eateryRepositry = eateryRepositry;
            _addressRepositry = addressRepositry;
            _orderRepositry = orderRepositry;
            _httpContextAccessor = httpContextAccessor;
            _roleRepositry = roleRepositry;
            _branchManagerRepositry = branchManagerRepositry;
            _userRepositry = userRepositry;
            _staffRepositry = staffRepositry;
            _mailService = mailService;
            _catigoriesRepositry = catigoriesRepositry;
        }

        public async Task<IEnumerable<BranchFood>> BranchFoods()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponce<BranchDto>> CreateBranch(CreateBranchModel model)
        {
               var checkExsit = await _userRepositry.Get(a => a.Email == model.User.Email);
            if (checkExsit != null)
            {
                return new BaseResponce<BranchDto>
                {
                    Message = "User already exist",
                    Status = false,
                };
            }
            // var branchExi = await _branchRepositry.Get(x => x.Name == model.Manager.BranchName);
            // if (model.Manager.BranchName.Contains(branchExi.Name))
            // {
            //     return new BaseResponce<BranchDto>
            //     {
            //         Message = "Not Match  ",
            //         Status = false,
            //     };
            // }
            var user = _httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier
            );
            int userId = int.Parse(user);
            var eateryAdmin = await _eateryAdminRepositry.Get(a => a.UserId == userId); //geting login user detil
            var ted = $"{eateryAdmin.Eatery.Name}  {model.BranchAddress.City} Branch";//

            var EateryExist = await _eateryRepositry.Get(x => x.Name == ted);
            
            var branchExist = await _branchRepositry.Get(x => x.Name == ted);
            if (branchExist != null)
            // if (branchExist == null)
            {
                return new BaseResponce<BranchDto>
                {
                    Message = "Branch already exist ",
                    // Message = "Branch found ",
                    Status = false
                };
            }

            var branchAddress = new Address
            {
                State = model.BranchAddress.State,
                Street = model.BranchAddress.Street,
                City = model.BranchAddress.City,
                ZipCode = model.BranchAddress.ZipCode
            };

            var branch = new Branch
            {
                //UserId = userId,
                AddressId = branchAddress.Id,
                Address = branchAddress,
                Name = $"{eateryAdmin.Eatery.Name}  {model.BranchAddress.City} Branch",
                EateryId = eateryAdmin.EateryId,
                Eatery = eateryAdmin.Eatery,
                
            };
            var userDetails = new User
            {
                 //Id = userId,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                Email = model.User.Email,
                Password = model.User.Password,
                PhoneNumber = model.User.PhoneNumber,
                AddressId =  branchAddress.Id,
                Address = branchAddress,
                Wallet = 0,
                
              
            };
            var role = await _roleRepositry.Get(g => g.Name == "Branch");
            var userRole = new UserRole
            {
                UserId = userDetails.Id,
                User = userDetails,
                RoleId = role.Id,
                Role = role,
            };
            userDetails.UserRoles.Add(userRole);

            var branchManager = new BranchManager
            {
                UserId = userDetails.Id,
                User = userDetails,
                BranchId = branch.Id,
                Branch = branch,
                RegNumber =
                    $"CAC" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper(),
            };
            
            await _branchManagerRepositry.Add(branchManager);
        
                var mailRequest = new MailRequestDto
            {  
                Subject = "Complete Your Registration",
                ToEmail = userDetails.Email,
                ToName = model.User.FirstName,
                HtmlContent =  $"Dear {userDetails.Email} , this is to inform you that you have register {userDetails.FirstName} {userDetails.LastName} on D Restaurant right now {DateTime.Now} ",
            };
            _mailService.SendEMailAsync(mailRequest);
             
            _branchManagerRepositry.Save();
            return new BaseResponce<BranchDto>
            {
                Message = "Sucessfully",
                Status = true,
                Data = new BranchDto
                {
                    Id = branch.Id,
                    Name = branch.Name,
                    AddressId = branch.AddressId,
                    EateryId = branch.EateryId,
                }
            };
        }

        public async Task<BaseResponce<BranchDto>> DeleteBranch(int id)
        {
            var branch = await _branchRepositry.Get(id);
            if (branch != null)
            {
                return new BaseResponce<BranchDto> { Message = "Not Found", Status = false };
            }
            branch.IsDeleted = true;
            _branchRepositry.Update(branch);
            _branchRepositry.Save();

            return new BaseResponce<BranchDto> { Message = "Successful", Status = true };
        }

        public async Task<BaseResponce<IEnumerable<BranchDto>>> GetAllBranch()
        {
            var branch = await _branchRepositry.GetAll();
            var allBranch = branch.Select(
                branchs =>
                    new BranchDto
                    {
                        Id = branchs.Id,
                        EateryId = branchs.EateryId,
                        Name = branchs.Name
                    }
            );
            return new BaseResponce<IEnumerable<BranchDto>>
            {
                Message = "done",
                Status = true,
                Data = allBranch,
            };
        }

        public async Task<BaseResponce<IEnumerable<BranchDto>>> GetBranchesByCompanyId()
        {
            var branches = await _branchRepositry.GetSelected(a => a.EateryId == a.Id);
            if (branches != null)
            {
                return new BaseResponce<IEnumerable<BranchDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = branches
                        .Select(
                            branch =>
                                new BranchDto
                                {
                                    Id = branch.Id,
                                    Name = branch.Name,
                                    AddressId = branch.AddressId,
                                    EateryId = branch.EateryId,
                                    AddressName = branch.Address,
                                    IsVerify = branch.Eatery.IsVerify,
                                    State = branch.Address.State,
                                }
                        ).ToList(),
                };
            }
            return new BaseResponce<IEnumerable<BranchDto>>
            {
                Message = "not found",
                Status = false,
            };
        }

        public async Task<BaseResponce<IEnumerable<BranchDto>>> GetBranchByEateryId(int EateryId)
        {
            
            var branch = await _branchRepositry.GetSelected(a => a.Id ==  EateryId  && !a.IsDeleted);//(b => b.EateryId == EateryId && !b.IsDeleted);
            if (branch == null)
            {
                return new BaseResponce<IEnumerable<BranchDto>>
                {
                    Status = false,
                    Message = "No branch found",
                };
            }
            return new BaseResponce<IEnumerable<BranchDto>>
            {
                Status = true,
                Message = "Successful",
                Data =  branch
                        .Select(
                            branch =>
                                new BranchDto
                                {
                                    Id = branch.Id,
                                    Name = branch.Name,
                                    AddressId = branch.AddressId,
                                    EateryId = branch.EateryId,
                                    IsVerify = branch.Eatery.IsVerify,
                                    State = branch.Address.State,
                                      Eatery = new EateryDto
                                    {
                                        //Eatery
                                        Id = branch.Eatery.Id,
                                        IsVerify = branch.Eatery.IsVerify,
                                        Name = branch.Eatery.Name,
                                        CertificationNumber = branch.Eatery.CertificationNumber,
                                        Certificate = branch.Eatery.Certificate,
                                        Logo = branch.Eatery.Logo,
                                        Note = branch.Eatery.Note,  
                                    }
                                }
                        ).ToList(),
                // branch.Select(b => CreateBranchDto(b))
            };
        }

        public async Task<BaseResponce<BranchDto>> UpdateBranch(
            int id,
            UpDateBranchRequestModel model
        )
        {
            var branch = await _branchRepositry.GetDetails( id);
            //var neww == $"{branch.Eatery.Name}  {model..City} Branch";
            var ted = $"{branch.Eatery.Name}  {model.City}  {model.State} {model.Street} Branch";
            if (branch != null)
            {
                branch.Name = ted;
                branch.Address.Street = model.Street;
                branch.Address.City = model.City;
                    return new BaseResponce<BranchDto>
                {
                    Message = "Updated successfully",
                    Status = true,
                   // Data = CreateBranchDto(branch)
                    Data = new BranchDto
                    {
                        Id = branch.Id,
                        EateryId = branch.EateryId,
                        //Name = branch.Name,
                        Name = ted,
                        AddressName = branch.Address,
                    }
                };
            }

            return new BaseResponce<BranchDto> { Message = "Unable to Update", Status = false, };
        }

         private BranchDto CreateBranchDto(Branch branch)
        {
            return new BranchDto
            {
                Id = branch.Id,
                Eatery = new EateryDto
                {
                    Id = branch.EateryId,
                    //CreatedBy = branch.Company.CreatedBy,
                    IsVerify = branch.Eatery.IsVerify,
                    CertificationNumber = branch.Eatery.CertificationNumber,
                    Logo = branch.Eatery.Logo,
                    Name = branch.Eatery.Name,
                },
                EateryId = branch.EateryId,
               // CreatedBy = branch.CreatedBy,
               AddressName = branch.Address,
                Name = branch.Name,
                
               // IsDeleted = branch.IsDeleted,
                //UpdatedBy = branch.UpdatedBy,
            };
        }

        public async Task<BaseResponce<IEnumerable<FoodDto>>> BranchFoodsByAddressId(string name)
        {
            var branch = await _branchRepositry.GetName(name);
            if (branch == null)
            {
                return new BaseResponce<IEnumerable<FoodDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            var food = await _branchRepositry.GetFoodByBranchId(branch.Id);
            return new BaseResponce<IEnumerable<FoodDto>>
            {
                Message = "Successful",
                Status = true,
                Data = food.Select(
                    f =>
                        new FoodDto
                        {
                            Id = f.Id,
                            FoodName = f.Food.Name,
                            Image = f.Food.Image,
                            Price = f.Food.Price,
                        }
                )
            };
        }

        public async Task<IEnumerable<Food>> SearchFoods(string searchTerm)
        {
            // Build the search expression based on the searchTerm
            Expression<Func<Food, bool>> searchExpression = food => food.Name.Contains(searchTerm);

            // Call the repository method to search for foods
            IEnumerable<Food> searchResults = await _foodRepositry.SearchFoods(searchExpression);

            return searchResults;
        }

     
        public async Task<BaseResponce<BranchDto>> GetBranchesByEateryId(int EateryId)
        {
            var branch = await _branchRepositry.Get(d => d.EateryId == d.Id);
            if (branch == null)
                return new BaseResponce<BranchDto>
                {
                    Message = "Branch not found",
                    Status = false,
                };
            return new BaseResponce<BranchDto>
            {
                Message = "Found",
                Status = true,
                Data = new BranchDto
                {
                    Id = branch.Id,
                    Name = branch.Name,
                    AddressId = branch.AddressId,
                    EateryId = branch.EateryId,
                    //AddressName  = branch.Address.Branch.Address,
                    AddressName  = branch.Address,
                }
            };
        }
        

        public async Task<BaseResponce<BranchDto>> Get(int id)
        {
            var getbranch = await _branchRepositry.Get(g => g.Id == id);
            if (getbranch != null)
                return new BaseResponce<BranchDto>
                {
                    Message = "User not found",
                    Status = false,
                };
                 return new BaseResponce<BranchDto>
            {
                Message = "success",
                Status = true,
                Data = new BranchDto 
                { 
                Id = getbranch.Id, 
                EateryId = getbranch.EateryId,
                Name = getbranch.Name,
                AddressName = getbranch.Address,
                }
            };
        }


        public async Task<BaseResponce<BranchDto>> GetBranchById( int id,int useId)
        {
            var getbranch = await _branchRepositry.Get(g => g.EateryId == id);
            if (getbranch != null)
            {
                 return new BaseResponce<BranchDto>
                    {
                        Message = "success",
                        Status = true,
                        Data = new BranchDto 
                        { 
                            IsVerify = getbranch.Eatery.IsVerify,
                            Id = getbranch.Id, 
                            EateryId = getbranch.EateryId,
                            Name = getbranch.Name,
                            State = getbranch.Address.State,
                                    Eatery = new EateryDto
                                    {
                                        //Eatery
                                        Id = getbranch.Eatery.Id,
                                        IsVerify = getbranch.Eatery.IsVerify,
                                        Name = getbranch.Eatery.Name,
                                        CertificationNumber = getbranch.Eatery.CertificationNumber,
                                        Certificate = getbranch.Eatery.Certificate,
                                        Logo = getbranch.Eatery.Logo,
                                        Note = getbranch.Eatery.Note,
                                    }
                        }
                    };
            }
            return new BaseResponce<BranchDto>
                {
                    Message = "User not found",
                    Status = false,
                };
        }
        public async Task<BaseResponce<IEnumerable<BranchDto>>> GetListBranchById( int EateryId ,int userId)
        {
            var getbranch = await _branchRepositry.GetSelected(g => g.EateryId == g.Id);
            if (getbranch != null)
            {
                 return new BaseResponce<IEnumerable<BranchDto>>
                    {
                        Message = "success",
                        Status = true,
                        Data =  getbranch.Select(
                        s => new  BranchDto
                         {
                            IsVerify = s.Eatery.IsVerify,
                            Id = s.Id, 
                            EateryId = s.EateryId,
                            Name = s.Name,
                            State = s.Address.State,
                             Eatery = new EateryDto
                                    {
                                        //Eatery
                                        Id = s.Eatery.Id,
                                        IsVerify = s.Eatery.IsVerify,
                                        Name = s.Eatery.Name,
                                        CertificationNumber = s.Eatery.CertificationNumber,
                                        Certificate = s.Eatery.Certificate,
                                        Logo = s.Eatery.Logo,
                                        Note = s.Eatery.Note,
                                    }
                         }
                       
                        )
                       
                    };
            }
            return new BaseResponce<IEnumerable<BranchDto>>
                {
                    Message = "User not found",
                    Status = false,
                };
        }

        public async Task<BaseResponce<BranchDeshResponceModel>> BranchDeshResponceModels()
        {
           var  ViewAllFood = await _foodRepositry.GetSelected(j => j.Id == j.Id);

           var AllStaff = await _staffRepositry.GetSelected(j => j.Id == j.Id);

           var ListofCatigories = await _catigoriesRepositry.GetSelected(j => j.Id == j.Id);
          
           return new BaseResponce<BranchDeshResponceModel>
           {
                 Message = "",
                 Status = true,
                 Data = new BranchDeshResponceModel
                 {
                    ViewAllFood = ViewAllFood.Count(),
                    AllStaff = AllStaff.Count(),
                    ListofCatigories = ListofCatigories.Count(),
                 }
                 
           };
        }


    }
}
