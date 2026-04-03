using BLL.Dtos.PharmacyPayment;

namespace BLL.ServiceAbstraction
{
    public interface IPharmacyPaymentService
    {
        Task<IEnumerable<PharmacyPaymentResultDto>> GetAll();
        Task<PharmacyPaymentResultDto> GetById(Guid id);
        Task<PharmacyPaymentResultDto> Create(CreateOrUpdatePharmacyPaymentDto dto);
        Task Update(Guid id, CreateOrUpdatePharmacyPaymentDto dto);
        Task Delete(Guid id);
    }
}
