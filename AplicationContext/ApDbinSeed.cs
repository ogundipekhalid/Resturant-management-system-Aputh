// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using RDMS.Models.Actors;
// using RDMS.Models.Entity;
// using RDMS.Models.Identity;

// namespace RDMS.AplicationContext
// {
//     public class ApDbinSeed
//     {
//         public static void Seed(IApplicationBuilder applicationBuilder)
//         {
//             using (var servicesScope = applicationBuilder.ApplicationServices.CreateScope())
//             {
//                 var context = servicesScope.ServiceProvider.GetService<ResturantDbContext>();

//                 context.Database.EnsureCreated();

//                 // add superAdmin enum
//                 if (!context.Roles.Any())
//                 {
//                     Role role = new Role()
//                     {
//                         Name = "SuperAdmin",
//                         Description = "SuperAdmin",
//                         IsDeleted = false,
//                         DateCreated = DateTime.Now,
//                     };
//                     // add role to Data base
//                     context.Roles.Add(role);

//                     var user = new User
//                     {
//                         FirstName = "Admide",
//                         LastName = "olamide",
//                         Email = "AdeMi@gmail.com",
//                         Password = "olamide",
//                         PhoneNumber = "090369762",
//                         Wallet = 0,
//                         IsDeleted = false,
//                         DateCreated = DateTime.Now,
//                     };
//                       context.Users.Add(user);

//                     var userRole = new UserRole
//                     {
//                         UserId =  1,
//                         RoleId = 1,
//                         IsDeleted = false,
//                         DateCreated = DateTime.Now  
//                     };

//                     var  Eatery  = new Eatery
//                     {
//                         Name = "",
//                         CertificationNumber = "",
//                         Certificate = "",
//                         Logo = "",
//                     };

//                 //    var  EateryManager  = new EateryManager
//                 //    {
                        
//                 //    }
//                 }
//                   context.Users.AddRange();
//                   context.SaveChanges();

//                 // if (!context.Roles.Any()) 
//                 // { 

//                 // }
//             }
//         }
//     }
// }
