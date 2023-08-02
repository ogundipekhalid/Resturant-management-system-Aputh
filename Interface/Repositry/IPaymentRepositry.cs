using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RDMS.Interface.Repository;
using RDMS.Models.Identity;

namespace RDMS.Interface.Repositry
{
    public interface IPaymentRepositry : IBaseRepository<PaymentReference>
    {
        Task<PaymentReference> GetAsync(string transactionReference);
         Task<PaymentReference> GetAsync(Expression<Func<PaymentReference, bool>> expression);
    }
}