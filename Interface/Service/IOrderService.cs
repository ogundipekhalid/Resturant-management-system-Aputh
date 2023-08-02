using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;
using RDMS.ViewModel;

namespace RDMS.Interface.Service
{
    public interface IOrderService
    {
        Task<List<OrderFood>> OrdersAsync();
        Task<BaseResponce<OrderDto>> Get(int id,int customerId);
        Task<BaseResponce<OrderDto>> Get(int id);
        Task<BaseResponce<OrderDto>> DeleteAysc(int id);
        Task<BaseResponce<OrderDto>>  CreateOrder(CreateOrderRequestModel model, int userId );
        Task<BaseResponce<IEnumerable<CustomerDto>>> GetAllCustomer();
        Task<BaseResponce<IEnumerable<OrderDto>>> GetOrderoBystaff(int id , int userId );
         Task<BaseResponce<IEnumerable<OrderDto>>> GetsingleOrder(int id,int customerId);
        Task<BaseResponce<OrderDto>> CansuleOrder(int id);
        Task<BaseResponce<IEnumerable<OrderDto>>> GetOrderByBrachId(int branchId);
        Task<BaseResponce<OrderDto>> CalculatePriceAsync(int id , double price);
        Task<BaseResponce<IEnumerable<OrderFood>>> GetAllOrderFood();
        Task<BaseResponce<IEnumerable<OrderDto>>> GetAllOrder();
        Task<BaseResponce<IEnumerable<OrderDto>>> GetAllOrdersWithCustomer(int branchId);
        Task<BaseResponce<OrderDto>> UpdateCustomer (int id ,UpdateOrderRequestModel model);
         Task<BaseResponce<OrderDto>> UpdateStatus(int id);
         Task<BaseResponce<IEnumerable<OrderDto>>> GetOrderUserId(int id);
         Task<BaseResponce<IEnumerable<OrderDto>>> GetOrderoForUserBystaff(int id, int userId);
         Task<BaseResponce<IEnumerable<OrderDto>>> GetListOfOrderByBranchId(List<int> ids);
        Task<BaseResponce<IEnumerable<OrderDto>>> GetAllOrdersWithCustomers();
        Task<BaseResponce<OrderDto>> UpdateRefrenceNumber( string ReferenceNumber);

    }
}
