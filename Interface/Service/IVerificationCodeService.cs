using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.Interface.Service
{
    public interface IVerificationCodeService
    {
         Task<BaseResponce<UserDto>> UpdateVeryficationCodeAsync(int id);
         Task<BaseResponce<UserDto>> VerifyCode(int id, int verificationcode);
       // Task<ResetPasswordResponseModel> SendForgetPasswordVerificationCode(string email);
    
    }
}