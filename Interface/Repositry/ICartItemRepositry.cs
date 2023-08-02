using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Entity;

namespace RDMS.Interface.Repositry
{
    public interface ICartItemRepositry: IBaseRepository<CartItem>
    {
        bool Find(int id);
        bool Find(Expression<Func<CartItem, bool>> expression);
        CartItem Get(int id);
        CartItem Get(Expression<Func<CartItem,bool>> expression);
        IEnumerable<CartItem> GetAll();
        IEnumerable<CartItem> GetSelected(List<int> ids);
        IEnumerable<CartItem> GetSelected(Expression<Func<CartItem,bool>> expression);
    }
   
}