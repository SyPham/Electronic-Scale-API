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
    public class GlueService : IGlueService
    {
        private readonly IGlueRepository _repoGlue;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public GlueService(IGlueRepository repoBrand, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoGlue = repoBrand;

        }

        //Thêm Brand mới vào bảng Glue
        public async Task<bool> Add(GlueCreateDto model)
        {
            var glue = _mapper.Map<Glue>(model);
            _repoGlue.Add(glue);
            return await _repoGlue.SaveAll();
        }

     

        //Lấy danh sách Brand và phân trang
        public async Task<PagedList<GlueCreateDto>> GetWithPaginations(PaginationParams param)
        {
            var lists = _repoGlue.FindAll().ProjectTo<GlueCreateDto>(_configMapper).OrderByDescending(x => x.ID);
            return await PagedList<GlueCreateDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        //public async Task<object> GetIngredientOfGlue(int glueid)
        //{
        //    return await _repoGlue.GetIngredientOfGlue(glueid);

        //    throw new System.NotImplementedException();
        //}
        //Tìm kiếm glue
        public async Task<PagedList<GlueCreateDto>> Search(PaginationParams param, object text)
        {
            var lists = _repoGlue.FindAll().ProjectTo<GlueCreateDto>(_configMapper)
            .Where(x => x.Code.Contains(text.ToString()))
            .OrderByDescending(x => x.ID);
            return await PagedList<GlueCreateDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        public async Task<bool> CheckExists(int id)
        {
            return await _repoGlue.CheckExists(id);
        }
        //Xóa Brand
        public async Task<bool> Delete(object id)
        {
            var glue = _repoGlue.FindById(id);
            _repoGlue.Remove(glue);
            return await _repoGlue.SaveAll();
        }

        //Cập nhật Brand
        public async Task<bool> Update(GlueCreateDto model)
        {
            var glue = _mapper.Map<Glue>(model);
            _repoGlue.Update(glue);
            return await _repoGlue.SaveAll();
        }

        //Lấy toàn bộ danh sách Brand 
        public async Task<List<GlueCreateDto>> GetAllAsync()
        {
            return await _repoGlue.FindAll().ProjectTo<GlueCreateDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        //Lấy Brand theo Brand_Id
        public GlueCreateDto GetById(object id)
        {
            return  _mapper.Map<Glue, GlueCreateDto>(_repoGlue.FindById(id));
        }

        public async Task<bool> CheckBarCodeExists(string code)
        {
            return await _repoGlue.CheckBarCodeExists(code);

        }
    }
}