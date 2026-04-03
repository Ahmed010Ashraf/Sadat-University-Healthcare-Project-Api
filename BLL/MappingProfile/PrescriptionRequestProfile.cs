using AutoMapper;
using BLL.Dtos.PrescriptionRequest;
using DAL.Models;
using Microsoft.Extensions.Configuration;

namespace BLL.MappingProfile
{
    public class PrescriptionRequestProfile : Profile
    {
        public PrescriptionRequestProfile()
        {
            CreateMap<PrescriptionRequest, PrescriptionRequestResultDto>()
                .ForMember(dest => dest.PrescriptionImagePath, opt => opt.MapFrom<PrescriptionImageUrlResolver>());

            CreateMap<CreateOrUpdatePrescriptionRequestDto, PrescriptionRequest>();
        }
    }

    public class PrescriptionImageUrlResolver : IValueResolver<PrescriptionRequest, PrescriptionRequestResultDto, string>
    {
        private readonly IConfiguration _configuration;

        public PrescriptionImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(PrescriptionRequest source, PrescriptionRequestResultDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PrescriptionImagePath))
            {
                return $"{_configuration.GetSection("URLs")["BaseUrl"]}{source.PrescriptionImagePath}";
            }
            return null;
        }
    }
}