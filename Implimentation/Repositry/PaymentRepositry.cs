using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repositry;
using RDMS.Models.Identity;

namespace RDMS.Implimentation.Repositry
{
    public class PaymentRepositry : BaseRepositry<PaymentReference>, IPaymentRepositry
    {
        public PaymentRepositry(ResturantDbContext context)
        {
            _context = context;
        }
        public async Task<PaymentReference> GetAsync(string transactionReference)
        {
            return await _context.PaymentReferences
            .Include(x => x.Order)
            .Include(x => x.Customer)
            .ThenInclude(c => c.User)
            .Where(x => x.ReferenceNumber == transactionReference).SingleOrDefaultAsync();
        }

        public async Task<PaymentReference> GetAsync(Expression<Func<PaymentReference, bool>> expression)
        {
            var entity = await _context.Set<PaymentReference>().FirstOrDefaultAsync(expression);
            return entity;
    
        }
    }
}