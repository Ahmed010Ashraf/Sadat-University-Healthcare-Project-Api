using AutoMapper;
using BLL.Dtos.PharmacyPayment;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.repositories.RepoAbstraction;

namespace BLL.ServiceImplementation
{
    public class PharmacyPaymentService : IPharmacyPaymentService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public PharmacyPaymentService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PharmacyPaymentResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<PharmacyPayment, Guid>().GetAll();
            return _mapper.Map<IEnumerable<PharmacyPaymentResultDto>>(items);
        }

        public async Task<PharmacyPaymentResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<PharmacyPayment, Guid>().GetById(id);
            if (entity is null) throw new PharmacyPaymentNotFoundException(id);
            return _mapper.Map<PharmacyPaymentResultDto>(entity);
        }

        public async Task<PharmacyPaymentResultDto> Create(CreateOrUpdatePharmacyPaymentDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            // Ensure the pharmacy exists
            var pharmacy = await _uow.GetReposatory<Pharmacy, Guid>().GetById(dto.PharmacyId);
            if (pharmacy is null) throw new PharmacyNotFoundException(dto.PharmacyId);

            var entity = _mapper.Map<PharmacyPayment>(dto);
            entity.PaidAt = dto.PaidAt ?? DateTime.UtcNow;

            await _uow.GetReposatory<PharmacyPayment, Guid>().Create(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to create pharmacy payment.");
            return _mapper.Map<PharmacyPaymentResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdatePharmacyPaymentDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var repo = _uow.GetReposatory<PharmacyPayment, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PharmacyPaymentNotFoundException(id);

            // Ensure the pharmacy exists
            var pharmacy = await _uow.GetReposatory<Pharmacy, Guid>().GetById(dto.PharmacyId);
            if (pharmacy is null) throw new PharmacyNotFoundException(dto.PharmacyId);

            _mapper.Map(dto, entity);
            repo.Update(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to update pharmacy payment.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<PharmacyPayment, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PharmacyPaymentNotFoundException(id);
            repo.Delete(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to delete pharmacy payment.");
        }
    }
}
