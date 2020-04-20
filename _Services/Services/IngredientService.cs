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
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repoIngredient;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public IngredientService(IIngredientRepository repoIngredient, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoIngredient = repoIngredient;

        }
        public async Task<bool> CheckExists(int id)
        {
            return await _repoIngredient.CheckExists(id);
        }
        //Thêm Brand mới vào bảng Ingredient
        public async Task<bool> Add(IngredientDto model)
        {
            var brand = _mapper.Map<Ingredient>(model);
            _repoIngredient.Add(brand);
            return await _repoIngredient.SaveAll();
        }



        //Lấy danh sách Brand và phân trang
        public async Task<PagedList<IngredientDto>> GetWithPaginations(PaginationParams param)
        {
            var lists = _repoIngredient.FindAll().ProjectTo<IngredientDto>(_configMapper).OrderByDescending(x => x.ID);
            return await PagedList<IngredientDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        //public async Task<object> GetIngredientOfGlue(int glueid)
        //{
        //    return await _repoIngredient.GetIngredientOfGlue(glueid);

        //    throw new System.NotImplementedException();
        //}
        //Tìm kiếm brand
        //public async Task<PagedList<IngredientDto>> Search(PaginationParams param, object text)
        //{
        //    var lists = _repoIngredient.FindAll().ProjectTo<IngredientDto>(_configMapper)
        //    .Where(x => x.Brand_ID.Contains(text.ToString()) || x.Brand_EN.Contains(text.ToString()) || x.Brand_LL.Contains(text.ToString()) || x.Brand_ZW.Contains(text.ToString()))
        //    .OrderByDescending(x => x.Updated_Time);
        //    return await PagedList<IngredientDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        //}

        //Xóa Brand
        public async Task<bool> Delete(object id)
        {
           
            var brand = _repoIngredient.FindById(id.ToInt());
            _repoIngredient.Remove(brand);
            return await _repoIngredient.SaveAll();
        }

        //Cập nhật Brand
        public async Task<bool> Update(IngredientDto model)
        {
            var brand = _mapper.Map<Ingredient>(model);
            _repoIngredient.Update(brand);
            return await _repoIngredient.SaveAll();
        }

        //Lấy toàn bộ danh sách Brand 
        public async Task<List<IngredientDto>> GetAllAsync()
        {
            return await _repoIngredient.FindAll().ProjectTo<IngredientDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        //Lấy Brand theo Brand_Id
        public IngredientDto GetById(object id)
        {
            return _mapper.Map<Ingredient, IngredientDto>(_repoIngredient.FindById(id));
        }

        public Task<PagedList<IngredientDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckBarCodeExists(string code)
        {
            return await _repoIngredient.CheckBarCodeExists(code);
        }
    }
}