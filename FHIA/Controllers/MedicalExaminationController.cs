using BLL.Dtos.MedicalExamination;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MedicalExaminationController : ControllerBase
    {
        private readonly IMedicalExaminationService _service;

        public MedicalExaminationController(IMedicalExaminationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalExaminationResultDto>>> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MedicalExaminationResultDto>> GetById(Guid id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateMedicalExaminationDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var userId))
                return Unauthorized("Authenticated user id not found or not a valid GUID.");

            var created = await _service.Create(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateOrUpdateMedicalExaminationDto dto)
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

        [HttpGet("MedicalExaminationByRequestId")]
        public async Task<ActionResult<MedicalExaminationResultDto>> GetMedicalExaminationByRequestId(Guid id)
        {
            var res = await _service.GetMedicalExaminationByRequestId(id);
            return Ok(res);
        }

        [HttpGet("User/{id}")]

        public async Task<ActionResult<IEnumerable<MedicalExaminationResultDto>>> GetMedicalExaminationByUserId(Guid id)
        {
            var res = await _service.GetMedicalExaminationByUserId(id);
            return Ok(res);
        }
    }
    }
