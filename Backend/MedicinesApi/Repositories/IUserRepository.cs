using MedicinesApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicinesApi.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }
    }
}
