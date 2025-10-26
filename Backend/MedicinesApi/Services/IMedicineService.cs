
namespace MedicinesApi.Services
{
    public interface IMedicineService
    {
        Task<(List<Medicine> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Medicine?> GetAsync(int id);
        Task<Medicine> CreateAsync(Medicine medicine);
        Task<Medicine> UpdateAsync(int id, Medicine medicine);
        Task<bool> DeleteAsync(int id);
    }
}