using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyLimitController(AppDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonthlyLimit>>> GetAll()
        {
            var res = await _context.MonthlyLimits.ToListAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<MonthlyLimit>> Create(int limits)
        {
            var limit = new MonthlyLimit { Days = limits };
            _context.MonthlyLimits.Add(limit);
            await _context.SaveChangesAsync();
            return Ok(limit);

        }

        [HttpPut]
        public async Task<ActionResult<MonthlyLimit>> Update(int limits)
        {
            var existingLimit = await _context.MonthlyLimits.FirstOrDefaultAsync();
            if (existingLimit == null)
            {
                return NotFound("No monthly limit found to update.");
            }
            existingLimit.Days = limits;
            await _context.SaveChangesAsync();
            return Ok(existingLimit);

        }
    }
}
