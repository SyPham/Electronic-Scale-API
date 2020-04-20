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
    public class ModelNameController : ControllerBase
    {
        private readonly IModelNameService _modelNameService;
        public ModelNameController(IModelNameService modelNameService)
        {
            _modelNameService = modelNameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetModelNames([FromQuery]PaginationParams param)
        {
            var modelNames = await _modelNameService.GetWithPaginations(param);
            Response.AddPagination(modelNames.CurrentPage,modelNames.PageSize,modelNames.TotalCount,modelNames.TotalPages);
            return Ok(modelNames);
        }

        [HttpGet(Name = "GetModelNames")]
        public async Task<IActionResult> GetAll()
        {
            var modelNames = await _modelNameService.GetAllAsync();
            return Ok(modelNames);
        }

        [HttpGet("{text}")]
        public async Task<IActionResult> Search([FromQuery]PaginationParams param, string text)
        {
            var lists = await _modelNameService.Search(param, text);
            Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
            return Ok(lists);
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(ModelNameDto create)
        {

            if (_modelNameService.GetById(create.ID) != null)
                return BadRequest("ModelName ID already exists!");
            create.CreatedDate = DateTime.Now;
            if (await _modelNameService.Add(create))
            {
                return NoContent();
            }

            throw new Exception("Creating the model name failed on save");
        }

        [HttpPut]
        public async Task<IActionResult> Update(ModelNameDto update)
        {
            if (await _modelNameService.Update(update))
                return NoContent();
            return BadRequest($"Updating model name {update.ID} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _modelNameService.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the model name");
        }
    }
}