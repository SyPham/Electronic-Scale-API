using EC_API.DTO;
using EC_API.Models;
using AutoMapper;

namespace EC_API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            CreateMap<UserForDetailDto, User>();
            CreateMap<GlueDto, Glue>();
            CreateMap<GlueCreateDto, Glue>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<LineDto, Line>();
            CreateMap<ModelNameDto, ModelName>().ForMember(d => d.ModelNos, o => o.Ignore());
            CreateMap<ModelNoDto, ModelNo>().ForMember(d => d.ModelName, o => o.Ignore()); ;
            CreateMap<ModelNoForMapModelDto, ModelNo>();
            CreateMap<PlanDto, Plan>();

            CreateMap<MapModelDto, MapModel>();
            CreateMap<UserDetail, UserDetailDto>();

            //CreateMap<AuditTypeDto, MES_Audit_Type_M>();
        }
    }
}