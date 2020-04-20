using AutoMapper;
using EC_API._Repositories.Interface;
using EC_API.DTO;
using EC_API.Helpers;
using EC_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using EC_API._Services.Interface;

namespace EC_API._Services.Services
{
    public class MapModelService: IMapModelService
    {
        private readonly IModelNameRepository _repoModelName;
        private readonly IModelNoRepository _repoModelNo;
        private readonly IMapper _mapper;
        private readonly IMapModelRepository _repoMapModel;
        private readonly MapperConfiguration _configMapper;
        public MapModelService(IModelNoRepository repoModelNo, IMapModelRepository repoMapModel, IModelNameRepository repoModelName, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoModelNo = repoModelNo;
            _repoModelName = repoModelName;
            _repoMapModel = repoMapModel;

        }

        public async Task<bool> Delete(int modelNameid, int modelNoid)
        {
            var mapModel = _repoMapModel.FindSingle(x => x.ModelNameID == modelNameid && x.ModelNoID == modelNoid);
            if (mapModel != null)
            {
                _repoMapModel.Remove(mapModel);

                return await _repoMapModel.SaveAll();
            }
            else return false;

        }

        public async Task<bool> MapMapModel(MapModel mapModel)
        {
            _repoMapModel.Add(mapModel);
            return await _repoMapModel.SaveAll();
        }

        public bool CheckExist(int modelNameid, int modelNoid)
        {
            var mapModel = _repoMapModel.FindSingle(x => x.ModelNameID == modelNameid && x.ModelNoID == modelNoid);
            return mapModel.ID > 0 ? true : false;

            throw new NotImplementedException();
        }
        public async Task<List<ModelNoForMapModelDto>> GetModelNos( int modelNameID)
        {
            var map =_repoMapModel.GetAll();
            var list =await _repoModelNo.FindAll().ProjectTo<ModelNoForMapModelDto>(_configMapper)
                .OrderByDescending(x => x.ID).Select(x => new ModelNoForMapModelDto
                {
                    ID = x.ID,
                    Name = x.Name,
                    Status = map.Any(a => a.ModelNameID == modelNameID && a.ModelNoID == a.ModelNoID)
                }).ToListAsync();
           
            return list;
        }

        public async Task<bool> Add(MapModelDto model)
        {
            var mapModel = _mapper.Map<MapModel>(model);
            _repoMapModel.Add(mapModel);
            return await _repoMapModel.SaveAll();
        }

        public Task<bool> Update(MapModelDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MapModelDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<MapModelDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<MapModelDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public MapModelDto GetById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
