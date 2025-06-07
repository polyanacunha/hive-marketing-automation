using AutoMapper;
using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;

namespace Hive.Application.Services;

public class CampaingService : ICampaingService
{
    private ICampaingRepository _campaingRepository;
    private readonly IMapper _mapper;
    public CampaingService(ICampaingRepository campaingRepository, IMapper mapper)
    {
        _campaingRepository = campaingRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CampaingDTO>> GetCampaings()
    {
        var categoriesEntity = await _campaingRepository.GetCampaings();
        return _mapper.Map<IEnumerable<CampaingDTO>>(categoriesEntity);
    }

    public async Task<CampaingDTO> GetById(int? id)
    {
        var campaingEntity = await _campaingRepository.GetById(id);
        return _mapper.Map<CampaingDTO>(campaingEntity);
    }

    public async Task Add(CampaingDTO campaingDto)
    {
        var campaingEntity = _mapper.Map<Campaing>(campaingDto);
        await _campaingRepository.Create(campaingEntity);
        campaingDto.Id = campaingEntity.Id;
    }

    public async Task Update(CampaingDTO campaingDto)
    {
        var campaingEntity = _mapper.Map<Campaing>(campaingDto);
        await _campaingRepository.Update(campaingEntity);
    }

    public async Task Remove(int? id)
    {
        var campaingEntity = _campaingRepository.GetById(id).Result;
        await _campaingRepository.Remove(campaingEntity);
    }
}
