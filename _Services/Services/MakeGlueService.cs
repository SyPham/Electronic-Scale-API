using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EC_API._Repositories.Interface;
using EC_API._Services.Interface;
using EC_API.DTO;
using EC_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EC_API._Services.Services
{
    public class MakeGlueService : IMakeGlueService
    {
        private readonly IGlueIngredientRepository _repoGlueIngredient;
        private readonly IMakeGlueRepository _repoMakeGlue;
        private readonly IGlueRepository _repoGlue;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public MakeGlueService(
            IGlueIngredientRepository repoGlueIngredient,
            IGlueRepository repoGlue, 
            IMakeGlueRepository repoMakeGlue,
            IMapper mapper, 
            MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoGlue = repoGlue;
            _repoMakeGlue = repoMakeGlue;
            _repoGlueIngredient = repoGlueIngredient;

        }

        public async Task<object> GetAllGlues()
        {
            return await _repoGlue.FindAll().ProjectTo<GlueCreateDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        public async Task<object> GetGlueWithIngredientByGlueCode(string code)
        {
            return await _repoMakeGlue.GetGlueWithIngredientByGlueCode(code);
        }

        public async Task<object> GetGlueWithIngredients(int glueid)
        {
            return await _repoGlueIngredient.GetIngredientOfGlue(glueid);
            throw new NotImplementedException();
        }

        public async Task<object> MakeGlue(int glueid)
        {

            throw new System.NotImplementedException();
        }
        public async Task<object> MakeGlue(string code)
        {
            return await _repoMakeGlue.MakeGlue(code);
            throw new System.NotImplementedException();
        }
    }
}