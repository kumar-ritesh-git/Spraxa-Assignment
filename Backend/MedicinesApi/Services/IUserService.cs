using MedicinesApi.Entities;
using MedicinesApi.Models;
using MedicinesApi.Repositories;

namespace MedicinesApi.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(UserDto userDto);
        Task<IEnumerable<User>> GetAllAsync();
    }


   
}
