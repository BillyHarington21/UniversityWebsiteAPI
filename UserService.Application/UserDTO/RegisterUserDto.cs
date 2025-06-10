using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Core.Entities;

namespace UserService.Application.UserDTO
{
    public class RegisterUserDto
    {
        
            public string Email { get; set; } = string.Empty;
            public string Surname { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public UserRole Role { get; set; } = UserRole.student;
        

    }
}
