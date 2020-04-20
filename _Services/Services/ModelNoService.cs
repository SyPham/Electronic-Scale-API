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
    public class ModelNoService : IModelNoService
    {
        private readonly IModelNoRepository _repoModelNo;
        private readonly IModelNameRepository _repoModelName;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ModelNoService(IModelNoRepository repoBrand, IModelNameRepository repoModelName, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoModelNo = repoBrand;
            _repoModelName = repoModelName;

        }

        //Thêm Brand mới vào bảng ModelNo
        public async Task<bool> Add(ModelNoDto model)
        {
            var ModelNo = _mapper.Map<ModelNo>(model);
            _repoModelNo.Add(ModelNo);
            return await _repoModelNo.SaveAll();
        }

     

        //Lấy danh sách Brand và phân trang
        public async Task<PagedList<ModelNoDto>> GetWithPaginations(PaginationParams param)
        {
            var lists = _repoModelNo.FindAll().Include(x => x.ModelName).ProjectTo<ModelNoDto>(_configMapper).OrderByDescending(x => x.ID);
            return await PagedList<ModelNoDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        //public async Task<object> GetIngredientOfModelNo(int ModelNoid)
        //{
        //    return await _repoModelNo.GetIngredientOfModelNo(ModelNoid);

        //    throw new System.NotImplementedException();
        //}
        //Tìm kiếm ModelNo
        public async Task<PagedList<ModelNoDto>> Search(PaginationParams param, object text)
        {
            var lists = _repoModelNo.FindAll().ProjectTo<ModelNoDto>(_configMapper)
            .Where(x => x.Name.Contains(text.ToString()))
            .OrderByDescending(x => x.ID);
            return await PagedList<ModelNoDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        //Xóa Brand
        public async Task<bool> Delete(object id)
        {
            var ModelNo = _repoModelNo.FindById(id);
            _repoModelNo.Remove(ModelNo);
            return await _repoModelNo.SaveAll();
        }

        //Cập nhật Brand
        public async Task<bool> Update(ModelNoDto model)
        {
            var ModelNo = _mapper.Map<ModelNo>(model);
            _repoModelNo.Update(ModelNo);
            return await _repoModelNo.SaveAll();
        }
      
        //Lấy toàn bộ danh sách Brand 
        public async Task<List<ModelNoDto>> GetAllAsync()
        {
            return await _repoModelNo.FindAll().Include(x=>x.ModelName).ProjectTo<ModelNoDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        //Lấy Brand theo Brand_Id
        public ModelNoDto GetById(object id)
        {
            return  _mapper.Map<ModelNo, ModelNoDto>(_repoModelNo.FindById(id));
        }

        public async Task<object> GetModelNoByModelNameID(int modelNameID)
        {
           
            return await _repoModelNo.GetModelNoByModelNameID(modelNameID);
            throw new NotImplementedException();
        }
    }
}