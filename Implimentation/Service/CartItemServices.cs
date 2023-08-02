using System.Security.Claims;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.Models.Enums;
using RDMS.Payment;

namespace RDMS.Implimentation.Service
{
    public class CartItemServices : ICartItemServices
    {
        private readonly ICartItemRepositry _cartItemRepository;
        private readonly IUserRepositry _userRepositry;
        private readonly IFoodRepositry _foodRepositry;
        private readonly ICustomerRepositry _customerRepositry;
        private readonly IOrderRepositry _orderRepositry;
        private readonly IPaymentRepositry _paymentRepositry;
        private readonly IPaystackServices _paymentServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartItemServices(
            ICartItemRepositry cartItemRepository,
            IUserRepositry userRepositry,
            IFoodRepositry foodRepositry,
            IHttpContextAccessor httpContextAccessor,
            ICustomerRepositry customerRepositry,
            IOrderRepositry orderRepositry,
            IPaystackServices paymentServices
        )
        {
            _cartItemRepository = cartItemRepository;
            _userRepositry = userRepositry;
            _foodRepositry = foodRepositry;
            _httpContextAccessor = httpContextAccessor;
            _customerRepositry = customerRepositry;
            _orderRepositry = orderRepositry;
            _paymentServices = paymentServices;
        }

        public async Task<BaseResponce<CartItemDto>> AddCartItem(AddCartItemViewModel model)
        {
            var userId = int.Parse(
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );
            var food = await _foodRepositry.Get(model.FoodId);

            if (food == null || food.Quantity < model.Quantity)
            {
                return new BaseResponce<CartItemDto>
                {
                    Status = false,
                    Message = "Food not available"
                };
            }

            var branch = food.BranchFoods.FirstOrDefault(f => f.FoodId == model.FoodId)?.Branch;

            var cartItem = _cartItemRepository.Get(c => c.FoodId == model.FoodId && !c.IsPaid);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    // Id = cartId,
                    FoodId = model.FoodId,
                    BranchId = branch.Id,
                    EateryId = branch.EateryId,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    FoodName = model.FoodName,
                    UserId = userId,
                    IsPaid = false,
                    Total = (decimal)model.Quantity * model.Price,
                    CreatedBy = userId.ToString(),
                };
                await _cartItemRepository.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += model.Quantity;
                cartItem.Total = (decimal)cartItem.Quantity * cartItem.Price;
                cartItem.CreatedBy = userId.ToString();
            }

            _cartItemRepository.Save();
            return new BaseResponce<CartItemDto>
            {
                Status = true,
                Message = "Item created",
                Data = CreateCartItemDto(cartItem)
            };
        }

        //      public async Task<BaseResponce<CartItemDto>> AddCartItem(AddCartItemViewModel model)
        // {
        //      try
        //     {
        //     var userId = int.Parse(
        //         _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        //     );
        //     var food = await _foodRepositry.Get(model.FoodId);

        //     if (food == null || food.Quantity < model.Quantity)
        //     {
        //         return new BaseResponce<CartItemDto>
        //         {
        //             Status = false,
        //             Message = "Food not available"
        //         };
        //     }

        //     var cartItem = _cartItemRepository.Get(
        //         c => c.FoodId == model.FoodId && !c.IsPaid || c.CreatedBy == userId.ToString()
        //     );
        //     //  var carts = _cartItemRepository
        //     //     .GetSelected(c => c.UserId == userId && !c.IsPaid)
        //     //     .ToList();

        //       var allOrder = _cartItemRepository.GetAll();
        //     var cartId = allOrder.Count() + 1;

        //     var branch = food.BranchFoods.FirstOrDefault(f => f.FoodId == model.FoodId).Branch;

        //     // if (cartItem == null)
        //     // {
        //         cartItem = new CartItem
        //         {
        //             Id = cartId,
        //             FoodId = model.FoodId,
        //             BranchId = branch.Id,
        //             EateryId = branch.EateryId,
        //             Quantity = model.Quantity,
        //             Price = model.Price,
        //             FoodName = model.FoodName,
        //             UserId = userId,
        //             IsPaid = false,
        //             Total = (decimal)model.Quantity * model.Price,
        //             CreatedBy = userId.ToString(),
        //         };
        //         await _cartItemRepository.Add(cartItem);
        //     // }
        //     // else
        //     // {
        //     //     cartItem.Quantity += model.Quantity;
        //     //     cartItem.Total = (decimal)cartItem.Quantity * cartItem.Price;
        //     //     //cartItem.CreatedBy = userId.ToString();
        //     // }
        //     _cartItemRepository.Save();

        //     return new BaseResponce<CartItemDto>
        //     {
        //         Status = true,
        //         Message = "Item created",
        //         Data = CreateCartItemDto(cartItem)
        //     };
        // }
        //      catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }


        public BaseResponce<CartItemDto> DeleteCartItem(int userId, int id)
        {
            var cartItem = _cartItemRepository.Get(id);
            if (cartItem == null)
            {
                return new BaseResponce<CartItemDto>
                {
                    Status = false,
                    Message = "Cart Item not found"
                };
            }

            _cartItemRepository.Delete(cartItem);
            _cartItemRepository.Save();

            return new BaseResponce<CartItemDto> { Status = true, Message = "Successful" };
        }

        public BaseResponce<IEnumerable<CartItemDto>> GetAllCartItem()
        {
            var categories = _cartItemRepository.GetAll();
            if (categories == null)
            {
                return new BaseResponce<IEnumerable<CartItemDto>>
                {
                    Status = false,
                    Message = "Category not found"
                };
            }

            return new BaseResponce<IEnumerable<CartItemDto>>
            {
                Status = true,
                Message = "Successful",
                Data = categories.Select(s => CreateCartItemDto(s))
            };
        }

        public BaseResponce<CartItemDto> GetCartItem(int id)
        {
            var category = _cartItemRepository.Get(id);
            if (category == null)
            {
                return new BaseResponce<CartItemDto>
                {
                    Status = false,
                    Message = "Category not found"
                };
            }

            return new BaseResponce<CartItemDto>
            {
                Status = true,
                Message = "Successful",
                Data = CreateCartItemDto(category)
            };
        }

        public BaseResponce<IEnumerable<CartItemDto>> GetCartItemByUserId(int userId)
        {
            var carts = _cartItemRepository.GetSelected(c => c.UserId == userId);
            if (carts == null)
            {
                return new BaseResponce<IEnumerable<CartItemDto>>
                {
                    Status = false,
                    Message = "Cart Item not found"
                };
            }

            return new BaseResponce<IEnumerable<CartItemDto>>
            {
                Status = true,
                Message = "Successful",
                Data = carts.Select(s => CreateCartItemDto(s))
            };
        }

        public BaseResponce<IEnumerable<CartItemDto>> GetCartItemByBranchId(int branchId)
        {
            var userId = int.Parse(
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );
            //get branch
            //
            var carts = _cartItemRepository.GetSelected(c => c.BranchId == branchId);
            if (carts == null)
            {
                return new BaseResponce<IEnumerable<CartItemDto>>
                {
                    Status = false,
                    Message = "Cart Item not found"
                };
            }

            return new BaseResponce<IEnumerable<CartItemDto>>
            {
                Status = true,
                Message = "Successful",
                Data = carts.Select(s => CreateCartItemDto(s))
            };
        }

        public BaseResponce<CartItemDto> RemoveCartItem(int id)
        {
            var cartItem = _cartItemRepository.Get(id);
            if (cartItem == null)
            {
                return new BaseResponce<CartItemDto>
                {
                    Status = false,
                    Message = "Cart Item not found"
                };
            }

            _cartItemRepository.Delete(cartItem);
            _cartItemRepository.Save();

            return new BaseResponce<CartItemDto> { Status = true, Message = "Successful" };
        }

        public BaseResponce<CartItemDto> Update(int userId, UpdateCartItemViewModel model)
        {
            var category = _cartItemRepository.Get(s => s.Id == model.Id);
            if (category == null)
            {
                return new BaseResponce<CartItemDto>
                {
                    Status = false,
                    Message = "Category does not exist"
                };
            }

            category.IsDeleted = model.IsDeleted;
            // category.UpdatedBy = userId;

            _cartItemRepository.Update(category);
            _cartItemRepository.Save();

            return new BaseResponce<CartItemDto>
            {
                Status = true,
                Message = "Category updated",
                Data = CreateCartItemDto(category)
            };
        }

        private CartItemDto CreateCartItemDto(CartItem cart)
        {
            return new CartItemDto
            {
                Id = cart.Id,
                BranchId = cart.BranchId,
                EateryId = cart.EateryId,
                FoodId = cart.FoodId,
                Quantity = cart.Quantity,
                Price = cart.Price,
                FoodName = cart.FoodName,
                userId = cart.UserId,
                Total = cart.Total,
                IsPaid = cart.IsPaid,
                // CreatedBy = cart.CreatedBy,
                IsDeleted = cart.IsDeleted,
            };
        }

        public async Task<BaseResponce<IEnumerable<CartItemDto>>> GetUnpaidCartItemByUserId()
        {
            int userId = int.Parse(
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );
            var customer = await _customerRepositry.Get(x => x.UserId == userId);
            var carts = _cartItemRepository
                .GetSelected(c => c.UserId == userId && !c.IsPaid)
                .ToList();
            foreach (var item in carts)
            {
                item.IsPaid = true;
            }
            if (carts == null)
            {
                return new BaseResponce<IEnumerable<CartItemDto>>
                {
                    Status = false,
                    Message = "Cart Item not found"
                };
            }
            var totalAmount = carts.Sum(x => x.Price);
            var branchId = carts[0].BranchId;
            var orders = new Order
            {
                BranchId = branchId,
                CustomerId = customer.Id,
                OrderStatus = OrderStatus.Processing,
                ReferenceNumber = $"ORD" + Guid.NewGuid().ToString(),
                TotalAmount = totalAmount,
            };
            await _orderRepositry.Add(orders);
            _orderRepositry.Saves();
            return new BaseResponce<IEnumerable<CartItemDto>>
            {
                Status = true,
                Message = "Successful",
                Data = carts.Select(s => CreateCartItemDto(s))
            };
        }

        public async Task<InitializePaymentResponseModel> GetUnpaidCartItemByUserIds()
        {
            int userId = int.Parse(
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );
            var customer = await _customerRepositry.Get(x => x.UserId == userId);
            var email = await _customerRepositry.Get(x => x.User.Email == x.User.Email); //

            var carts = _cartItemRepository
                .GetSelected(c => c.UserId == userId && !c.IsPaid)
                .ToList();
            foreach (var item in carts)
            {
                item.IsPaid = true;
            }
            if (carts == null)
            {
                return new InitializePaymentResponseModel
                {
                    Status = false,
                    Message = "Cart Item not found"
                };
            }
            var totalAmount = carts.Sum(x => x.Total);
            var em = email.User.Email; //
            var branchId = carts[0].BranchId;
            var orders = new Order
            {
                BranchId = branchId,
                CustomerId = customer.Id,
                OrderStatus = OrderStatus.Processing,
                ReferenceNumber = $"ORD" + Guid.NewGuid().ToString(),
                TotalAmount = totalAmount,
            };
            await _orderRepositry.Add(orders);
            // _orderRepositry.Save();
            var intial = new InitializePaymentRequestModel
            {
                Amount = totalAmount,
                RefrenceNo = orders.ReferenceNumber,
                Email = em,
                CallbackUrl = "https://localhost:7119/Order/Reciept",
                //  CallbackUrl = "https://localhost:7119/Order/OrderDetails",
            };
            var thispay = await _paymentServices.InitializePayment(intial);
            _orderRepositry.Save();

            return thispay;
        }
    }
}
