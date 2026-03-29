using AutoMapper;
using BLL.Dtos.user;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MappingProfile
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser,UserReturnedResultDto>();
            CreateMap<UpdateUserDto, AppUser>();
        }
    }
}
