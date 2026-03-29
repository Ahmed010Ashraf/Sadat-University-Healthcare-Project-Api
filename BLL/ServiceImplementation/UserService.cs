using AutoMapper;
using BLL.Dtos.user;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceImplementation
{
    public class UserService(UserManager<AppUser> _usermanager , RoleManager<IdentityRole<Guid>> _rolemanger , IMapper _mapper) : IUserService
    {

        public async Task<IEnumerable<UserReturnedResultDto>> GetAll()
        {
            var users = await _usermanager.Users.ToListAsync();
            var returnedUsers = _mapper.Map<IEnumerable<UserReturnedResultDto>>(users);
            return returnedUsers;
        }

        public async Task<UserReturnedResultDto> GetBYId(Guid id)
        {
            var user = await _usermanager.FindByIdAsync(id.ToString())??throw new UserNotFoundException(id);
            var returnedUser = _mapper.Map<UserReturnedResultDto>(user);
            return returnedUser;
        }


        public async Task<UserReturnedResultDto> Update(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await _usermanager.FindByIdAsync(id.ToString()) ?? throw new UserNotFoundException(id);
            var updatedUser = _mapper.Map(updateUserDto,user);
            var res = await _usermanager.UpdateAsync(updatedUser);
            if (!res.Succeeded) {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return _mapper.Map<UserReturnedResultDto>(updatedUser);
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await _usermanager.FindByIdAsync(id.ToString()) ?? throw new UserNotFoundException(id);

            var res = await _usermanager.DeleteAsync(user);
            if (!res.Succeeded) {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return true;
        }

        public async Task<bool> AssignRoleAsync(Guid userId, string roleName)
        {
            var user = await _usermanager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);
            var role = await _rolemanger.FindByNameAsync(roleName)??throw new RoleNotFoundException(roleName);
            var res = await _usermanager.AddToRoleAsync(user, roleName);
            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return true;
        }

        public async Task<bool> RemoveRoleAsync(Guid userId, string roleName)
        {
            var user = await _usermanager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);
            var role = await _rolemanger.FindByNameAsync(roleName) ?? throw new RoleNotFoundException(roleName);
            var res = await _usermanager.RemoveFromRoleAsync(user, roleName);
            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return true;
        }
    }
}
