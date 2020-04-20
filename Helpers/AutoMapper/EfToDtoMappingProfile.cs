using EC_API.DTO;
using EC_API.Models;
using AutoMapper;

namespace EC_API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        public EfToDtoMappingProfile()
        {
            CreateMap<User, UserForDetailDto>();
            CreateMap<Glue, GlueDto>().ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.CreatedDate.ToParseStringDateTime()));
            CreateMap<Glue, GlueCreateDto>().ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.CreatedDate.ToParseStringDateTime()));
            CreateMap<Ingredient, IngredientDto>();

            CreateMap<Line, LineDto>();
            CreateMap<ModelNo, ModelNoDto>().ForMember(d => d.ModelName, o => o.MapFrom(s => s.ModelName.Name));
            CreateMap<ModelNo, ModelNoForMapModelDto>();

            CreateMap<ModelName, ModelNameDto>().ForMember(d => d.ModelNumberDtos, o => o.MapFrom(s => s.ModelNos));
            CreateMap<Plan, PlanDto>();

            CreateMap<MapModel, MapModelDto>();
            CreateMap<UserDetailDto, UserDetail> ();

        }

    }
}