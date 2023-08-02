using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Entity;
using RDMS.Models.Identity;

namespace RDMS.Interface.Repositry
{
    public interface IOrderRepositry : IBaseRepository<Order>
    {
        Task<Order> Get(int id);
        Task<Order> GetDetail(int id);
        Task<Order> DeleteAsync(int OrderId);
        Task<Order> Get(Expression<Func<Order, bool>> expression);
        Task<IList<Order>> GetSelected(List<int> ids);
        Task<List<OrderFood>> OrdersAsyncDrop();
        Task<IList<Order>> GetSelected(Expression<Func<Order, bool>> expression);
        Task<IEnumerable<OrderFood>> GetAllOrderFood();
        Task<IEnumerable<Order>> GetAllOrder();
        Task<IEnumerable<Order>> GetAllOrdersWithCustomer(Expression<Func<Order, bool>> expression);
        Task<IEnumerable<Order>> SearchProductsProductName(string searchInput, User user);
    }
}
