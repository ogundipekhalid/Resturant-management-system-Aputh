using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;
using static PayStack.Net.Charge;

namespace RDMS.Implimentation.Service
{
    public class AddressServices : IAddressServices
    {
        private readonly IAddressRepositry _addressRepositry;
        private readonly IBranchRepositry _branchRepositry;
        private readonly ICustomerRepositry _customerRepositry;

        public AddressServices(
            IAddressRepositry addressRepositry,
            IBranchRepositry branchRepositry,
            ICustomerRepositry customerRepositry
        )
        {
            _addressRepositry = addressRepositry;
            _branchRepositry = branchRepositry;
            _customerRepositry = customerRepositry;
        }

        public Task<BaseResponce<AddressDto>> Create(int id, CreateAddressRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponce<AddressDto>> Get(int id)
        {
            var address = await _addressRepositry.Get(id);
            if (address == null)
            {
                return new BaseResponce<AddressDto>
                {
                    Message = "Address not found",
                    Status = false
                };
            }
            return new BaseResponce<AddressDto>
            {
                Message = "Address found",
                Status = true,
                Data = new AddressDto
                {
                    State = address.State,
                    City = address.City,
                    Street = address.Street,
                    ZipCode = address.ZipCode
                }
            };
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            var add = await _addressRepositry.GetAll();
            if (add == null)
                return null;
            return add.ToList();
        }

        public async Task<List<Address>> SingleListAddress(int addressid)
        {
            var add = await _addressRepositry.GetAll();
            if (add == null)
                return null;
            return add.ToList();
        }

        public async Task<List<Address>> AddressOfAvaialableBranches()
        {
            var allbranch = await _branchRepositry.GetAll();
            if (allbranch is null)
                return null;
            var addressIds = allbranch.Select(c => c.AddressId).ToList();
            var listofaddressInfo = new List<Address>();
            foreach (var item in addressIds)
            {
                var addressinfo = await _addressRepositry.Get(item);
                listofaddressInfo.Add(addressinfo);
            }
            return listofaddressInfo;
        }

        public async Task<List<Address>> UserAllAddresses(int addressid)
        {
            var add = await _addressRepositry.GetSelected(k => k.Branch.AddressId == addressid);
            if (add == null)
                return null;
            return add.ToList();
        }

        public async Task<BaseResponce<Address>> GetAdds(int addressId)
        {
            var food = await _branchRepositry.GetBranchFoods(
                x => x.BranchFoods.Count() == addressId
            );
            if (food == null)
            {
                return new BaseResponce<Address> { Message = "Not found", Status = false, };
            }
            // var foo = new List<Address>();
            var foo = new Address();
            foreach (var branc in food)
            {
                foreach (var item in branc.BranchFoods)
                {
                    var fold = new Address
                    {
                        Id = item.Food.Id,
                        City = item.Branch.Eatery.User.Address.City,
                        State = item.Branch.Eatery.User.Address.State,
                        Street = item.Branch.Eatery.User.Address.Street,
                        ZipCode = item.Branch.Eatery.User.Address.ZipCode,
                    };
                    // return foo.Add(fold);
                    _addressRepositry.Add(foo);
                    _addressRepositry.Save();
                    _branchRepositry.Save();
                    // foo.Add(fold);
                    // foo.Add(fold);
                }
            }
            ;
            // var cap = new BaseResponce<List<Address>>
            var cap = new BaseResponce<Address>
            {
                Message = "Successful",
                Status = true,
                Data = foo,
            };
            return new BaseResponce<Address> { Message = "NOT FOUND", Status = true, };
        }

        public async Task<BaseResponce<List<Address>>> UserAddressId(int addressId)
        {
            // var food = await _branchRepositry.GetBranchFoods(x => x.AddressId == addressId);
            var food = await _addressRepositry.GetSelected(x => x.Branch.AddressId == addressId);
            if (food == null)
            {
                return new BaseResponce<List<Address>> { Message = "Not found", Status = false, };
            }

            var foo = new List<Address>();
            foreach (var branc in food)
            {
                foreach (var item in branc.Branch.BranchFoods)
                {
                    var fold = new Address
                    {
                        Id = item.Food.Id,
                        City = item.Branch.Eatery.User.Address.City,
                        State = item.Branch.Eatery.User.Address.State,
                        Street = item.Branch.Eatery.User.Address.Street,
                        ZipCode = item.Branch.Eatery.User.Address.ZipCode,
                        //Name = item.Food.Name,
                        //Image = item.Food.Image,
                        //Price = item.Food.Price,
                        // Address = item.Branch.Address,
                    };
                    foo.Add(fold);
                }
            }
            var cap = new BaseResponce<List<Address>>
            {
                Message = "Successful",
                Status = true,
                Data = foo,
            };
            return new BaseResponce<List<Address>> { Message = "NOT FOUND", Status = true, };
        }

        //  public async Task<BaseResponce<List<Customer>>> CustomerAddressId(int addressId)
        //     {
        //         var customer = await _customerRepositry.getadcu(o => o.User.AddressId == addressId);
        //         if (customer == null)
        //         {
        //             return new BaseResponce<List<Customer>>
        //             {
        //                 Message = "Not found",
        //                 Status = false,
        //             };
        //         }
        //                 var foo = new List<Customer>();
        //         foreach (var branc in  )
        //         {
        //             foreach (var item in branc.BranchFoods)
        //             {
        //                 var fold = new Address
        //                 {
        //                     Id = item.Food.Id,
        //                     City = item.Branch.Eatery.User.Address.City,
        //                     State = item.Branch.Eatery.User.Address.State,
        //                     Street = item.Branch.Eatery.User.Address.Street,
        //                     ZipCode = item.Branch.Eatery.User.Address.ZipCode,
        //                     //Name = item.Food.Name,
        //                     //Image = item.Food.Image,
        //                     //Price = item.Food.Price,
        //                    // Address = item.Branch.Address,
        //                 };
        //                 foo.Add(fold);
        //             }
        //         }
        //         var cap = new BaseResponce<List<Address>>
        //         {
        //             Message = "Successful",
        //             Status = true,
        //             Data = foo,
        //         };
        //        // return new BaseResponce<List<Address>> { Message = "NOT FOUND", Status = true, };

        //     }


        public async Task<BaseResponce<IEnumerable<FoodDto>>> UserFoodsByAddressId(int addressId)
        {
            var food = await _branchRepositry.GetBranchFoods(x => x.AddressId == addressId);
            if (food == null)
            {
                return new BaseResponce<IEnumerable<FoodDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }

            var foo = new List<FoodDto>();
            foreach (var branc in food)
            {
                foreach (var item in branc.BranchFoods)
                {
                    var fold = new FoodDto
                    {
                        Id = item.Food.Id,
                        FoodName = item.Food.Name,
                        Image = item.Food.Image,
                        Price = item.Food.Price,
                        Address = item.Branch.Address,
                    };
                    foo.Add(fold);
                }
            }
            var cap = new BaseResponce<IEnumerable<FoodDto>>
            {
                Message = "Successful",
                Status = true,
                Data = foo,
            };
            return cap;
        }

        public Task<BaseResponce<List<Customer>>> CustomerAddressId(int addressId)
        {
            throw new NotImplementedException();
        }
    }
}
