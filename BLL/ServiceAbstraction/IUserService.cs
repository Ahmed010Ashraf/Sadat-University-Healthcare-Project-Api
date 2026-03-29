using BLL.Dtos;
using BLL.Dtos.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceAbstraction
{
    public interface IUserService
    {
        Task<IEnumerable<UserReturnedResultDto>> GetAll();

        Task<UserReturnedResultDto> GetBYId(Guid id);

        Task<UserReturnedResultDto> Update(Guid id , UpdateUserDto updateUserDto);

        Task<bool> Delete(Guid id);

        Task<bool> AssignRoleAsync(Guid userId, string roleName);
        Task<bool> RemoveRoleAsync(Guid userId, string roleName);


    }
}
