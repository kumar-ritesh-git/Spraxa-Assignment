using MedicinesApi.Entities;
using MedicinesApi.Models;
using MedicinesApi.Repositories;

namespace MedicinesApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> CreateAsync(UserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Provider = userDto.Provider,
                Timestamp = DateTime.UtcNow
            };

            await _repo.AddAsync(user);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
