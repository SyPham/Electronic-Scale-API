using System.Threading.Tasks;
using EC_API.DTO;
using EC_API.Helpers;
using EC_API.Models;

namespace EC_API._Services.Interface
{
    public interface IGlueIngredientService 
    {
        Task<PagedList<GlueCreateDto>> GetGluesWithPaginations(PaginationParams param);
        Task<PagedList<IngredientDto>> GetIngredientsWithPaginations(PaginationParams param, int glueid);
        Task<bool> MapGlueIngredient(GlueIngredient glueIngredient);
        Task<bool> Delete(int glueid, int ingredientid);
    }
}