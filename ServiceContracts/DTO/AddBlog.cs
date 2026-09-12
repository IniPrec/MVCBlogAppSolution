using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.DTO
{
    public class AddBlog
    {
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
    }
}
