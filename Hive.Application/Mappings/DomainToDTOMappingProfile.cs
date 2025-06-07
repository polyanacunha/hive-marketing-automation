using AutoMapper;
using Hive.Application.DTOs;
using Hive.Domain.Entities;

namespace Hive.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Campaing, CampaingDTO>().ReverseMap();
        CreateMap<Midia, MidiaDTO>().ReverseMap();
    }
}
