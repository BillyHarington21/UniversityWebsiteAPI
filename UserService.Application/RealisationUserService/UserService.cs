using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.UserDTO;
using UserService.Application.UserServices;
using UserService.Core.Entities;
using UserService.Infrastructure;

namespace UserService.Application.RealisationUserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                Surname = dto.Surname,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email.ToLower(),
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await _repository.AddAsync(user);
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
