using ArmoryManagerApi.DataTransferObjects.BulletPurchaseDtos;
using ArmoryManagerApi.DataTransferObjects.BulletTemplateDtos;
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

        CreateMap<CreateBulletTemplateDto, BulletTemplate>();
        CreateMap<BulletTemplate, BulletTemplateDto>().ReverseMap();

        CreateMap<CreatePowderTemplateDto, PowderTemplate>();
        CreateMap<PowderTemplate, PowderTemplateDto>().ReverseMap();

        CreateMap<CreatePrimerTemplateDto, PrimerTemplate>();
        CreateMap<PrimerTemplate, PrimerTemplateDto>().ReverseMap();

        CreateMap<CreateBulletPurchaseDto, BulletPurchase>();
        CreateMap<BulletPurchase, BulletPurchaseDto>().ReverseMap();

        CreateMap<CreatePrimerPurchaseDto, PrimerPurchase>();
        CreateMap<PrimerPurchase, PrimerPurchaseDto>().ReverseMap();

        CreateMap<CreatePowderPurchaseDto, PowderPurchase>();
        CreateMap<PowderPurchase, PowderPurchaseDto>().ReverseMap();

        CreateMap<CreateReloadDto, Reload>();
        CreateMap<Reload, ReloadDto>().ReverseMap();
    }
}
