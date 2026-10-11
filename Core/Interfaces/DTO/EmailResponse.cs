using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DTO
{
    public class EmailResponse
    {
        public int ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
