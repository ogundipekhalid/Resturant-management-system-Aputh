using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.Interface.Service
{
    public interface IMailServices
    {
        void SendEMailAsync(MailRequestDto mailRequest);

    }
}