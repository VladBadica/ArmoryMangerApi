using ArmoryManagerApi.DataTransferObjects.CasingDtos;
using ArmoryManagerApi.DataTransferObjects.CasingTemplateDtos;
using ArmoryManagerApi.DataTransferObjects.PowderDtos;
using ArmoryManagerApi.DataTransferObjects.PowderTemplateDtos;
using ArmoryManagerApi.DataTransferObjects.PrimerDtos;
using ArmoryManagerApi.DataTransferObjects.PrimerTemplateDtos;
using ArmoryManagerApi.DataTransferObjects.ReloadDtos;
using ArmoryManagerApi.Models;
using AutoMapper;

namespace ArmoryManagerApi.Helper;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{

        CreateMap<CreateCasingTemplateDto, CasingTemplate>();
        CreateMap<CasingTemplate, CasingTemplateDto>().ReverseMap();

        CreateMap<CreatePowderTemplateDto, PowderTemplate>();
        CreateMap<PowderTemplate, PowderTemplateDto>().ReverseMap();

        CreateMap<CreatePrimerTemplateDto, PrimerTemplate>();
        CreateMap<PrimerTemplate, PrimerTemplateDto>().ReverseMap();

        CreateMap<CreateCasingDto, Casing>();
        CreateMap<Casing, CasingDto>().ReverseMap();

        CreateMap<CreatePrimerDto, Primer>();
        CreateMap<Primer, PrimerDto>().ReverseMap();

        CreateMap<CreatePowderDto, Powder>();
        CreateMap<Powder, PowderDto>().ReverseMap();

        CreateMap<CreateReloadDto, Reload>();
        CreateMap<Reload, ReloadDto>().ReverseMap();
    }
}
