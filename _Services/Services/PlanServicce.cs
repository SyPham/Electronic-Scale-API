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
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _repoPlan;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public PlanService(IPlanRepository repoBrand, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoPlan = repoBrand;

        }

        //Thêm Brand mới vào bảng Plan
        public async Task<bool> Add(PlanDto model)
        {
            var plan = _mapper.Map<Plan>(model);
            _repoPlan.Add(plan);
            return await _repoPlan.SaveAll();
        }

     

        //Lấy danh sách Brand và phân trang
        public async Task<PagedList<PlanDto>> GetWithPaginations(PaginationParams param)
        {
            var lists = _repoPlan.FindAll().ProjectTo<PlanDto>(_configMapper).OrderByDescending(x => x.ID);
            return await PagedList<PlanDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        //public async Task<object> GetIngredientOfPlan(int Planid)
        //{
        //    return await _repoPlan.GetIngredientOfPlan(Planid);

        //    throw new System.NotImplementedException();
        //}
        //Tìm kiếm Plan
        public async Task<PagedList<PlanDto>> Search(PaginationParams param, object text)
        {
            var lists = _repoPlan.FindAll().ProjectTo<PlanDto>(_configMapper)
            .Where(x => x.Title.Contains(text.ToString()))
            .OrderByDescending(x => x.ID);
            return await PagedList<PlanDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
        //Xóa Brand
        public async Task<bool> Delete(object id)
        {
            var Plan = _repoPlan.FindById(id);
            _repoPlan.Remove(Plan);
            return await _repoPlan.SaveAll();
        }

        //Cập nhật Brand
        public async Task<bool> Update(PlanDto model)
        {
            var plan = _mapper.Map<Plan>(model);
            _repoPlan.Update(plan);
            return await _repoPlan.SaveAll();
        }
      
        //Lấy toàn bộ danh sách Brand 
        public async Task<List<PlanDto>> GetAllAsync()
        {
            return await _repoPlan.FindAll().ProjectTo<PlanDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        //Lấy Brand theo Brand_Id
        public PlanDto GetById(object id)
        {
            return  _mapper.Map<Plan, PlanDto>(_repoPlan.FindById(id));
        }

    }
}