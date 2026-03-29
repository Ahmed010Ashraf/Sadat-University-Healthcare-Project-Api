using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> Login(LoginDto LoginDto);
        Task<UserResultDto> Register (RegisterDto RegisterDto);

    }
}
