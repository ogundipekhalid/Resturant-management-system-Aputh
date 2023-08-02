    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.AplicationContext;
using RDMS.Aputh;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Actors;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;
using RDMS.ViewModel;

namespace RDMS.Implimentation.Service
{
    public class CustomerServices : ICustomerService
    {
        private readonly ICustomerRepositry _customerRepo;
        private readonly IRoleRepositry _roleRepositry;
        private readonly IUserRepositry _userRepositry;
        private readonly IBranchRepositry _branchRepositry;
        private readonly IAddressRepositry _addressRepositry;
        private readonly IMailServices _mailService;
      
        public CustomerServices(IBranchRepositry branchRepositry,ICustomerRepositry customerRepo, IRoleRepositry roleRepositry, IUserRepositry userRepositry, IAddressRepositry addressRepositry, IMailServices mailService)
        {
            _customerRepo = customerRepo;
            _roleRepositry = roleRepositry;
            _userRepositry = userRepositry;
            _addressRepositry = addressRepositry;
            _mailService = mailService;
             _branchRepositry  = branchRepositry;
        }

        public Task<BaseResponce<CustomerDto>> CheckWalletAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponce<CustomerDto>> Create(CreateCustomerRequestModel model)
        {
            var checkExsit = await _customerRepo.Get(a => a.User.Email == model.Email);
            if (checkExsit != null)
            {
                return new BaseResponce<CustomerDto>
                {
                    Message = "Email already exist",
                    Status = false,
                };
            }
            var address = new Address
            {
                Street = model.Street,
                State = model.State,
                City = model.City,
                ZipCode = model.ZipCode,
            };
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                AddressId = address.Id,
                Address = address,
                Wallet = 0,
            };
             
            var role = await _roleRepositry.Get(b => b.Name == "Customer");
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                Role = role,
                User = user,
            };
            
            // await _branchRepositry.GetDetails(model.id);
            await _userRepositry.Update(user);
            var customer = new Customer 
            { 
                UserId = user.Id,
                User = user,
                // Branch = user.Id,
              };
            user.UserRoles.Add(userRole);
           var adduser = await _customerRepo.Add(customer);
                var mailRequest = new MailRequestDto
            {  
                Subject = "Complete Your Registration",
                ToEmail = user.Email,
                ToName = model.FirstName,
                HtmlContent = $"Dear {user.Email} , this is to inform Mr {user.FirstName} {user.LastName} has been regiter to D feane  Retaurant  {DateTime.Now}<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\"><title>Email Confirmation</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><style type=\"text/css\">@media screen {{@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 400;src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');}}@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 700;src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');}}body,table,td,a {{-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}}table,td {{mso-table-rspace: 0pt;mso-table-lspace: 0pt;}}img {{-ms-interpolation-mode: bicubic;}}a[x-apple-data-detectors] {{font-family: inherit !important;font-size: inherit !important;font-weight: inherit !important;line-height: inherit !important;color: inherit !important;text-decoration: none !important;}}div[style*=\"margin: 16px 0;\"] {{margin: 0 !important;}}body {{width: 100% !important;height: 100% !important;padding: 0 !important;margin: 0 !important;}}table {{border-collapse: collapse !important;}}a {{color: #1a82e2;}}img {{height: auto;line-height: 100%;text-decoration: none;border: 0;outline: none;}}</style></head><body style=\"background-color: #e9ecef;\"><div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.</div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 36px 24px;\"><a href=\"https://sendgrid.com\" target=\"_blank\" style=\"display: inline-block;\"><img src=\"https://media.licdn.com/dms/image/C510BAQHtR8AdDc-aJg/company-logo_200_200/0/1519909536138?e=2147483647&v=beta&t=n-uF8UVHI5jdSuAZ61e6OVnV1n8PWocgp3lZ0igTpyg\" alt=\"Logo\" border=\"0\" width=\"100\" height=\"100\" style=\"display: block;border-radius: 50%;\"></a></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;\"><h2 style=\"margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;\">Confirm Your Email Address</h2></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\"><p style=\"margin: 0;\">Tap the button below to confirm your email address. If you didn't create an account with <strong>Dansnom</strong>, you can safely delete this email.</p></td></tr><tr><td align=\"left\" bgcolor=\"#ffffff\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#ffffff\" style=\"padding: 12px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td align=\"center\" bgcolor=\"#1a82e2\" style=\"border-radius: 6px;\"><a href=\"http://127.0.0.1:5501/FrontEnd/AdminFrontEnd/completeRegistration.html?token={adduser.User.Token}\" target=\"_blank\" style=\"display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;\">Confirm</a></td></tr></table></td></tr></table></td></tr></td></tr></table><body></html>"
              //HtmlContent = $"<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\"><title>Email Confirmation</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><style type=\"text/css\">@media screen {{@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 400;src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');}}@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 700;src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');}}body,table,td,a {{-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}}table,td {{mso-table-rspace: 0pt;mso-table-lspace: 0pt;}}img {{-ms-interpolation-mode: bicubic;}}a[x-apple-data-detectors] {{font-family: inherit !important;font-size: inherit !important;font-weight: inherit !important;line-height: inherit !important;color: inherit !important;text-decoration: none !important;}}div[style*=\"margin: 16px 0;\"] {{margin: 0 !important;}}body {{width: 100% !important;height: 100% !important;padding: 0 !important;margin: 0 !important;}}table {{border-collapse: collapse !important;}}a {{color: #1a82e2;}}img {{height: auto;line-height: 100%;text-decoration: none;border: 0;outline: none;}}</style></head><body style=\"background-color: #e9ecef;\"><div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.</div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 36px 24px;\"><a href=\"https://sendgrid.com\" target=\"_blank\" style=\"display: inline-block;\"></a></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;\"><h2 style=\"margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;\">Confirm Your Email Address</h2></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\"><p style=\"margin: 0;\">Tap the button below to confirm your email address. If you didn't create an account with <strong>Dansnom</strong>, you can safely delete this email.</p></td></tr><tr><td align=\"left\" bgcolor=\"#ffffff\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#ffffff\" style=\"padding: 12px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td align=\"center\" bgcolor=\"#1a82e2\" style=\"border-radius: 6px;\"><a href=\"http://127.0.0.1:5501/FrontEnd/AdminFrontEnd/completeRegistration.html?token={adduser.User.Token}\" target=\"_blank\" style=\"display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;\">Confirm</a></td></tr></table></td></tr></table></td></tr></td></tr></table><body></html>"

                //HtmlContent =  $"Dear {user.Email} , this is to inform Mr {user.FirstName} {user.LastName} has been regiter to Lasix Retaurant  {DateTime.Now} ",
            };
            _mailService.SendEMailAsync(mailRequest);
            _customerRepo.Save();
          
            return new BaseResponce<CustomerDto>
            {
                Message = "Created successfully",
                Status = true,
                Data = new CustomerDto
                {
                    UserId = customer.User.Id,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    Email = customer.User.Email,
                    //FullName = $"{model.FirstName} {model.LastName}",
                }
            };
        }

        public async Task<BaseResponce<CustomerDto>> Delete(int id)
        {
             var customer = await _customerRepo.Get(a => a.Id  == a.Id);
            if (customer == null)
            {
                return new BaseResponce<CustomerDto>
                {
                    Message = "Account not found",
                    Status = false
                };
               
            }
                customer.IsDeleted = true;
                //await _customerRepo.Update(customer);
                await _customerRepo.Delete(customer);
                 _customerRepo.Save();
            return new BaseResponce<CustomerDto>
            {
                Message = "Your Account has being Deleted",
                Status = true
            };
        }

      public async Task<BaseResponce<CustomerDto>> Funds(
            int id,double Wallet)
        {
            var user = await _userRepositry.Get(id);
            if (user == null)
            return new BaseResponce<CustomerDto>
            {
                 Message = "User not found",
                 Status = false,
            };

             if (Wallet <= 0) return new BaseResponce<CustomerDto>
                {
                    Message = "Invalid Transaction",
                    Status = false,
                };
                else
                 {
                user.Wallet += Wallet;
                await  _userRepositry.Update(user);
                _userRepositry.Save();
                return  new BaseResponce<CustomerDto>
                {
                    Message = "success",
                    Status = true,
                };
             }
        }
        public async Task<BaseResponce<CustomerDto>> FundWallet(
            int id,
            double Wallet
        )
        {
            var customer = await _customerRepo.Get(v => v.Id == v.Id);
            customer.User.Wallet += Wallet;
            await _customerRepo.Update(customer);
            _customerRepo.Save();

            return new BaseResponce<CustomerDto>
            {
                Message = "Wallet Updated Successfully",
                Status = false,
            };
        }


        public async Task<BaseResponce<CustomerDto>> AddMoneyToWallet(int id, double Wallet )
        {
            var customer = await _customerRepo.Get(f => f.Id == f.Id);
            var currentBalance = customer.User.Wallet;
            if (Wallet <= 0)
            {
                return null;
          
            }
            var newBalance = currentBalance + Wallet;
            // customer.User = newBalance;
             await _customerRepo.Update(customer);
             _customerRepo.Save();
            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                // walle = newBalance
            };
              return new BaseResponce<CustomerDto>
            {
                Message = "Wallet Updated Successfully",
                Status = false,
            };
        }

        public async Task<BaseResponce<CustomerDto>> Get(int id)
        {
            var getcustomer = await _userRepositry.Get(i => i.Id == id);
            
            if (getcustomer == null)
            {

                return new BaseResponce<CustomerDto>
                {
                    Message = "User not found",
                    Status = false,
                };
            }

            return new BaseResponce<CustomerDto>
            {
                Message = "success",
                Status = true,
                Data = new CustomerDto 
                { 
                    Id = getcustomer.Id, 
                    UserId = getcustomer.Id,
                    // FirstName = getcustomer.User.FirstName,
                    // LastName = getcustomer.User.LastName,
                    // Email = getcustomer.User.Email,
                    // PhoneNumber = getcustomer.User.PhoneNumber
                    FirstName = getcustomer.FirstName,
                    LastName = getcustomer.LastName,
                    Email = getcustomer.Email,
                    PhoneNumber = getcustomer.PhoneNumber
                 }
            };
        }

        public async Task<BaseResponce<IEnumerable<CustomerDto>>> GetAll()
        {
            var customer = await _customerRepo.GetAll();

            var getall = customer.Select(
                customers =>
                    new CustomerDto
                    {
                        Id = customers.Id,
                        UserId = customers.User.Id,
                        FirstName = customers.User.FirstName,
                        LastName = customers.User.LastName,
                        Email = customers.User.Email,
                        PhoneNumber  = customers.User.PhoneNumber
                    }
            );
            return new BaseResponce<IEnumerable<CustomerDto>>
            {
                Message = "Sueccufullly",
                Status = true,
                Data = getall,
            };
        }

        public async Task<BaseResponce<CustomerDto>> UpdateCustomer(
            int id,
            UpdateCustomerRequestModel model
        )
        {
            var customer = await _userRepositry.Get(s => s.Id == model.Id);
            if (customer == null)
            {
                return new BaseResponce<CustomerDto>
                {
                    Message = "Profile Not Found",
                    Status = false
                };
            }

                customer.FirstName = model.FirstName ;
                customer.LastName = model.LastName;
                customer.Email = model.Email;
                customer.PhoneNumber = model.PhoneNumber;
                // customer.User.Password = model.user.Password;
                //get.User.FirstName = customer.FirstName ?? get.User.FirstName;
            await _userRepositry.Update(customer);
            _customerRepo.Save();
            return new BaseResponce<CustomerDto>
            {
                Message = "Profile Successfully Updated",
                Status = true
            };
        }

        public async Task<BaseResponce<CustomerDto>> UpdateWalletAsync(
            int id,
            UpdateWalletRequestModel model
        )
        {
            var customer = await _customerRepo.Get(c => c.User.Id == id);
            customer.User.Wallet += model.Wallet;
            await _userRepositry.Update(customer.User);
            return new BaseResponce<CustomerDto>
            {
                Message = "Wallet Updated Successfully",
                Status = false,
            };
        }

        public async Task<BaseResponce<CustomerDto>> VeiwProfileAsync(string email)
        {
            var custmer = await _customerRepo.Get(a => a.User.Email == email);
            if (custmer == null)
            {
                return new BaseResponce<CustomerDto>
                {
                    Message = "Profile not found",
                    Status = false
                };
            }
            return new BaseResponce<CustomerDto>
            {
                Message = "Profile was found",
                Status = true,
                Data = new CustomerDto
                {
                    Id = custmer.Id,
                    UserId = custmer.User.Id,
                    FirstName = custmer.User.FirstName,
                    LastName = custmer.User.LastName,
                    Email = custmer.User.Email
                }
            };
        }

            //private string generatedToken = null;

    }
}
