using Duende.IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Infrastructure.Identity.Security;

namespace UserService.Infrastructure.Identity.Auth
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userRepository.GetByUsernameAsync(context.UserName); // логин через Email

            if (user != null && PasswordHasher.Verify(context.Password, user.PasswordHash, user.PasswordSalt))
            {
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "custom",
                    claims: new[]
                    {
                    new Claim("name", $"{user.FirstName} {user.LastName}"),
                    new Claim("email", user.Email),
                    new Claim("role", user.Role.ToString()),
                    new Claim("surname", user.Surname)
                    });
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
            };
        }
       
    }
}
