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
            var glueIngredient = _repoGlueIngredient.FindSingle(x => x.GlueID == glueid && x.IngredientID == ingredientid);
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
            var lists = _repoIngredient.FindAll().ProjectTo<IngredientDto>(_configMapper).OrderByDescending(x => x.ID).Select(x => new IngredientDto
            {
                ID = x.ID,
                Name = x.Name,
                Code = x.Code,
                CreatedDate = x.CreatedDate,
                Percentage = glueIngredient.FirstOrDefault(a => a.GlueID == glueid && a.IngredientID == x.ID).Percentage,
                Status = glueIngredient.Any(a => a.GlueID == glueid && a.IngredientID == x.ID)
            });
            return await PagedList<IngredientDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        public async Task<List<IngredientDto>> GetIngredientsByGlueID(int glueid)
        {
            var glueIngredient = _repoGlueIngredient.GetAll();
            var lists = await _repoIngredient.FindAll()
                .ProjectTo<IngredientDto>(_configMapper)
                .OrderByDescending(x => x.ID)
                .Select(x => new IngredientDto
            {
                ID = x.ID,
                Name = x.Name,
                Code = x.Code,
                CreatedDate =x.CreatedDate,
                Percentage = glueIngredient.FirstOrDefault(a => a.GlueID == glueid && a.IngredientID == x.ID).Percentage,
                Status = glueIngredient.Any(a => a.GlueID == glueid && a.IngredientID == x.ID)
            }).ToListAsync();
            return lists;
        }
        public async Task<bool> MapGlueIngredient(GlueIngredient glueIngredient)
        {
            _repoGlueIngredient.Add(glueIngredient);
            return await _repoGlueIngredient.SaveAll();
        }

        public bool CheckExist(int glueid, int ingredientid)
        {
            var glueIngredient = _repoGlueIngredient.FindSingle(x => x.GlueID == glueid && x.IngredientID == ingredientid);
            return glueIngredient.ID > 0 ? true : false;

            throw new NotImplementedException();
        }

        public object GetGlueIngredientDetail(int glueid)
        {
            var glueIngredient = (from a in _repoGlueIngredient.FindAll()
                                  join b in _repoGlue.FindAll() on a.GlueID equals b.ID
                                  join c in _repoIngredient.FindAll() on a.IngredientID equals c.ID
                                  where a.GlueID == glueid
                                  select new GlueIngredientDetailDto
                                  {
                                      ID = a.ID,
                                      GlueID = a.GlueID,
                                      IngredientID = a.IngredientID,
                                      GlueName = b.Name,
                                      IngredientName = c.Name,
                                      CreatedDate = a.CreatedDate,
                                      Percentage = a.Percentage
                                  });

            var parent = new GlueIngredientForGroupByDto();
            var childs = new List<GlueIngredientDetailDto>();
            foreach (var item in glueIngredient)
            {
                parent.ID = item.ID;
                parent.Name = item.GlueName;
                childs.Add(new GlueIngredientDetailDto
                {
                    IngredientID = item.IngredientID,
                    IngredientName = item.IngredientName,
                    GlueID = item.GlueID,
                    CreatedDate = item.CreatedDate,
                    GlueName = item.GlueName,
                    Percentage = item.Percentage
                });
            }
            parent.GlueIngredients = childs;
            return parent;
        }

        public async Task<bool> EditPercentage(int glueid, int ingredientid, int percentage)
        {
            return await _repoGlueIngredient.EditPercentage(glueid, ingredientid, percentage);
        }
        public async Task<bool> Guidance(List<GlueIngredientForGuidanceDto> glueIngredientForGuidanceDto)
        {
            return await _repoGlueIngredient.Guidance(glueIngredientForGuidanceDto);
        }
    }
}