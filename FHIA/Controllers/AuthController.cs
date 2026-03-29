using BLL.Dtos;
using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FHIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthenticationService _AuthService) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            var res = await _AuthService.Login(loginDto);
            return Ok(res);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto RegisterDto)
        {
            var res = await _AuthService.Register(RegisterDto);
            return Ok(res);
        }




    }
}
