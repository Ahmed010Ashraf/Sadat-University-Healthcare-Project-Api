using BLL.Dtos.PrescriptionItem;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PrescriptionItemController : ControllerBase
    {
        private readonly IPrescriptionItemService _service;
        public PrescriptionItemController(IPrescriptionItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionItemResultDto>>> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PrescriptionItemResultDto>> GetById(Guid id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<PrescriptionItemResultDto>> Create([FromBody] CreateOrUpdatePrescriptionItemDto dto)
        {
            var created = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateOrUpdatePrescriptionItemDto dto)
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
    }
}
