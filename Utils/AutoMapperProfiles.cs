using ArmoryManagerApi.ViewModels;
using ArmoryManagerApi.Models;
using AutoMapper;

namespace ArmoryManagerApi.Helper;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{

        CreateMap<CreateCasingTemplateVM, CasingTemplate>();
        CreateMap<CasingTemplate, CasingTemplateVM>().ReverseMap();

        CreateMap<CreatePowderTemplateVM, PowderTemplate>();
        CreateMap<PowderTemplate, PowderTemplateVM>().ReverseMap();

        CreateMap<CreatePrimerTemplateVM, PrimerTemplate>();
        CreateMap<PrimerTemplate, PrimerTemplateVM>().ReverseMap();

        CreateMap<CreateCasingVM, Casing>();
        CreateMap<Casing, CasingVM>().ReverseMap();

        CreateMap<CreatePrimerVM, Primer>();
        CreateMap<Primer, PrimerVM>().ReverseMap();

        CreateMap<CreatePowderVM, Powder>();
        CreateMap<Powder, PowderVM>().ReverseMap();

        CreateMap<CreateReloadVM, Reload>();
        CreateMap<Reload, ReloadVM>().ReverseMap();
    }
}
