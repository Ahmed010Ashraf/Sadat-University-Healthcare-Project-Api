using AutoMapper;
using BLL.Dtos.Prescription;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Enums;
using DAL.repositories.RepoAbstraction;

namespace BLL.ServiceImplementation
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public PrescriptionService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrescriptionResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<Prescription, Guid>().GetAll();
            return _mapper.Map<IEnumerable<PrescriptionResultDto>>(items);
        }

        public async Task<PrescriptionResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<Prescription, Guid>().GetById(id);
            if (entity is null)
                throw new PrescriptionNotFoundException(id);

            return _mapper.Map<PrescriptionResultDto>(entity);
        }

        public async Task<PrescriptionResultDto> Create(Guid userId, CreateOrUpdatePrescriptionDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            // Ensure prescription request exists
            var requestRepo = _uow.GetReposatory<PrescriptionRequest, Guid>();
            var request = await requestRepo.GetById(dto.PrescriptionRequestId);
            if (request is null)
                throw new PrescriptionRequestNotFoundException(dto.PrescriptionRequestId);

            // Check if a prescription already exists for this request
            var existing = await _uow.GetReposatory<Prescription, Guid>()
                .GetAll(p => p.PrescriptionRequestId == dto.PrescriptionRequestId);
            if (existing.Any())
                throw new InvalidOperationException("A prescription already exists for this request.");

            var entity = _mapper.Map<Prescription>(dto);
            entity.UserId = userId;
            entity.PrescriptionCode = Guid.NewGuid();

            await _uow.GetReposatory<Prescription, Guid>().Create(entity);
            var affected = await _uow.SaveChnagesAsync();
            if (affected <= 0)
                throw new InvalidOperationException("Failed to persist new Prescription.");

            return _mapper.Map<PrescriptionResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdatePrescriptionDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var repo = _uow.GetReposatory<Prescription, Guid>();
            var entity = await repo.GetById(id);

            if (entity is null)
                throw new PrescriptionNotFoundException(id);


            if (entity.Status == PrescriptionStatus.Expired)
                throw new Exception("Prescription already Expired and cannot be updated.");

            // Ensure request still exists
            var requestRepo = _uow.GetReposatory<PrescriptionRequest, Guid>();
            var request = await requestRepo.GetById(dto.PrescriptionRequestId);
            if (request is null)
                throw new PrescriptionRequestNotFoundException(dto.PrescriptionRequestId);

            _mapper.Map(dto, entity);
            // PrescriptionCode should not be updated
            repo.Update(entity);
            var affected = await _uow.SaveChnagesAsync();
            if (affected <= 0)
                throw new InvalidOperationException("Failed to persist Prescription update.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<Prescription, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null)
                throw new PrescriptionNotFoundException(id);
            repo.Delete(entity);
            var affected = await _uow.SaveChnagesAsync();
            if (affected <= 0)
                throw new InvalidOperationException("Failed to delete Prescription.");
        }

        public async Task<PrescriptionResultDto> GetPrescriptionByRequestId(Guid requestId)
        {
            var request = await _uow.GetReposatory<PrescriptionRequest, Guid>().GetById(requestId)
                ?? throw new PrescriptionRequestNotFoundException(requestId);

            var prescriptions = await _uow.GetReposatory<Prescription, Guid>()
                .GetAll(p => p.PrescriptionRequestId == requestId);
            var prescription = prescriptions.FirstOrDefault();

            if (prescription is null)
                throw new NotFoundException("There is no prescription for this request.");

            return _mapper.Map<PrescriptionResultDto>(prescription);
        }

        public async Task<IEnumerable<PrescriptionResultDto>> GetByUserId(Guid userId)
        {
            var prescriptions = await _uow.GetReposatory<Prescription, Guid>()
                .GetAll(p => p.UserId == userId);
            return _mapper.Map<IEnumerable<PrescriptionResultDto>>(prescriptions);
        }


    }
}
