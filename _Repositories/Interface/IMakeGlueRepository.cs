using System.Threading.Tasks;
using EC_API.Data;
using EC_API.Models;

namespace EC_API._Repositories.Interface
{
    public interface IMakeGlueRepository
    {
        Task<object> MakeGlue(int glueid);
        Task<object> MakeGlue(string code);
        Task<object> GetGlueWithIngredientByGlueCode(string code);
        //viet them ham o day neu chua co trong ECRepository
    }
}