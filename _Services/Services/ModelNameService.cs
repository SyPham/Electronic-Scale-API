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
    public class ModelNameService : IModelNameService
    {
        private readonly IModelNameRepository _repoModelName;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ModelNameService(IModelNameRepository repoBrand, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoModelName = repoBrand;

        }

        //Thêm Brand mới vào bảng ModelName
        public async Task<bool> Add(ModelNameDto model)
        {
            var ModelName = _mapper.Map<ModelName>(model);
            _repoModelName.Add(ModelName);
            return await _repoModelName.SaveAll();
        }

     

        //Lấy danh sách Brand và phân trang
        public async Task<PagedList<ModelNameDto>> GetWithPaginations(PaginationParams param)
        {
            var lists = _repoModelName.FindAll().ProjectTo<ModelNameDto>(_configMapper).OrderByDescending(x => x.ID);
            return await PagedList<ModelNameDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        //public async Task<object> GetIngredientOfModelName(int ModelNameid)
        //{
        //    return await _repoModelName.GetIngredientOfModelName(ModelNameid);

        //    throw new System.NotImplementedException();
        //}
        //Tìm kiếm ModelName
        public async Task<PagedList<ModelNameDto>> Search(PaginationParams param, object text)
        {
            var lists = _repoModelName.FindAll().ProjectTo<ModelNameDto>(_configMapper)
            .Where(x => x.Name.Contains(text.ToString()))
            .OrderByDescending(x => x.ID);
            return await PagedList<ModelNameDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        //Xóa Brand
        public async Task<bool> Delete(object id)
        {
            var ModelName = _repoModelName.FindById(id);
            _repoModelName.Remove(ModelName);
            return await _repoModelName.SaveAll();
        }

        //Cập nhật Brand
        public async Task<bool> Update(ModelNameDto model)
        {
            var ModelName = _mapper.Map<ModelName>(model);
            _repoModelName.Update(ModelName);
            return await _repoModelName.SaveAll();
        }
      
        //Lấy toàn bộ danh sách Brand 
        public async Task<List<ModelNameDto>> GetAllAsync()
        {
            return await _repoModelName.FindAll().ProjectTo<ModelNameDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        //Lấy Brand theo Brand_Id
        public ModelNameDto GetById(object id)
        {
            return  _mapper.Map<ModelName, ModelNameDto>(_repoModelName.FindById(id));
        }

    }
}