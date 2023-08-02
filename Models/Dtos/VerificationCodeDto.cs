using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class VerificationCodeDto
    {
        public int Code { get; set; }
        public bool isExpired { get; set;}
    }
}