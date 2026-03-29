using BLL.Dtos.Hospital;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.ServiceAbstraction
{
    public interface IHospitalService
    {
        Task<IEnumerable<HospitalResultDto>> GetAll();
        Task<HospitalResultDto> GetById(Guid id);
        Task<HospitalResultDto> Create(CreateOrUpdateHospitalDto dto);
        Task Update(Guid id, CreateOrUpdateHospitalDto dto);
        Task Delete(Guid id);
    }
}
