using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.UserDTO;

namespace UserService.Application.UserServices
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserDto RegisterDto);
        Task<AuthResultDto> AuthenticateAsync(LoginDto loginDto);
    }
}
