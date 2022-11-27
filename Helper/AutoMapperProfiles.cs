using ArmoryManagerApi.DataTransferObjects;
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

        CreateMap<BulletPurchase, BulletPurchaseDto>();
    }
}
