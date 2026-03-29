using BLL.Dtos.Role;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleService _roleservice) : ControllerBase
    {
        [HttpGet("Roles")]
        public async Task<ActionResult<IEnumerable<RoleResultDto>>> GetAll()
        {
            var rolse =await _roleservice.GetAllRoles();
            return Ok(rolse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResultDto>>GetById(Guid id)
        {
            var role = await _roleservice.GetRole(id);
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleResultDto>>CreateRole(CreateOrUpdateRoleDto NewRole)
        {
            var res = await _roleservice.CreateRole(NewRole);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleResultDto>>Update(Guid id , CreateOrUpdateRoleDto NewRole)
        {
            var res = await _roleservice.UpdateRole(id, NewRole);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>>Delete(Guid id)
        {
            var res = await _roleservice.DeleteRole(id);
            return Ok(res);
        }
    }
}
