using BLL.Dtos.MedicalExamination;
using BLL.Dtos.MedicalExaminationRequest;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicalExaminationRequestController(IMedicalExaminationRequestService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalExaminationRequestResultDto>>> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalExaminationRequestResultDto>> GetById(Guid id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }


        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<MedicalExaminationRequestResultDto>>> GetMedicalExaminationRequestByUserId(Guid id)
        {
            var res = await _service.GetMedicalExaminationRequestByUserId(id);
            return Ok(res);
        }


        [HttpPost]
        public async Task<ActionResult<MedicalExaminationRequestResultDto>> Create(CreateOrUpdateMedicalExaminationRequestDto dto)
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

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalExaminationRequestResultDto>> Update(Guid id, CreateOrUpdateMedicalExaminationRequestDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var userId))
            {
                return Unauthorized("Authenticated user id not found or not a valid GUID.");
            }
            var res = await _service.Update(userId, id, dto);
            return Ok(res);
        }


        [HttpGet("userRequests")]
        public async Task<ActionResult<IEnumerable<MedicalExaminationRequestResultDto>>> GetMyRequests()
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var userId))
            {
                return Unauthorized("Authenticated user id not found or not a valid GUID.");
            }

            var requests = await _service.GetByUserId(userId);
            return Ok(requests);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var res = await _service.Delete(id);
            return Ok(res);
        }
    }
}
