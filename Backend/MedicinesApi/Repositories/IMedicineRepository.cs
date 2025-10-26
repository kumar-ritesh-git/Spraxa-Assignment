namespace MedicinesApi.Repositories
{
    public interface IMedicineRepository
    {
        Task<List<Medicine>> GetAllAsync(int page, int pageSize);
        Task<int> CountAsync();
        Task<Medicine?> GetByIdAsync(int id);
        Task<Medicine> AddAsync(Medicine medicine);
        Task<Medicine> UpdateAsync(Medicine medicine);
        Task<bool> DeleteAsync(int id);

    }
}
