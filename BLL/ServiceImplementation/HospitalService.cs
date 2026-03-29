using AutoMapper;
using BLL.Dtos.Hospital;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.repositories.RepoAbstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.ServiceImplementation
{
    public class HospitalService : IHospitalService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public HospitalService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HospitalResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<Hospital, Guid>().GetAll();
            return _mapper.Map<IEnumerable<HospitalResultDto>>(items);
        }

        public async Task<HospitalResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<Hospital, Guid>().GetById(id);
            if (entity is null)
                throw new HospitalNotFoundException(id);
            return _mapper.Map<HospitalResultDto>(entity);
        }

        public async Task<HospitalResultDto> Create(CreateOrUpdateHospitalDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var entity = _mapper.Map<Hospital>(dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _uow.GetReposatory<Hospital, Guid>().Create(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to create hospital.");
            return _mapper.Map<HospitalResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdateHospitalDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var repo = _uow.GetReposatory<Hospital, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new HospitalNotFoundException(id);

            _mapper.Map(dto, entity);
            repo.Update(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to update hospital.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<Hospital, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new HospitalNotFoundException(id);
            repo.Delete(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to delete hospital.");
        }
    }
}
