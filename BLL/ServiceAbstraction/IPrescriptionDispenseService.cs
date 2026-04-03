using BLL.Dtos.PrescriptionDispense;

namespace BLL.ServiceAbstraction
{
    public interface IPrescriptionDispenseService
    {
        Task<IEnumerable<PrescriptionDispenseResultDto>> GetAll();
        Task<PrescriptionDispenseResultDto> GetById(Guid id);
        Task<PrescriptionDispenseResultDto> Create(CreateOrUpdatePrescriptionDispenseDto dto);
        Task Update(Guid id, CreateOrUpdatePrescriptionDispenseDto dto);
        Task Delete(Guid id);
    }
}
