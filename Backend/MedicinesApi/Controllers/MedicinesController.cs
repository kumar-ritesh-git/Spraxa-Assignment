using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MedicinesApi.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authenticated requests (Google-issued JWT bearer token)
public class MedicinesController : ControllerBase
{
    private readonly IMedicineService _medicineService;

    public MedicinesController(IMedicineService medicineService)
    {
        _medicineService = medicineService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
    {
        var (items, total) = await _medicineService.GetPagedAsync(page, pageSize);
        return Ok(new { total, items });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var med = await _medicineService.GetAsync(id);
        if (med == null) return NotFound();
        return Ok(med);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Medicine med)
    {
        med.CreatedOn = DateTime.UtcNow;
        var createdMed = await _medicineService.CreateAsync(med);
        return CreatedAtAction(nameof(Get), new { id = createdMed.MedicineId }, createdMed);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Medicine updated)
    {
        var med = await _medicineService.GetAsync(id);
        if (med == null) return NotFound();

        med.Name = updated.Name;
        med.Company = updated.Company;
        med.Price = updated.Price;
        med.ExpiryDate = updated.ExpiryDate;
        med.Stock = updated.Stock;

        await _medicineService.UpdateAsync(id, med);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var med = await _medicineService.GetAsync(id);
        if (med == null) return NotFound();
        await _medicineService.DeleteAsync(id);
        return NoContent();
    }
}
