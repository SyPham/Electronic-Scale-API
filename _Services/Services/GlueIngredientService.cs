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
    public class GlueIngredientService : IGlueIngredientService
    {
        private readonly IGlueRepository _repoGlue;
        private readonly IIngredientRepository _repoIngredient;
        private readonly IMapper _mapper;
        private readonly IGlueIngredientRepository _repoGlueIngredient;
        private readonly MapperConfiguration _configMapper;
        public GlueIngredientService(IIngredientRepository repoIngredient, IGlueIngredientRepository repoGlueIngredient, IGlueRepository repoGlue, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoIngredient = repoIngredient;
            _repoGlue = repoGlue;
            _repoGlueIngredient = repoGlueIngredient;

        }

        public async Task<bool> Delete(int glueid, int ingredientid)
        {
            var glueIngredient = _repoGlueIngredient.FindSingle(x=>x.GlueID==glueid && x.IngredientID == ingredientid);
            if (glueIngredient != null)
            {
                _repoGlueIngredient.Remove(glueIngredient);

                return await _repoGlueIngredient.SaveAll();
            }
            else return false;
                
        }

        public async Task<PagedList<GlueCreateDto>> GetGluesWithPaginations(PaginationParams param)
        {
            var lists = _repoGlue.FindAll().ProjectTo<GlueCreateDto>(_configMapper).OrderByDescending(x => x.ID);
            return await PagedList<GlueCreateDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }

        public async Task<PagedList<IngredientDto>> GetIngredientsWithPaginations(PaginationParams param, int glueid)
        {
            var glueIngredient = _repoGlueIngredient.GetAll();
            var lists = _repoIngredient.FindAll().ProjectTo<IngredientDto>(_configMapper).OrderByDescending(x => x.ID).Select(x=> new IngredientDto {
                ID =x.ID,
                Name = x.Name,
                Percentage = glueIngredient.FirstOrDefault(a => a.GlueID == glueid && a.IngredientID == x.ID).Percentage,
                Status = glueIngredient.Any(a=>a.GlueID == glueid && a.IngredientID == x.ID)
            });
            return await PagedList<IngredientDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }

        public async Task<bool> MapGlueIngredient(GlueIngredient glueIngredient)
        {
            _repoGlueIngredient.Add(glueIngredient);
            return await _repoGlueIngredient.SaveAll();
        }
    }
}