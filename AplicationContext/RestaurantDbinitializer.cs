// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using RDMS.Models.Entity;
// using RDMS.Models.Identity;

// namespace RDMS.AplicationContext
// {
//     public class RestaurantDbinitializer
//     {
//         public static async void Seed(IApplicationBuilder applicationBuilder)
//         {
//             var userId = Convert.ToInt32("USERID53D4EB9E-1C51-44A4-A012-25C98042E32A");
//             // var adminId = "ADMINID8726B43F-83F4-4586-AF5D-DF9DFF91ADBD";
//             // var admin = new SuperAdmin()
//             // {
//             //     Id = adminId,
//             //     IsDeleted = false,
//             //     CreatedBy = "Auto Create",
//             //     CreatedOn = DateTime.Now,
//             //     UserId = userId,
//             // };

//             var user = new List<User>
//             {
//                 new User()
//                 {
//                     Id = userId,
//                     FirstName = "Olayemi",
//                     LastName = "Ahmad",
//                     Password = "Admin", // BCrypt.Net.BCrypt.HashPassword("Admin", SaltRevision.Revision2B),
//                     Email = "aymoneyay@gmail.com",
//                     PhoneNumber = "08066117783",
//                     CreatedBy = "Auto Create",
//                 }
//             };

//             var listOfBanks = new List<AcceptedNigerianBank>
//             {
//                 new AcceptedNigerianBank { BankName = "Access Bank", BankCode = "044", },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Access Bank (Diamond)",
//                     BankCode = "063",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "ALAT by WEMA",
//                     BankCode = "035A",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "ASO Savings and Loans",
//                     BankCode = "401",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Bowen Microfinance Bank",
//                     BankCode = "50931",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "CEMCS Microfinance Bank",
//                     BankCode = "50823",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Citibank Nigeria",
//                     BankCode = "023",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Ecobank Nigeria",
//                     BankCode = "050",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Ekondo Microfinance Bank",
//                     BankCode = "562",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Eyowo",
//                     BankCode = "50126",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Fidelity Bank",
//                     BankCode = "070",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "First Bank of Nigeria",
//                     BankCode = "011",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "First City Monument Bank",
//                     BankCode = "214",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "FSDH Merchant Bank Limited",
//                     BankCode = "501",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Globus Bank",
//                     BankCode = "00103",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Guaranty Trust Bank",
//                     BankCode = "058",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Hackman Microfinance Bank",
//                     BankCode = "51251",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Hasal Microfinance Bank",
//                     BankCode = "50383",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Heritage Bank",
//                     BankCode = "030",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Ibile Microfinance Bank",
//                     BankCode = "51244",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Jaiz Bank",
//                     BankCode = "301",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Keystone Bank",
//                     BankCode = "082",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Kuda Bank",
//                     BankCode = "50211",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Lagos Building Investment Company Plc.",
//                     BankCode = "90052",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "One Finance",
//                     BankCode = "565",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Parallex Bank",
//                     BankCode = "526",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Parkway - ReadyCash",
//                     BankCode = "311",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Polaris Bank",
//                     BankCode = "076",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Providus Bank",
//                     BankCode = "101",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Rubies MFB",
//                     BankCode = "125",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Sparkle Microfinance Bank",
//                     BankCode = "51310",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Stanbic IBTC Bank",
//                     BankCode = "221",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Standard Chartered Bank",
//                     BankCode = "068",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Sterling Bank",
//                     BankCode = "232",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Suntrust Bank",
//                     BankCode = "100",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "TAJ Bank",
//                     BankCode = "302",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "TCF MFB",
//                     BankCode = "51211",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Titan Bank",
//                     BankCode = "102",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Union Bank of Nigeria",
//                     BankCode = "032",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "United Bank For Africa",
//                     BankCode = "033",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Unity Bank",
//                     BankCode = "215",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "VFD",
//                     BankCode = "566",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Wema Bank",
//                     BankCode = "035",
//                     // CreatedBy=admin.UserId ,
//                     // CreatedOn=admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//                 new AcceptedNigerianBank
//                 {
//                     BankName = "Zenith Bank",
//                     BankCode = "057",
//                     // CreatedBy = admin.UserId,
//                     // CreatedOn = admin.CreatedOn,
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                 },
//             };

//             var listOfCategories = new List<CatigoriesBank>
//             {
//                new CatigoriesBank
//                 {
//                     Id = Convert.ToInt32(Guid.NewGuid().ToString()),
//                     Name = "Graphic Design",
//                     Description = "Creative Arts",
//                     // CreatedBy = admin.UserId,
//                     // CreatedOn = admin.CreatedOn,
//                     IsAvailable = true,
//                     CategoryImage = "DefaultCategoryImage.jpg",
//                 }
//             };

//             using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//             {
//                 var context = serviceScope.ServiceProvider.GetService<>(ResturantDbContext);

//                 await context.Database.MigrateAsync();

//                 if (!context.Roles.Any())
//                 {
//                     var listOfRoles = new List<Role>
//                     {
//                         new Role()
//                         {
//                             // RoleName = "Admin",
//                             // Description = "Admin",
//                             // CreatedOn = DateTime.Now,
//                             Id = Convert.ToInt32("Admin"),
//                             CreatedBy = "Auto Create",
//                             UserRoles = user,
//                         },
//                         new Role()
//                         {
//                             RoleName = "Instructor",
//                             Description = "Instructor",
//                             CreatedOn = DateTime.Now,
//                             CreatedBy = "Auto Create",
//                             Id = "Instructor",
//                         },
//                         new Role()
//                         {
//                             RoleName = "Moderator",
//                             Description = "Moderator",
//                             CreatedOn = DateTime.Now,
//                             CreatedBy = "Auto Create",
//                             Id = "Moderator",
//                         },
//                         new Role()
//                         {
//                             RoleName = "Student",
//                             Description = "Student",
//                             CreatedOn = DateTime.Now,
//                             CreatedBy = "Auto Create",
//                             Id = "Student",
//                         }
//                     };

//                     context.Categories.AddRange(listOfCategories);
//                     context.SaveChanges();
//                     context.AcceptedNigerianBanks.AddRange(listOfBanks);
//                     context.SaveChanges();
//                     context.Roles.AddRange(listOfRoles);
//                     context.SaveChanges();
//                 }
//             }
//         }
//     }
// }
