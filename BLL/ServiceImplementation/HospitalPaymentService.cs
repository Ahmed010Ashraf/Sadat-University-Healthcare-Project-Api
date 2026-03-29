using AutoMapper;
using BLL.Dtos.HospitalPayment;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.repositories.RepoAbstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.ServiceImplementation
{
    public class HospitalPaymentService : IHospitalPaymentService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public HospitalPaymentService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HospitalPaymentResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<HospitalPayment, Guid>().GetAll();
            return _mapper.Map<IEnumerable<HospitalPaymentResultDto>>(items);
        }

        public async Task<HospitalPaymentResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<HospitalPayment, Guid>().GetById(id);
            if (entity is null) throw new HospitalPaymentNotFoundException(id);
            return _mapper.Map<HospitalPaymentResultDto>(entity);
        }

        public async Task<HospitalPaymentResultDto> Create(CreateOrUpdateHospitalPaymentDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            // ensure hospital exists
            var hospital = await _uow.GetReposatory<Hospital, Guid>().GetById(dto.HospitalId);
            if (hospital is null) throw new HospitalNotFoundException(dto.HospitalId);

            var entity = _mapper.Map<HospitalPayment>(dto);
            entity.PaidAt = dto.PaidAt ?? DateTime.UtcNow;

            await _uow.GetReposatory<HospitalPayment, Guid>().Create(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to create hospital payment.");
            return _mapper.Map<HospitalPaymentResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdateHospitalPaymentDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var repo = _uow.GetReposatory<HospitalPayment, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new HospitalPaymentNotFoundException(id);

            // ensure hospital exists
            var hospital = await _uow.GetReposatory<Hospital, Guid>().GetById(dto.HospitalId);
            if (hospital is null) throw new HospitalNotFoundException(dto.HospitalId);

            _mapper.Map(dto, entity);
            repo.Update(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to update hospital payment.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<HospitalPayment, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new HospitalPaymentNotFoundException(id);
            repo.Delete(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to delete hospital payment.");
        }
    }
}
