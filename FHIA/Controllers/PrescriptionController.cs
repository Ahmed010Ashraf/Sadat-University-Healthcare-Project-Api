using BLL.Dtos.Prescription;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] 
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionResultDto>>> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PrescriptionResultDto>> GetById(Guid id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdatePrescriptionDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var userId))
                return Unauthorized("Authenticated user id not found or not a valid GUID.");

            var created = await _service.Create(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateOrUpdatePrescriptionDto dto)
        {
            await _service.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }

        [HttpGet("PrescriptionByRequestId")]
        public async Task<ActionResult<PrescriptionResultDto>> GetPrescriptionByRequestId(Guid id)
        {
            var res = await _service.GetPrescriptionByRequestId(id);
            return Ok(res);
        }
    }
}
