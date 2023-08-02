using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDMS.AplicationContext;
using RDMS.Interface.Repository;
using RDMS.Interface.Repositry;
using RDMS.Models.Entity;

namespace RDMS.Implimentation.Repositry
{
    public class BaseRepositry<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private protected ResturantDbContext _context;

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
            return entity;
        }

        public void Save()
        {
             _context.SaveChanges();
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

         public async Task Saves()
        {
             _context.SaveChangesAsync();
        }

    }
}