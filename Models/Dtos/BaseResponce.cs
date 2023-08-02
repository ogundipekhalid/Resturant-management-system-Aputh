using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDMS.Models.Dtos
{
    public class BaseResponce<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public  T Data { get; set; }

    }
}