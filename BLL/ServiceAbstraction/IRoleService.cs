using BLL.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceAbstraction
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResultDto>> GetAllRoles();

        Task<RoleResultDto> GetRole(Guid id);

        Task<RoleResultDto> CreateRole(CreateOrUpdateRoleDto role);

        Task<RoleResultDto> UpdateRole(Guid id ,CreateOrUpdateRoleDto role);

        Task<bool> DeleteRole(Guid id);
    }
}
