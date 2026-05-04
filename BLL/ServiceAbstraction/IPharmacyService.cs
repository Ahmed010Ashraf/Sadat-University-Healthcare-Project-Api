using BLL.Dtos.Pharmacy;

namespace BLL.ServiceAbstraction
{
    public interface IPharmacyService
    {
        Task<IEnumerable<PharmacyResultDto>> GetAll();
        Task<PharmacyResultDto> GetById(Guid id);
        Task<PharmacyResultDto> Create(CreateOrUpdatePharmacyDto dto);
        Task Update(Guid id, CreateOrUpdatePharmacyDto dto);
        Task Delete(Guid id);
    }
}
