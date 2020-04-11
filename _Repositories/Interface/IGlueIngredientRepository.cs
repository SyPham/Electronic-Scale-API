using System.Threading.Tasks;
using EC_API.Data;
using EC_API.Models;

namespace EC_API._Repositories.Interface
{
    public interface IGlueIngredientRepository : IECRepository<GlueIngredient>
    {
        Task<object> GetIngredientOfGlue(int glueid);
        //viet them ham o day neu chua co trong ECRepository
    }
}