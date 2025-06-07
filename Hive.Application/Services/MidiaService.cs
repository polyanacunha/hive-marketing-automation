using AutoMapper;
using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;

namespace Hive.Application.Services
{
    public class MidiaService : IMidiaService
    {
        private readonly IMapper _mapper;
        private readonly IMidiaRepository _repository;

        public MidiaService(IMapper mapper,
                              IMidiaRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<MidiaDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Produto com id {id} não encontrado.");

            return _mapper.Map<MidiaDTO>(entity);
        }

        public async Task UpdateAsync(MidiaDTO dto)
        {
            // mapeia DTO → Domain
            var entity = _mapper.Map<Midia>(dto);
            var exists = await _repository.GetByIdAsync(entity.Id);
            if (exists == null)
                throw new KeyNotFoundException($"Produto com id {entity.Id} não encontrado.");

            await _repository.UpdateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            var exists = await _repository.GetByIdAsync(id);
            if (exists == null)
                throw new KeyNotFoundException($"Produto com id {id} não encontrado.");

            await _repository.DeleteAsync(id);
        }
    }
}
