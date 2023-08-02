using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.ViewModel;

namespace RDMS.Interface.Service
{
    public interface ICustomerService
    {
        Task<BaseResponce<CustomerDto>> Create(CreateCustomerRequestModel model);
        Task<BaseResponce<CustomerDto>> UpdateCustomer (int id ,UpdateCustomerRequestModel model);
        Task<BaseResponce<IEnumerable<CustomerDto>>> GetAll();
        Task<BaseResponce<CustomerDto>> Get(int id);
        Task<BaseResponce<CustomerDto>> Delete(int id);
        Task<BaseResponce<CustomerDto>> FundWallet(int id,  double Wallet);
        Task<BaseResponce<CustomerDto>> AddMoneyToWallet(int id, double Wallet );
        Task<BaseResponce<CustomerDto>> VeiwProfileAsync(string email);
        Task<BaseResponce<CustomerDto>> UpdateWalletAsync(int id, UpdateWalletRequestModel model);
    }
}
