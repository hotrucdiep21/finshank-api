using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class CreateRegisterDto
    {
        [Required]
        public string? Username = string.Empty;
        [Required]
        [EmailAddress]
        public string? Email = string.Empty;
        [Required]
        public string? Password = string.Empty;
    }
}