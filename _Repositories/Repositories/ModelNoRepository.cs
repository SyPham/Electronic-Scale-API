using System.Threading.Tasks;
using EC_API._Repositories.Interface;
using EC_API.Data;
using EC_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EC_API.DTO;
using System.Collections.Generic;
using AutoMapper;

namespace EC_API._Repositories.Repositories
{
    public class ModelNoRepository : ECRepository<ModelNo>, IModelNoRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ModelNoRepository(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> GetModelNoByModelNameID(int modelNameID)
        {
            var list = await _context.ModelNames.Include(x => x.ModelNos).FirstOrDefaultAsync(x => x.ID == modelNameID);
            return list;
        }

        //public async Task<GlueCreateDto> GetModelNoByModelNameID(int modelNameID)
        // {
        //     return  await  _context.ModelNames.Include(x => x.ModelNos).FirstOrDefaultAsync(x=>x.ID == modelNameID).ProjectTo<GlueCreateDto>(_configMapper);
        // }


        //Login khi them repo
    }
}