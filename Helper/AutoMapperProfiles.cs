using ArmoryManagerApi.DataTransferObjects.CasingPurchaseDtos;
using ArmoryManagerApi.DataTransferObjects.CasingTemplateDtos;
using ArmoryManagerApi.DataTransferObjects.PowderPurchaseDtos;
using ArmoryManagerApi.DataTransferObjects.PowderTemplateDtos;
using ArmoryManagerApi.DataTransferObjects.PrimerPurchaseDtos;
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

        CreateMap<CreateCasingPurchaseDto, CasingPurchase>();
        CreateMap<CasingPurchase, CasingPurchaseDto>().ReverseMap();

        CreateMap<CreatePrimerPurchaseDto, PrimerPurchase>();
        CreateMap<PrimerPurchase, PrimerPurchaseDto>().ReverseMap();

        CreateMap<CreatePowderPurchaseDto, PowderPurchase>();
        CreateMap<PowderPurchase, PowderPurchaseDto>().ReverseMap();

        CreateMap<CreateReloadDto, Reload>();
        CreateMap<Reload, ReloadDto>().ReverseMap();
    }
}
