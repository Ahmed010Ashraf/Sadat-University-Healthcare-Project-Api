using AutoMapper;
using BLL.Dtos.Role;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceImplementation
{
    public class RoleService(RoleManager<IdentityRole<Guid>>_rolemanager , IMapper _mapper) : IRoleService
    {

        public async Task<IEnumerable<RoleResultDto>> GetAllRoles()
        {
            var res = await _rolemanager.Roles.ToListAsync();
            var roles = _mapper.Map<IEnumerable<RoleResultDto>>(res);
            return roles;
        }

        public async Task<RoleResultDto> GetRole(Guid id)
        {
            var role = await _rolemanager.FindByIdAsync(id.ToString())??throw new RoleWithThisIdIsNotFound(id);
            return _mapper.Map<RoleResultDto>(role);

        }

        public async Task<RoleResultDto> CreateRole(CreateOrUpdateRoleDto role)
        {
            var NewRole = new IdentityRole<Guid>()
            {
                Name = role.Name
            };

            var res = await _rolemanager.CreateAsync(NewRole);
            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }

            return _mapper.Map<RoleResultDto>(NewRole);

        }

        public async Task<RoleResultDto> UpdateRole(Guid id, CreateOrUpdateRoleDto role)
        {
            var Myrole = await _rolemanager.FindByIdAsync(id.ToString()) ?? throw new RoleWithThisIdIsNotFound(id);
            Myrole.Name = role.Name;
            var res =await _rolemanager.UpdateAsync(Myrole);
            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return _mapper.Map<RoleResultDto>(Myrole);

        }

        public async Task<bool> DeleteRole(Guid id)
        {
            var Myrole = await _rolemanager.FindByIdAsync(id.ToString()) ?? throw new RoleWithThisIdIsNotFound(id);
            var res =await _rolemanager.DeleteAsync(Myrole);
            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return true;
        }




    }
}
