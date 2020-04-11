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
            //CreateMap<AuditTypeDto, MES_Audit_Type_M>();
        }
    }
}