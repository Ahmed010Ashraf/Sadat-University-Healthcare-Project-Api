using BLL.Dtos.Pharmacy;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Mvc;


namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _service;
        public PharmacyController(IPharmacyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PharmacyResultDto>>> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PharmacyResultDto>> GetById(Guid id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<PharmacyResultDto>> Create([FromBody] CreateOrUpdatePharmacyDto dto)
        {
            var created = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateOrUpdatePharmacyDto dto)
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
