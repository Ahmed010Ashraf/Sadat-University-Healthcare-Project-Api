using BLL.Dtos.user;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _userservice) : ControllerBase
    {
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<UserReturnedResultDto>>> GetALL()
        {
            var users = await _userservice.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReturnedResultDto>> GetById(Guid id)
        {
            var user = await _userservice.GetBYId(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserReturnedResultDto>> Update(Guid id , UpdateUserDto updateUser)
        {
            var user = await _userservice.Update(id , updateUser);
            return Ok(user);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var res = await _userservice.Delete(id);
            return Ok(res);
        }


        [HttpGet("assignToRole")]
        public async Task<ActionResult<bool>> AssignRole(Guid userId, string roleName)
        {
            var result = await _userservice.AssignRoleAsync(userId, roleName);
            return Ok(result);
        }

        [HttpGet("DeleteFromRole")]
        public async Task<ActionResult<bool>> DeleteRole(Guid userId, string roleName)
        {
            var result = await _userservice.RemoveRoleAsync(userId, roleName);
            return Ok(result);
        }


    }
}
