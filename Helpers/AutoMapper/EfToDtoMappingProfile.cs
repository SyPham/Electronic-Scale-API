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
            CreateMap<Glue, GlueDto>();
            CreateMap<Glue, GlueCreateDto>();
            CreateMap<Ingredient, IngredientDto>();
            //CreateMap<MES_Audit_Brand, BrandDto>();
            //CreateMap<MES_Audit_Type_M, AuditTypeDto>();
        }
        
    }
}