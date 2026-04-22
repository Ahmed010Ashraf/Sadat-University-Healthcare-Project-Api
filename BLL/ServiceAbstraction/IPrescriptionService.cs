using BLL.Dtos.Prescription;

namespace BLL.ServiceAbstraction
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionResultDto>> GetAll();
        Task<PrescriptionResultDto> GetById(Guid id);
        Task<PrescriptionResultDto> Create(Guid userId, CreateOrUpdatePrescriptionDto dto);
        Task Update(Guid id, CreateOrUpdatePrescriptionDto dto);
        Task Delete(Guid id);
        Task<IEnumerable<PrescriptionResultDto>> GetByUserId(Guid userId);
        Task<PrescriptionResultDto> GetPrescriptionByRequestId(Guid requestId);
    }
}
