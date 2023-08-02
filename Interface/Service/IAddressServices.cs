using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;
using static PayStack.Net.Charge;

namespace RDMS.Interface.Service
{
    public interface IAddressServices
    {
        Task<BaseResponce<AddressDto>> Create(int id, CreateAddressRequestModel model);
        Task<BaseResponce<AddressDto>> Get(int id);
        Task<List<Address>> GetAllAddresses();
        Task<BaseResponce<List<Address>>> UserAddressId(int addressId);
        Task<BaseResponce<Address>> GetAdds(int addressId);
        Task<List<Address>> SingleListAddress(int addressid);
        Task<List<Address>> AddressOfAvaialableBranches();
         Task<BaseResponce<List<Customer>>> CustomerAddressId(int addressId);
        Task<List<Address>> UserAllAddresses(int addressid);
    }
}
