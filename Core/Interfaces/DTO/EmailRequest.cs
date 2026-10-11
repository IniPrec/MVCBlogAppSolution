using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DTO
{
    public class EmailRequest
    {
        public string? Sender { get; set; }
        public string? Reciever { get; set; }
        public List<string>? Cc { get; set; }
        public List<string>? Bcc { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
