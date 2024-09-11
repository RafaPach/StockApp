using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class RegisterDto
    {
        [Requried]
        public string? UserName { get; set; } 

        [Required]
        public string? Email { get; set; }

        [Required]

        public string? Password { get; set; }
    }
}