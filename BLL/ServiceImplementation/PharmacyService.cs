using AutoMapper;
using BLL.Dtos.Pharmacy;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.repositories.RepoAbstraction;

namespace BLL.ServiceImplementation
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public PharmacyService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PharmacyResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<Pharmacy, Guid>().GetAll();
            return _mapper.Map<IEnumerable<PharmacyResultDto>>(items);
        }

        public async Task<PharmacyResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<Pharmacy, Guid>().GetById(id);
            if (entity is null)
                throw new PharmacyNotFoundException(id);
            return _mapper.Map<PharmacyResultDto>(entity);
        }

        public async Task<PharmacyResultDto> Create(CreateOrUpdatePharmacyDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var entity = _mapper.Map<Pharmacy>(dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _uow.GetReposatory<Pharmacy, Guid>().Create(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to create pharmacy.");
            return _mapper.Map<PharmacyResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdatePharmacyDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var repo = _uow.GetReposatory<Pharmacy, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PharmacyNotFoundException(id);

            _mapper.Map(dto, entity);
            repo.Update(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to update pharmacy.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<Pharmacy, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PharmacyNotFoundException(id);
            repo.Delete(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to delete pharmacy.");
        }
    }
}