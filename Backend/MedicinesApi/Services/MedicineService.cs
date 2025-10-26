using MedicinesApi.Repositories;

namespace MedicinesApi.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repo;

        public MedicineService(IMedicineRepository repo)
        {
            _repo = repo;
        }

        public async Task<(List<Medicine> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var items = await _repo.GetAllAsync(page, pageSize);
            var total = await _repo.CountAsync();
            return (items, total);
        }

        public async Task<Medicine?> GetAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<Medicine> CreateAsync(Medicine medicine)
        {
            medicine.CreatedOn = DateTime.UtcNow;
            return await _repo.AddAsync(medicine);
        }

        public async Task<Medicine> UpdateAsync(int id, Medicine medicine)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new Exception("Medicine not found");

            existing.Name = medicine.Name;
            existing.Company = medicine.Company;
            existing.Price = medicine.Price;
            existing.Stock = medicine.Stock;
            existing.ExpiryDate = medicine.ExpiryDate;

            return await _repo.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id)
            => await _repo.DeleteAsync(id);
    }
}

