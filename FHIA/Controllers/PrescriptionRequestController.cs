using BLL.Dtos.PrescriptionRequest;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrescriptionRequestController : ControllerBase
    {
        private readonly IPrescriptionRequestService _service;

        public PrescriptionRequestController(IPrescriptionRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionRequestResultDto>>> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PrescriptionRequestResultDto>> GetById(Guid id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<PrescriptionRequestResultDto>> Create([FromForm] CreateOrUpdatePrescriptionRequestDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var userId))
            {
                return Unauthorized("Authenticated user id not found or not a valid GUID.");
            }

            var created = await _service.Create(userId, dto);
            return Ok(created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PrescriptionRequestResultDto>> Update(Guid id, [FromForm] CreateOrUpdatePrescriptionRequestDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var userId))
            {
                return Unauthorized("Authenticated user id not found or not a valid GUID.");
            }

            var updated = await _service.Update(userId, id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var res = await _service.Delete(id);
            return Ok(res);
        }
    }
}
