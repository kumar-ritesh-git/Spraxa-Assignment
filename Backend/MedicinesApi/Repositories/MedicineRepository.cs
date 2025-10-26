using Microsoft.EntityFrameworkCore;

namespace MedicinesApi.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly AppDbContext _context;
        public MedicineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medicine>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Medicines
                .OrderBy(m => m.MedicineId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync() => await _context.Medicines.CountAsync();

        public async Task<Medicine?> GetByIdAsync(int id)
            => await _context.Medicines.FirstOrDefaultAsync(x => x.MedicineId == id);

        public async Task<Medicine> AddAsync(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            return medicine;
        }

        public async Task<Medicine> UpdateAsync(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
            return medicine;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var m = await GetByIdAsync(id);
            if (m == null) return false;
            _context.Medicines.Remove(m);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

