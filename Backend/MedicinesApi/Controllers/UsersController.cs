using MedicinesApi.Models;
using MedicinesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicinesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Name and Email are required.");
            }
            var user = await _service.CreateAsync(dto);
            return Ok(user);
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }
    }
}
