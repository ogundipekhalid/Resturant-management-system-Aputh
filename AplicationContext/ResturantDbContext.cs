using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RDMS.Models.Actors;
using RDMS.Models.Entity;
using RDMS.Models.Identity;

namespace RDMS.AplicationContext
{
    public class ResturantDbContext : DbContext
    {
        public ResturantDbContext(DbContextOptions<ResturantDbContext> options) : base(options)
        {
            
        }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderFood> OrderFoods { get; set; }
        public DbSet<Role> Roles  { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set;}
        public DbSet<Branch> Branches { get; set;}
        public DbSet<BranchManager> BranchManagers { get; set;}
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Eatery> Eateries{ get; set; }
        public DbSet<EateryAdmin> EateryAdmins { get; set; }
        public DbSet<BranchFood> BranchFoods {get; set;}
        public DbSet<Position> Positions {get;set;}
        public DbSet<PaymentReference> PaymentReferences {get;set;}
        public DbSet<Comment> Comments {get;set;}
        public DbSet<CartItem> CartItems {get;set;}
    }
}