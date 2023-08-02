using System.Linq.Expressions;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.Models.Enums;

namespace RDMS.Implimentation.Service
{
    public class FoodServices : IFoodServices
    {
        private readonly IFoodRepositry _foodRepositry;
        private readonly IOrderRepositry _orderRepositry;
        private readonly IFileUploders _fileUploders;
        private readonly ICategoryRepositry _catigoriesrepo;
        private readonly IBranchRepositry _branchRepositry;
        private readonly IUserRepositry _userRepositry;
        private readonly IBranchManagerRepositry _branchManagerRepositry;
        private readonly ICustomerRepositry _customerRepositry;

        public FoodServices(
            IFoodRepositry foodRepositry,
            IOrderRepositry orderRepositry,
            IFileUploders fileUploders,
            ICategoryRepositry catigoriesrepo,
            IBranchRepositry branchRepositry,
            IUserRepositry userRepositry,
            IBranchManagerRepositry branchManagerRepositry,
            ICustomerRepositry customerRepositry
        )
        {
            _foodRepositry = foodRepositry;
            _orderRepositry = orderRepositry;
            _fileUploders = fileUploders;
            _catigoriesrepo = catigoriesrepo;
            _branchRepositry = branchRepositry;
            _userRepositry = userRepositry;
            _branchManagerRepositry = branchManagerRepositry;
            _customerRepositry = customerRepositry;
        }

        public async Task<BaseResponce<FoodDto>> AddFood(
            CreateFoodRequestModel model,
            int managerId
        )
        {
            var foodExsit = await _foodRepositry.GetDetails(f => f.Name == model.FoodName);
            var mager = await _branchManagerRepositry.Get(managerId);
            if (foodExsit != null)
            {
                return new BaseResponce<FoodDto> { Message = "Food Already Exist", Status = false };
            }

            var catigories = await _catigoriesrepo.Get(b => b.Name == b.Name);

            var Image = _fileUploders.UploadFile(model.Image);
            Food food = new Food
            {
                Id = model.Id,
                Name = model.FoodName,
                Image = Image,
                Price = Math.Round(model.Price, 5),
                // Price = Math.Ceiling(model.Price),
                Category = catigories,
                CategoryId = catigories.Id,
                Quantity = model.Quantity,
                OrderStatus = model.OrderStatus,
            };
            var branchFoo = new List<BranchFood>
            {
                new BranchFood
                {
                    Id = model.Id,
                    BranchId = mager.BranchId,
                    FoodId = food.Id,
                }
            };
            food.BranchFoods = branchFoo;
            await _foodRepositry.Add(food);
            _foodRepositry.Save();
            return new BaseResponce<FoodDto> { Message = "Food Successfully Added", Status = true };
        }

        public async Task<BaseResponce<IEnumerable<FoodDto>>> AllAvailaleFood()
        {
            var food = await _foodRepositry.GetAll();
            if (food != null)
            {
                return new BaseResponce<IEnumerable<FoodDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = food.Select(
                            f =>
                                new FoodDto
                                {
                                    Id = f.Id,
                                    FoodName = f.Name,
                                    Image = f.Image,
                                    Price = f.Price,
                                    OrderStatus = f.OrderStatus,
                                    BranchFoods = f.BranchFoods,
                                }
                        )
                        .ToList()
                };
            }
            return new BaseResponce<IEnumerable<FoodDto>> { Message = "Not found", Status = true, };
        }

        public async Task<BaseResponce<IEnumerable<FoodDto>>> AllAvailableFoodInBranch()
        {
            var food = await _foodRepositry.GetSelected(f => f.Name == f.Name);
            if (food != null)
            {
                return new BaseResponce<IEnumerable<FoodDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = food.Select(
                            f =>
                                new FoodDto
                                {
                                    Id = f.Id,
                                    FoodName = f.Name,
                                    Image = f.Image,
                                    Price = f.Price,
                                    OrderStatus = f.OrderStatus,
                                    BranchFoods = f.BranchFoods,
                                }
                        )
                        .ToList()
                };
            }
            return new BaseResponce<IEnumerable<FoodDto>> { Message = "Not found", Status = true, };
        }

        public async Task<BaseResponce<IList<FoodDto>>> BranchFoodsByAddressIds(int branchId)
        {
            var food = await _foodRepositry.GetSelectedlast(k => k.Branch.AddressId == branchId);
            if (food == null)
            {
                return new BaseResponce<IList<FoodDto>> { Message = "Not found", Status = false, };
            }

            var foo = new List<FoodDto>();
            foreach (var item in food)
            {
                var fold = new FoodDto
                {
                    Id = item.Food.Id,
                    BranchId = item.BranchId,
                    FoodName = item.Food.Name,
                    Image = item.Food.Image,
                    Price = item.Food.Price,
                    Address = item.Branch.Address,
                    // Name = item.Branch.Name
                };
                foo.Add(fold);
            }
            var cap = new BaseResponce<IList<FoodDto>>
            {
                Message = "Successful",
                Status = true,
                Data = foo,
            };
            return cap;
        }

        public async Task<BaseResponce<IEnumerable<FoodDto>>> AllFoodByaBranch(int branchId)
        {
            var branchmanger = await _branchManagerRepositry.Get(b => b.Branch.Id == b.BranchId);
            var food = await _foodRepositry.GetSelectedlast(
                x => x.Branch.Id == branchmanger.Branch.BranchFoods.Count()
            );
            if (food == null)
            {
                return new BaseResponce<IEnumerable<FoodDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            var foo = new List<FoodDto>();
            foreach (var item in food)
            {
                var fold = new FoodDto
                {
                    Id = item.Id,
                    FoodName = item.Food.Name,
                    Image = item.Food.Image,
                    Price = item.Food.Price,
                    OrderStatus = item.Food.OrderStatus,
                    Quantity = item.Food.Quantity,
                };
                foo.Add(fold);
            }
            var cap = new BaseResponce<IEnumerable<FoodDto>>
            {
                Message = "Successful",
                Status = true,
                Data = foo,
            };
            return cap;
        }

        public async Task<BaseResponce<FoodDto>> DeleteFood(int id)
        {
            var food = await _foodRepositry.DeleteAsync(id);
            return new BaseResponce<FoodDto>
            {
                Message = "Food Deleted Successfully",
                Status = true
            };
        }

        public async Task<BaseResponce<FoodDto>> DeleteFoodAsync(int id)
        {
            var food = await _foodRepositry.Get(id);
            if (food != null)
            {
                return new BaseResponce<FoodDto> { Message = "Not Found", Status = true };
                food.IsDeleted = true;
            }
            _foodRepositry.Update(food);
            _foodRepositry.Save();
            return new BaseResponce<FoodDto>
            {
                Message = "Your Account has being Deleted",
                Status = true
            };
        }

        public async Task<BaseResponce<FoodDto>> GetFood(int id)
        {
            var food = await _foodRepositry.Get(id);

            if (food != null)
            {
                return new BaseResponce<FoodDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new FoodDto
                    {
                        Id = food.Id,
                        CategoryId = food.CategoryId,
                        FoodName = food.Name,
                        Price = food.Price,
                        Image = food.Image,
                        AvailableTime = food.DateCreated,
                        OrderStatus = food.OrderStatus,
                    }
                };
            }
            return new BaseResponce<FoodDto> { Message = "Not found", Status = false, };
        }

        public async Task<BaseResponce<IEnumerable<FoodDto>>> GetList(int id, int userId)
        {
            var user = await _userRepositry.Get(userId);
            if (user == null)
            {
                return new BaseResponce<IEnumerable<FoodDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }

            var customer = await _customerRepositry.Get(x => x.UserId == user.Id);
            if (customer == null)
            {
                return new BaseResponce<IEnumerable<FoodDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }
            var foods = await _foodRepositry.GetSelected(x => x.BranchFoods.Count() == customer.Id);
            if (foods != null)
            {
                return new BaseResponce<IEnumerable<FoodDto>>
                {
                    Data = foods
                        .Select(
                            j =>
                                new FoodDto
                                {
                                    Id = j.Id,
                                    Image = j.Image,
                                    FoodName = j.Name,
                                    Price = j.Price,
                                    AvailableTime = j.DateCreated,
                                    Category = j.Category,
                                    Quantity = j.Quantity,
                                    OrderStatus = j.OrderStatus
                                    // cartItem = j.cartItem,
                                    //BranchFoods = j.BranchFoods.Select(f => f.Food == )
                                }
                        )
                        .ToList()
                };
            }

            return new BaseResponce<IEnumerable<FoodDto>>
            {
                Message = "Order is not Found",
                Status = false
            };
        }

        public async Task<BaseResponce<FoodDto>> Get(int id, int customerId)
        {
            var user = await _userRepositry.Get(customerId);

            var food = await _foodRepositry.Get(id);

            if (food != null)
            {
                return new BaseResponce<FoodDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new FoodDto
                    {
                        Id = food.Id,
                        CategoryId = food.CategoryId,
                        FoodName = food.Name,
                        Price = food.Price,
                        Image = food.Image,
                        AvailableTime = food.DateCreated,
                        OrderStatus = food.OrderStatus
                    }
                };
            }
            return new BaseResponce<FoodDto> { Message = "Not found", Status = false, };
        }

        public async Task<BaseResponce<FoodDto>> UpdateFood(UpdateFoodRequestModel model, int id)
        {
            var food = await _foodRepositry.Get(id);
            if (food == null)
            {
                return new BaseResponce<FoodDto> { Message = "Food not found", Status = false };
            }
            var image = "";
            if (model.Image != null)
            {
                image = _fileUploders.UploadFile(model.Image);
            }
            food.Name = model.FoodName;
            food.Price = model.Price;
            food.Image = image;
            // food.AvailableTime = model.AvailableTime;
            food.OrderStatus = model.OrderStatus;
            await _foodRepositry.Update(food);
            _foodRepositry.Save();
            return new BaseResponce<FoodDto>
            {
                Message = "Food Updated Successfully",
                Status = true
            };
        }

        public async Task UpdateFoodStatus()
        {
            var foods = await _foodRepositry.GetAll();
            foreach (var food in foods)
            {
                if (
                    food.DateCreated <= DateTime.Now && food.OrderStatus != OrderStatus.NotAvailable
                )
                {
                    food.OrderStatus = OrderStatus.Available;
                    var response = await _foodRepositry.Update(food);
                    _foodRepositry.Save();
                }
            }
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
    }
}
