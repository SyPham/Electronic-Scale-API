using EC_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_API._Services.Interface
{
   public interface IIngredientService: IECService<IngredientDto>
    {
        Task<bool> CheckExists(int id); 
        Task<bool> CheckBarCodeExists(string code);
    }
}
