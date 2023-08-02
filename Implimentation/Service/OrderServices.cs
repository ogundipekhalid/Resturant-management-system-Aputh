using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.Models.Enums;
using RDMS.Models.Identity;
using RDMS.ViewModel;

namespace RDMS.Implimentation.Service
{
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepositry _orderRepositry;
        private readonly IUserRepositry _userRepositry;
        private readonly ICartItemRepositry _cartItemRepositry;
        private readonly ICustomerRepositry _customerRepositry;
        private readonly IAddressRepositry _addressRepositry;
        private readonly IFoodRepositry _foodRepositry;
        private readonly IStaffRepositry _staffRepositry;
        private readonly IBranchRepositry _branchRepositry;
        private readonly IPaymentRepositry _paymentRepositry;

        public OrderServices(IOrderRepositry orderRepositry, IUserRepositry userRepositry, ICartItemRepositry cartItemRepositry, ICustomerRepositry customerRepositry, IAddressRepositry addressRepositry, IFoodRepositry foodRepositry, IStaffRepositry staffRepositry, IBranchRepositry branchRepositry, IPaymentRepositry paymentRepositry)
        {
            _orderRepositry = orderRepositry;
            _userRepositry = userRepositry;
            _cartItemRepositry = cartItemRepositry;
            _customerRepositry = customerRepositry;
            _addressRepositry = addressRepositry;
            _foodRepositry = foodRepositry;
            _staffRepositry = staffRepositry;
            _branchRepositry = branchRepositry;
            _paymentRepositry = paymentRepositry;
        }

        public async Task<BaseResponce<OrderDto>> CalculatePriceAsync(int id, double price)
        {
            var customer = await _customerRepositry.Get(id);
            if (customer.User.Wallet < price)
            {
                return new BaseResponce<OrderDto>
                {
                    Message = "Insuficient Balance",
                    Status = false
                };
            }
            customer.User.Wallet -= price;
            await _customerRepositry.Update(customer);
            _customerRepositry.Save();
            return new BaseResponce<OrderDto>
            {
                Message = $"Your Balance is {customer.User.Wallet}",
                Status = true
            };
        }

        public async Task<BaseResponce<OrderDto>> CreateOrder(CreateOrderRequestModel model, int userId)
        {
        // var paymentExist = await _paymentRepositry.GetAsync(x => x.StudentId == studentId && x.CourseId == model.CourseId);
            var customer = await _customerRepositry.GetDetails(userId);
            if (customer == null)
            {
                return new BaseResponce<OrderDto>
                {
                    Message = "Something went wrong",
                    Status = false,
                };
            }
            var cartItem = _cartItemRepositry.GetSelected(model.CartId);

            var allOrder = await _orderRepositry.GetAllOrder();
            var OrderId = allOrder.Count() + 1;
            var Order = new Order
            {
                Id = OrderId,
                BranchId = model.BranchId,
              
                ReferenceNumber = $"ORD" + Guid.NewGuid().ToString(),
                    // $"ORD" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper(),
                CustomerId = customer.Id,
                OrderStatus = OrderStatus.IsPaid,
                DateCreated = DateTime.Now,
                CreatedBy = "Customer",
            };

            var OrderFood = cartItem.Select(
                c =>
                    new OrderFood
                    {
                        DateCreated = DateTime.Now,
                        BranchId = c.BranchId,
                        // BranchId = 1,
                        FoodId = c.FoodId,
                        OrderId = userId,
                        Quantity = c.Quantity,
                        Price = c.Price,
                    }
            );

            foreach (var item in OrderFood)
            {
                var food = await _foodRepositry.GetDetails(
                    g => g.Id == item.FoodId && g.Quantity >= item.Quantity
                );

                if (food == null)
                {
                    return new BaseResponce<OrderDto> { Message = "not Avilble", Status = false };
                }
                else
                {
                    food.Quantity -= item.Quantity;
                }

                Order.OrderFoods.Add(item);
            }

            await _orderRepositry.Add(Order);
            _orderRepositry.Save();
            return new BaseResponce<OrderDto>
            {
                Message = "Successfully Ordered",
                Status = true,
                Data = new OrderDto
                {
                    Id = OrderId,
                    CustomerId = Order.CustomerId,
                    OrderFoods = Order.OrderFoods,
                    // Name = Order.Branch.Name,
                    AvailableTime = Order.DateCreated

                    //CustomerDto = Order.Customer,
                }
                // Data =  CreateProdutOrder(Order)
            };
        }

        public async Task<BaseResponce<IEnumerable<CustomerDto>>> GetAllCustomer()
        {
            var customer = await _customerRepositry.GetAll();

            var getall = customer.Select(
                customers =>
                    new CustomerDto
                    {
                        Id = customers.Id,
                        UserId = customers.User.Id,
                        FirstName = customers.User.FirstName,
                        LastName = customers.User.LastName,
                        Email = customers.User.Email,
                        PhoneNumber = customers.User.PhoneNumber
                    }
            );
            return new BaseResponce<IEnumerable<CustomerDto>>
            {
                Message = "Sueccufullly",
                Status = true,
                Data = getall,
            };
        }

        public async Task<BaseResponce<IEnumerable<OrderFood>>> GetAllOrderFood()
        {
            var order = await _orderRepositry.GetAllOrderFood();
            if (order == null)
            {
                return new BaseResponce<IEnumerable<OrderFood>>
                {
                    Message = "Not found",
                    Status = true
                };
            }
            return new BaseResponce<IEnumerable<OrderFood>>
            {
                Message = "Successful",
                Status = false,
                Data = order.Select(
                    a =>
                        new OrderFood
                        {
                            // Id = a.Id,
                            Food = a.Food,
                            FoodId = a.FoodId,
                            Order = a.Order,
                            OrderId = a.OrderId,
                            Quantity = a.Quantity,
                        }
                )
            };
        }


        public async Task<BaseResponce<OrderDto>> Get(int id,int customerId)
        {    
            var user = await _userRepositry.Get(id);
            var food = await _foodRepositry.Get(i => i.Name == i.Name);

            var customer = await _customerRepositry.Get(x => x.UserId == user.Id);

            // var order = await _orderRepositry.GetSelected(x => x.Customer.Id == customer.Id);
            var order = await _orderRepositry.Get(o => o.Id == customer.Id);


            if (order == null)
            {
                return new BaseResponce<OrderDto> { Status = false, Message = "Order not found" };
            }
            return new BaseResponce<OrderDto>
            {
                Status = true,
                Message = "Successful",
                Data = new OrderDto
                {
                    Id = order.Id,
                    ReferenceNumber = order.ReferenceNumber,
                    CustomerId = order.CustomerId,
                    OrderFoods = order.OrderFoods,
                    AvailableTime = order.DateCreated,
                    // Price = order.OrderFoods.Select(o => (double)o.Price * o.Quantity).Sum(),
                     TotalAmount = order.TotalAmount,
                    Street = order.Customer.User.Address.Street,
                    State = order.Customer.User.Address.State,
                    City = order.Customer.User.Address.City,
                    // OrderFoods = order.OrderFoods.getall.
                }
            };
        }


        public async Task<BaseResponce<OrderDto>> Get(int id)
        {    
            var user = await _userRepositry.Get(id);
            var food = await _foodRepositry.Get(i => i.Name == i.Name);
            var order = await _orderRepositry.Get(o => o.Id == id);

            if (order == null)
            {
                return new BaseResponce<OrderDto> { Status = false, Message = "Order not found" };
            }
            return new BaseResponce<OrderDto>
            {
                Status = true,
                Message = "Successful",
                Data = new OrderDto
                {
                    Id = order.Id,
                    ReferenceNumber = order.ReferenceNumber,
                    CustomerId = order.CustomerId,
                    OrderFoods = order.OrderFoods,
                    AvailableTime = order.DateCreated,
                    // Price = order.OrderFoods.Select(o => (double)o.Price * o.Quantity).Sum(),
                     TotalAmount = order.TotalAmount,
                    Street = order.Customer.User.Address.Street,
                    State = order.Customer.User.Address.State,
                    City = order.Customer.User.Address.City,
                    // OrderFoods = order.OrderFoods.getall.
                }
            };
        }



        public async Task<BaseResponce<IEnumerable<OrderDto>>> GetOrderUserId(int id)
        {
            var user = await _userRepositry.Get(id);
            //
            if (user == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }
            var customer = await _customerRepositry.Get(x => x.UserId == user.Id);
            //
            if (customer == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }

            var orders = await _orderRepositry.GetSelected(x => x.CustomerId == customer.Id);

            if (orders.Count != 0)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Data = orders
                        .Select(
                            x =>
                                new OrderDto
                                {
                                    Id = x.Id,
                                    ReferenceNumber = x.ReferenceNumber,
                                    City = x.Customer.User.Address.City,
                                    Street = x.Customer.User.Address.Street,
                                    State = x.Customer.User.Address.State,
                                    // Price = x.OrderFoods.Select(o => (double)o.Price * o.Quantity).Sum(),
                                    TotalAmount = x.TotalAmount,
                                    OrderFoods = x.OrderFoods,
                                    CustomerDto = new CustomerDto
                                    {
                                        Id = user.Id,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        Email = user.Email,
                                        PhoneNumber = user.PhoneNumber,
                                    }
                                }
                        )
                        .ToList()
                };
            }
            return new BaseResponce<IEnumerable<OrderDto>>
            {
                Message = "Order is not Found",
                Status = false
            };
        }

        public async Task<BaseResponce<OrderDto>> CansuleOrder(int id)
        {
            var order = await _orderRepositry.Get(id);
            if (order is null)
            {
                return new BaseResponce<OrderDto> { Message = "Not Found", Status = false };
            }
            order.IsDeleted = true;
            _orderRepositry.Update(order);
            _orderRepositry.Save();
            return new BaseResponce<OrderDto> { Message = "Successful", Status = true };
        }

        public async Task<BaseResponce<OrderDto>> DeleteAysc(int id)
        {
            var food = await _orderRepositry.DeleteAsync(id);
            return new BaseResponce<OrderDto>
            {
                Message = "Food Deleted Successfully",
                Status = true
            };
        }

        public async Task<List<OrderFood>> OrdersAsync()
        {
            var orders = await _orderRepositry.GetAllOrderFood();
            return orders.OrderBy(x => x.DateCreated).ToList();
        }

        public async Task<BaseResponce<IEnumerable<OrderDto>>> GetAllOrder()
        {
            var order = await _orderRepositry.GetAllOrder();
            if (order == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "Not found",
                    Status = true
                };
            }
            return new BaseResponce<IEnumerable<OrderDto>>
            {
                Message = "Successful",
                Status = false,
                Data = order.Select(d => CreateProdutOrder(d))
            };
        }

            
        public async Task<BaseResponce<IEnumerable<OrderDto>>> GetAllOrdersWithCustomer(int userId)
        {
            var staff = await _staffRepositry.GetDetails(x => x.UserId == userId);
            //
            var order = await _orderRepositry.GetSelected(s => s.Branch.Id == staff.BranchId && s.OrderStatus == OrderStatus.Processing);
            if (order == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "Not found",
                    Status = true
                };
            }
            return new BaseResponce<IEnumerable<OrderDto>>
            {
                Message = "Successful",
                Status = false,
                Data = order.Select(
                    d =>
                        new OrderDto
                        {
                            Id = d.Id,
                            ReferenceNumber = d.ReferenceNumber,
                            // Price = d.OrderFoods.Select(o => (double)o.Price * o.Quantity).Sum(),
                            // Price = d.OrderFoods.Select(o => (double)o.Order.TotalAmount).Sum(),
                            City = d.Customer.User.Address.City,
                            State = d.Customer.User.Address.State,
                            Street = d.Customer.User.Address.Street,
                            TotalAmount = d.TotalAmount,
                            AvailableTime = d.DateCreated,
                            OrderStatus = OrderStatus.IsPaid,
                            CustomerDto = new CustomerDto
                            {
                                Id = d.Customer.Id,
                                FirstName = d.Customer.User.FirstName,
                                LastName = d.Customer.User.LastName,
                                PhoneNumber = d.Customer.User.PhoneNumber,
                                Email = d.Customer.User.Email,
                            },
                        }    
                )
            };
        }



        public async Task<BaseResponce<OrderDto>> UpdateCustomer(
            int id,
            UpdateOrderRequestModel model
        )
        {
            var order = await _orderRepositry.Get(id);
            if (order is null)
            {
                return new BaseResponce<OrderDto> { Message = "Not found", Status = false, };
            }

            order.OrderStatus = model.OrderStatus;
            // order.OrderStatus = OrderStatus.Delivered;
            _orderRepositry.Update(order);
            _orderRepositry.Save();
            return new BaseResponce<OrderDto> { Message = "Successful", Status = true, };
        }

        //  public async Task<BaseResponce<OrderDto>> UpdateRefrenceNumber(int id, UpdateRefrenceNumber model)
         public async Task<BaseResponce<OrderDto>> UpdateRefrenceNumber( string ReferenceNumber)
        {

            var order = await _orderRepositry.Get(h => h.ReferenceNumber  == h.ReferenceNumber && h.OrderStatus == OrderStatus.IsPaid);
            if (order == null)
            {
                return new BaseResponce<OrderDto>{ Message = "Not Found", Status = false };
            }
            order.ReferenceNumber = ReferenceNumber;
            order.OrderStatus = OrderStatus.IsPaid;
            await _orderRepositry.Update(order);
            _orderRepositry.Save();
            return new BaseResponce<OrderDto>
            {
                Message = " Successfully Found",
                Status = true,
            };
        }

        public async Task<BaseResponce<OrderDto>> UpdateStatus(int id)
        {
            var order = await _orderRepositry.Get(id);
            if (order == null)
            {
                return new BaseResponce<OrderDto>
                 { Message = "Order not found", Status = false };
            }
            // order.OrderStatus = order.OrderStatus;
            order.OrderStatus = OrderStatus.Delivered;
            await _orderRepositry.Update(order);
            _orderRepositry.Save();
            return new BaseResponce<OrderDto>
            {
                Message = "Order updated Successfully",
                Status = true,
            };
        }

        private OrderDto CreateProdutOrder(Order OrdrProduct)
        {
            return new OrderDto
            {
                Id = OrdrProduct.Id,
                ReferenceNumber = OrdrProduct.ReferenceNumber,
                Price = OrdrProduct.OrderFoods.Select(o => (double)o.Price * o.Quantity).Sum(),
                CustomerDto = new CustomerDto
                {
                    Id = OrdrProduct.Customer.Id,
                    FirstName = OrdrProduct.Customer.User.FirstName,
                    UserDto = new UserDto
                    {
                        Id = OrdrProduct.Customer.User.Id,
                        FirstName = OrdrProduct.Customer.User.FirstName,
                        Address = new AddressDto { Id = OrdrProduct.Customer.User.Address.Id, }
                    },
                },
                CustomerId = OrdrProduct.CustomerId,
                OrderFoods = OrdrProduct.OrderFoods,
                OrderStatus = OrderStatus.Available,
            };
        }

        public async Task<BaseResponce<IEnumerable<OrderDto>>> GetsingleOrder(
            int id,
            int customerId
        )
        {
            var user = await _userRepositry.Get(customerId);
            var food = await _foodRepositry.Get(i => i.Name == i.Name);
            if (user == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }
            var customer = await _customerRepositry.Get(x => x.UserId == user.Id);
            //
            if (customer == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }
            var order = await _orderRepositry.GetSelected(x => x.CustomerId == customer.Id);
            // var order = await _orderRepositry.GetSelected(h => h.Id == id && !h.IsDeleted);
            if (order == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Status = false,
                    Message = "Order not found"
                };
            }
            var Orders = order
                .Select(
                    h =>
                        new OrderDto
                        {
                            Id = h.Id,
                            City = h.Customer.User.Address.City,
                            Street = h.Customer.User.Address.Street,
                            State = h.Customer.User.Address.State,
                            Price = h.OrderFoods.Select(o => (double)o.Price * o.Quantity).Sum(),
                             TotalAmount = h.TotalAmount,
                            OrderStatus = OrderStatus.Available,
                            AvailableTime = h.DateCreated,
                             Food = new FoodDto
                            {
                                Id = food.Id,
                                FoodName = food.Name,
                                Price = food.Price,
                            },
                            CustomerDto = new CustomerDto
                            {
                                Id = user.Id,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                               
                            }
                             
                        }
                        
                )
                .ToList();

            return new BaseResponce<IEnumerable<OrderDto>>
            {
                Status = true,
                Message = "Successful",
                Data = Orders
            };
        }

        public async Task<BaseResponce<IEnumerable<OrderDto>>> GetOrderoBystaff(int id, int userId)
        {
            var user = await _userRepositry.Get(userId);

            if (user == null)
            {
                Debug.WriteLine("User not found.");
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "User not found",
                    Status = false
                };
            }

            var customer = await _customerRepositry.Get(x => x.User.Id == user.Id);

            if (customer == null)
            {
                Debug.WriteLine("Customer not found.");
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "Customer not found",
                    Status = false
                };
            }

            var order = await _orderRepositry.GetSelected(x => x.CustomerId == customer.Id);

            if (order == null)
            {
                Debug.WriteLine("Order not found.");
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "Order not found",
                    Status = false
                };
            }

            var Orders = order
                .Select(
                    h =>
                        new OrderDto
                        {
                            Id = h.Id,
                            ReferenceNumber = h.ReferenceNumber,
                            City = h.Customer.User.Address.City,
                            Street = h.Customer.User.Address.Street,
                            State = h.Customer.User.Address.State,
                            Price = h.OrderFoods.Select(o => (double)o.Price * o.Quantity).Sum(),
                            OrderStatus = OrderStatus.Available,
                            AvailableTime = h.DateCreated,
                            CustomerDto = new CustomerDto
                            {
                                Id = user.Id,
                                UserId = userId,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                            }
                        }
                )
                .ToList();

            Debug.WriteLine($"Orders count: {Orders.Count}");

            return new BaseResponce<IEnumerable<OrderDto>>
            {
                Status = true,
                Message = "Successful",
                Data = Orders
            };
        }

        public async Task<BaseResponce<IEnumerable<OrderDto>>> GetOrderoForUserBystaff(
            int id,
            int userId
        )
        {
            var user = await _userRepositry.Get(id);
            //
            if (user == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }
            var customer = await _customerRepositry.Get(x => x.UserId == user.Id);
            //
            if (customer == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }
            var branch = await _branchRepositry.Get(x => x.Orders.Count() == user.Id);
            //
            if (customer == null)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "not Found",
                    Status = false,
                };
            }

            var orders = await _orderRepositry.GetSelected(x => x.CustomerId == customer.Id);

            if (orders.Count != 0)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Data = orders
                        .Select(
                            x =>
                                new OrderDto
                                {
                                    Id = x.Id,
                                    ReferenceNumber = x.ReferenceNumber,
                                    City = x.Customer.User.Address.City,
                                    Street = x.Customer.User.Address.Street,
                                    State = x.Customer.User.Address.State,
                                    Price = x.OrderFoods
                                        .Select(o => (double)o.Price * o.Quantity)
                                        .Sum(),
                                    OrderFoods = x.OrderFoods,
                                    CustomerDto = new CustomerDto
                                    {
                                        Id = user.Id,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        Email = user.Email,
                                        PhoneNumber = user.PhoneNumber,
                                    }
                                }
                        )
                        .ToList()
                };
            }
            return new BaseResponce<IEnumerable<OrderDto>>
            {
                Message = "Order is not Found",
                Status = false
            };
        }

       
        public async Task<BaseResponce<IEnumerable<OrderDto>>> GetOrderByBrachId(int branchId)
        {
            var customer = await _customerRepositry.GetSelected(o => o.User.Id == o.Id);

            var branch = await _branchRepositry.GetSelected(
                k => k.Orders.Count() == customer.Count()
            );

            var order = await _orderRepositry.GetSelected(o => o.BranchId == branchId);
            // var order = await  _orderRepositry.GetSelected(o => o.Customer.Id == branchId);
            if (order.Count != 0)
            {
                return new BaseResponce<IEnumerable<OrderDto>>
                {
                    Message = "Found",
                    Status = true,
                    Data = order
                        .Select(
                            orders =>
                                new OrderDto
                                {
                                    Id = orders.Id,
                                    BranchId = orders.BranchId,
                                    AvailableTime = orders.DateCreated,
                                    ReferenceNumber = orders.ReferenceNumber,
                                    Price = orders.OrderFoods
                                        .Select(o => (double)o.Price * o.Quantity)
                                        .Sum(),
                                    City = orders.Customer.User.Address.City,
                                    Street = orders.Customer.User.Address.Street,
                                    State = orders.Customer.User.Address.State,
                                    OrderStatus = OrderStatus.Available,
                                }
                        )
                        .ToList(),
                };
            }
            return new BaseResponce<IEnumerable<OrderDto>>
            {
                Message = "Not Found",
                Status = false,
                // Data =
            };
        }

        public async Task<BaseResponce<IEnumerable<OrderDto>>> GetListOfOrderByBranchId(
            List<int> ids
        )
        {
            var ListOrders = await _orderRepositry.GetAllOrder();
            var ListOrder = await _orderRepositry.GetSelected(
                c => ids.Contains(c.BranchId) && !c.IsDeleted
            );
            foreach (var item in ListOrder)
            {
                var food = await _orderRepositry.Get(
                    g => g.Id == item.BranchId && g.OrderFoods == item.OrderFoods
                );

                if (food == null)
                {
                    return new BaseResponce<IEnumerable<OrderDto>>
                    {
                        Message = "not Avilble",
                        Status = false
                    };
                }
            }
            return new BaseResponce<IEnumerable<OrderDto>> { Message = "Found", Status = true, };
        }

        public Task<BaseResponce<IEnumerable<OrderDto>>> GetAllOrdersWithCustomers()
        {
            throw new NotImplementedException();
        }

    }
}
