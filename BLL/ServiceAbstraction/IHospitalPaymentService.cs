using BLL.Dtos.HospitalPayment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.ServiceAbstraction
{
    public interface IHospitalPaymentService
    {
        Task<IEnumerable<HospitalPaymentResultDto>> GetAll();
        Task<HospitalPaymentResultDto> GetById(Guid id);
        Task<HospitalPaymentResultDto> Create(CreateOrUpdateHospitalPaymentDto dto);
        Task Update(Guid id, CreateOrUpdateHospitalPaymentDto dto);
        Task Delete(Guid id);
    }
}
