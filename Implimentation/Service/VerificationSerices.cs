using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;

namespace RDMS.Implimentation.Service
{
    public class VerificationSerices : IVerificationCodeService
    {

           private readonly IMailServices _mailService;
        private readonly ICustomerRepositry _customerRepository;
        // private readonly IVerificationCodeRepositry _verificationCodeRepository;
         private readonly IUserRepositry _userRepository;
               
        public Task<BaseResponce<UserDto>> UpdateVeryficationCodeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<UserDto>> VerifyCode(int id, int verificationcode)
        {
            throw new NotImplementedException();
        }
    }
}