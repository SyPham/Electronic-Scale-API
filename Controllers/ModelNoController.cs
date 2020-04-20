using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EC_API.Helpers;
using EC_API._Services.Interface;
using EC_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ModelNoController : ControllerBase
    {
        private readonly IModelNoService _modelNoService;
        public ModelNoController(IModelNoService modelNoService)
        {
            _modelNoService = modelNoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetModelNos([FromQuery]PaginationParams param)
        {
            var modelNos = await _modelNoService.GetWithPaginations(param);
            Response.AddPagination(modelNos.CurrentPage,modelNos.PageSize,modelNos.TotalCount,modelNos.TotalPages);
            return Ok(modelNos);
        }

        [HttpGet(Name = "GetModelNos")]
        public async Task<IActionResult> GetAll()
        {
            var modelNos = await _modelNoService.GetAllAsync();
            return Ok(modelNos);
        }
        [HttpGet("{modeNameID}")]
        public async Task<IActionResult> GetModelNoByModelNameID(int modeNameID)
        {
            var lists = await _modelNoService.GetModelNoByModelNameID(modeNameID);
            return Ok(lists);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item =  _modelNoService.GetById(id);
            return Ok(item);
        }
        [HttpGet("{text}")]
        public async Task<IActionResult> Search([FromQuery]PaginationParams param, string text)
        {
            var lists = await _modelNoService.Search(param, text);
            Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
            return Ok(lists);
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(ModelNoDto create)
        {

            if (_modelNoService.GetById(create.ID) != null)
                return BadRequest("ModelNo ID already exists!");
            create.CreatedDate = DateTime.Now;
            if (await _modelNoService.Add(create))
            {
                return NoContent();
            }

            throw new Exception("Creating the model no failed on save");
        }

        [HttpPut]
        public async Task<IActionResult> Update(ModelNoDto update)
        {
            if (await _modelNoService.Update(update))
                return NoContent();
            return BadRequest($"Updating model no {update.ID} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _modelNoService.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the model no");
        }
    }
}